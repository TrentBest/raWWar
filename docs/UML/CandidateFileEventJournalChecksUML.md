# CandidateFileEventJournalChecks — class diagram

```mermaid
classDiagram
    class CandidateFileEventJournalChecks {
      <<internal static>>
      +Run(check)
      -Create temporary journal files
      -Assert recovery and rejection paths
    }
    class CandidateFileEventJournal {
      <<internal>>
      +Commit(event) EventCommitResult
      +ReadOrdered(seed, model, domain, address)
      +Count
    }
    class CandidateEventFrameCodec {
      +Encode(event) byte[]
      +Decode(frame) SimulationEvent
    }
    class SimulationEvent {
      +Create(...)
      +Id
      +Payload
    }
    class ContractTestRunner {
      +Check(condition, message)
    }
    CandidateFileEventJournalChecks ..> CandidateFileEventJournal : opens, commits, reads
    CandidateFileEventJournalChecks ..> CandidateEventFrameCodec : creates malformed/valid frames
    CandidateFileEventJournalChecks ..> SimulationEvent : constructs test events
    CandidateFileEventJournalChecks ..> ContractTestRunner : reports assertions through callback
```

## Use case

Validate journal behavior using isolated temporary files and named assertions.

## Boundary

The tests cover declared scenarios only; they are not a power-loss certification or production storage qualification.
