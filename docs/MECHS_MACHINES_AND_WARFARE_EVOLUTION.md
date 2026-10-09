# raWWar — Mechs, Machines, and the Evolution of Warfare

> **The mech fantasy is in scope. The engineering makes it worth piloting.**

Status: Candidate capability design. Walking combat mechs are an explicit desired player capability. Specific chassis, faction assignments, performance, and chronology remain open until authored.

## The pilotable battle mech

The player should be able to climb into a machine, close the hatch, power it up, check actuators and sensors, release safeties, move into the world, fight or work, and deal with the consequences. The cockpit is a physical place with buttons, levers, pedals, grips, mounted displays, warning lamps, and emergency controls. Those controls are subject to the same damage and repair rules as other world objects.

This is not a promise that humanoid machines are best everywhere. It is a commitment to make walking combat mechs a real pilotable capability, not merely an enemy silhouette or menu icon.

## Warfare branches instead of climbing one ladder

| Environment | Machine families | Interesting constraints |
|---|---|---|
| Terrestrial warfare | Wheeled, tracked, walking, hybrid-legged, crawling | Mass, ground bearing, stability, maintenance, recovery |
| Powered personal systems | Exoskeletons, loaders, breaching rigs | Human vulnerability, power, heat, ergonomics |
| Atmospheric/orbital service | High-altitude and station-service rigs | Pressure, thermal load, radiation, restraint |
| Space industry/combat | Free-flying manipulators, anchored or tethered rigs | Momentum, propellant, anchoring, debris, tool reaction |
| Deep ocean/subsurface | Pressure-rated walkers, crawlers, bore rigs | Pressure, corrosion, visibility, communications |
| Cryogenic/volcanic/contaminated zones | Sealed research walkers, remote extraction rigs | Thermal envelope, seals, calibration, sample custody |
| Low or variable gravity | Hoppers, anchored climbers, reaction-assisted rigs | Traction, rebound, anchoring, attitude control |

Technology does not progress in a straight line. Older platforms persist; doctrine branches; machines are abandoned, rediscovered, captured, and repurposed. Humanoid articulation can help with irregular obstacles and human-designed interfaces. Wheels and tracks remain better on many surfaces. In vacuum, legs do not stop drift or absorb recoil: anchoring, tethers, and reaction control matter.

## Six candidate machine families

- **Walking combat mech:** mobility, protection, sensors, manipulators, weapons, pilot station, and egress.
- **Industrial exo-loader:** lifting, construction, casualty recovery, and field repair.
- **Research/survey walker:** instruments, sampling, drilling, calibration, and hazardous exploration.
- **Resource harvester:** cutting, boring, collection, sorting, material transfer, tool wear, and waste.
- **Vacuum construction/salvage rig:** hull work, module alignment, anchoring, and wreck recovery.
- **Exotic-environment specialist:** deep ocean, subsurface caverns, dense atmospheres, cryogenic or volcanic terrain, and unusual gravity.

They share engineering concepts but are not one universal chassis with different names.

## What makes the machine work

A configuration needs a chassis envelope, mass and inertia, locomotion, actuators, power and energy storage, thermal rejection, structural load paths, protection, pilot/remote station, sensors, communications, control software, weapons and tools, consumables, qualified crew, calibration, and maintenance state.

A mount-compatible weapon can still destabilize the chassis, overload its power bus, exceed cooling capacity, or prevent recovery from a fall. Fit requires checks for mass, interfaces, power, thermal limits, terrain, crew, and maintenance—not just an unlocked upgrade.

## Damage changes capability

Do not reduce a mech to a single health bar. A damaged joint can reduce stride or prevent weight-bearing. A damaged foot sensor can degrade placement. A severed coolant line can force power derating. A damaged sight can create a blind sector. A weapon feed fault can leave ammunition aboard but unavailable. A jammed hatch can turn shutdown into emergency egress.

A mech may be degraded, immobilized, recovered under fire, abandoned, captured, repaired, or salvaged. Pilot injury, rescue, egress, and medical care are physical consequences. A wreck can block a route, provide cover, leak hazardous material, become a recovery objective, or supply evidence in a contested historical account.

## Machines also research, build, rescue, and harvest

A survey walker might carry a spectrometer and core sampler instead of a weapon. A construction rig needs manipulators and alignment sensors. A resource machine can damage a deposit, wear a tool, contaminate a sample, or produce waste. A vacuum salvage rig needs an anchor or reaction control when cutting something massive, or the rig itself may rotate and send debris onto a hazardous trajectory.

Research is not instant success. Designs are built, instrumented, tested, failed, inspected, revised, and qualified. Prototype failures become persistent history and evidence.

## Pilot experience and implementation order

1. Approach and inspect the machine.
2. Board, secure the pilot, close the hatch.
3. Power up and run self-test.
4. Verify actuator, thermal, sensor, and control readiness.
5. Check tools, weapons, payload, and consumables.
6. Release safeties and move under control.
7. Operate, fight, manipulate, or collect.
8. React to damage and degraded feedback.
9. Recover, evacuate, or egress.
10. Shut down, log faults, and begin maintenance.

Build in vertical slices: one walker chassis; a physical cockpit; locomotion and terrain tests; localized damage to an actuator, sensor, and control; one repair and recovery scenario; then reuse the foundations for a survey or resource platform. Space salvage follows only when reaction-control and anchoring contracts exist.

The machine-readable contract is [mech-and-exotic-platforms.json](../data/mech-and-exotic-platforms.json). It records candidate capability requirements, not a claim that mechs already run in the current runtime.
