# raWWar — Platform and Vessel Classes

Status: Living design model.

> **The chassis pattern is not a ground-vehicle pattern. It is the common physical model for machines that carry capability through an environment.**

## 1. Scope

raWWar must not treat "vehicle" as synonymous with wheeled or tracked ground equipment.

The same underlying configuration model must support machines operating:

- on land;
- across difficult terrain;
- through atmosphere;
- above planetary surfaces;
- underwater;
- across oceans;
- in orbit;
- between orbital bodies;
- between star systems;
- as fixed or mobile space infrastructure.

The environment changes the constraints. It does not require a completely separate conceptual system.

Core pattern:

`Chassis → Sockets → Components → Configuration → Capability → Qualified Crew → Readiness`

The surrounding environment adds additional constraints.

## 2. Major platform classes

| Class | Environment | Primary constraints | Typical examples |
|---|---|---|---|
| Ground Vehicle | Terrain/surface | traction, terrain, slope, mass, clearance | scout, tank, carrier, logistics vehicle |
| Surface Craft | Planetary surface, non-road terrain | terrain interaction, flotation where applicable, power, stability | crawler, rover, industrial carrier |
| Atmospheric Aircraft | Atmosphere | lift, drag, propulsion, altitude, weather, structural loads | aircraft, VTOL, dropship |
| Ocean Vessel | Surface water | buoyancy, displacement, sea state, corrosion, propulsion | transport, warship, refinery tender |
| Submersible | Underwater | pressure, buoyancy, propulsion, atmosphere, communications | submarine, research craft |
| Orbital Vehicle | Orbit | orbital mechanics, delta-v, thermal control, docking, life support | shuttle, interceptor, orbital transport |
| Spacecraft | Space/interplanetary | propulsion, power, thermal control, life support, radiation, navigation | cargo ship, warship, exploration craft |
| Starship | Interstellar | all spacecraft constraints plus long-duration systems and warp preparation | fleet vessel, troop ship, strategic transport |
| Space Station | Fixed/orbiting infrastructure | station keeping or fixed orbital geometry, power, life support, docking, structural integrity | habitat, shipyard, command station |
| Planetary Station | Fixed surface infrastructure | terrain, foundations, utilities, environment, logistics | spaceport, sensor station, industrial station |

These are capability classes, not necessarily rigid inheritance classes.

## 3. Atmospheric aircraft vs spacecraft

An aircraft and a spacecraft should share configuration technology but not pretend to share the same operating environment.

An atmospheric aircraft is constrained by:

- atmospheric density;
- lift;
- drag;
- weather;
- altitude;
- aerodynamic/propulsive performance;
- flight envelope;
- runway/pad requirements where applicable;
- atmospheric maintenance.

A spacecraft is constrained by:

- vacuum;
- thermal rejection;
- orbital/interplanetary navigation;
- reaction control;
- delta-v;
- power;
- life support;
- radiation;
- docking;
- communications;
- space debris/environment;
- propulsion endurance.

A vehicle may be designed for both environments.

That should be represented as a deliberately engineered multi-environment capability, not by pretending the environments are interchangeable.

## 4. Ocean worlds

Ocean-based worlds require vessels to be first-class.

A world can have:

- coastlines;
- shallow-water infrastructure;
- deep-ocean infrastructure;
- floating bases;
- seabed facilities;
- underwater resource extraction;
- ocean shipping lanes;
- naval combat;
- submarine operations;
- storms and sea-state hazards.

An ocean vessel therefore needs the same depth of cut sheet as a land vehicle:

- hull/chassis;
- propulsion;
- power;
- buoyancy/displacement;
- protection;
- sensors;
- communications;
- weapons/tools;
- crew stations;
- qualifications;
- maintenance;
- logistics;
- docking/port requirements;
- environmental limits.

## 5. Space stations

A station is not merely a very large ship.

A station may be:

- fixed relative to a planetary surface;
- orbital;
- positioned at a strategic orbital location;
- industrial;
- military;
- scientific;
- residential;
- logistical;
- commercial;
- mixed-use.

