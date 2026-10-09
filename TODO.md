# raWWar — Maintained Work Queue

**Owner:** raWWar engineering assistant (raWWar repository only)  
**Working branch:** `development`  
**Current integration PR:** [#2 — Reset raWWar as a manifest-driven Experience](https://github.com/TrentBest/raWWar/pull/2)  
**Last reviewed:** 2026-10-09  
**Purpose:** Durable handoff and execution queue. Update this file as work is completed, priorities change, or the conversation is reset. The repository—not chat history—is the source of truth.

## Operating rules

- Work systematically from the highest-priority actionable item; keep this list current as work proceeds.
- **Branch discipline (creator-defined):** persistent branches are limited to `master`, `development`, `alpha`, and `beta`. Use a short-lived transient branch only when work benefits from isolation; once its functionality is verified, merge it into `development` and remove the transient branch. Do not create additional long-lived branches or parallel integration lanes. Continue on `development` by default. Never merge or delete branches without checking the intended target and current state; do not treat this policy as permission to merge without the creator's approval.
- **Branch inventory follow-up:** GitHub currently lists `master`, `development`, `Alpha`, and `Release`. The intended persistent set was described as master/development/alpha/beta, so `Release` versus `beta` and branch-name casing remain a discrepancy to reconcile deliberately; do not rename or delete branches unilaterally.
- **Repository boundary:** make changes only in `TrentBest/raWWar`. Other repositories are external dependencies. Record the exact requirement/blocker here, but do not open issues, edit files, create branches/PRs, or otherwise perform implementation work in those repositories. The creator coordinates their responsible LLMs.
- Treat creator-authored vision as authoritative. Mark uncertain decisions **Candidate**, **Experiment**, or **Illumination Needed** instead of quietly making them canon.
- raWWar owns game/world meaning. Reuse Workshop capabilities for FSMs, composition, MicroBundles, input, hosting, rendering, persistence and networking where those capabilities actually exist.
- Verify APIs and package versions from their source repositories before depending on them. Do not invent APIs or duplicate generic infrastructure inside raWWar.
- Maintain `examples/README.md` as the usage-example index. When consuming another package, API, schema, or capability, add a checked-in raWWar-side usage example, record the exact verified version/commit, cover important boundaries, link it from relevant docs, and state what it does not prove. Prefer runnable examples and keep them aligned with contract tests; examples are both documentation source material and agent handoff artifacts.
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
- [x] Corrected the earlier inaccurate `PhysicalControlResolver` claim, then added a narrow `FighterPilotStation` domain prototype and executable contract checks for occupancy, restraint/connect/raise ordering, qualification, control readiness, bounded pilot intent, desktop/VR parity, damaged/unpowered blocking, invalid inputs, and emergency release. This is not a generic interaction framework. Build, package-content check, contract suite, and example all passed in [run 38001820718](https://github.com/TrentBest/raWWar/actions/runs/38001820718) at code/test head `7ec40343f15a15639951a096165169cf991d7920`; the suite reported **195/195 checks passed**. Subsequent commits add documentation/visuals and a comment-only clarification that the pilot axes are candidates; no behavior changed after the verified code/test head.
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
- [x] Added a runnable `KeplerOrbit` usage example under `examples/`, indexed it with the manifest/composition-contract examples, and added the example project to the solution. The example shares the real raWWar project and has a repeatability guard; the workflow now builds the solution and runs the example. The GitHub connector has not exposed a workflow run/status for the latest push-triggered commit, so verification is pending rather than claimed green.

## P1 — Protect authoritative world-state contracts

- [x] Located the actual world-model sources: `docs/SPATIOTEMPORAL_WORLD_MODEL_AND_OBSERVATION.md`, `data/galaxy-generation-contract.json`, and `data/milky-way-reference-model.json`. The first defines the conceptual time-addressed query and observer boundary; the machine-readable galaxy contract records spatial addressing, deterministic identity/event keys, persistence, and explicit unresolved implementation choices. This discovery does not mean the model is implemented or fully reconciled.
- [ ] Extend the executable world-model proof beyond the current Keplerian evaluator: implement and test canonical spatial-address encoding/hash reference vectors, explicit domain time ranges/units, coordinate transforms, and event/history reconstruction. The galaxy contract explicitly marks Squirrel3-style verified reference vectors as not yet added; do not claim the generator or replay model is implemented.
- [ ] Validate stable identity, explicit logical time, coordinate-frame conventions, supported orbit domains, deterministic queries and unsupported cases. Added explicit tests for non-finite time/parameter rejection and a high-eccentricity elliptic boundary; [run 37988442216](https://github.com/TrentBest/raWWar/actions/runs/37988442216) passed these code changes with 180/180 checks, 0 warnings, and 0 errors. The newer queue/documentation commit's run is tracked separately.
- [ ] Keep derived GPU/texture state explicitly packed and bounded; never treat a texel as a full FSM record/history or let camera/rendering operations mutate canonical state.
- [ ] Continue cross-catalogue validation for unique IDs, valid joins, prerequisites, physical capacities, materials, staffing, power/thermal budgets, installed upgrades, ship parts and causal damage/salvage records.
- [ ] Keep every numeric engineering example labeled as candidate/illustrative until the full inventory and constraints reconcile. No fake precision.

## P2 — Deliver one meaningful first-person vertical slice

- [x] Define and prototype the first station-and-rig contract from the creator's vision: station occupancy → physical restraint state → interface connection → raised control rig → bounded pilot-control request. Visible hands/grips and station animation remain presentation work, not implemented behavior.
- [ ] Start with one role (fighter pilot is the current example) and trace the whole chain: station state → qualified occupant → physical controls → FSM/procedure → authoritative world action → durable outcome/event → observable feedback.
- [ ] Model requirements and behavior as data and reusable FSM/MicroBundle capabilities where appropriate; avoid a pile of one-off animation scripts.
- [x] Add deterministic contract checks for valid/invalid occupancy, qualification/access, control availability, power/damage failure gates, bounded axes, emergency release, and desktop/VR semantic parity.
- [ ] Add partial/interrupted securing and connection procedure handling with durable action/outcome history; keep semantic interaction separate from desktop/VR presentation.
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

## Examples are part of the engineering contract

- Every newly consumed external capability should have a raWWar-side example that shows the intended call/data shape, relevant assumptions, and meaningful success/boundary/failure behavior where applicable.
- Prefer runnable examples that compile against the exact package/source version being claimed. If only a proposed fixture is possible, label it as unverified and explain why.
- Link the complete relevant example set from the architecture/API documentation. Generate or expand documentation from the example set rather than selecting one convenient snippet and treating it as exhaustive.
- Keep examples in the solution or otherwise verify them in CI when practical. Report builds, runs, and contract tests separately; never equate a checked-in example with a passing test or end-to-end integration proof.
- Record negative scope explicitly: what an example does not establish about host loading, execution, persistence, networking, rendering, or release readiness.

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

## Documentation integrity pass — 2026-10-09

- [x] Removed duplicated paragraphs from `docs/TECHNICAL_DESIGN.md` and clarified the difference between the implemented explicit-time elliptic-orbit slice and still-planned world/renderer/host capabilities. Added links to the relevant world-model docs and manifest bridge.
- [x] Fixed the malformed Milky Way visual-atlas entry in `docs/README.md`.
- [x] Re-checked CI for the latest documentation commit: workflow run [37998973548](https://github.com/TrentBest/raWWar/actions/runs/37998973548) completed successfully for head `7d3e8cd3c950fefe35e535535d45fa39d2f4bfef`.


## Documentation standard alignment — 2026-10-09

- [x] Analyzed the shared [FSM_COS Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/docs/ecosystem-documentation-standard/DOCUMENTATION_STANDARD.md) on its `docs/ecosystem-documentation-standard` branch. It defines semantic README IDs 00–13, marker colors, reader paths, ownership boundaries, evidence discipline, visuals, runnable-example expectations, and the rule to **edify, not mystify**. The reference is still a proposal under review; do not represent it as merged or universally adopted.
- [x] Reworked the raWWar README around the shared section identifiers and color markers, including a responsibility-first explanation of FSM_COS, the manifest/runtime/host boundary, reader paths, developer commands, examples, implementation status, and Workshop footer.
- [x] Added [Documentation Conformance](docs/DOCUMENTATION_CONFORMANCE.md) to record what has been applied and what remains to be audited.
- [x] Linked the conformance guide from the documentation index.
- [x] Reviewed Experience Architecture's responsibility boundary and added an implementation-evidence table separating package/manifest checks, local FSM_COS composition, orbit-example coverage, and the unimplemented station-and-rig/host integration.
- [x] Clarified Game Design Bible authority and added purpose/owner/audience/evidence metadata to the Bible, GDD, Technical Design, and Production Plan.
- [x] Added scope/status/evidence metadata to Vision and Pillars, Systems Design, Player Roles, and UX and Interaction.
- [x] Updated the conformance audit with concrete findings and a more precise remaining-work list.
- [x] Added status/owner/audience/evidence metadata to `docs/OPEN_QUESTIONS.md`; clarified that unanswered questions are not canon and normalized the Gesture question heading.
- [x] Added status/owner/audience/evidence metadata to `docs/images/README.md`, explicitly separating explanatory artwork from runtime evidence and engineering-ready assets.
- [ ] Complete the line-by-line audit of the GDD and companion design docs for contradictions, source/API accuracy, and link validity; the current pass is not exhaustive.
- [x] Recorded a concrete unresolved campaign-opening/faction-selection conflict as [Open Question 22](docs/OPEN_QUESTIONS.md): faction choice among thirteen doors is not yet reconciled with the player already being a Commander and the Empress's short compliance deadline. Preserved the conflict rather than silently choosing canon.
- [x] Extended the relative Markdown file-target audit to another 12 specialist/design documents; no missing local file targets were found in that historical batch. The later repository-wide inline-link, Markdown fragment, raw HTML attribute, and SVG-reference checks are recorded in the Documentation alignment follow-up below.
- [x] Re-checked CI for the latest documentation commit; the current head's workflow run completed successfully (run [37998973548](https://github.com/TrentBest/raWWar/actions/runs/37998973548)).

## Documentation alignment follow-up — 2026-10-09

- [x] Re-read the current FSM_COS documentation-standard proposal rather than relying only on the prior summary. The proposal emphasizes accurate identity, applicable README section IDs, audience paths, precise boundaries, evidence, visuals, runnable-or-labeled examples, and link validation. It remains a proposal on the FSM_COS documentation branch, not a merged ecosystem mandate.
- [x] Added README section **00 — identity** and real MIT-license / development-CI badges. Deliberately did not add a NuGet release badge: raWWar is an Experience and this branch has not been published as a package release.
- [x] Updated the conformance record to describe the actual section-00 treatment.
- [x] Corrected the public README and PR summary so they no longer imply the faction-selection/Empress-order chronology is settled; both now point to Open Question 22.
- [x] Re-checked CI after the README/conformance updates: PR-triggered run [38000282573](https://github.com/TrentBest/raWWar/actions/runs/38000282573) passed at head `b07d1312584a964878475df31b366fc26bceeaf8`. A subsequent queue-only note cleanup is on the current head and its own run must be checked before reporting the current head green.
- [x] Completed a repository-wide relative inline-Markdown-link target pass across all 102 Markdown files: 263 link targets checked, no missing local file targets found. This does not yet validate heading anchors, reference-style links, raw HTML `href`/`src` attributes, or external URL availability.
- [x] Checked all 21 checked-in SVG assets for `href` / `xlink:href` references; none were present, so there were no local SVG-linked assets to resolve.
- [x] Scanned all 102 Markdown files for raw HTML `href` / `src` attributes and inline Markdown fragment links; none were found in the checked content.
- [ ] Validate reference-style Markdown links and external URL availability, then continue the line-by-line design-contradiction audit.
