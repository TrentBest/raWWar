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


## 15. Damage control is executable physical work

Battle damage does not create a generic hit-point deduction. It creates a casualty condition that changes the physical state of the ship and creates work for qualified people.

A simplified casualty flow is:

Damage → Casualty Detection → Damage Assessment → Dispatch → Physical Response → Verification → Recovery/Degradation → History

A newly created hull breach can produce an immediately understandable task. A damage-control crew member may be released from another assignment, sent to the casualty station, and shown an Ideal Gesture for the repair:

1. reach the breach;
2. inspect the opening and surrounding structure;
3. retrieve the correctly sized emergency plate;
4. position the plate against the hull;
5. hold it in the required spatial relationship;
6. apply instant adherent around the sealing interface;
7. verify the seal;
8. monitor for renewed leakage or structural movement;
9. report the compartment restored or escalate the casualty.

The player therefore sees the repair rather than a progress bar labeled Repair Hull.

### 15A. Casualty work can interrupt normal work

Crew are not permanently attached to one animation loop. A watchstander, technician, or other qualified crew member may be ordered to suspend routine work when a higher-priority casualty appears. The system records the interruption, moves the person through the ship, and assigns the casualty procedure they are qualified to perform.

Routine Work → Casualty Appears → Priority Re-evaluation → Crew Released → Transit → Casualty Procedure → Verification → Resume/Redirect

The consequence of pulling that person away is also real. The original procedure may become late, another station may need coverage, or another qualified person may have to take the watch.

### 15B. Fire is a physical casualty

Fire fighting follows the same model. A fire can have location, intensity, fuel source, atmosphere, heat, smoke, spread direction, containment state, suppression equipment, structural consequences, and personnel risk.

The response may include alarm and casualty communication, compartment isolation, route selection, protective equipment, extinguisher or suppression-system selection, approach to the fire, physical suppression Gesture, temperature and atmosphere verification, re-entry decision, damage inspection, and restoration of the compartment.

A failed response can allow the fire to spread. A successful response can still leave heat damage, smoke contamination, consumed suppression material, damaged wiring, or a maintenance requirement.

### 15C. The reactor officer's worst day

The reactor officer should be allowed to experience the consequences of being responsible for a real machine.

A severe reactor casualty can produce an all-reactors scram.

The event is not merely a health value reaching zero. It is a cascade:

Reactor Casualty → All Reactors Scram → Power Margin Collapses → Loads Shed → Propulsion Restricted → Systems Degraded → Engineering Casualty Response → Ship Mission Capability Reduced

The engineering department immediately changes state. Qualified personnel move to casualty stations. Emergency power becomes important. Cooling and reactor condition must be verified. The command crew receives a new operational picture.

If the ship can no longer maintain mission requirements, the commander may have no glamorous choice at all: limp home.

The ship turns toward a space station at reduced capability while the crew manages the casualty, protects life support, preserves remaining systems, and prepares for repair. The station must have the required repair capability, qualified personnel, tooling, spares, and docking capacity before the repair can actually happen.

### 15D. Damage creates history

Every casualty should leave a record containing at least: initiating event, location, equipment affected, people assigned, procedures attempted, timing, physical damage, temporary repairs, permanent repairs, materials consumed, secondary damage, mission impact, downtime, and lessons or research implications.

A ship returning to a station is visibly the ship that survived that battle. A plate may still be temporary. A burned compartment may still show evidence of the fire. A reactor plant may carry maintenance history. A crew member may have gained experience from the casualty. The next sortie begins from that history rather than resetting the ship to pristine condition.

> **Damage creates work. Work creates decisions. Decisions create consequences. Consequences become history.**
