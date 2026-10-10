# raWWar — Construction and Configuration

Status: Living design model.

> **Construction is the physical act of turning a configuration into a capability.**

raWWar should treat construction with the same provenance and configuration logic used for vehicles, exoskeletons, factories, soldiers, and other capabilities.

## 1. Construction is a capability system

A project is not simply “building X.”

    Desired Capability
      ↓
    Structure Definition
      ↓
    Site + Terrain + Access
      ↓
    Design / Configuration
      ↓
    Materials + Components
      ↓
    Construction Equipment
      ↓
    Qualified Construction Personnel
      ↓
    Logistics + Staging
      ↓
    Utilities / Temporary Services
      ↓
    Construction Procedure
      ↓
    Inspection / Commissioning
      ↓
    Complete Structure
      ↓
    Staffing + Qualification
      ↓
    Operational Capability

Every step can carry authoritative state.

## 2. Construction has a metric shit ton of useful data

A construction project can include:

- structure definition;
- configuration;
- dimensions;
- orientation;
- terrain relationship;
- foundation requirements;
- site preparation;
- access requirements;
- utility connections;
- material requirements;
- component requirements;
- construction equipment;
- crew requirements;
- crew qualifications;
- construction sequence;
- dependencies;
- staging locations;
- transport requirements;
- weather exposure;
- environmental constraints;
- inspection requirements;
- commissioning requirements;
- expected capacity;
- maintenance requirements;
- future expansion interfaces;
- owner;
- project history;
- modifications;
- current condition.

This data is not bureaucracy.

It is the explanation of **why the thing exists, what it can do, and what it takes to keep it doing it.**

> **The world should know why something works. Then the player can decide how to make it work better.**

## 3. 3D terrain and elevation are construction data

A construction project records not only where a structure is placed, but how that structure relates to the terrain beneath and around it.

Relevant planning data includes:

- selected elevation;
- local terrain elevation;
- excavation volume;
- support depth;
- support material;
- maximum permitted ordinary support;
- cantilever/span requirement;
- structural load;
- terrain stability;
- access elevation;
- infrastructure elevation;
- resulting construction cost.

The placement preview is therefore a projected construction state.

If terrain must be removed, the excavation is shown transparently and its cost is included before commitment.

If terrain is below the structure, up to two layers of PlasCrete may provide ordinary support. Beyond that limit, the design must become an engineered spanning/cantilevered solution rather than silently accumulating more fill.

Elevation also has operational meaning. Raised walls, platforms, command positions, and terraces can deliberately create defensive height advantages. The construction system therefore feeds tactical geometry rather than treating terrain as scenery.

## 4. Infrastructure corridors carry utilities

Roads and other planned corridors should be able to carry subsurface infrastructure such as:

- electrical distribution;
- communications;
- control/data networks;
- other buried utilities.

A road is consequently both movement infrastructure and a utility corridor.

Rerouting a road can require rerouting those services. Damaging a road can affect traffic independently from buried services, while utility damage can occur without completely destroying the surface route.

Planning data should preserve these relationships so that a player can understand the consequences of changing a corridor before construction or demolition is committed.


## 5. Configuration comes before commitment

Players should be able to try configurations and arrangements before committing physical resources.

Examples:

- two small factories versus one large factory;
- centralized storage versus distributed storage;
- short logistics routes versus hardened longer routes;
- centralized power versus redundant local generation;
- training near barracks versus a dedicated training district;
- additional maintenance versus additional production;
- different runway or VTOL-pad orientations;
- defensive layouts with different fields of fire;
- duplicated critical utilities versus a single efficient installation.

The game should expose the consequences rather than hiding them behind a single “optimal” build.

## 6. Planning is an engineering workspace

A planned configuration should be inspectable before construction.

For a proposed factory, the player might discover:

    Factory
      → power requirement
      → material inputs
      → qualified crew
      → logistics access
      → production output
      → storage requirement
      → maintenance requirement

For a proposed barracks:

    Barracks
      → personnel capacity
      → training modules
      → utility demand
      → qualification throughput
      → future staffing capacity

The player is designing a system, not selecting a bonus.

## 7. Bottlenecks emerge from real dependencies

There should be no arbitrary rule such as “you may build three factories.”

Expansion can instead be constrained by:

- construction crews;
- qualified personnel;
- materials;
- transport;
- equipment;
- power;
- site access;
- weather;
- competing projects;
- production capacity;
- maintenance;
- administrative capacity.

That permits different strategies.

One player may invest heavily in construction capacity.

Another may build logistics first.

Another may create compact centralized bases.

Another may distribute facilities for resilience.

Another may accept inefficient layouts because they are easier to defend.

The simulation should make these choices meaningfully different.

## 8. Construction itself has configuration

Construction equipment, crews, staging areas, temporary utilities, material handling, and site access are capabilities.

