# raWWar — Maintained Work Queue

**Owner:** raWWar engineering assistant (raWWar repository only)  
**Working branch:** `development`  
**Current integration PR:** [#2 — Reset raWWar as a manifest-driven Experience](https://github.com/TrentBest/raWWar/pull/2)  
**Last reviewed:** 2026-10-09  
**Purpose:** Durable handoff and execution queue. Update this file as work is completed, priorities change, or the conversation is reset. The repository—not chat history—is the source of truth.

## Operating rules

- Work systematically from the highest-priority actionable item; keep this list current as work proceeds.
- **Repository boundary:** make changes only in `TrentBest/raWWar`. Other repositories are external dependencies. Record the exact requirement/blocker here, but do not open issues, edit files, create branches/PRs, or otherwise perform implementation work in those repositories. The creator coordinates their responsible LLMs.
- Treat creator-authored vision as authoritative. Mark uncertain decisions **Candidate**, **Experiment**, or **Illumination Needed** instead of quietly making them canon.
- raWWar owns game/world meaning. Reuse Workshop capabilities for FSMs, composition, MicroBundles, input, hosting, rendering, persistence and networking where those capabilities actually exist.
- Verify APIs and package versions from their source repositories before depending on them. Do not invent APIs or duplicate generic infrastructure inside raWWar.
- Record cross-repository needs as `docs/requests/REQUEST-FROM-raWWar-<Dependency>-<Capability>.md`; maintain the convention and active requests in [the request index](docs/requests/README.md). These are raWWar-owned requirements for review by the receiving repository's agent, not assignments or permission to change that repository.
- Keep canonical world state independent from cameras, render loops, GUI, textures and presentation caches. Queries at an explicit logical time should be deterministic and order-independent where the model promises it.
- Add or extend executable contract checks with each meaningful data/model change. Run CI and report actual results; never call a pending run green.
- Do **not** merge PRs or publish NuGet packages without explicit creator approval. A GitHub token is permission to continue engineering, not permission to merge or publish.
- Keep changes reviewable and explain what changed, why, how it was verified, and what remains.

## Current state (verified during this review)

- [x] **Integration PR #2 remains open and unmerged** (verified 2026-10-09); it is the current integration lane. Do not create a competing PR without a reason.
- [x] Repository has been reset to a .NET 8, manifest-oriented raWWar Experience; old engine-bound shell is no longer the architecture.
- [x] A .NET 8 executable contract-check project exists; it has no test-framework dependency, and now uses a test-only FSM_COS package reference to prove root assembly.
- [x] The first spatiotemporal implementation slice includes immutable `KeplerOrbit` / `Vector3d` types for explicit-time two-body elliptic orbit queries.
- [x] The design foundation, visual atlas, content catalogues, and engineering-data documents are substantial and should be extended rather than replaced.
- [x] Added a test-only in-memory catalog and composed the checked-in raWWar root through the same published FSM_COS `0.1.0-alpha.5` package currently referenced by AnyApp. [Run 37985434148](https://github.com/TrentBest/raWWar/actions/runs/37985434148) passed 175/175 checks with 0 warnings/errors. The test validates manifest version against the bundle descriptor, then adapts the root to alpha.5's unversioned `MicroBundleDependencyRequest` API; it does not claim direct runtime-manifest deserialization.
- [ ] **Manifest/host integration gap verified:** raWWar's authoring and runtime manifests are not AnyApp's publication manifest. AnyApp `development` does have a repository-backed `ComposeAsync` path, but raWWar has no immutable artifact identity and is not in the compiled native catalog. See [the manifest bridge investigation](docs/integration/ANYAPP_MANIFEST_BRIDGE.md), including a second verified issue: the current repository catalog queues transitive dependencies but only has artifact addresses listed in the publication manifest, so missing dependency addresses fail before composition. Preserve version validation before adapting to the currently consumed FSM_COS API.
- [x] Fixed the project-file manifest path mismatch: the root authoring manifest and runtime manifest are now explicitly linked into the raWWar project output and packed as content files.
- [x] Added and passed a CI step that locally packs the Experience package and checks both manifest files in the `.nupkg` (run [37985018118](https://github.com/TrentBest/raWWar/actions/runs/37985018118)); packaging verification only, not publication.
- [ ] **The actual Experience composition remains a scaffold.** `RaWWarMicroBundle.Load` currently validates its context but composes no capabilities; `Arbitrate` returns `false`. The manifest/runtime-manifest relationship and host integration still need a tested end-to-end contract.
- [x] Corrected an inaccurate interaction status claim: the current source does not contain `Interaction/PhysicalControlResolver.cs`, and the contract tests do not prove keyboard/VR physical-control equivalence. The interaction document now labels that work as design-level and lists the first required proofs. Do not report the interaction slice as implemented until source and tests exist.
- [x] Added spatiotemporal numerical boundary checks in `tests/raWWar.ContractTests/Program.cs`: non-finite logical time, non-finite gravitational parameter, high-eccentricity finite output, periodicity, and periapsis bound. These passed in [run 37988442216](https://github.com/TrentBest/raWWar/actions/runs/37988442216): 180/180 checks, 0 warnings, and 0 errors.
- [x] Reconciled the contract-test README with the actual suite; it documents the current contract areas and distinguishes local composition proof from end-to-end integration (175 checks on the latest verified run).
- [x] Verified the workflow triggered by the work-queue commit: [run 37984743404](https://github.com/TrentBest/raWWar/actions/runs/37984743404) succeeded; build had **0 warnings and 0 errors**, and the executable suite reported **171/171 checks passed**. This validates that commit only; re-check CI after subsequent changes.

## P0 — Establish a trustworthy current baseline

- [x] Confirmed PR #2 is open/unmerged and the current integration lane is `development`; workflow run 37984743404 passed on the queue commit. Re-check head SHA and CI after subsequent edits.
- [x] Reviewed the .NET 8 projects, executable contract checks, root `manifest.json`, `runtime-manifest.json`, and `data/workshop-composition-contract.json`. Many architecture assertions are present, but end-to-end composition is not implemented.
- [ ] Finish the ecosystem API/version inventory. Verified so far: MicroBundleDomain `1.0.1`; AnyApp uses FSM_COS `0.1.0-alpha.5`; FSM_COS `development` has the newer versioned-root contract. Still verify FSM_API, FSM_UserIO, and the approved integration plan. Do not upgrade or publish packages as part of this inventory.
- [ ] Reconcile the Experience authoring manifest, FSM_COS runtime manifest and AnyApp publication manifest. Define one explicit, tested source of truth for Experience/runtime/bundle identity, version pinning, configuration, artifact identity and supported manifestations. AnyApp's current native `AnyAppMicroBundleCatalog` is compiled and only registers Moniker and Forge; the repository-backed catalog is implemented on AnyApp `development` but does not yet prove raWWar loading. The local-pack CI check passed on run 37985018118; raWWar-to-AnyApp loading remains unproven. Track the dependency-address closure constraint in [the bridge contract](docs/integration/ANYAPP_MANIFEST_BRIDGE.md).
- [x] Queue records the verified baseline and 171/171 check count for run 37984743404.
- [x] Request convention, MicroBundleDomain experience-configuration request, AnyApp artifact-closure request, and links from the manifest bridge were committed on `development`; workflow run [37989886690](https://github.com/TrentBest/raWWar/actions/runs/37989886690) completed successfully. This is documentation/queue validation, not proof of AnyApp loading.
- [ ] Next bridge work remains blocked on the receiving repository's artifact-closure contract; continue independent raWWar-owned validation and design without inventing an external API.

## P1 — Make the Experience actually compose

- [ ] Replace the empty MicroBundle scaffold with the smallest real composition that uses an existing Workshop capability. If the current contracts cannot express the needed behavior, document the exact missing platform contract rather than faking it locally.
- [ ] Add contract checks for manifest parsing/validation, identity consistency, declared dependencies, missing capabilities, deterministic loading/arbitration, and invalid configurations as supported by the real API.
- [ ] Ensure the Experience can be loaded by its intended host (AnyApp first) without raWWar taking ownership of host lifecycle or generic manifest machinery. The repository catalog and `ComposeAsync` exist on AnyApp `development`; prove artifact-address closure, immutable identity validation, and actual raWWar manifestation before relying on that path.
- [ ] Ensure packaging remains non-publishing by default; `GeneratePackageOnBuild=false` and no automated NuGet publication without explicit approval.
- [ ] Expand the contract-test README so every stated check matches the actual code and clearly states model limits.

## P1 — Protect authoritative world-state contracts

- [ ] Locate and reconcile the actual spatiotemporal world-model document and machine-readable galaxy/world contract (paths have changed from earlier notes; discover actual paths rather than assuming names).
- [ ] Validate stable identity, explicit logical time, coordinate-frame conventions, supported orbit domains, deterministic queries and unsupported cases. Added explicit tests for non-finite time/parameter rejection and a high-eccentricity elliptic boundary; [run 37988442216](https://github.com/TrentBest/raWWar/actions/runs/37988442216) passed these code changes with 180/180 checks, 0 warnings, and 0 errors. The newer queue/documentation commit's run is tracked separately.
- [ ] Keep derived GPU/texture state explicitly packed and bounded; never treat a texel as a full FSM record/history or let camera/rendering operations mutate canonical state.
- [ ] Continue cross-catalogue validation for unique IDs, valid joins, prerequisites, physical capacities, materials, staffing, power/thermal budgets, installed upgrades, ship parts and causal damage/salvage records.
- [ ] Keep every numeric engineering example labeled as candidate/illustrative until the full inventory and constraints reconcile. No fake precision.

## P2 — Deliver one meaningful first-person vertical slice

- [ ] Define a station-and-rig interaction slice grounded in the creator's vision: the station physically secures the soldier, connects them to the system, and presents the rig and hands/grips appropriate to the station's real function.
- [ ] Start with one role (fighter pilot is the current example) and trace the whole chain: station state → qualified occupant → physical controls → FSM/procedure → authoritative world action → durable outcome/event → observable feedback.
- [ ] Model requirements and behavior as data and reusable FSM/MicroBundle capabilities where appropriate; avoid a pile of one-off animation scripts.
- [ ] Add deterministic tests for valid/invalid occupancy, qualification/access, control availability, interruption/failure and outcome recording. Separate interaction semantics from desktop/VR presentation.
- [ ] Host the slice through AnyApp first. Keep MyVR as a later manifestation of the same Experience rather than a separate game implementation.

## P2 — Close the highest-value world-model gaps

- [ ] Establish the canonical world event/history contract: initiating cause, logical time, affected identities, pre/post state or changes, causal links, provenance and deterministic replay/reconstruction expectations.
- [ ] Define how expensive world work is scheduled/deferred without dropping consequences or allowing presentation distance to freeze the authoritative world.
- [ ] Reconcile the Wayfarer reference ship's part inventory and mass roll-up before presenting it as a fully sized design; continue center-of-mass/inertia, connections, structure, power, thermal, pressure, access, damage and salvage validation.
- [ ] Promote mature content-lab concepts into authoritative records only when their status and prerequisites are clear; preserve Canon/Candidate/Experiment/Illumination Needed labels.

## P3 — Keep the design experience coherent

- [ ] Maintain the master GDD as the primary reading path; companion documents and the Design Hub are views into the same design knowledge, not competing sources of truth.
- [ ] Keep the visual asset catalogue accurate. Distinguish conceptual diagrams from runtime captures and construction-ready engineering drawings.
- [ ] Add visuals where they materially clarify systems, relationships, timelines, equipment, work and consequences; do not add decorative art in place of a tested model.
- [ ] Keep navigation links valid and clean malformed escaped-newline artifacts when encountered.
- [ ] Maintain clear non-coder explanations alongside technical deep dives: what it is, how it works in raWWar, and which Workshop capability enables it.

## Definition of done for a work item

1. The change fits the Experience/Workshop boundary and preserves the creator's stated vision.
2. The smallest useful behavior or data contract is implemented—not merely described—when implementation is the objective.
3. Relevant executable checks pass, or the exact failure/blocker is recorded.
4. Documentation and catalogue references match the implementation.
5. The current branch/commit and CI status are verified.
6. This queue is updated, and a concise progress note is added to PR #2 when useful.
7. No merge or package publication occurs without explicit approval.

## Handoff for a resumed conversation

Start by reading this file, then inspect the actual current GitHub state (branches, PR #2, latest commit, workflows, open issues, and the files named in the top unchecked items). Treat this file as a prioritized guide, not proof that a task is still outstanding: verify before acting. Continue on `development` unless the creator redirects you. Ask the creator only when a real design decision is blocked on their intent; otherwise make safe, reversible engineering decisions and keep moving.


## Integration investigation update — 2026-10-09

- [x] Added [docs/integration/ANYAPP_MANIFEST_BRIDGE.md](docs/integration/ANYAPP_MANIFEST_BRIDGE.md), recording the three distinct manifest contracts and source links to AnyApp's current implementation.
- [x] Verified AnyApp `development` calls `ComposeAsync` from its launch path; this supersedes older notes that described only a compiled catalog. The compiled catalog still contains only Moniker and Forge.
- [ ] Investigate the repository catalog's dependency-address model before writing an adapter: the catalog queues declared dependencies but its address dictionary is populated only from `ExperienceManifest.Bundles`. An unlisted dependency therefore fails with “No repository address is registered”. The design must resolve this without accidentally changing which bundles are FSM_COS roots.
- [ ] Keep the existing local composition proof scoped accurately: it tests the published FSM_COS alpha.5 adapter with an in-memory catalog, not AnyApp repository retrieval or a game capability.


## Configuration and provider contract clarification — 2026-10-09

- [x] Recorded the creator's intended responsibility chain in [Configuration and Provider Resolution](docs/architecture/CONFIGURATION_AND_PROVIDER_RESOLUTION.md): the Experience supplies parameter-ID/value overrides; the owning MicroBundle defines parameter meaning/defaults/validation and exposes providers; consumers use checked lookup and handle absence explicitly.
- [x] Distinguished parameter IDs, literal values, and provider IDs. Configuration transport is not the semantic configuration contract; missing overrides preserve MicroBundle defaults.
- [ ] Verify the exact MicroBundleDomain provider-lookup API and configuration-source contract from the current source branch before writing implementation against it.
- [ ] Evaluate ProtocolAi as an optional stable-vocabulary aid for parameter/provider identities. Do not add a mandatory dependency without an executable use case; it must not become an LLM/runtime requirement.
- [ ] Add executable checks for default behavior, valid/invalid overrides, duplicate/unknown parameter IDs, provider present/absent, and required versus optional capability handling when the owning API can express them.
- [ ] Continue the AnyApp bridge separately: solve immutable artifact identities for dependency closure without accidentally promoting every dependency to an FSM_COS root.


### API verification note

- [x] Checked the current `TheSingularityWorkshop.MicroBundleDomain` `development` documentation: `IMicroBundleLoadContext.TryGetConfiguration(bundleId, out configuration)` provides opaque configuration bytes, and Adventure 4 demonstrates schema-like `MicroBundleDefinition` / `MicroBundleField` descriptions.
- [ ] **Version boundary remains open:** raWWar currently references NuGet MicroBundleDomain `1.0.1`, while the current Domain `development` getting-started guide describes `2.0.0-alpha.1`. Do not assume development-only configuration APIs exist in 1.0.1; verify the exact published contract or intentionally stage a package upgrade only with its broader integration implications understood.
- [ ] Current source/documentation inspection did not establish a public provider-lookup method named `TryGetProvider`; verify the actual descriptor/provider API from source before implementing lookup. Do not invent an API from the conceptual contract.


## External dependency constraints — raWWar-owned tracking only

- [x] Clarified the repository boundary: this assistant implements only raWWar. Other Workshop repositories are inspected only as needed to verify a dependency contract or identify a blocker; no external implementation work is performed from this lane.
- [ ] When MicroBundleDomain's owner finalizes the Experience override contract, verify the actual published package/API relevant to raWWar before consuming it. The Experience must supply parameter-ID-to-literal-value overrides only; MicroBundles own parameter meaning/defaults/validation and provider availability.
- [ ] When AnyApp's owner resolves artifact closure identity, verify the supported manifest path for raWWar without promoting transitive dependencies into runtime roots accidentally.
- [ ] Keep external blockers concise here and continue raWWar-owned design, data validation, and tests that do not depend on those changes.

