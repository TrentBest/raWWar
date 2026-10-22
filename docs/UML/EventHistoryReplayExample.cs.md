# EventHistoryReplay example — Program.cs

**User moniker:** Commit, order, and replay a scoped event stream

## Purpose

Executable walkthrough of the in-memory event-history path: explicitly commit events, recognize identical retries, reject identity conflicts, read a domain/address-scoped ordered stream, and compare full resource-balance replay with checkpoint-plus-tail replay.

## Source and evidence

- [Example source](../../examples/EventHistoryReplay/Program.cs)
- [SimulationEvent](../../src/raWWar/History/SimulationEvent.cs)
- [ResourceBalanceReplay](../../src/raWWar/History/ResourceBalanceReplay.cs)
- [ResourceBalanceCheckpoint](../../src/raWWar/History/ResourceBalanceCheckpoint.cs)
- [Dedicated diagram](EventHistoryReplayExampleUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Top-level console program |
| Stream scope | Canonical hierarchical address plus resource-balance event domain |
| Expected result | Initial balance 5; ordered deltas +10 and −3; final balance 12 |
| Failure | Returns exit code 1 if commit outcomes, checkpoint equivalence, count, or final balance differ |

## Use case

Run the sample to observe that event construction is separate from committing; duplicate identity/content is idempotent, while the same identity with different payload is a conflict. The reducer sees only events read for the requested stream.

## Evidence boundary

This sample uses `InMemoryEventHistory`. It does not demonstrate file-journal durability, process restart, power-loss behavior, or distributed event storage.
