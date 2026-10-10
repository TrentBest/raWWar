# raWWar — Embodied Interfaces, Mechs, and Physical Operations

> **The interface is part of the world. The player does not click an abstract command; they operate a physical thing, through a body, with consequences.**

Status: Candidate simulation contract. The machine-readable contracts are [Embodied Interaction and Control](../data/embodied-interaction-and-control.json), [Manned Mech Platforms](../data/manned-mech-platforms.json), and [Engineering Watch and Maintenance](../data/engineering-watch-and-maintenance.json). These establish architecture and validation requirements; they do not claim that a finished interaction runtime, mech simulator, or renderer already exists.

## 1. The world is the GUI

A fighter cockpit, reactor console, maintenance terminal, station access rig, mech harness, engineering logger, and cargo crane should all be interfaces to actual systems in the world.

The player walks up to a control, sees its physical display and controls, reaches for it, and operates it. A screen has power, condition, visibility, latency, access rules, and only the information available to its connected system. A lever has travel, force, detents, and interlocks. A button can be jammed, broken, unpowered, locked out, or wired to a different function in a different installation.

Shoot a screen and its display may crack, go dark, become intermittently readable, or expose internal components. Shoot a control panel and damage follows its construction, wiring, protection, and connected systems. It should not simply subtract generic “interface health.” Damage is physical, and its effect follows actual dependencies.

This does **not** mean every interaction must be cumbersome. The same action model supports VR tracking, first-person reach/use, mouse, keyboard, controller, and accessibility inputs. A key can cause the avatar's hand to reach for a valid joystick and operate it. The player gets responsive input; the world still gets a visible, stateful action with preconditions and consequences.

## 2. One semantic action, multiple input methods

The architecture separates six things:

1. **World object:** the actual lever, grip, panel, logger port, latch, connector, or screen.
2. **Semantic action:** press, pull, rotate, hold, release, inspect, connect, record, isolate, lock, tag, or acknowledge.
3. **Input adapter:** VR hands, FPS use, mouse, keyboard, controller, or accessibility device.
4. **Actor performance:** reach, posture, force, timing, qualification, fatigue, injury, restraint, and Gesture variation.
5. **Target response:** interlocks, authorization, damage, power, system state, and actual outcome.
6. **Event history:** who did what, where, when, under which conditions, and with what result.

That common path is essential. Otherwise the keyboard player gets a magical shortcut while the VR player has to physically do the job, or the NPC engineers obey a different universe from the player.

### Example: pilot moves a mech forward

- The player presses the forward binding.
- The occupied pilot station resolves the binding to a valid throttle/grip.
- The pilot's visible hand moves to and advances the control.
- The motion-capture rig and control law interpret the input.
- The controller requests actuator forces, but the mech's installed actuators, power, cooling, joint loads, traction, balance and damage constrain what actually happens.
- The mech moves, slips, slows, or fails according to the resulting physical state.
- The action and any important result enter persistent history.

A joystick input is not a direct teleport of the vehicle. It is an input to a machine.

## 3. Breathing Gesture guidance

The translucent, breathing hologram demonstrates the ideal next physical action. If the next step is a button, that button breathes. If it is a lever, the lever and expected pull direction breathe. If a reading is due, the logger port breathes.

The ghost shows a target, ideal pose, direction, and useful timing window. It never pushes the button for the player. The actual system still checks reach, access, interlocks, authorization, power, damage, and operating state. If conditions change, the hint changes too.

This is how we teach complicated procedures without turning the game into a wall of tutorial text. Experienced characters can perform familiar Gestures more consistently; green or exhausted characters may be slower or less precise. Guidance can explain an error without secretly correcting the action.

## 4. Boarded mechs: a vehicle, a workplace, a control system

Battle mechs are explicitly part of the intended experience. The goal includes the joy of climbing into a large, configurable walking combat machine—not just commanding one from a map.

A mech is assembled from parts and systems: structural frame, joints, actuators, power source, distribution, cooling, mobility/contact system, sensors, compute, stability control, armor, pressure boundaries, manipulators, weapons, feeds, navigation, communications, pilot station, restraints, service access, and recovery points. Each part has identity, interfaces, mass, power and heat requirements, operating envelope, damage modes, inspection procedure, qualifications, spares, repair time, and history.

