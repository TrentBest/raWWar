# raWWar — Industrial Dependency Graph

Status: Living design model.

> Factories are not menus. They are the physical machinery of technological civilization.

## Industrial chain

Resource → material processing → components → specialized subsystems → assembly → quality control → storage/logistics → fielding → crew + maintenance → operation.

Research runs alongside this chain by determining what technology can be developed. Production determines what can actually be manufactured.

## Factory families

Materials → processed materials.

Components → mechanical and structural components.

Precision → high-tolerance parts and assemblies.

Electronics → computing, control, communication, and electronic subsystems.

Sensors → cameras, sensors, and observation systems.

Armor → protection/material systems.

Weapons → weapon systems.

Power Systems → generators, storage, and distribution hardware.

Propulsion → engines and thrusters.

Ground Vehicle → ground vehicle assembly.

Aerospace → aircraft and VTOL assembly.

Spacecraft → space vehicle assembly.

Robotics → robotic systems.

Medical → medical equipment.

Specialized → advanced or rare systems.

## Ground vehicle chain

Materials + armor + power + propulsion + electronics + sensors + weapons → Ground Vehicle Factory → assembly/integration → quality control → depot → qualified crew → operational vehicle.

## Atmospheric vehicle chain

Aerospace structure + armor + propulsion + power + avionics + sensors + communications + countermeasures → Aerospace Factory → integration → flight testing → quality control → hangar/pad → flight crew + ground crew → flight-ready capability.

## Space vehicle chain

Advanced materials + structure + armor + propulsion + power + avionics + sensors + communications + life support + mission systems → Spacecraft Factory → integration/test → maintenance → crew qualification → warp preparation where applicable → operational spacecraft.

The exact space-industry hierarchy remains Illumination Needed.

## Factory readiness

Factory built → equipment installed → qualified staff → maintenance available → materials available → power available → tooling available → production procedure available → production can run.

A factory with ten stations and one qualified crew is not a ten-crew production miracle.

## Research does not equal production

Research completion can unlock a design or new research branch without making the result automatically manufacturable.

Production may still require new tooling, materials, factory equipment, crew qualifications, quality procedures, energy, and logistics.

## PlasmaCrete example

PlasmaCrete research → threshold unlock → lightweight formulation research → prototype → production process development → armor factory tooling → qualified armor fabrication crew → production → fielded armor → field experience → new research.

The threshold changes what the organization can attempt. It does not teleport the result into inventory.

## Natural bottlenecks

The industrial network can produce meaningful constraints:

- insufficient trained operators;
- insufficient engineers;
- insufficient power;
- insufficient processed material;
- inadequate tooling;
- insufficient quality-control capacity;
- maintenance backlog;
- transportation delays;
- factory occupied by another run;
- lack of qualified field crews;
- research complete but production unprepared.

These are consequences of the organization rather than arbitrary resource gates.

## Production quality

Production quality can depend on technology generation, tooling, operator qualification, materials, maintenance, quality control, process maturity, and production experience.

This supports the existing principle that nominally similar equipment can behave differently because it was produced under different conditions.

## Open illumination

Exact raw resources, extraction, throughput, shifts, inventories, power model, tooling model, quality formula, production costs, and faction-specific industrial doctrine remain unresolved until the economic and production models are designed together.


## Production acceptance, equipment twins, and maintenance

A completed manufacturing step does not automatically create a field-ready part. Each product follows an explicit, inspectable state transition:

`in_process → produced → awaiting_acceptance → under_test → accepted | rejected → outgoing_inventory → dispatched`

Accepted items retain their acceptance evidence and provenance. Rejected items are quarantined for rework, retest, salvage, or scrap; they never silently enter outgoing inventory. A player can personally operate supplied test tooling, inspect measurements, make the acceptance decision where authorized, and use the outgoing tooling to release accepted work. Routine production and checks may be automated, but the work and evidence still exist.

### Production machinery is also a digital twin

A production line is composed of identifiable assets, not an undifferentiated factory statistic. The model can descend through machine assemblies to replaceable components and individual devices: motors, bearings, sensors, limit switches, relays, contactors, interlocks, wiring, power supplies, actuators, fixtures, calibration references, and control logic. Each asset can have connections, expected states, operating limits, qualification requirements, inspection procedures, condition, service history, and relationships to upstream and downstream equipment.

The simulation should be able to answer questions such as: Which switch proves the guard is closed? Which relay enables the next operation? What happens if a contact sticks, a wire opens, a sensor drifts, or an interlock is bypassed? Which downstream station is consequently inhibited? These are modeled relationships and state transitions—not flavor text attached to a pretty machine.

Digital-twin fidelity is hierarchical. A user can manage the factory as a production capability, enter a machine to diagnose a stoppage, or inspect a specific device when they want the deeper experience. Detail can be expanded where it matters without forcing every player to manually operate every device.

### Player-owned maintenance policy

Production equipment accumulates cycle counts, load exposure, wear, thermal or environmental exposure, calibration drift, alarms, failed tests, and repair history. The player chooses a preventive-maintenance interval such as every **N production cycles**, rather than being handed a supposedly universal best number. The right interval is a trade-off the player learns from the equipment and its consequences.

- **Too-frequent planned maintenance:** safer margins may be purchased at the cost of downtime, labor, spares, and reduced throughput.
- **Too-infrequent planned maintenance:** production continues longer, but wear, drift, defects, and the chance of an unplanned stoppage can grow.
- **Condition-based work:** observed measurements and warning thresholds can trigger service before a fixed interval.
- **Corrective or emergency work:** a fault stops or degrades the affected capability until the equipment is made safe and restored.

The underlying model should derive risk from equipment design, operating conditions, observed state, and maintenance quality. It should not invent a universal magic value for N. When a station is shut down, it is unavailable; queued work waits, is rerouted if a suitable alternative exists, or creates a downstream logistics delay.

### Hands-on repair is a playable job

Players can work inside a repair facility instead of issuing a generic `Repair` command. A repair task may require authorization, isolation, diagnostics, correct tooling, spare parts, disassembly, replacement or adjustment, calibration, functional test, and commissioning. The player can follow a procedure or explore the equipment to diagnose the fault. Qualifications, experience, tool condition, available technical data, and execution against the Ideal Gesture influence the result.

The repaired asset returns to service only after required verification. A failed test can expose a second fault, consume a part, require rework, or leave the asset restricted. Work produces a maintenance record and updates the asset's actual state. Equipment histories can inform future maintenance policy, training, design changes, and research.

The same principle applies aboard ships and throughout their industrial support chain. Every asset can have a lifecycle, configuration, connections, operating envelope, inspections, defects, temporary fixes, permanent repairs, and provenance. A ship's twin is not just its hull and compartments; it includes the systems and devices that make the vessel function.

See [Production Acceptance and Maintenance](PRODUCTION_ACCEPTANCE_AND_MAINTENANCE.md) for the deeper player and system loop.
