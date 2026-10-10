# Production Acceptance, Digital Twins, and Maintenance

## Design intent

raWWar gamifies the digital-twin ambition of architecture, engineering, construction, industrial operations, and military sustainment. The target is not a visually detailed shell with hidden magic. It is a world of assets whose structure, state, connections, procedures, limits, and history explain what the player sees.

**Automation removes repetitive input; it does not remove the underlying work or its consequences.**

The player may supervise an entire industrial network, tune a production policy, personally test a completed part, or enter a repair bay and work on the failed equipment. These are different scales of interaction with the same underlying model.

## 1. The production loop

`Design/configuration → manufacture → awaiting acceptance → test → accept/reject → outgoing inventory → dispatch → installation/use`

A part does not become accepted merely because a machine finished making it.

1. The production system creates an identified output with a configuration and provenance.
2. The item enters the acceptance queue.
3. A qualified operator or the player uses the available fixture, instrument, and acceptance procedure.
4. Measurements are compared against the applicable specification and limits.
5. The item is accepted, rejected, or held for an inconclusive test.
6. Accepted items are released to outgoing inventory; rejected items are quarantined for diagnosis, rework, retest, salvage, or scrap.
7. Dispatch records preserve the identity and custody of the item.

Automation can execute routine steps. The player can take over any supported hands-on step, inspect the evidence, and make decisions permitted by their role and qualifications. The process remains observable even when automated.

## 2. An asset tree, not a single health value

The twin is hierarchical and composable:

`site → facility → production line → machine → assembly → component → device`

Depending on the system, the deepest modeled objects may include a motor winding, bearing, sensor, limit switch, relay coil, relay contact, contactor, fuse, terminal, cable, actuator, valve, connector, power supply, interlock, or control element.

A device can have:

- a stable identity and installed configuration;
- physical location and mounting relationships;
- electrical, mechanical, fluid, data, and control connections where applicable;
- normal and abnormal states;
- operating limits and environmental exposure;
- upstream prerequisites and downstream effects;
- failure modes and diagnostic evidence;
- inspection, testing, isolation, and repair procedures;
- qualification and tooling requirements;
- wear, calibration, defect, and service history.

Not every detail needs a bespoke simulation routine. The aim is to describe equipment and its relationships in data, then let reusable FSM-driven behavior execute the state transitions. A limit switch can share a common device model with other switches while its role, wiring, trip conditions, and consequences remain specific to its installation.

## 3. Control logic and interlocks

Consider a guarded production station. The operator requests a cycle, but the cycle may only begin if required prerequisites are true: the guard is closed, the relevant limit switch confirms position, the safety relay is healthy, downstream equipment is ready, and the correct configuration is selected.

The control system should distinguish:
- the requested action;
- the actual physical state;
- sensor evidence about that state;
- the logic evaluating the evidence;
- the actuator command;
- the resulting physical response;
- timeout, disagreement, or fault behavior.

A switch stuck in the asserted state, a relay with a failed contact, a broken conductor, a drifting sensor, or an unavailable downstream station should therefore have consequences that follow from its connections and logic. It should not simply subtract an arbitrary percentage from factory efficiency.

Safety interlocks are part of the modeled system. A player may investigate a fault or restore a circuit, but the simulation should not reward bypassing safety logic as if it were an ordinary optimization. Bypasses, where the scenario permits them, should be explicit configuration changes with authorization, warnings, and real operational consequences.

## 4. Maintenance interval is the player's decision

Production equipment tracks its own cycles and observed condition. The player chooses preventive maintenance at every N production cycles, with no universal magic N supplied by the game.

The player weighs:
- planned downtime and lost production;
- labor and qualification availability;
- spare parts and consumables;
- observed wear and calibration drift;
- failed tests, alarms, and quality escapes;
- consequence severity if the equipment fails in operation;
- availability of redundant or alternate stations.

An interval can be changed as experience accumulates. Maintenance history and defect rates should help the player understand whether their policy is effective. Exact degradation curves and thresholds remain explicit model decisions to be validated; they should not be invented as arbitrary hidden penalties.

Preventive maintenance can coexist with condition-based inspection, corrective work, and emergency shutdown. Intrusive work makes the affected equipment unavailable until it is safely isolated and restored. Production may wait, back up, or route to a compatible station if one exists.

## 5. The repair bay is a playable workplace

A player can take a technician role and perform the work directly:

`Report → make safe → isolate → diagnose → inspect → repair/replace → reassemble → calibrate → test → commission → record`

The task can involve finding the right drawing or procedure, identifying the installed revision, selecting the right tools, checking isolation, removing a failed component, installing a replacement, making adjustments, and proving operation. The ideal Gesture defines spatial placement, orientation, action order, and timing. Qualification, experience, fatigue, tool condition, and environmental constraints affect execution.

Repair completion is not the same as successful commissioning. If a test fails, the asset remains out of service or restricted; the player may have to diagnose another cause, correct the installation, obtain another part, or repeat a test. The result is recorded against the asset and its configuration.

Automated technicians can use the same procedure model. The player is not required to perform every bolt, but may choose to do so when the job is interesting, urgent, educational, or consequential.

## 6. Apply the same discipline to ships

A ship is a digital twin of a working system, not only a hull model. Its hierarchy can include compartments, power distribution, propulsion shafts, control stations, machinery, relays, switches, sensors, actuators, cables, interlocks, alarms, communications, and supporting maintenance equipment.

Control orders, interlocks, and actual response must remain distinct. A requested change is not proof that the machinery responded correctly. A shaft can lag, fail to answer, or disagree with a command; its actual state and the rest of the plant determine the outcome.

For multi-shaft propulsion, the model must represent shared intent and per-shaft execution. If the bridge orders a bell, the control system translates that order into coordinated commands to the applicable shafts, checks permissives and feedback, detects disagreement, and reports the result. The goal is not to make four independent operators blindly duplicate a button press; it is to model why coordinated control exists and what happens when timing or feedback differs.

The same architecture can support several control arrangements:
- one authorized operator issues a common order that coordinates all selected shafts;
- two-station operation requires the configured concurrence or interlock before actuation;
- individual stations can retain per-shaft control where the vessel's configuration requires it;
- alarms and feedback reveal a mismatch between ordered and actual state.

These are configuration-driven operating arrangements, not assumptions hard-coded for every vessel. The model must distinguish command, permissive, interlock, actuation, feedback, and resulting plant response.

## 7. History makes the twin useful

Every meaningful operation can append evidence:
- asset identity and configuration;
- command and observed response;
- readings and test limits;
- operator or automation identity and qualification;
- procedure revision and execution result;
- defects found and parts consumed;
- maintenance and calibration;
- acceptance or rejection;
- downtime, operational impact, and return-to-service evidence.

This history supports inspection, diagnosis, training, quality assurance, research, and after-action review. The world should be able to explain not just that something failed, but what was observed, what was commanded, what actually happened, and which earlier decisions contributed.

## 8. Reusable Workshop behavior

The desired implementation is not a one-off factory minigame or ship-specific collection of special cases. Reusable FSM-driven capabilities should cover asset state, procedure execution, qualification, control logic, diagnostics, maintenance, acceptance testing, work queues, and event history. Facility, ship, and device data describe the actual installation and its rules.

That lets one interaction model span a production line, a vehicle repair shop, a reactor plant, a ship's propulsion controls, or a single failed limit switch. The experience can scale from command-level management to hands-on work without changing the underlying truth.

> **The data describes the equipment. The FSMs make it behave. The procedures make it operable. The records make its history explainable.**
