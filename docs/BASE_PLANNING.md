# raWWar — 3D Base Planning and Construction State

Status: Living design model.

> **The player does not place a building icon. The player lays out a physical military installation.**

## 1. The base is spatial

The base exists in three-dimensional space.

Buildings have:

- position;
- orientation;
- footprint;
- height/volume;
- terrain relationship;
- access requirements;
- infrastructure relationships;
- construction state;
- operational state.

Non-orthogonal placement is intentional.

A building may be rotated to:

- follow terrain;
- face a road;
- align with a runway;
- create a defensive field;
- connect to another structure;
- optimize logistics;
- fit an irregular site;
- create the physical character of the base.

There is no universal rectangular-grid requirement.

## 2. Palette to world

The player can drag a building from a palette into the world.

The sequence is:

    Choose capability
    → drag building
    → ghost appears
    → position
    → rotate
    → inspect physical relationships
    → commit plan
    → construction becomes scheduled
    → construction crew begins work
    → building becomes physically complete
    → capability becomes available when requirements are satisfied

The ghost is a planning representation, not a building.

## 3. Visual construction states

### Ghosted — Future

The building is only a proposed future object.

It has:

- no construction activity;
- no physical obstruction;
- no operational capability.

### Semi-transparent — Planned

The player has committed the building to the base plan.

The structure is visible in its intended final position.

The organization can now account for:

- construction requirements;
- materials;
- crew;
- dependencies;
- expected completion.

### Semi-transparent — Under Construction

The planned object is actively being built.

The world should show:

- construction crews;
- equipment;
- materials;
- deliveries;
- work zones;
- partial physical structure;
- interruptions or delays.

The planned structure should not simply snap from ghost to solid.

### Solid — Complete

The physical structure exists.

It is still not necessarily operational.

### Solid + activity — Operational

People, equipment, utilities, procedures, and readiness make the capability actually function.

## 4. Planning is not construction

The player can plan more than the organization can immediately build.

That distinction matters.

A player might lay out:

- five factories;
- three barracks;
- a research laboratory;
- a vehicle depot;
- a heavy VTOL pad.

The plan can exist even if:

- there are insufficient construction squads;
- materials are unavailable;
- power capacity is insufficient;
- prerequisites are missing;
- qualified staff do not exist.

The base therefore becomes a visible future state of the organization as well as its current state.

## 5. Replanning

Before construction becomes irreversible, the player can potentially:

- move;
- rotate;
- cancel;
- reorder;
- reprioritize;
- replace;
- connect/disconnect infrastructure.

Once construction has reached an appropriate commitment point, changing the plan should have actual organizational cost.

The exact cancellation/refund/commitment model remains Illumination Needed.

## 6. Construction queue as procedure

A construction queue is not merely a sorted list.

Each project represents a procedure with:

- required building definition;
- placement;
- construction crew;
- required qualifications;
- equipment;
- materials;
- power;
- site preparation;
- dependencies;
- current progress;
- interruptions;
- completion criteria.

Construction crews are real people.

The player should be able to encounter them working.

## 7. Spatial relationships

The planning system should expose physical consequences rather than hiding them in arbitrary rules.

Examples:

- runway needs clear approach/departure space;
- VTOL pads need safe operating volumes;
- vehicle depots need access;
- factories need material delivery routes;
- research facilities need administrative support;
- defensive installations need fields of observation/fire;
- barracks need access to training infrastructure;
- warehouses need loading access;
- power infrastructure needs distribution connections.

The exact engineering constraints remain to be illuminated.

## 8. Base state

A base can be understood as:

    Base
    ├── Terrain
    ├── Buildings
    ├── Infrastructure
    ├── Construction Projects
    ├── Personnel
    ├── Equipment
    ├── Vehicles
    ├── Training Capacity
    ├── Production Capacity
    ├── Research Programs
    ├── Logistics
    ├── Defensive Systems
    └── Operational Procedures

The visible arrangement and the organizational state describe the same place from different perspectives.

## 9. Future visual rule

The planning visualization should make three temporal truths obvious without conventional status panels:

**Ghost = someday.**

**Translucent = becoming.**

**Solid = here.**

The player should be able to walk through the base and physically see the organization's future taking shape.
