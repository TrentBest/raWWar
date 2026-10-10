# raWWar — Technical Design

**Status:** Living technical companion; implementation evidence is explicitly separated from intended architecture.  
**Owner:** raWWar Experience engineering.  
**Audience:** Contributors deciding what belongs in raWWar, what should be consumed from the Workshop, and what is actually verified.  
**Evidence rule:** Architecture diagrams and future-tense requirements describe intent, not completed integration.

## 00. Identity and purpose

This document defines the technical boundaries needed to build raWWar as an Experience without duplicating reusable Workshop machinery. It is subordinate to explicit creator design authority for game meaning and does not claim every described subsystem exists.

## 01. Responsibility boundary

- **raWWar owns:** world meaning, soldiers, factions, rules, procedures, game-specific data, content, and consequences.
- **MicroBundleDomain owns:** MicroBundle contracts.
- **FSM_API owns:** state-machine primitives and execution semantics.
- **FSM_COS owns:** capability composition; it is not the game loop, host, or renderer.
- **AnyApp owns:** host/manifestation lifecycle and artifact-loading responsibilities.
- **Workshop Renderer owns:** observer-relative presentation, not authoritative world truth.

## 02. Architecture at a glance

```text
raWWar Experience definition
        │ declares requested capability roots
        ▼
MicroBundle contracts and capability definitions
        │ validated composition request
        ▼
FSM_COS — composition boundary
        │ composed RuntimeAssembly
        ▼
AnyApp or another supported host — execution / manifestation
        │ authoritative state supplied for observation
        ▼
Workshop Renderer — observer-relative presentation
```

This is an **intended responsibility flow**, not a claim that every edge is implemented end to end. In particular, the current authoring/runtime manifests are not interchangeable with AnyApp's publication manifest, and repository-backed artifact closure is not yet proven for raWWar. See the [AnyApp manifest bridge investigation](integration/ANYAPP_MANIFEST_BRIDGE.md) for the exact blockers.

The diagram is conceptual: it does not imply that the renderer is the source of truth, that FSM_COS runs the game loop, or that every capability must be a separate package.

## Architectural position

raWWar is an Experience.

The intended dependency direction is:

Experience Manifest  
→ MicroBundles  
→ FSM_COS  
→ Host / manifestation  
→ Renderer

The Experience owns its meaning. Hosts and renderers provide execution and presentation capabilities.

## Simulation

FSM_API is the behavioral foundation for state-driven entities and interdependent procedures.

The goal is to express behavior through data, relationships, requirements, providers, and FSMs rather than hard-coding a bespoke implementation for every occupation.

Gestures extend this model into physical motion. A Gesture is a sequence of poses plus a mathematical transition rule. Gesture FSMs can be supplied by the capability that owns a physical interaction, such as a vehicle or building MicroBundle. The general Gesture pipeline remains an architectural direction; do not infer that a complete runtime pipeline exists from this description.

### Fighter-pilot station contract — historical prototype, not current code

