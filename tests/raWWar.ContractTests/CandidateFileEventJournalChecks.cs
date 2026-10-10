using System.Diagnostics;
using System.Reflection;
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

            var concurrentPath = Path.Combine(directory, "concurrent-retries.rwej");
            var concurrentEvent = SimulationEvent.Create(123, "v1", address, "resource.depleted",
                77, "tick:000077", new byte[] { 0x77 });
            using (var concurrentJournal = new CandidateFileEventJournal(concurrentPath))
            {
                var outcomes = new System.Collections.Concurrent.ConcurrentBag<EventCommitResult>();
                Parallel.For(0, 32, _ => outcomes.Add(concurrentJournal.Commit(concurrentEvent)));
                check(outcomes.Count(result => result == EventCommitResult.Committed) == 1
                    && outcomes.Count(result => result == EventCommitResult.AlreadyCommitted) == 31,
                    "Concurrent identical journal commits produce one append and 31 idempotent retries");
                check(concurrentJournal.Count == 1,
                    "Concurrent identical journal commits leave exactly one recovered identity in the live index");
            }

            var distinctConcurrentPath = Path.Combine(directory, "concurrent-distinct-events.rwej");
            using (var distinctJournal = new CandidateFileEventJournal(distinctConcurrentPath))
            {
                var distinctOutcomes = new System.Collections.Concurrent.ConcurrentBag<EventCommitResult>();
                Parallel.For(0, 32, i =>
                {
                    var distinctEvent = SimulationEvent.Create(123, "v1", address, "resource.depleted",
                        checked((ulong)(100 + i)), $"tick:{100 + i:D6}", new byte[] { checked((byte)i) });
                    distinctOutcomes.Add(distinctJournal.Commit(distinctEvent));
                });
                check(distinctOutcomes.Count == 32
                    && distinctOutcomes.All(result => result == EventCommitResult.Committed),
                    "Concurrent distinct journal commits each report Committed");
                check(distinctJournal.Count == 32
                    && distinctJournal.ReadOrdered("resource.depleted", address).Count == 32,
                    "Concurrent distinct journal commits preserve all 32 events in the live index and ordered reads");
            }

            // A clean child-process exit followed by a fresh process opening the journal checks
            // process-restart reconstruction, but does not simulate a crash or power loss.
            var processRestartPath = Path.Combine(directory, "process-restart.rwej");
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var start = new ProcessStartInfo("dotnet")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            start.ArgumentList.Add(assemblyPath);
            start.ArgumentList.Add("--journal-child-write");
            start.ArgumentList.Add(processRestartPath);
            using (var child = Process.Start(start) ?? throw new InvalidOperationException("Could not start journal child process."))
            {
                if (!child.WaitForExit(30000))
                {
                    child.Kill(entireProcessTree: true);
                    throw new TimeoutException("Journal child process did not exit within 30 seconds.");
                }
                var childOutput = child.StandardOutput.ReadToEnd();
                var childError = child.StandardError.ReadToEnd();
                check(child.ExitCode == 0,
                    $"Child process committed its journal event and exited successfully (exit {child.ExitCode}; stdout: {childOutput}; stderr: {childError})");
            }
            using (var restarted = new CandidateFileEventJournal(processRestartPath))
            {
                var recovered = restarted.ReadOrdered("process.restart.probe", address);
                check(recovered.Count == 1 && recovered[0].Payload.Span.SequenceEqual(new byte[] { 0xC1, 0xC2 }),
                    "A new process can reopen and reconstruct an event committed by an exited process");
                var restartEvent = SimulationEvent.Create(123, "v1", address, "process.restart.probe",
                    1, "tick:000001", new byte[] { 0xC1, 0xC2 });
                check(restarted.Commit(restartEvent) == EventCommitResult.AlreadyCommitted,
                    "Retry after clean process restart resolves to AlreadyCommitted without duplicate append");
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
