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


## 15. Non-orthogonal base planning

Base geometry is not constrained to a grid.

A placed structure has a three-dimensional transform:

    Position = X, Y, Z
    Rotation = orientation

The footprint remains a physical shape in world space.

For fast broad-phase checks, the rotated footprint can be enclosed by a **maximum bounding box**: the box that inscribes/encloses the rotated footprint.

The maximum bounding box is the first inexpensive test for:

- collision;
- placement overlap;
- access;
- navigation;
- infrastructure clearance.

A precise rotated-footprint test can then be performed only when the broad-phase test indicates a possible conflict.

This keeps non-orthogonal construction practical without pretending that the base is orthogonal.

> **The box is a first check, not the truth.**

The actual building remains oriented geometry in a three-dimensional world.

## 16. Buildings project intent infrastructure

Placing a building should reveal the infrastructure its capability requires.

These are not merely decorative guides. They can be **intent GUI**: projected physical requirements that the player can inspect and commit.

Examples:

- headquarters → review/parade yard;
- headquarters → approach and ceremonial lanes;
- factory → loading/staging areas;
- warehouse → loading lanes;
- barracks → personnel movement routes;
- starport → approach/departure areas;
- vehicle factory → vehicle staging and departure lanes;
- research facility → service/access zones;
- processing plant → material delivery and output routes.

The player should see these requirements before construction.

A highlighted area can answer:

    What does this building need around itself?
    How much space does it need?
    What traffic can it handle?
    What infrastructure can connect to it?
    What future capability can this space support?

## 17. Infrastructure guides are clickable

Projected infrastructure can become a construction intent.

For example:

    HQ placed
      ↓
    Review yard projected
      ↓
    Player clicks the yard
      ↓
    Review-yard construction added to project
      ↓
    Construction proceeds through normal capability rules

Likewise, a player can click highlighted road/traffic lanes beside a structure.

The selected lane count becomes an infrastructure commitment.

The system should then preserve that planned corridor when additional buildings are placed nearby.

The player can deliberately:

- remove lanes;
- add lanes;
- widen corridors;
- reroute infrastructure;
- change priorities;
- accept a constrained layout.

The game should explain the resulting consequences rather than silently optimizing the base.

> **The guide shows what the building wants. The player decides what the base actually provides.**

## 18. Lanes are physical capacity

A lane is a usable movement band with physical spacing appropriate to the traffic using it.

A road's lane count therefore limits how much traffic can pass simultaneously.

A lane can represent:

- marching formation space;
- vehicle movement;
- cargo movement;
- emergency access;
- ceremonial movement;
- service traffic.

Different traffic classes can require different numbers of lanes.

A vehicle's footprint should therefore include a **lane requirement**.

For example, a three-lane vehicle requires three formation rows of usable passage.

If it drives through a marching formation occupying those lanes, the physical consequences are real.

That means the amusing version of the problem is also the simulation problem:

> **If the tank needs three lanes and the commander sends it through a four-lane formation, the game should not magically move the soldiers aside.**

The resulting collision, casualties, interruption, traffic blockage, disciplinary consequences, and political consequences should follow the same physical/procedural systems as everything else.

This creates a useful planning puzzle:

> **How do I arrange the base so that the things I want to happen can actually happen without the organization fucking itself up?**

## 19. Infrastructure preserves spatial intent

When a player places a new structure beside an existing planned road, lane system, yard, or access corridor, the placement system should detect the existing intent.

The player should receive a visible indication when a proposed structure:

- preserves the corridor;
- narrows it;
- blocks it;
- consumes planned capacity;
- requires rerouting;
- creates a new intersection;
- creates a new bottleneck.

The system should not silently delete previously planned infrastructure.

The player explicitly chooses whether to:

- preserve;
- modify;
- reroute;
- remove.

This makes the base layout persistent design data rather than transient placement UI.

## 20. Facilities have fixed and configurable dimensions

Not every building should be freely stretchable.

### Fixed or constrained-size structures

Some structures have dimensions dictated by their physical function.

Examples:

- barracks;
- refineries;
- processing plants;
- defensive structures;
- research facilities where laboratory geometry is capability-specific;
- specialized launch/maintenance structures.

Their footprint is therefore part of the capability definition.

### Dynamically sizeable structures

Other structures scale naturally with the capacity the player wants to provide.

Examples:

- factories;
- storage facilities;
- starports.

For these, size becomes a configuration variable.

    Structure
      → dimensions
      → capacity
      → equipment / work cells / storage positions
      → crew requirement
      → utility requirement
      → logistics requirement
      → throughput

