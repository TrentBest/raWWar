# CandidateFileEventJournal — class diagram

```mermaid
classDiagram
    class CandidateFileEventJournal {
      <<internal sealed>>
      +Count int
      +Commit(SimulationEvent) EventCommitResult
      +ReadOrdered(seed, model, domain, address) IReadOnlyList
      +Dispose()
      -Recover()
      -EnsureUsable()
    }
    class CandidateEventFrameCodec {
      <<internal static>>
      +Encode(SimulationEvent) byte[]
      +Decode(ReadOnlySpan~byte~) SimulationEvent
    }
    class SimulationEvent {
      +SimulationEventId Id
      +string StreamKey
      +string LogicalTimeKey
      +ulong EventOrdinal
    }
    class FileStream
    class Dictionary {
      <<identity index>>
    }
    CandidateFileEventJournal ..> CandidateEventFrameCodec : append/recovery frames
    CandidateFileEventJournal --> FileStream : exclusive append and recovery
    CandidateFileEventJournal --> Dictionary : in-memory identity index
    CandidateFileEventJournal ..> SimulationEvent : commits and ordered snapshots
```

## Use case

Commit writes an encoded frame and flushes it before updating the index. Recovery validates each frame from the start of the file and rebuilds the index; uncertain write outcomes require reopening.

## Boundary

Single-process experimental persistence only. No multi-process coordination, world-state transaction, or actual power-loss test is claimed.
