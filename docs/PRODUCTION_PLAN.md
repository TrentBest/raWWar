# raWWar — Production Plan

Status: Initial production framework.

## Purpose
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
- animation
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
Demonstrate one soldier, one physical environment, one meaningful procedure, FSM-driven behavior, and Workshop Renderer presentation.

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
Continue elicitation and design before committing to a large runtime implementation. The immediate goal is to make the intended Experience precise enough that implementation can follow the design rather than invent it.
