# SingleBundleCatalog — class diagram

```mermaid
classDiagram
    class SingleBundleCatalog {
      <<internal sealed>>
      -IMicroBundle bundle
      +SingleBundleCatalog(IMicroBundle)
      +TryResolve(ulong, string, out IMicroBundle)
      +TryResolve(ulong, out IMicroBundle)
    }
    class IMicroBundleCatalog {
      <<interface>>
      +TryResolve(ulong, string, out IMicroBundle)
      +TryResolve(ulong, out IMicroBundle)
    }
    class IMicroBundle {
      <<interface>>
      +Id
      +Descriptor
    }
    SingleBundleCatalog ..|> IMicroBundleCatalog : implements
    SingleBundleCatalog o--> IMicroBundle : retains one candidate
```

## Use case

Resolve the locally constructed root bundle in a focused composition contract test.

## Boundary

Single-candidate lookup only; no repository or artifact-fetch semantics.
