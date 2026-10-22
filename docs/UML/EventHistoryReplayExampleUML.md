# EventHistoryReplay example — diagram

```mermaid
classDiagram
    class EventHistoryReplayExample {
      <<top-level program>>
      +Commit events
      +Check retry and conflict
      +Read ordered stream
      +Compare full replay and checkpoint tail
    }
    class InMemoryEventHistory {
      +Commit(SimulationEvent)
      +ReadOrdered(domain, address)
    }
    class SimulationEvent {
      +Create(...)
      +StreamKey
      +Id
      +Payload
    }
    class ResourceBalanceReplay {
      +EncodeDelta(int)
      +Replay(initialBalance, orderedEvents)
    }
    class ResourceBalanceCheckpoint {
      +Capture(initialBalance, orderedPrefix)
      +ReplayTail(remainingEvents)
    }
    EventHistoryReplayExample ..> InMemoryEventHistory : commits and reads
    EventHistoryReplayExample ..> SimulationEvent : creates events
    EventHistoryReplayExample ..> ResourceBalanceReplay : reduces deltas
    EventHistoryReplayExample ..> ResourceBalanceCheckpoint : verifies checkpoint replay
```

## Use case

Show the minimal event commit boundary and deterministic scoped replay.

## Boundary

In-memory sample only; the file journal has separate contract checks.
