# SimulationEvent.cs

**User moniker:** Deterministic event identity and in-memory reference history

## Purpose

Defines the stable identity and immutable value model for one proposed simulation event, the commit outcome enum, and a thread-safe in-memory reference ledger. Creating an event does not commit it.

## Source and executable evidence

- Implementation: [SimulationEvent.cs](../../src/raWWar/History/SimulationEvent.cs)
- Candidate durable-format consumer: [CandidateEventFrameCodec.cs](../../src/raWWar/History/CandidateEventFrameCodec.cs)
- Candidate journal consumer: [CandidateFileEventJournal.cs](../../src/raWWar/History/CandidateFileEventJournal.cs)
- Dedicated diagram: [SimulationEventUML.md](SimulationEventUML.md)

## Class card

| Type | Contract |
|---|---|
| `SimulationEventId` | Readonly record struct; V1 SHA-256 identity rendered as uppercase hexadecimal |
| `SimulationEvent` | Sealed event value; copies canonical address and payload inputs |
| `EventCommitResult` | `Committed`, `AlreadyCommitted`, or `IdentityConflict` |
| `InMemoryEventHistory` | Lock-protected, non-durable reference ledger |

## Identity and ordering

V1 identity hashes a versioned binary encoding of world seed, model version, canonical address bytes, event domain, event ordinal, and domain-defined logical-time key. Payload is deliberately excluded from identity; a repeated identity with different content is therefore reported as a conflict by the ledger.

Ordered reads sort by ordinal string comparison of logical-time key, then event ordinal, then event-ID value. Logical-time keys remain opaque strings: this code does not define units, numeric interpretation, or a universal domain ordering.

## Observed constraints

- Model version and event domain must be non-empty/non-whitespace; address bytes must be non-empty; logical-time key must be non-null.
- Payload and address inputs are copied; `Payload` returns a copy through `ReadOnlyMemory<byte>`.
- The in-memory ledger serializes commits and snapshots with one lock, but is not durable and does not apply payloads to world state.
- The event identity's hash is deterministic for its encoded identity fields; it is not a signature or proof of event authenticity.

## Use case

Create an event with canonical address bytes and domain-defined ordering metadata. Submit it to `InMemoryEventHistory.Commit`: an identical retry returns `AlreadyCommitted`, a new identity returns `Committed`, and the same identity with different event content returns `IdentityConflict`.

## Evidence boundary

This is an event identity/value model and a reference ledger, not a scheduler, universal event schema, transaction coordinator, or production event store.
