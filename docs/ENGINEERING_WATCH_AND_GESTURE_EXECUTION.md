# Engineering Watch, Gesture Execution, and Maintenance Consequence

## Purpose

Engineering is not a menu of reactor statistics.

It is a department full of qualified people performing procedures against physical equipment on a schedule.

The player should be able to observe that work directly.

The engineering head officer assigns qualified crew to stations. Each crew member then performs the procedures required by that station, moving through the department, inspecting equipment, taking readings, operating controls, recording observations, and responding to deviations.

The result is a living engineering department rather than a collection of hidden numbers.

## 1. The engineering department

The engineering head officer is responsible for turning the ship's engineering capability into an organized watch.

A simplified assignment flow is:

Engineering Head Officer → Watch Assignment → Station → Procedure → Gesture → Observation/Action → Log → Consequence

The officer assigns qualified crew according to:

- qualification;
- station requirements;
- current watch;
- equipment condition;
- mission state;
- fatigue;
- workload;
- maintenance requirements;
- emergency conditions;
- redundancy and backup coverage.

The assignment itself is a procedure.

The crew member then executes the work.

## 2. Lowest generator watch

A simple generator watch provides a useful first example.

The watchstander may be responsible for navigating between reactor or generator plants and periodically recording required operating conditions.

The simulation does not need to tell the player:

> "Generator maintenance efficiency: 94%."

Instead, the player can see the watchstander perform the work.

A tricorder-like diagnostic device, scanner, terminal, or equivalent instrument is brought into close physical approximation with the equipment or its projected diagnostic hologram.

The equipment can **breathe** the required action:

- move here;
- hold the instrument here;
- orient it this way;
- wait for the reading;
- record the result;
- proceed to the next station.

The hologram is therefore simultaneously instruction, animation target, and simulation interface.

## 3. Ideal Gesture

Every required action has an Ideal Gesture.

The Ideal Gesture defines:

- target position;
- body orientation;
- hand/device position;
- equipment interaction;
- required sequence;
- acceptable approach;
- expected completion state.

The watchstander does not need to hit a button called "inspect generator."

The watchstander physically performs the inspection.

The simulation evaluates the resulting execution against the ideal.

Ideal Gesture + Soldier State + Experience → Executed Gesture

The same model already used for combat skill therefore applies to engineering.

## 4. Ideal time

Position is only half of the requirement.

Many engineering procedures also have an ideal observation or action time.

Next Reading Due → Transit Window → Ideal Observation Time → Acceptable Window → Late → Maintenance Risk

The player can see a countdown or time window indicating when the reading should occur.

This creates a real engineering rhythm.

The watchstander has enough time to:

1. recognize the next task;
2. navigate to the station;
3. prepare the instrument;
4. reach the equipment;
5. establish the correct position;
6. take the reading;
7. record it;
8. move to the next station.

The ideal is not merely "be there."

It is:

> **Be in the right place, in the right orientation, performing the right action, at the right time.**

## 5. Execution scoring

Execution quality can be derived from physical deviation rather than a hidden skill roll.

Relevant dimensions can include:

- positional error;
- orientation error;
- instrument placement;
- procedural sequence;
- timing error;
- duration;
- hesitation;
- unnecessary movement;
- equipment condition;
- operator fatigue;
- environmental interference.

A simplified conceptual model is:

Execution Quality = Spatial Accuracy × Temporal Accuracy × Procedural Accuracy × State Fitness

The actual implementation can remain deterministic and data-driven.

The important point is that the score describes the execution.

It does not replace it.

## 6. Why the timing matters

Some readings are not decorative bookkeeping.

A reactor plant, generator, propulsion system, pressure vessel, turbine, cooling system, or structural member can be changing continuously.

A missed observation can therefore allow a developing condition to pass unnoticed.

For example:

Normal → slight deviation → missed reading → deviation grows → warning threshold → serious deviation → emergency

The simulation can accumulate this as actual equipment state and maintenance history.

A crew member who consistently performs inspections close to the ideal can identify developing conditions earlier.

A crew member who is repeatedly late can allow small deviations to become expensive repairs.

Eventually the player may encounter the moment where everyone wishes someone had done the boring inspection correctly.

That is much more powerful than a random failure event because the history is explainable.

## 7. Engineering skill is visible

A newly qualified watchstander should not simply receive a lower statistic.

The player should see:

- slower navigation;
- wider positional variance;
- less precise instrument placement;
- more hesitation;
- poorer timing;
- more correction movements;
- less confidence;
- greater likelihood of missing the ideal observation window.

