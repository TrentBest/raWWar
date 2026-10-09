# AnyApp ↔ raWWar manifest bridge

**Status:** Integration contract under investigation; not yet an end-to-end loading claim.  
**Reviewed:** 2026-10-09  
**Working branch:** `development`  

**Dependency request:** [AnyApp artifact closure](../requests/REQUEST-FROM-raWWar-AnyApp-ArtifactClosure.md). The corresponding MicroBundle configuration requirement is tracked in [the MicroBundleDomain request](../requests/REQUEST-FROM-raWWar-MicroBundleDomain-ExperienceConfiguration.md). See the [request convention and index](../requests/README.md).

## The three documents have different jobs

| Document/contract | Owner | Job |
|---|---|---|
| raWWar `manifest.json` | raWWar authors | Human-/author-facing Experience metadata and pointer to its runtime manifest |
| raWWar `runtime-manifest.json` | raWWar + FSM_COS contract | Runtime ID and versioned bundle roots: `{ bundleId, version }` |
| AnyApp `ExperienceManifest` | AnyApp host/publication contract | Experience ID/version, runtime ID, bundle requests/configuration, optional intent, and optional immutable repository artifact identity |

Do not deserialize one schema as another or silently discard version information.

## Verified AnyApp development path

The current AnyApp `development` branch contains:

1. `ExperienceManifest.ToRuntimeManifest()`, which maps every listed bundle request to an FSM_COS `MicroBundleDependencyRequest` with optional configuration bytes.
2. `AnyAppRuntime.ComposeAsync(experience, repositoryEndpoint)`, which chooses the compiled native catalog unless every manifest bundle has both `ArtifactVersion` and `ContentHash`.
3. `RepositoryMicroBundleCatalog`, which retrieves artifacts using bundle ID + artifact version + content hash, materializes an `IMicroBundle`, checks the materialized bundle ID, and queues declared dependencies.
4. `MainWindow.LaunchManifestAsync`, which calls `ComposeAsync` and then renders the returned runtime.

The native `AnyAppMicroBundleCatalog` still registers only Moniker and Forge. raWWar is not registered there. Repository-backed loading is a real host path, but it is not proof that raWWar has been published or can currently load.

Source references:
- [AnyApp ExperienceManifest.cs](https://github.com/TrentBest/AnyApp/blob/development/ExperienceManifest.cs)
- [AnyApp AnyAppRuntime.cs](https://github.com/TrentBest/AnyApp/blob/development/AnyAppRuntime.cs)
- [AnyApp RepositoryMicroBundleCatalog.cs](https://github.com/TrentBest/AnyApp/blob/development/RepositoryMicroBundleCatalog.cs)
- [AnyApp MainWindow.xaml.cs](https://github.com/TrentBest/AnyApp/blob/development/MainWindow.xaml.cs)
- [AnyApp AnyAppMicroBundleCatalog.cs](https://github.com/TrentBest/AnyApp/blob/development/AnyAppMicroBundleCatalog.cs)

## Concrete bridge requirements

Before claiming raWWar is host-loadable, the bridge must prove all of the following:

- **Identity consistency:** Experience ID/version, runtime ID, root bundle IDs, and requested versions agree across the authoring manifest, runtime manifest, publication manifest, and actual artifact descriptor.
- **Immutable artifact identity:** Every artifact address needed by the repository catalog includes an explicit version and content hash. Never invent a hash or treat an authoring version as proof of a published artifact.
- **Configuration boundary:** Configuration remains separate from the FSM_COS runtime manifest. Conceptually it is a map of parameter IDs to literal override values; a parameter ID is not the value and neither is automatically a provider ID. The Experience supplies overrides, the owning MicroBundle defines parameter meaning/defaults/validation and declares providers, and consumers use checked provider lookup because a provider may be absent. AnyApp's `configurationBase64` is transport, not the domain configuration contract. Missing overrides preserve bundle defaults. See [Configuration and Provider Resolution](../architecture/CONFIGURATION_AND_PROVIDER_RESOLUTION.md).
- **Dependency closure:** The current AnyApp catalog builds its artifact-address table only from `ExperienceManifest.Bundles`, then queues each materialized bundle's declared dependencies. If a dependency is not present in that address table, preload fails with “No repository address is registered”. The bridge must therefore either supply verified identities for the required closure in a form the host supports, or the host/repository contract must gain a verified way to resolve dependency artifact identities. Do not assume dependency discovery alone provides artifact addresses.
- **Root versus closure semantics:** FSM_COS receives every `ExperienceManifest.Bundles` entry as a request, while the repository catalog also uses that list as its address table. Tests must distinguish intended runtime roots from entries included to make artifact closure addressable; do not silently promote dependencies into roots without confirming the desired contract.
- **Duplicate and invalid entries:** Reject conflicting duplicate bundle IDs, blank versions, malformed/non-SHA-256 hashes, and identity mismatches before composition. Confirm the host currently enforces these constraints; do not assume it does.
- **Provider and parameter semantics:** Keep parameter IDs, override values, and provider IDs distinct. Provider lookup may fail; optional absence must follow an explicit fallback, while required capability absence must produce a clear failure. ProtocolAi is a candidate optional vocabulary aid, not a required dependency.
- **Host manifestation:** After FSM_COS returns `RuntimeAssembly`, the host must identify and present the actual raWWar Experience/capability surface. A successful assembly alone is not an executed game.
- **No unauthorized release:** Build and test locally/through CI. Do not publish NuGet artifacts or merge PRs without explicit creator approval.

## Current raWWar state

- The authoring manifest has `experienceId: 3301`, `version: 1.0.0`, and `runtimeId: 3311`.
- The runtime manifest names bundle `3301` at version `1.0.0`.
- `RaWWarMicroBundle` is still a scaffold: `Load` does not compose a real reusable capability, and `Arbitrate` returns false.
- The current contract-test suite adapts the versioned root to the published FSM_COS `0.1.0-alpha.5` API. It does not prove direct JSON deserialization into FSM_COS, repository artifact retrieval, or AnyApp end-to-end presentation.

## Next proof sequence

1. Define a deterministic, validated conversion from raWWar authoring/runtime metadata to AnyApp's publication manifest; do not duplicate the host DTO without a reason.
2. Decide and test how the repository catalog receives verified addresses for the full dependency closure without changing FSM_COS root semantics accidentally.
3. Create a test artifact or fake repository fixture that exercises version/hash verification, materialization, dependency lookup, cancellation, and clear failure modes without publishing.
4. Add a real raWWar capability only after verifying an existing Workshop bundle contract suitable for the behavior.
5. Add a host-level test proving the composed raWWar surface is consumed, not merely assembled.