A major project can therefore become:

    Construction Project
      → required equipment
      → equipment availability
      → qualified crew
      → material supply
      → staging
      → transport
      → site preparation
      → construction sequence
      → inspection
      → commissioning

A player who improves construction infrastructure should actually be able to construct more effectively.

This is the same philosophy as the rest of raWWar.

## 9. Structures can be designed for future change

A structure can expose physical interfaces for later expansion:

- additional power;
- additional water;
- additional communications;
- additional production lines;
- additional storage;
- additional training modules;
- additional docking/launch capacity;
- additional environmental control;
- additional defensive systems.

Future capacity is therefore something the player can deliberately design into the physical installation.

> **A base should be able to grow because the player built it to grow.**

## 10. Construction history matters

A structure should remember:

- who designed it;
- who authorized it;
- who built it;
- where materials came from;
- construction duration;
- delays;
- accidents;
- modifications;
- expansions;
- repairs;
- damage;
- environmental exposure;
- captured/reassigned ownership;
- notable events.

The base therefore accumulates institutional memory.

A facility that has survived decades of storms, attacks, expansions, repairs, and changing commanders should not be indistinguishable from a freshly completed facility.

## 11. Experimentation is a first-class player activity

The player should be encouraged to ask:

> “What happens if I arrange this differently?”

That means the system should support:

- planning;
- comparison;
- temporary layouts;
- phased construction;
- expansion plans;
- retrofit;
- duplication;
- specialization;
- redundancy;
- relocation where physically possible;
- demolition where permitted.

Planning is where experimentation is cheap.

Once construction is authorized, consequences become real.

> **Experimentation belongs in planning. Commitment belongs in the world.**

## 12. Data enables discovery rather than forcing optimization

The purpose of exposing all this information is not to turn raWWar into a spreadsheet.

The purpose is to let the player discover relationships.

A player might realize:

    Factory placement
      → shorter transport
      → lower logistics burden
      → faster production
      → fewer exposed convoys
      → greater readiness

Or:

    Distributed storage
      → more facilities
      → more staff
      → more maintenance
      → greater resilience
      → less catastrophic single-point failure

Neither should simply be labeled “better.”

The player discovers the trade.

## 13. The API is supposed to handle this

This is exactly the kind of domain where FSM_API's purpose becomes visible.

The game does not need one enormous state machine called Construction.

It can have many related procedures and state machines:

    Site Preparation FSM
    Material Delivery FSM
    Crew Assignment FSM
    Equipment Allocation FSM
    Foundation FSM
    Structural Assembly FSM
    Utility Connection FSM
    Inspection FSM
    Commissioning FSM
    Maintenance FSM
    Expansion FSM

Those procedures consume and produce data.

The result is a construction system that is complicated because the **world is complicated**, not because the implementation is artificially monolithic.

> **Tons of interrelated FSMs empower the capability.**

## 14. Same abstraction across the game

Construction follows the same pattern already established for vehicles and exoskeletons.

Vehicle:

    Chassis → Sockets → Components → Configuration → Capability

Exoskeleton:

    Chassis → Sockets → Components → Configuration → Capability

Base:

    Site → Structures → Infrastructure → Staffing → Procedures → Configuration → Capability

The shared principle is:

> **The player is configuring a system, not selecting a bonus.**

## 15. The player should be able to become dangerous through understanding

A player who studies the data should eventually discover configurations that produce outcomes other players did not anticipate.

That can mean:

- better logistics;
- stronger defensive geometry;
- faster training throughput;
- more resilient infrastructure;
- unusual vehicle configurations;
- specialized soldier loadouts;
- asymmetric faction strategies;
- unexpected combinations of capabilities.

The game should reward understanding, not merely reaction speed or memorized build orders.

This is where the historical lesson from classic factional strategy games becomes useful: asymmetric capabilities are most interesting when the player learns how to arrange them, not when the game hands them a “correct strategy.”

## 16. No universal optimal configuration

There should not be one arrangement that dominates every situation.

Configuration quality depends on:

    Mission
    + Environment
    + Faction
    + Resources
    + Personnel
    + Technology
    + Logistics
    + Threat
    + Player Intent
    + History

A configuration that is brilliant on one world may be terrible on another.

A configuration that is ideal for one faction may violate another faction's doctrine or industrial reality.

A configuration that works during peace may collapse during sustained war.

## 17. Construction is another way the galaxy remembers

When a player changes a base, the change becomes part of the world.

The galaxy can remember:

- what was built;
- where;
- why;
- by whom;
- with which technology;
- with which resources;
- how it was configured;
- what happened there;
- how it was modified later.

The player's base is therefore not a menu state.

It is part of history.

> **The galaxy remembers what the player built just as it remembers what the player destroyed.**