An experienced watchstander should visibly converge on the Ideal Gesture:

- direct movement;
- precise positioning;
- minimal wasted motion;
- accurate instrument placement;
- consistent timing;
- rapid recognition of abnormal readings.

Skill therefore becomes observable.

The player can watch someone become better.

## 8. Maintenance debt

Engineering procedures should leave history.

A reading can produce:

- normal observation;
- early warning;
- maintenance recommendation;
- deferred maintenance;
- active fault;
- emergency condition.

Maintenance debt is therefore not an arbitrary penalty.

It is the accumulated consequence of physical work that was delayed, missed, performed poorly, or correctly completed.

A useful conceptual chain is:

Procedure → Observation → Equipment State → Maintenance Need → Maintenance Queue → Repair → Readiness

The organization can respond automatically through its normal logistics and maintenance procedures.

The commander sees the consequence.

The engineering deep dive explains the cause.

## 9. The "oh shit" moment

The system should deliberately support delayed consequences.

A watchstander misses a reading.

Nothing explodes.

The player may not even notice.

Hours later:

- vibration changes;
- thermal margin decreases;
- structural stress rises;
- a bearing begins degrading;
- a cooling loop becomes less efficient;
- a maintenance warning appears;
- a repair is scheduled.

If enough opportunities are missed, the condition can become dangerous.

The player can then discover that the problem did not begin when the alarm sounded.

It began when a small procedure was not performed.

That is much more powerful than a random failure event because the history is explainable.

## 10. Training through the same system

Engineering training can use the exact same Gestures.

A holographic instructor can demonstrate:

1. where to go;
2. what equipment to approach;
3. how to position the body;
4. how to position the diagnostic device;
5. what reading to obtain;
6. when to obtain it;
7. where to record it;
8. what abnormal conditions look like;
9. what procedure follows.

The trainee then performs the procedure.

Training narrows execution variance.

Qualification establishes that the person is authorized and capable of performing the procedure.

Experience determines how reliably the person reproduces the Ideal Gesture under real conditions.

## 11. Emergencies

Normal watch procedures can transition into emergency procedures.

For example:

Abnormal Reading → Diagnose → Notify → Isolate → Stabilize → Repair/Evacuate

The same physical model applies.

A crew member may need to:

- reach an emergency station;
- manipulate physical controls;
- don protective equipment;
- communicate with another station;
- close valves;
- isolate a subsystem;
- operate backup equipment;
- assist an injured crew member.

The player can see the procedure unfold.

The simulation does not need to fake competence with a percentage.

It can simply execute the procedure with the actor's actual qualification, state, experience, equipment, and physical accuracy.

## 12. The engineering department as a living Experience

The engineering department becomes an Experience the player can enter and understand.

At a glance:

**Engineering Watch**

- Current watch: Generator Plant 2
- Watchstander: Ensign Rao
- Next reading: 00:47
- Required qualification: Generator Watch
- Equipment state: Nominal
- Maintenance warnings: 2
- Current procedure: Generator inspection

A deeper view reveals the physical work.

The player can follow the watchstander.

They can watch the hologram demonstrate the ideal.

They can watch the crew member attempt it.

They can see the difference.

They can see the reading.

They can see what the reading changes.

That is the game.

## 13. The underlying architecture

The system can be composed from existing Workshop concepts:

- Crew/Personnel FSM;
- Qualification FSM;
- Watch Assignment FSM;
- Navigation FSM;
- Procedure FSM;
- Gesture Provider;
- Equipment FSM;
- Sensor/Diagnostic FSM;
- Logging FSM;
- Maintenance FSM;
- Communications FSM;
- Command FSM;
- Training FSM;
- Emergency Procedure FSM.

No monolithic "engineering minigame" is required.

The department emerges from qualified people performing procedures against physical systems.

> **The equipment provides the procedure.
> The Gesture shows the ideal.
> The crew performs it.
> The physical state records the consequence.**

## 14. The same architecture everywhere

This model is not limited to reactors.

It applies to:

- propulsion plants;
- aircraft engines;
- spacecraft life support;
- structural inspections;
- weapons maintenance;
- missile guidance systems;
- vehicle maintenance;
- factories;
- refineries;
- power plants;
- construction;
- medical procedures;
- communications;
- sensor calibration.

The same underlying question remains:

> **What should a qualified person physically do, where, when, and with what consequence if they do it poorly or fail to do it?**

That is exactly the kind of question raWWar can simulate.