The player boards the machine. The station secures the pilot, checks restraint and harness fit, calibrates motion capture, verifies controls, and connects the pilot to the vehicle. In first person, the player sees the rig, their hands on the grips, instrument screens, and physical emergency controls. In VR, tracked movement can provide direct motion/force input. In keyboard or mouse play, bindings animate the same rig and feed the same control path.

### The pilot does not need to imitate every actuator

A large machine may coordinate thousands of actuators or impulse thrusters. The pilot supplies intent and physical input; control laws translate that input into a coordinated response. Sensors feed back actual state. Stability, traction, available thrust, power, thermal limits, damage, latency, and pilot performance all constrain the result.

A sustained raised control posture can demand real modeled exertion where the machine's control design calls for it. Fatigue should follow ergonomic demand, training, equipment support, and the pilot's condition—not be a universal arbitrary tax. Alternative control layouts can trade precision, workload, redundancy, and endurance.

### Mechs are not one universal unit

The candidate family includes:
- utility exoframes for load handling, hazardous maintenance, and casualty extraction;
- light recon walkers for scouting, sensor placement, and rapid response;
- combat walkers for direct fire, escort, and broken-terrain fighting;
- heavy support walkers for fire support, engineering breach, and heavy manipulation;
- orbital work mechs for hull repair, station construction, cargo, and rescue;
- environmental research walkers for sample collection, hazard survey, and resource prospecting.

The evolutionary path is not a simplistic tech tree where a tank becomes a mech and then a spaceship. Different environments create different pressures: rubble, high ground pressure, vacuum, low gravity, deep-ocean pressure, dust, radiation, thermal cycling, corrosive fluids, high gravity, and unknown hazards. Some designs succeed; others become expensive, fragile, dangerous, or useful only for a narrow job. Failed experiments leave wrecks, test records, patents, casualties, salvaged components, and doctrine changes.

## 5. The long arc of machine warfare

raWWar can explore successive and overlapping waves of machinery:
- terrestrial powered frames, logistics machines, and industrial walkers;
- armored combat walkers, remote systems, and specialized breaching machines;
- aerospace and orbital construction platforms, external hull-work rigs, and rescue machines;
- lunar/planetary machines designed around dust, gravity, radiation, and thermal extremes;
- maritime and subsurface platforms where pressure, buoyancy, corrosion, and visibility dominate;
- research and extraction machines built for places where a human body cannot safely work;
- hybrid designs that combine familiar mechanics with exotic propulsion, sensor, material, or energy systems when the setting's researched technology supports them.

This is a history of attempted solutions, not a guarantee that every environment favors a humanoid shape. The biped can be a tactical, industrial, cultural, or maintenance compromise. The simulation should allow the player to ask whether the machine is worth its mass, power, logistics, signature, training, repair burden, and recovery risk.

## 6. Damage and maintenance are component-level

A damaged actuator can reduce stride or balance. A failed sensor can make stabilization uncertain. A cut power bus can isolate a limb or weapon. A damaged cooling loop can force power derating. A broken mount can make a weapon unsafe even when the weapon itself still works. A damaged pilot restraint or pressure boundary can make the vehicle unsafe to occupy.

These are different failure modes and different jobs. A mech should not have one hit-point bar that magically represents all of them. Visual mesh merging or instancing is allowed only as a rendering optimization; it must not erase component identity, damage state, independent animation, interaction, culling, or streaming boundaries.

## 7. Engineering as active gameplay

Engineering crew should physically move around the ship or station, operate equipment, collect readings, inspect systems, perform procedures, and communicate the actual limits to command. “We're giving her everything she's got” should be the end of a real load and maintenance story: temperatures, power margins, vibration, coolant flow, actuator wear, bypasses, and repair capacity have been measured and pushed.

When a reading is due, the guidance ghost points to the log port. The engineer retrieves a logger, connects it, and captures the readings available through that port: values, units, timestamps, calibration state, gaps, and provenance. The player compares the result with operating thresholds and trend history. A logger is an instrument, not an omniscient truth detector.

