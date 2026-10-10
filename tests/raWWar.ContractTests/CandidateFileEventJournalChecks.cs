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


            // Inject an exception after bytes are written but before Flush(true). This tests
            // the journal's uncertainty guard and reopen/retry protocol, not actual power loss.
            var uncertainPath = Path.Combine(directory, "uncertain-outcome.rwej");
            var uncertainEvent = SimulationEvent.Create(123, "v1", address, "resource.depleted",
                99, "tick:000099", new byte[] { 0x55, 0x66 });
            using (var journal = new CandidateFileEventJournal(uncertainPath,
                () => throw new IOException("Injected failure before durable flush.")))
            {
                var injectedFailureObserved = false;
                try { _ = journal.Commit(uncertainEvent); }
                catch (IOException) { injectedFailureObserved = true; }
                check(injectedFailureObserved, "Injected pre-flush failure is surfaced to the caller");
                var furtherCommitBlocked = false;
                try { _ = journal.Commit(second); }
                catch (InvalidOperationException) { furtherCommitBlocked = true; }
                check(furtherCommitBlocked, "Uncertain write outcome blocks further commits until reopen");
                var furtherReadBlocked = false;
                try { _ = journal.ReadOrdered("resource.depleted", address); }
                catch (InvalidOperationException) { furtherReadBlocked = true; }
                check(furtherReadBlocked, "Uncertain write outcome blocks reads against a potentially stale index");
            }
            using (var recovered = new CandidateFileEventJournal(uncertainPath))
            {
                check(recovered.Count == 1, "Reopen resolves the injected pre-flush failure from the journal bytes");
                check(recovered.Commit(uncertainEvent) == EventCommitResult.AlreadyCommitted,
                    "Retry after uncertain outcome does not append a duplicate event");
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