Its capability comes from modules, utilities, personnel, procedures, docking interfaces, storage, production, sensors, communications, and defensive systems.

A station can therefore expose sockets for:

- docking;
- power;
- thermal systems;
- communications;
- sensors;
- weapons;
- manufacturing;
- storage;
- life support;
- habitation;
- resource processing;
- spacecraft servicing.

Station construction follows the same broader physical logic as base construction:

`Site/Orbit → Structure → Utilities → Modules → Staffing → Procedures → Capability`

## 6. Starships

A starship is a crewed organization as much as it is a machine.

A crewed combat vessel may contain positions for:

- captain/command;
- helm;
- navigation;
- propulsion;
- engineering;
- sensors;
- communications;
- weapons;
- damage control;
- flight operations;
- medical;
- logistics;
- mission-specific systems.

Each position has:

- physical location;
- controls;
- information available;
- authority;
- qualification;
- procedures;
- dependencies;
- emergency duties.

A ship battle should therefore be playable by multiple players occupying different qualified stations.

The captain does not become a magical RTS cursor.

The helm player actually operates helm procedures. Weapon crews operate weapon stations. Sensor personnel interpret sensor information. Engineering manages power/propulsion/damage procedures.

The centralized FSM model is what lets these stations cooperate without turning the ship into one monolithic scripted object.

## 7. Shipboard data flow

Example:

`Sensor → Sensor Controller → Ship Data Network → Qualified Station → Crew Member → Command Network`

A player should only see and control what the station, installed systems, communications state, environmental conditions, and qualification permit.

A damaged data network can therefore matter.

A destroyed sensor can matter.

An unqualified replacement crew member can matter.

A disconnected weapon station can matter.

A ship can remain physically present while losing portions of its capability.

## 8. Crew as the common workhorse

The soldier/workforce remains central across scales.

A person can qualify for:

- vehicle operator;
- aircraft pilot;
- aircraft systems operator;
- ship helm;
- ship weapons;
- ship engineering;
- station operations;
- refinery operation;
- resource extraction;
- construction;
- maintenance;
- command.

The qualification graph determines what a person can actually do.

The platform determines what procedures are available.

The player determines organizational intent.

## 9. Environmental capability

Every platform should expose environmental compatibility rather than a single generic "movement type."

Candidate dimensions include:

- gravity range;
- atmospheric compatibility;
- pressure range;
- temperature range;
- radiation tolerance;
- terrain/water compatibility;
- altitude;
- depth;
- weather tolerance;
- propulsion mode;
- docking/landing requirements;
- life-support requirements.

This lets the same design language express a rover on a high-gravity world, an aircraft in a thin atmosphere, a submarine in a deep ocean, and a spacecraft in vacuum.

## 10. Logistics interfaces

Every mobile platform should have explicit logistics interfaces:

- fuel/energy;
- ammunition;
- raw materials;
- refined materials;
- spare parts;
- food;
- medical supplies;
- crew replacement;
- maintenance;
- docking/loading;
- communication;
- data.

A ship does not merely "have fuel."

It has a fuel type, storage capacity, transfer interface, consumption rate, replenishment procedure, qualified personnel, and logistics source.

## 11. Vehicle cut-sheet extension

The existing vehicle cut-sheet workflow applies to every mobile platform.

For environmental classes that require additional data, add:

1. environment;
2. environmental envelope;
3. propulsion mode;
4. infrastructure interface;
5. docking/landing/launch requirements;
6. environmental maintenance;
7. emergency survival procedures.

This prevents aircraft, ships, spacecraft, and starships from becoming disconnected bespoke systems.

## 12. Strategic consequence

A player should be able to progress from:

`Scout → Aircraft → Ocean Vessel → Orbital Craft → Starship`

without learning a completely different game.

The physical constraints become deeper as capability expands.

The organizational model remains recognizable.

> **Different environments. Different machines. One underlying capability architecture.**
