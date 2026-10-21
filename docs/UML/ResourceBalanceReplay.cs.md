# ResourceBalanceReplay.cs

**User moniker:** Narrow resource-balance event reducer

## Purpose

Replay an already ordered stream of `resource.balance.delta` events into a signed 64-bit balance. This is a small domain example demonstrating deterministic event reduction, not a general world-state engine.

## Source and executable evidence

- Implementation: [ResourceBalanceReplay.cs](../../src/raWWar/History/ResourceBalanceReplay.cs)
- Event model and in-memory reference history: [SimulationEvent.cs](../../src/raWWar/History/SimulationEvent.cs)
- Repository contract-test suite: [raWWar contract tests](../../tests/raWWar.ContractTests/README.md)
- Dedicated diagram: [ResourceBalanceReplayUML.md](ResourceBalanceReplayUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public static class ResourceBalanceReplay` |
| Domain | `resource.balance.delta` |
| Payload V1 | Exactly one signed Int64 delta, big-endian two's-complement |
| Entry point | `Replay(long initialBalance, IEnumerable<SimulationEvent> orderedEvents)` |
| Arithmetic | Checked Int64 addition; overflow throws |
| Ordering | Lexical logical-time key, then event ordinal, then event ID |

## Observed constraints

- Each event must belong to the expected domain and same address stream, world seed, and simulation-model version.
- Repeated event IDs are rejected.
- Events supplied out of the declared deterministic order are rejected; the reducer does not sort its input.
- Payloads of any size other than eight bytes are rejected with `FormatException`.
- An empty sequence returns the initial balance.
- The reducer does not query history, commit events, infer time units, or implement checkpoints.

## Use case

Read the events for one address and domain using the history layer's ordering contract, then pass that sequence and an initial balance to `Replay`. The result is a checked balance computed from the supplied event prefix.

## Evidence boundary

This is a CPU-side sample reducer. It does not establish a universal event-sourcing design, durable replay service, atomic world-state transaction, or production-scale persistence guarantee.
