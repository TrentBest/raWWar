# CandidateFileEventJournal.cs

**User moniker:** Experimental single-process append-only event journal

## Purpose

Provide a provisional file-backed companion to the event identity model: append encoded events, flush commits to disk, rebuild an in-memory index at open, and return deterministically ordered snapshots for a specific world/model/domain/address stream.

## Source and executable evidence

- Implementation: [CandidateFileEventJournal.cs](../../src/raWWar/History/CandidateFileEventJournal.cs)
- Frame format: [CandidateEventFrameCodec.cs](../../src/raWWar/History/CandidateEventFrameCodec.cs)
- Event identity: [SimulationEvent.cs](../../src/raWWar/History/SimulationEvent.cs)
- Contract checks: [CandidateFileEventJournalChecks.cs](../../tests/raWWar.ContractTests/CandidateFileEventJournalChecks.cs)
- Dedicated diagram: [CandidateFileEventJournalUML.md](CandidateFileEventJournalUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `internal sealed class CandidateFileEventJournal : IDisposable` |
| Writer access | `FileShare.None`; one open writer for the file |
| Commit durability path | Append frame, optional test fault seam, `Flush(flushToDisk: true)`, then update live index |
| Recovery | Sequentially validates each frame and rejects truncated/corrupt frames and duplicate identities |
| Ordered read | World seed + model version + domain + canonical address; logical-time key, ordinal, event ID ordering |

## Commit and recovery behavior

Identical event content under an existing identity returns `AlreadyCommitted`; different content under that identity returns `IdentityConflict`. If append or flush throws, the journal marks itself as requiring reopen and blocks subsequent reads/commits because the outcome may be uncertain. Reopening replays the file to determine what records are present.

The contract checks exercise idempotent retry, identity conflict, concurrent commits, ordering, corruption and truncation rejection, injected failure before durable flush, and a clean child-process restart. The test source explicitly says these checks do not simulate power loss.

## Use case

Open the journal, commit events, and read ordered events for a single stream. After any write/flush exception, dispose and reopen before further operations so recovery can reconstruct the index from the bytes on disk.

## Evidence boundary and limitations

The implementation is explicitly experimental, internal, single-process and provisional. It is not a public storage API, multi-process store, atomic world-state transaction, or proof of crash/power-loss safety. The test seam is evidence for the uncertainty guard, not a hardware durability simulation.