A larger factory should therefore not merely look larger.

It should provide additional physical capacity while creating additional requirements.

## 21. Starport scale

A starport is a particularly clear dynamic structure.

Its size can determine:

- number of simultaneous approaches;
- landing/takeoff capacity;
- staging capacity;
- servicing capacity;
- maintenance positions;
- cargo throughput;
- passenger/personnel throughput;
- communications demand;
- control staff;
- emergency response capacity.

A small starport may handle a modest flow of craft.

A massive starport can handle much more traffic, but it also requires:

- more physical area;
- more infrastructure;
- more qualified personnel;
- more power;
- more maintenance;
- more logistics;
- more traffic management.

> **Capacity is physical.**

## 22. Starting bases are intentionally incomplete

The opening military base should already be functioning, but it should not be finished.

The player arrives at a base with:

- an existing command capability;
- enough infrastructure to operate;
- visible personnel and activity;
- incomplete expansion;
- obvious inefficiencies;
- available future building footprints;
- opportunities for configuration improvement.

This gives the player a working reference system before asking them to build a perfect base.

The player can immediately observe:

- how traffic moves;
- how soldiers form up;
- how vehicles are staged;
- how factories operate;
- how logistics arrives;
- where bottlenecks occur;
- what infrastructure is missing.

A skilled player should begin thinking:

> **I already see how I would improve this.**

The base therefore becomes the first construction tutorial without being a tutorial screen.

## 23. The first conquered world is deliberately worse

After the opening base, the player is sent to the poorly defended, resource-rich enemy world established by the campaign premise.

The new headquarters should be substantially less capable.

Its construction technology can initially provide only the weakest available **PlasCrete** tier.

The player must operate with that infrastructure while researching or acquiring better construction technology.

This creates a direct connection between:

    Technology
      ↓
    Construction capability
      ↓
    Material quality
      ↓
    Structure durability
      ↓
    Vehicle / equipment support
      ↓
    Base capability

## 24. Three initial PlasCrete tiers

The initial construction model should expose three meaningful quality tiers.

| Tier | Character | Consequence |
|---|---|---|
| PlasCrete I | basic / weakest | low durability, lower supported loads, greater maintenance exposure |
| PlasCrete II | improved | stronger structures and improved operational margin |
| PlasCrete III | advanced | high durability and greater supported loads/capacity |

Exact material values, strength limits, environmental resistance, cost, production requirements, and research thresholds remain **Illumination Needed**.

The tiers should not be simple cosmetic levels.

They should affect what the physical installation can safely support.

## 25. Technology can invalidate existing capability

A structure can become unsuitable when technology or requirements advance.

For example, a vehicle factory may eventually need to support a vehicle whose mass or operational load exceeds the installed floor, pad, route, or structural rating.

If the existing PlasCrete cannot support that vehicle:

    Vehicle requirement
      ↓
    Base infrastructure capacity
      ↓
    Insufficient structural rating
      ↓
    Vehicle cannot safely operate there
      ↓
    Vehicle remains in factory bay
      ↓
    Bay cannot release the vehicle
      ↓
    Production bay becomes occupied
      ↓
    Production pauses or changes

The game should not teleport the vehicle outside.

The player must upgrade, retrofit, relocate, or otherwise provide an appropriate physical capability.

This is the intended consequence of technology progression.

> **Technology does not merely unlock the vehicle. It can reveal that the world around the vehicle is no longer good enough.**

## 26. Base footprints are known before commitment

The player should be able to inspect the complete set of structures currently available to them.

Each available building can expose:

- footprint;
- height/volume;
- orientation;
- required clearances;
- access requirements;
- infrastructure guides;
- lane requirements;
- service zones;
- environmental requirements;
- utility interfaces;
- expansion interfaces;
- dynamic sizing rules;
- structural requirements;
- construction requirements.

This lets the player plan ahead without constructing everything.

A player can therefore reserve physical space for a future capability even before they can afford to build it.

> **Planning for something is not the same as having it.**

## 27. Base planning is a spatial dependency graph

The base can be understood as:

    Buildings
      ↕
    Clearances
      ↕
    Infrastructure
      ↕
    Traffic capacity
      ↕
    Logistics
      ↕
    Personnel movement
      ↕
    Production / training / command
      ↕
    Capability

A good layout reduces unnecessary conflicts.

A bad layout can remain physically valid while being operationally terrible.

That is intentional.

The player should be able to create a base that technically works but produces constant friction.

> **The puzzle is not making buildings fit. The puzzle is making the organization fit inside the buildings.**
