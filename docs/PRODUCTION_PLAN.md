# raWWar — Production Plan

**Status:** Living, capability-based production framework.  
**Owner:** raWWar Experience production.  
**Audience:** Creator and contributors coordinating design, art, audio, engineering, and playable demonstrations.  
**Evidence rule:** A milestone is complete only when its acceptance evidence exists; a plan or source file alone is not proof of a working player-facing capability.

## 00. Identity and purpose

This document provides a production structure without pretending that implementation is ready before the design is known.

## Workstreams
### Design
- Game Design Document
- Game Design Bible
- Vision and Pillars
- Player Roles
- Persistent Universes
- Open Questions
- Systems Design
- Narrative Design
- Multiplayer Design

### Art
- Art Direction
- soldier design
- exoskeleton design
- equipment families
- vehicle families
- environments
- animation and Gesture authoring
- effects
- UI assets

### Audio
- Audio Direction
- environmental sound
- machinery
- voices
- communications
- music
- accessibility

### Technology
- FSM-driven simulation
- Gesture system
- GPU population-state processing
- Experience manifest
- MicroBundle composition
- Workshop Renderer
- asset/content pipeline
- persistence
- networking
- AnyApp host integration

### Experience production
- campaign opening
- first mission
- qualification progression
- first military base
- first vehicle
- first research interaction
- first construction interaction
- cooperative scenario
- persistent-universe prototype

## Milestones
Milestones should be capability-based rather than date-based until the design stabilizes.

### M0 — Design foundation
Document the game, identify contradictions, and maintain the open-question queue.

### M1 — First living slice
Demonstrate one soldier, one physical environment, one meaningful procedure, FSM-driven behavior, at least one reusable Gesture, individualized motion, and Workshop Renderer presentation.

### M2 — Occupational slice
Demonstrate multiple qualifications and at least two substantially different occupations sharing the same world.

### M3 — Military organization slice
Demonstrate staffing, training, readiness, command, procedures, and observable organizational behavior.

### M4 — Cooperative slice
Demonstrate multiple players participating in the same systemic scenario.

### M5 — Persistent slice
Demonstrate persistence, re-entry, consequences, and reconstruction.

### M6 — Large-scale world
Scale population, organizations, networking, persistence, and observation-relative detail toward the long-term vision.

## Definition of done
A feature is not complete merely because code exists.

A production feature should eventually have:
- design intent;
- authoritative data shape;
- ownership/dependency boundaries;
- player-facing behavior;
- test strategy;
- performance expectations;
- documentation;
- known limitations.

## Current priority

The creator has redirected the primary objective to defining the raWWar universe for the Autonomous Development pipeline. Sourcebooks are supporting references, not the default production task.

1. Establish the creator-canon envelope and unresolved setting decisions in [Universe Foundation and Campaign Zero](UNIVERSE_FOUNDATION.md).
2. Turn the Imperial Summons opening into a validated, deterministic authored campaign seed.
3. Build the historical spine, political atlas, galactic atlas, material economy, military atlas, and current-conflict chronology as connected data.
4. Produce human-readable atlas views and executable validation evidence alongside the structured records.
5. Use creator illumination for names, motives, political structures, faction identities, war origins, and other canon decisions; never invent them to satisfy automation.

The immediate goal is to make the intended universe precise enough that Autonomous Development can build from it without silently authoring canon on the creator's behalf.
