# raWWar — Vehicle Cut Sheets and Qualification Model

Status: Living design model.

## Purpose

Every vehicle in raWWar is both:

1. a physical machine built from a chassis and configuration; and
2. an organizational capability that consumes people, training, production, logistics, maintenance, fuel/energy, and time.

The cut sheet is the authoritative design surface for that vehicle.

It should answer two very different questions:

**Soldier:** What do I have to know and do to operate this position?

**Commander:** What does this vehicle cost, require, enable, and improve?

The commander-facing model intentionally reports consequences rather than forcing the player to understand every engineering reason behind them.

> **The soldier operates the machine. The commander allocates the capability.**


## Scope across environments

"Vehicle" is the cut-sheet family name, not a limitation to ground vehicles. The same model extends to atmospheric aircraft, ocean vessels, submersibles, orbital craft, spacecraft, starships, and related mobile platforms. Fixed planetary and orbital stations use the same chassis/socket/configuration philosophy at infrastructure scale.

See **[Platform and Vessel Classes](PLATFORM_AND_VESSEL_CLASSES.md)** for the environmental and crewed-platform model, and **[Resource Extraction and Keyed Logistics](RESOURCE_EXTRACTION_AND_KEYED_LOGISTICS.md)** for resource-specific industrial logistics.

A platform's environment adds constraints; it does not create a disconnected technology stack.

## 1. Vehicle identity

Each vehicle definition should contain:

- vehicle family;
- faction default classification;
- chassis;
- configuration/class;
- intended role;
- locomotion type;
- power-source compatibility;
- fuel/energy requirements;
- crew complement;
- passenger/cargo capacity;
- protection envelope;
- sensors;
- communications;
- weapons;
- mission modules;
- maintenance requirements;
- qualification requirements;
- production requirements;
- research dependencies;
- logistics dependencies;
- deployment state.

A faction may define a default configuration, but the underlying vehicle remains configurable.

## 2. Chassis specification

The chassis cut sheet records:

| Field | Meaning |
|---|---|
| Chassis family | Physical platform identity |
| Generation | Research/production generation |
| Mass envelope | Structural mass capacity |
| Volume envelope | Available internal/external volume |
| Power envelope | Supported power generation/distribution |
| Cooling envelope | Heat rejection capability |
| Structural envelope | Loads and attachment limits |
| Socket map | Physical interfaces available |
| Mobility interface | Supported locomotion systems |
| Protection interface | Armor/protection attachment capability |
| Sensor interface | Sensor/data attachment capability |
| Weapon interface | Weapon/feed/energy interfaces |
| Utility interface | Communications, power, tools, mission systems |

A research breakthrough may change the envelope itself. That is not a hidden bonus; it changes what configurations are physically legal.

## 3. Configuration cut sheet

Every operational configuration records:

- configuration identifier;
- parent chassis;
- installed components;
- occupied sockets;
- mass;
- power demand;
- cooling demand;
- ammunition/energy demand;
- fuel demand;
- crew positions;
- passenger/cargo capacity;
- protection;
- mobility;
- sensor capability;
- communication capability;
- weapon capability;
- maintenance burden;
- qualification burden;
- production cost;
- operating cost;
- expected survivability;
- expected range/endurance;
- readiness requirements.

The same chassis can therefore produce multiple vehicles.

## 4. Crew and qualification

Every crew position is a qualification.

A position is not simply "crew count = 4". It is a semantic role attached to a physical and procedural location.

Each position should define:

- position identifier;
- physical location in the vehicle;
- entry/exit interaction;
- required equipment;
- controls/interfaces available at that position;
- data connections;
- communication connections;
- sensors available;
- procedures owned by the position;
- qualification level;
- training curriculum;
- minimum qualification;
- advanced qualification;
- emergency procedures;
- replacement/backup rules;
- command authority;
- dependencies on other positions.

The soldier remains the workhorse.

A vehicle does not magically "have scouting capability." A qualified soldier occupies a position, interacts with the vehicle's systems, interprets information, and performs procedures.

## 5. Vehicle data connections

A vehicle should expose a physical/semantic connection graph.

Example:

```
Sensor
  ↓
Sensor Controller
  ↓
Vehicle Data Bus
  ↓
Crew Station
  ↓
Qualified Soldier
  ↓
Command / Communications Network
```