A narrow `FighterPilotStation` domain prototype was previously implemented at code/test head `7ec40343f15a15639951a096165169cf991d7920`. Its executable contract suite passed 195/195 checks in [CI run 38001820718](https://github.com/TrentBest/raWWar/actions/runs/38001820718), covering occupancy, qualification, restraint/connect/raise ordering, bounded pilot intent, desktop/VR intent parity, failure gates, and emergency release. That prototype was subsequently reverted; the current branch must not be described as containing it or its tests until they are reintroduced and verified.

The historical prototype was a **domain-contract experiment**, not the reusable Workshop physical-interaction capability. Even at that revision, it did not animate restraints or hands, model actual forces, persist an event stream, run aircraft control laws, move a vehicle, or prove AnyApp execution. The candidate [Fighter Pilot Action Slice](FIGHTER_PILOT_ACTION_SLICE.md) records a possible future causal chain; it is not implementation evidence.

### Current spatiotemporal implementation slice

The code currently includes immutable `KeplerOrbit` and `Vector3d` types for querying an elliptic two-body orbit at an explicit logical time. The contract checks exercise repeatability, periodicity, periapsis/apoapsis bounds, inclination, non-finite input rejection, and a high-eccentricity elliptic case.

This is deliberately narrower than a complete world simulation. It does not establish an integrated galaxy state, general coordinate-frame transformations, multi-body dynamics, persistence, or host-level time advancement.

Related design contracts:

- [Mapping the Milky Way](MAPPING_THE_MILKY_WAY.md) — scientific reference, procedural population, and presentation boundaries.
- [Navigation Frames and Chart Evolution](NAVIGATION_FRAMES_AND_CHART_EVOLUTION.md) — frame identity, epoch, transformation provenance, and uncertainty.
- [Authored Galaxies, Campaigns, and Lineage](AUTHORED_GALAXIES_CAMPAIGNS_AND_LINEAGE.md) — world-generation and reproducibility goals.

## Renderer

The Workshop Renderer is its own technology and should remain platform/API independent.

It turns authoritative world state into an appropriate presentation; it does not define world state.

Observation-relative detail and event-horizon/level-of-detail concepts belong in the rendering architecture without allowing visual detail to become simulation truth.

The Renderer should be capable of presenting enormous populations without requiring a traditional CPU animation instance for every entity. Candidate GPU state representations may encode compact per-entity state and allow compute shaders to transform only the relevant channels.

Gesture evaluation is a potential Renderer workload: authoritative FSM/state determines what should happen, while GPU-oriented computation determines how that state becomes large-scale visible motion. The GPU representation is derived presentation state, not a substitute for authoritative records or durable history.

## Manifest and MicroBundles

raWWar should remain manifest-driven.

Capabilities should be independently composable where practical. MicroBundles should represent meaningful capability boundaries rather than arbitrary code packaging.

For physical interaction, a vehicle MicroBundle could own the vehicle's interaction points and Gesture Providers rather than requiring raWWar to contain vehicle-specific animation logic. This is an ownership goal, not proof that the provider pipeline has been implemented.

The current manifest files, local package checks, and test-only FSM_COS composition establish only the contracts explicitly covered by those checks. They do not establish successful AnyApp artifact retrieval or a functioning game loop. See [Experience Architecture](EXPERIENCE_ARCHITECTURE.md) and the [AnyApp manifest bridge investigation](integration/ANYAPP_MANIFEST_BRIDGE.md).

## Data

The Experience will require explicit schemas for:

- entities and stable identities;
- qualifications and roles;
- equipment, vehicles, and factions;
- facilities and procedures;
- resources, rules, and relationships;
- events and causal history;
- persistence and content versions.

These are domain concerns. Reusable serialization, execution, composition, hosting, and rendering machinery should remain in the Workshop where it can serve other Experiences.

## Determinism and reconstruction

Persistent systems should be designed so state can be reconstructed or resumed without relying on a rendered scene as the source of truth.

Where a model promises deterministic queries at an explicit logical time, the answer should not depend on frame rate, camera state, or the order in which unrelated queries were made. Event/history contracts must preserve causal outcomes rather than allowing presentation distance or deferred execution to erase consequences.

## Performance

Performance requirements should be derived from intended world scale and observation requirements.

Distinguish simulation cost, persistence cost, network cost, and rendering cost.

For soldier populations, the target is to keep authoritative state compact and move large amounts of repetitive visual computation into GPU-parallel work. Event horizons may reduce pose/Gesture fidelity as distance increases. The same world can therefore contain very large populations without treating every visible soldier as an equally expensive simulation object. These are design goals, not benchmark claims until measured against an implemented workload.

## Build and deployment

The repository should remain lightweight and Experience-oriented.

AnyApp is the primary host target. Future manifestations should consume the same Experience definition rather than requiring a separate game implementation for each host.

Package creation and local package inspection are not publication. NuGet publishing remains gated by explicit creator approval.

## Technical unknowns

The exact schemas, serialization strategy, networking model, renderer contract, asset pipeline, simulation-fidelity model, and persistence implementation remain under design. The AnyApp manifest/artifact-closure bridge and actual Experience composition are also not yet proven end to end.

## Experience boundary

raWWar is an Experience, not a monolithic application. The technical design must distinguish **Experience-owned meaning** from **Workshop-provided machinery**.

- **raWWar:** world, soldiers, factions, combat, research, construction, missions, narrative, terrain, rules, and content.
- **MicroBundles:** reusable capability boundaries.
- **FSM_API:** behavioral primitives and state-machine execution.
- **FSM_COS:** composition boundary.
- **AnyApp:** host/manifestation.
- **Renderer:** observer-relative presentation.

When a new requirement appears, classify it first as Experience meaning, reusable Workshop capability, host/manifestation concern, or presentation concern. Only the first category automatically belongs in raWWar.
