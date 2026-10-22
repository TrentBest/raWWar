# raWWar.ContractTests Program — class diagram

```mermaid
classDiagram
    class ContractTestProgram {
      <<top-level executable>>
      +Check(condition, message)
      +Near(actual, expected, tolerance, message)
      +Run spatial/event/orbit/interaction checks
      +Compose RuntimeManifest through FSM_COS
      +Return process exit code
    }
    class CandidateFileEventJournalChecks {
      +Run(check)
    }
    class SingleBundleCatalog {
      +TryResolve(bundleId, version, out bundle)
      +TryResolve(bundleId, out bundle)
    }
    class RaWWarMicroBundle {
      +BundleId
      +Descriptor
    }
    class FsmCos {
      +Execute(RuntimeManifest)
    }
    ContractTestProgram ..> CandidateFileEventJournalChecks : delegates journal scenarios
    ContractTestProgram ..> SingleBundleCatalog : supplies local catalog
    ContractTestProgram ..> RaWWarMicroBundle : verifies runtime bundle
    ContractTestProgram ..> FsmCos : exercises manifest composition
```

## Use case

One executable entry point provides a visible pass/fail contract boundary across the current runtime.

## Boundary

The local catalog deliberately does not simulate repository retrieval or artifact materialization. The test suite does not itself authorize package publication.
