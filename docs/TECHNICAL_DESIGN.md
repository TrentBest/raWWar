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

## Renderer
The Workshop Renderer is its own technology and should remain platform/API independent.

It is responsible for turning authoritative world state into an appropriate presentation, not for defining the world state.

Observation-relative detail and event-horizon/LOD concepts belong in the rendering architecture without allowing visual detail to become simulation truth.

## Manifest and MicroBundles
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

## Build and deployment
The repository should remain lightweight and Experience-oriented.

AnyApp is the primary host target. Future manifestations can consume the same Experience definition.

## Technical unknowns
The exact schemas, serialization strategy, networking model, renderer contract, asset pipeline, simulation fidelity model, and persistence implementation remain under design.