Each connection has:

- source;
- destination;
- interface type;
- bandwidth/capacity;
- power dependency;
- control authority;
- qualification requirement;
- failure modes;
- redundancy;
- physical location.

The same model applies to:

- radar;
- cameras;
- targeting systems;
- navigation;
- engine telemetry;
- fuel systems;
- weapon controls;
- communications;
- electronic warfare;
- medical systems;
- maintenance diagnostics.

## 6. Commander-facing economics

The commander should not need to understand every engineering dependency.

A configuration can expose consequence-oriented statistics such as:

- vehicle cost;
- operating cost;
- fuel cost;
- range;
- endurance;
- speed;
- survivability;
- detection range;
- sensor coverage;
- firepower;
- transport capacity;
- production time;
- maintenance burden;
- crew burden;
- training burden;
- readiness requirement.

For example:

> Extended fuel system: +$2,000 vehicle cost, +10% operating range.

The interface does not need to say:

> This requires additional tankage, plumbing, structural reinforcement, pump capacity, mass allowance, inspection, and maintenance.

Those details exist in the simulation and can be exposed through a deep dive when desired.

The commander sees the consequence first.

## 7. Research → adoption → deployment

Research unlocks capability. It does not instantly equip the force.

The organizational chain is:

```
Research threshold reached
        ↓
Capability becomes available
        ↓
Player approves adoption
        ↓
Military advisor creates adoption/deployment plan
        ↓
Factories/tooling/materials are prepared
        ↓
Production begins
        ↓
Units receive the new equipment
        ↓
Personnel train/qualify
        ↓
Readiness checks
        ↓
Capability enters service
        ↓
Deployment continues until planned saturation
```

A deployment plan should record:

- technology;
- intended units;
- priority;
- factory sources;
- production rate;
- resource requirements;
- training requirements;
- replacement policy;
- fielding order;
- target saturation;
- estimated completion;
- actual completion;
- exceptions and delays.

The player says **make it happen**.

The organization determines how.

## 8. Throughput and acceleration

A deployment is constrained by physical throughput.

Higher factory throughput can shorten deployment time when:

- resources are available;
- tooling exists;
- qualified workers exist;
- transport capacity exists;
- receiving units can train;
- maintenance support exists.

The player may spend more resources to accelerate production.

Acceleration therefore consumes real capacity rather than creating a magical instant deployment.

## 9. Vehicle capability progression

Vehicle roles should be allowed to become obsolete or transform.

A vehicle can move through capability generations:

```
Scout
  ↓
Fast reconnaissance
  ↓
Advanced reconnaissance
  ↓
Sensor carrier
  ↓
Networked detection node
```

At some point, sending a fast vehicle to physically scout becomes inferior to deploying a persistent sensor network.

That is not a balance rule. It is a technological and organizational consequence.

## 10. FSM implementation

The centralized FSM architecture is what makes this practical.

The vehicle is not one giant FSM.

It is a composition of interacting FSMs representing:

- crew positions;
- engine/power;
- mobility;
- fuel;
- sensors;
- communications;
- weapons;
- ammunition/energy;
- maintenance;
- damage;
- environmental response;
- navigation;
- mission procedures;
- qualification;
- deployment;
- logistics;
- command.

The vehicle's observable capability emerges from these cooperating processes.

> **The vehicle is a machine made of systems. The crew is a machine made of procedures. FSMs connect the two.**

## 11. Cut-sheet workflow

For every vehicle, produce the following in order:

1. Identity and role.
2. Chassis.
3. Default faction configuration.
4. Physical interfaces/sockets.
5. Power and locomotion.
6. Protection.
7. Sensors.
8. Communications.
9. Weapons/tools.
10. Crew positions.
11. Position data connections.
12. Qualification requirements.
13. Training pipeline.
14. Maintenance pipeline.
15. Logistics/fuel/energy.
16. Commander-facing statistics.
17. Upgrade/configuration options.
18. Research dependencies.
19. Production/deployment path.
20. Obsolescence or successor capabilities.

This cut sheet is the bridge between content authoring, FSM composition, soldier qualification, manufacturing, logistics, and the commander's economic interface.
