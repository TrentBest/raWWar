# raWWar — Building Catalogue

Status: Living design catalogue.

> **A building is a physical capability in the world, not a button that grants a bonus.**

This catalogue identifies the first building families raWWar should support. It intentionally describes capability families before freezing thousands of named structures.

## 1. Building lifecycle

Every placed building has a physical lifecycle:

    Palette
      ↓
    Ghost / Future
      ↓
    Planned
      ↓
    Construction Authorized
      ↓
    Under Construction
      ↓
    Complete
      ↓
    Operational
      ↓
    Maintained / Modified / Expanded

The visual state is a presentation of authoritative construction state:

| World state | Presentation |
|---|---|
| Future | Ghosted / translucent |
| Planned | Semi-transparent planned structure |
| Construction | Semi-transparent structure plus visible construction activity |
| Complete | Solid physical building |
| Operational | Solid + active people/equipment/procedures |
| Disabled | Solid physical structure, visibly inactive or degraded |

A building does not become real merely because it appears in a placement palette.

## 2. 3D base planning

The player lays out the base directly in three-dimensional space.

The placement interaction supports:

- drag from a building palette;
- move;
- rotate;
- reposition;
- non-orthogonal orientation;
- terrain-aware placement;
- physical access;
- connection to infrastructure;
- construction sequencing;
- cancellation/replanning before construction begins.

There is no requirement that buildings align to a rectangular grid.

> **The base is a place, not a spreadsheet.**

The player can create a base whose buildings are intentionally angled to terrain, roads, defensive arcs, flight paths, logistics routes, or one another.

### Placement transform

A building instance needs, at minimum:

- building definition;
- world position;
- world rotation;
- footprint/volume;
- terrain relationship;
- planned state;
- construction state;
- assigned construction procedure;
- dependencies;
- infrastructure connections;
- current capability state.

The exact transform/storage representation remains an implementation concern; the Experience owns the meaning.

## 3. Headquarters

### Surface Headquarters
Provides the visible command presence of the base.

Possible spaces:

- command reception;
- command offices;
- operations;
- security;
- briefing;
- ceremonial areas;
- communications.

### Underground Headquarters
The administrative and protected organizational core.

Initial confirmed requirement:

**Research facilities require an available administrative office in the underground HQ.**

Possible spaces:

- administrator offices;
- research administration;
- personnel administration;
- logistics administration;
- secure communications;
- records;
- planning;
- intelligence;
- command staff.

## 4. Personnel and living

- Standard barracks
- Specialized barracks
- Officer quarters
- Senior officer quarters
- Medical quarters
- Temporary accommodation
- Dining facility
- Recreation facility
- Welfare/morale facility
- Medical facility

### Standard barracks

A standard barracks contains **four VR training modules**.

That gives one squad-sized training session at a time.

The barracks therefore represents both:

1. personnel accommodation; and
2. training throughput.

This makes barracks a fundamental bottleneck in military expansion.

## 5. Training

- VR training barracks
- Weapons range
- Heavy weapons range
- Vehicle training area
- Flight training facility
- Pilot simulator
- Engineering training facility
- Construction training facility
- Research training laboratory
- Command simulation facility
- Intelligence training facility
- Medical training facility

Training buildings should visibly contain drills, instructors, trainees, equipment, queues, failures, recovery, and completed qualifications.

## 6. Logistics

- Warehouse
- Armory
- Ammunition storage
- Energy storage
- Fuel/energy servicing
- Spare-parts storage
- Vehicle depot
- Aircraft hangar
- Maintenance bay
- Repair depot
- Cargo terminal
- Loading yard
- Transportation hub

These buildings make movement and maintenance of a military force physically observable.

## 7. Industrial

- Materials processing plant
- Metal/plasma-material plant
- Chemical processing plant
- Component factory
- Precision factory
- Electronics factory
- Sensor factory
- Armor factory
- Weapons factory
- Power systems factory
- Propulsion factory
- Ground vehicle factory
- Aerospace factory
- Spacecraft factory
- Robotics factory
- Medical factory
- Specialized factory
- Quality-control facility
- Heavy fabrication facility

A factory can exist while remaining incapable of production because its equipment, materials, power, tooling, procedures, or qualified crew are unavailable.

## 8. Research

- Research laboratory
- Materials laboratory
- Weapons laboratory
- Propulsion laboratory
- Sensor laboratory
- Medical laboratory
- Biological laboratory
- Environmental laboratory
- Robotics laboratory
- Computing facility
- Prototype workshop
- Test chamber
- Demonstration facility

Research is not an abstract progress bar hidden in a menu.

The player can:

1. discuss research with the administrator;
2. review available deterministic research programs;
3. choose how much research capacity to allocate;
4. schedule a demonstration;
5. visit the facility;
6. observe the experiment;
7. authorize/reject the resulting opportunity.

## 9. Aerospace and deployment

- Runway
- VTOL pad
- Heavy VTOL pad
- Vehicle loading pad
- Aircraft hangar
- Air traffic control
- Aerospace maintenance
- Fuel/energy servicing
- Launch control
- Vehicle staging area

These structures support:

    stage → load → launch → transit → survive AA → arrive → deploy → return/recover

## 10. Defensive

- Wall
- Gate
- Watchtower
- Guard post
- Bunker
- Hardened shelter
- Defensive turret
- Anti-air installation
- Sensor tower
- Radar/sensor array
- Missile battery
- Energy-defense installation
- Command bunker

Defenses are physical systems with crews, power, maintenance, ammunition/energy, observation, procedures, and readiness.

## 11. Utilities and civil support

- Power generation
- Power distribution
- Water processing
- Waste processing
- Food production/storage
- Environmental control
- Communications
- Medical support
- Repair utilities
- Transportation infrastructure

These are not background decorations. Failure or insufficient capacity can constrain the rest of the base.

## 12. Construction infrastructure

Construction itself requires capability.

Possible construction-support buildings:

- Construction equipment depot
- Materials staging yard
- Heavy equipment yard
- Temporary construction office
- Site logistics area
- Fabrication/staging facility

Advanced buildings require advanced construction crews.

Therefore:

    New building
    → construction qualification
    → trained construction squad
    → barracks capacity
    → construction equipment
    → materials
    → site preparation
    → construction procedure
    → completed building

## 13. Building state is capability state

A building can be:

- physically present but incomplete;
- complete but unstaffed;
- staffed but underqualified;
- qualified but missing resources;
- resourced but awaiting maintenance;
- ready but not assigned a task;
- fully operational.

The building renderer should never be the authority for whether the building works.

> **The building is geometry. The capability is the truth.**

## 14. Catalogue rule

New buildings should be added by answering:

- What capability does it provide?
- Who operates it?
- What qualifications are required?
- What does it consume?
- What does it produce?
- What infrastructure must connect to it?
- What procedures occur inside it?
- What can prevent it from operating?
- What research can improve it?
- What factories can build its required equipment?
- What other capabilities depend on it?

This prevents the catalogue from becoming a list of decorative assets.
