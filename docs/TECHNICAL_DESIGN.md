# raWWar — Technical Design

Status: Initial architecture document.

## Architectural position
raWWar is an Experience.

The intended dependency direction is:

Experience Manifest
→ MicroBundles
→ FSM_COS
→ Host / manifestation
→ Renderer

The Experience owns its meaning. Hosts and renderers provide execution/presentation capabilities.

## Simulation

FSM_API is the behavioral foundation for state-driven entities and interdependent procedures.

The goal is to express behavior through data, relationships, requirements, providers, and FSMs rather than hard-coding an enormous bespoke implementation for every occupation.

Gestures extend this model into physical motion. A Gesture is a sequence of poses plus a mathematical transition rule. Gesture FSMs can be supplied by the capability that owns a physical interaction, such as a vehicle or building MicroBundle.
FSM_API is the behavioral foundation for state-driven entities and interdependent procedures.

The goal is to express behavior through data, relationships, requirements, providers, and FSMs rather than hard-coding an enormous bespoke implementation for every occupation.

## Renderer

The Workshop Renderer is its own technology and should remain platform/API independent.

It is responsible for turning authoritative world state into an appropriate presentation, not for defining the world state.

Observation-relative detail and event-horizon/LOD concepts belong in the rendering architecture without allowing visual detail to become simulation truth.

The Renderer should be capable of presenting enormous populations without requiring a traditional CPU animation instance for every entity. Candidate GPU state representations can encode compact per-entity state and allow compute shaders to transform only the relevant channels.

Gesture evaluation is therefore a natural Renderer workload: the authoritative FSM/state determines what should happen, while GPU-oriented computation determines how that state becomes large-scale visible motion.
The Workshop Renderer is its own technology and should remain platform/API independent.

It is responsible for turning authoritative world state into an appropriate presentation, not for defining the world state.

Observation-relative detail and event-horizon/LOD concepts belong in the rendering architecture without allowing visual detail to become simulation truth.

## Manifest and MicroBundles

raWWar should remain manifest-driven.

Capabilities should be independently composable where practical. MicroBundles should represent meaningful capability boundaries rather than arbitrary code packaging.

This is especially important for physical interaction. A vehicle MicroBundle can own the vehicle's interaction points and Gesture Providers rather than requiring raWWar to contain vehicle-specific animation logic.
raWWar should remain manifest-driven.

Capabilities should be independently composable where practical. MicroBundles should represent meaningful capability boundaries rather than arbitrary code packaging.

## Data
The Experience will eventually require explicit schemas for:
- entities;
- identifiers;
- qualifications;
- roles;
- equipment;
- vehicles;
- factions;
- facilities;
- procedures;
- resources;
- events;
- rules;
- relationships;
- persistence;
- content versions.

## Determinism and reconstruction
Persistent systems should be designed so state can be reconstructed or resumed without relying on a rendered scene as the source of truth.

## Performance

Performance requirements should be derived from intended world scale and observation requirements.

The technical design should distinguish simulation cost, persistence cost, network cost, and rendering cost.

For soldier populations, the target is to keep authoritative state compact and move large amounts of repetitive visual computation into GPU-parallel work. Event horizons should reduce pose/Gesture fidelity as distance increases. The same world can therefore contain very large populations without treating every visible soldier as an equally expensive simulation object.
Performance requirements should be derived from intended world scale and observation requirements.

The technical design should distinguish simulation cost, persistence cost, network cost, and rendering cost.

## Build and deployment
The repository should remain lightweight and Experience-oriented.

AnyApp is the primary host target. Future manifestations can consume the same Experience definition.

## Technical unknowns
The exact schemas, serialization strategy, networking model, renderer contract, asset pipeline, simulation fidelity model, and persistence implementation remain under design.


## Experience boundary

raWWar is an Experience, not a monolithic application. The technical design must distinguish **Experience-owned meaning** from **Workshop-provided machinery**.

- **raWWar:** world, soldiers, factions, combat, research, construction, missions, narrative, terrain, rules, and content.
- **MicroBundles:** reusable capability boundaries.
- **FSM_API:** behavioral primitives and state-machine execution.
- **FSM_COS:** composition boundary.
- **AnyApp:** host/manifestation.
- **Renderer:** observer-relative presentation.

When a new requirement appears, classify it first as Experience meaning, reusable Workshop capability, host/manifestation concern, or presentation concern. Only the first category automatically belongs in raWWar.