If equipment is approaching a threshold, the engineering team must decide whether to derate, redistribute load, inspect, repair, replace, or accept risk. If it crosses a limit, consequences follow the actual system: alarms, load shedding, reduced propulsion, degraded stabilization, equipment isolation, or a shutdown.

## 8. Lockout/tagout: deliberately frustrating for the right reasons

Lockout/tagout should be an intentionally annoying operational system—because the annoyance comes from coordinating real isolation and proving the equipment is safe, not from a random timer.

The modeled sequence is identify energy sources, notify affected crew, shut down, isolate each source, apply personal locks and tags, release or block stored energy, verify zero energy, perform a safe test, complete the work, inspect and clear the area, remove locks under an authorized procedure, restore energy in stages, verify return to service, and record sign-off.

Electrical power is not the only energy source. Capacitors, batteries, rotating machinery, pressure, hydraulics, heat, propellant, suspended loads, remote commands, and re-accumulating energy can all matter. A checkbox cannot isolate an actual source. A tag identifies a worker and intent but does not itself prevent energization. A status lamp is not proof of zero energy.

The friction should create decisions and stories:
- the drawing is stale and a hidden feed must be traced;
- a second crew member's lock prevents premature restoration;
- stored pressure returns after isolation;
- an emergency bypass is authorized and becomes a permanent incident record;
- a shortcut injures a worker, damages equipment, invalidates a qualification, or triggers an investigation.

Not every trivial interaction needs the entire procedure. The correct depth depends on energy, risk, access, and fidelity tier. When a job genuinely demands full isolation, it must be real and auditable.

## 9. Renderer performance: test the hardware, don't worship a hypothesis

GPU-side work is not unlimited. Pixel coverage is one possible bottleneck among many. Memory capacity, memory bandwidth, bus transfers, cache locality, command submission, synchronization, geometry processing, skinning, physics, simulation, and CPU/GPU coordination can dominate instead.

The rendering strategy is to profile before deciding:
- use instancing for repeated compatible parts;
- join static compatible meshes when draw-call overhead is measured to be the bottleneck;
- preserve independently damaged, articulated, interactable, streamed, or animated parts as independent logical components;
- use hierarchical culling and cluster/mesh LOD for large assemblies;
- separate authoritative simulation state from render caches;
- lower fidelity for distant/background actors without erasing consequential history;
- test both pixel-heavy scenes and simulation-heavy scenes.

Benchmarks should vary resolution, number of mechs, visible component count, damage, smoke, lighting, transparent ghosts, cockpit displays, physics contacts, and off-screen population. Measure frame time by pass, draw/dispatch count, pixel coverage, geometry, GPU memory residency, streaming churn, bandwidth, bus transfer and stalls, CPU simulation time, physics cost, and interaction latency.

The hypotheses are falsifiable:
- If the frame is pixel-limited, lowering resolution should improve GPU time predictably.
- If dispatch overhead dominates, instancing or batching should help without breaking damage or culling.
- If bandwidth dominates, reducing uploads and improving data locality should help.
- If simulation dominates, reducing resolution alone will not fix the frame.

The target is not a promise of infinite objects. It is the maximum useful living world that measured hardware budgets can support, with fidelity adapted to what matters now.

## 10. Implementation order

1. Define stable world-object and semantic-action contracts.
2. Build one end-to-end control example shared by keyboard and VR-style input: occupied mech station, hand animation, grip, control law, actuator state, and event history.
3. Build a single mech with a small real component hierarchy, power/thermal constraints, damage propagation, and repair.
4. Add physical screen damage, a lever, a logger port, and the breathing guidance ghost.
5. Build one engineering log-and-threshold task and one complete lockout/tagout job with persistent evidence.
6. Add benchmark scenes before expanding actor/component counts; use measurements to choose batching, instancing, streaming, and LOD.
7. Expand mech roles and environments only as component, qualification, supply, repair, and test data become credible.

The rule that joins it all: **the data describes the machine; the control system makes it move; the crew makes it useful; damage creates work; work creates decisions; and the world remembers what happened.**
