# SimulationEvent — class diagram

```mermaid
classDiagram
    class SimulationEventId {
      <<readonly record struct>>
      +byte CurrentVersion
      +Create(seed, modelVersion, address, domain, ordinal, logicalTimeKey) SimulationEventId
    }
    class SimulationEvent {
      <<sealed>>
      +SimulationEventId Id
      +ulong WorldSeed
      +string SimulationModelVersion
      +string EventDomain
      +string StreamKey
      +string LogicalTimeKey
      +ulong EventOrdinal
      +ReadOnlyMemory~byte~ Payload
      +Create(...) SimulationEvent
    }
    class EventCommitResult {
      <<enumeration>>
      Committed
      AlreadyCommitted
      IdentityConflict
    }
    class InMemoryEventHistory {
      +int Count
      +Commit(SimulationEvent) EventCommitResult
      +ReadOrdered(domain, address) IReadOnlyList
      +ReadOrdered(domain) IReadOnlyList
    }
    SimulationEvent --> SimulationEventId : derives stable identity
    InMemoryEventHistory --> SimulationEvent : indexes event values
    InMemoryEventHistory --> EventCommitResult : reports idempotency/conflict
```

## Use case

A domain creates a proposed event, then commits it to the in-memory ledger or passes it to a journal. Consumers request a deterministic snapshot and apply their own domain-specific replay rules.

## Boundary

Identity excludes payload; conflict detection compares content separately. The in-memory ledger does not provide durability or atomic world-state updates.
