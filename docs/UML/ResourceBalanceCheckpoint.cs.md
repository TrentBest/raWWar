# ResourceBalanceCheckpoint.cs

**User moniker:** Explicit replay-prefix boundary

## Purpose

Capture the result and ordering boundary of a non-empty resource-balance event prefix so a caller can replay a later tail without replaying the full prefix again.

## Source and executable evidence

- Implementation: [ResourceBalanceCheckpoint.cs](../../src/raWWar/History/ResourceBalanceCheckpoint.cs)
- Reducer contract: [ResourceBalanceReplay.cs](ResourceBalanceReplay.cs.md)
- Event identity and stream model: [SimulationEvent.cs](../../src/raWWar/History/SimulationEvent.cs)
- Repository contract-test suite: [raWWar contract tests](../../tests/raWWar.ContractTests/README.md)
- Dedicated diagram: [ResourceBalanceCheckpointUML.md](ResourceBalanceCheckpointUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public sealed record ResourceBalanceCheckpoint` |
| Schema | `CurrentSchemaVersion = 1` |
| Captured state | Balance, world seed, model version, stream key, last event ID, logical-time key, and ordinal |
| Capture | Requires a non-empty already ordered prefix and replays it through `ResourceBalanceReplay` |
| Tail replay | Requires every tail event to match the captured stream and to sort strictly after the boundary |

## Observed constraints

- Capture materializes the supplied prefix, rejects an empty prefix, and relies on the reducer to reject invalid ordering, repeated IDs, domain mismatches, and arithmetic overflow.
- Tail replay rejects unsupported schema versions, events from another domain/address/seed/model version, events at or before the boundary, and non-strictly ordered tails.
- The checkpoint is an immutable in-memory record; this class does not define serialization, durable storage, checkpoint discovery, or atomic commit behavior.

## Use case

Replay a known prefix, capture a checkpoint, then supply only events strictly after its ordering boundary to `ReplayTail`. The tail is validated before being applied to the checkpoint balance.

## Evidence boundary

This type is a narrow example for resource-balance replay. It is not a general world checkpoint or a durable checkpoint file format.
