# CandidateFileEventJournalChecks.cs

**User moniker:** File-journal recovery and failure-boundary contract suite

## Purpose

Run temporary-file contract checks against the internal candidate append-only journal. The checks exercise commit outcomes, recovery, concurrency, stream isolation, ordering, malformed framing, and an injected uncertain write outcome.

## Source and evidence

- [Contract-check source](../../tests/raWWar.ContractTests/CandidateFileEventJournalChecks.cs)
- [Journal implementation](../../src/raWWar/History/CandidateFileEventJournal.cs)
- [Frame codec](../../src/raWWar/History/CandidateEventFrameCodec.cs)
- [Event model](../../src/raWWar/History/SimulationEvent.cs)
- [Dedicated diagram](CandidateFileEventJournalChecksUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Internal static helper |
| Entry point | `Run(Action<bool, string> check)` |
| Storage | Unique temporary directory and journal files |
| Cleanup | Attempts recursive directory deletion in `finally` |
| Failure reporting | Delegates named assertions to the main test runner |

## Verified areas

- Empty journal, exclusive writer access, commit, idempotent retry, and identity conflict.
- Reopen recovery and deterministic logical-time/ordinal ordering.
- Concurrent identical retries and distinct concurrent commits.
- World-seed/model/domain/address stream isolation.
- Injected exception before durable flush; reads and commits remain blocked until reopen.
- Clean child-process exit followed by a new process reopening the journal.
- Fail-closed handling for malformed headers, unsupported versions/flags, invalid UTF-8, truncation, duplicate identities, checksum corruption, and oversized frame declarations.

## Evidence boundary

The source explicitly states that these checks do **not** simulate power loss. The injected exception is a controlled pre-flush failure, and child-process recovery follows a clean exit; neither establishes behavior under arbitrary OS crash, hardware failure, or physical-media loss.
