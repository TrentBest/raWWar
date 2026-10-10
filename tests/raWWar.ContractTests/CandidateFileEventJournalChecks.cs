using TheSingularityWorkshop.raWWar.History;

namespace TheSingularityWorkshop.raWWar.ContractTests;

/// <summary>Temporary-file checks for the internal Candidate journal. These do not simulate power loss.</summary>
internal static class CandidateFileEventJournalChecks
{
    internal static void Run(Action<bool, string> check)
    {
        var directory = Path.Combine(Path.GetTempPath(), "raWWar-journal-contract-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "events.rwej");
        var address = Convert.FromHexString("5257534101002A00000002030405090001");
        var first = SimulationEvent.Create(123, "v1", address, "resource.depleted", 7, "tick:000042", new byte[] { 0x10, 0x20, 0x30 });
        var second = SimulationEvent.Create(123, "v1", address, "resource.depleted", 8, "tick:000043", new byte[] { 0x40 });

        try
        {
            using (var journal = new CandidateFileEventJournal(path))
            {
                check(journal.Count == 0, "New candidate journal starts empty");
                var secondWriterRejected = false;
                try { using var competing = new CandidateFileEventJournal(path); }
                catch (IOException) { secondWriterRejected = true; }
                check(secondWriterRejected, "Exclusive file access rejects a second simultaneous journal writer");
                check(journal.Commit(first) == EventCommitResult.Committed, "First candidate journal commit reports Committed");
                var lengthAfterFirst = new FileInfo(path).Length;
                check(journal.Commit(first) == EventCommitResult.AlreadyCommitted, "Identical in-process retry is idempotent");
                check(new FileInfo(path).Length == lengthAfterFirst, "Identical retry does not append a duplicate frame");
                var conflict = SimulationEvent.Create(123, "v1", address, "resource.depleted", 7, "tick:000042", new byte[] { 0x99 });
                check(journal.Commit(conflict) == EventCommitResult.IdentityConflict, "Same identity with different payload is a conflict");
                check(journal.Commit(second) == EventCommitResult.Committed, "Distinct event appends to the candidate journal");
            }

            using (var journal = new CandidateFileEventJournal(path))
            {
                check(journal.Count == 2, "Closing and reopening reconstructs both committed events");
                check(journal.Commit(first) == EventCommitResult.AlreadyCommitted, "Retry after reopen resolves to AlreadyCommitted");
                var events = journal.ReadOrdered("resource.depleted", address);
                check(events.Count == 2 && events[0].Id == first.Id && events[1].Id == second.Id,
                    "Recovered address-scoped reads preserve deterministic logical order");
                var conflict = SimulationEvent.Create(123, "v1", address, "resource.depleted", 7, "tick:000042", new byte[] { 0xAA });
                check(journal.Commit(conflict) == EventCommitResult.IdentityConflict, "Identity conflict remains detectable after reopen");
            }

            var validBytes = File.ReadAllBytes(path);
            File.WriteAllBytes(path, validBytes[..^1]);
            var rejectedTruncation = false;
            try { using var ignored = new CandidateFileEventJournal(path); }
            catch (FormatException) { rejectedTruncation = true; }
            check(rejectedTruncation, "Candidate journal fails closed on a truncated final frame");
            File.WriteAllBytes(path, validBytes);

            var duplicatePath = Path.Combine(directory, "duplicate-events.rwej");
            var duplicateFrame = CandidateEventFrameCodec.Encode(first);
            File.WriteAllBytes(duplicatePath, duplicateFrame.Concat(duplicateFrame).ToArray());
            var rejectedDuplicateIdentity = false;
            try { using var ignored = new CandidateFileEventJournal(duplicatePath); }
            catch (FormatException) { rejectedDuplicateIdentity = true; }
            check(rejectedDuplicateIdentity, "Candidate journal fails closed on duplicate event identities during recovery");

            var corruptBytes = (byte[])validBytes.Clone();
            corruptBytes[20] ^= 0x01;
            File.WriteAllBytes(path, corruptBytes);
            var rejectedCorruption = false;
            try { using var ignored = new CandidateFileEventJournal(path); }
            catch (FormatException) { rejectedCorruption = true; }
            check(rejectedCorruption, "Candidate journal fails closed on a corrupted frame");
        }
        finally
        {
            try { Directory.Delete(directory, recursive: true); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}
