# raWWar — Unit Formation, Training, Drilling, and Cohesion

Status: Living design model.

> **A unit is not a number of soldiers. A unit is soldiers who have learned to operate together.**

## 1. Squad baseline

The canonical squad size is **4 soldiers**.

A squad therefore represents both:

- four individual people with individual qualifications; and
- a trained organizational capability whose members can coordinate.

The second property is not automatic.

A newly assembled squad may possess four soldiers while having poor collective performance.

Training, practice, drilling, shared experience, leadership, and repeated procedures increase cohesion.

## 2. Vehicle-mounted unit capability

Mobility platforms can change how a squad operates without changing the squad's underlying personnel identity.

### Motorcycle unit

A motorcycle unit can consist of **two motorcycles operating as a coordinated pair**.

The motorcycles can:

- travel together;
- separate tactically;
- reconverge;
- cover different approaches;
- establish independent observation positions;
- support one another;
- dismount soldiers;
- relocate rapidly.

The pair is therefore a unit capability even when the motorcycles are physically separated.

A motorcycle may carry one qualified soldier.

Example baseline:

`2 motorcycles + 2 soldiers → motorcycle scout element`

The soldiers remain individuals and retain their own qualifications.

Possible progression:

`Light arms → improved arms → heavy weapon → anti-air → specialized missile/SAM capability`

A particularly advanced configuration could allow a motorcycle soldier to deploy as a **mobile SAM operator**, establishing a temporary anti-air position before relocating.

The important distinction is that the capability comes from:

`Vehicle + weapon + soldier qualification + training + logistics`

not from a vehicle having a hidden "SAM unit" stat.

## 3. Quad unit

A quad platform can provide greater carried firepower and personnel capacity.

Example baseline:

`1 quad + 2 lightly armed soldiers + heavy machine gun`

The platform therefore trades some mobility/stealth characteristics for:

- heavier direct fire;
- two-person dismounted capability;
- greater ammunition capacity;
- greater equipment-carrying capacity.

Upgrades can change the weapon system or carried equipment.

The same chassis/configuration model determines what is physically possible.

## 4. APC progression

An APC extends the same concept.

A small APC might transport:

`1 squad = 4 soldiers`

Larger APCs can transport:

`2 squads = 8 soldiers`

and larger troop carriers can transport still more personnel.

Eventually a sufficiently large carrier may transport **smaller vehicles**:

- motorcycles;
- quads;
- drones;
- specialist equipment;
- small utility platforms.

This should be constrained by:

- internal volume;
- loading geometry;
- ramp/door dimensions;
- mass;
- power;
- structural loading;
- securing equipment;
- crew;
- loading/unloading procedures;
- compatible vehicle dimensions.

The carrier does not magically "contain two motorcycles."

The motorcycles physically occupy space and require loading/unloading procedures.

## 5. Unit formation is a physical capability

A player should be able to form larger tactical organizations, but larger organizations require infrastructure and training.

For example:

### One squad

`1 barracks + 1 squad`

### Two-squad formation

`2 barracks + 2 squads + combined-training capability`

### Three-squad formation

`3 barracks + 3 squads + combined-training capability`

The barracks count matters because the soldiers need physical accommodation, equipment, scheduling, instructors, and training capacity.

The formation is not created merely by clicking "combine squads."

## 6. Training and drilling

Soldiers become skilled through practice.

Training is therefore continuous rather than a one-time character-generation event.

A soldier with nothing else to do should not appear perfectly disciplined.

Early observations might include:

- awkward marching;
- poor synchronization;
- inconsistent spacing;
- slow formation changes;
- missed commands;
- equipment handling mistakes;
- weak coordinated movement.

With repeated practice, these behaviors improve.

Training produces measurable capabilities:

- individual skill;
- procedural memory;
- weapon proficiency;
- vehicle proficiency;
- formation discipline;
- communication;
- reaction time;
- coordinated movement;
- emergency response;
- leadership;
- unit cohesion.



## 6A. Skill is Gesture execution variance

Skill does not need to be represented as a conventional abstract "skill stat."

The ideal Gesture already defines the desired physical action.

For a precision action such as firing a weapon:

1. The soldier receives the intent to engage a target.
2. The weapon/interaction procedure identifies the required Gesture.
3. The Gesture describes the ideal sequence and positioning required for a perfect execution.
4. The soldier attempts to execute that Gesture.
5. The soldier's experience determines the variance allowed around the ideal motion.
6. The resulting physical position determines the actual outcome.

Conceptually:

`Ideal Gesture + Soldier State + Experience → Executed Gesture`

A highly experienced soldier approaches the ideal trajectory and final position closely.

A green soldier has a wider fuzzy range around the same ideal.

The important point is that the green soldier is **not aiming at a different target** and does not need a separate inaccurate animation.

The soldier is attempting the same ideal action.

The difference is the precision of execution.

### Example: precision fire

Suppose Soldier A is ordered to shoot Soldier B.

The ideal Gesture produces a position that would place the weapon's aim exactly on Soldier B's head.

An experienced soldier may execute within a very small positional range around that ideal:

`Ideal aim → very small execution variance → head hit is likely`

A green soldier may have a much larger range:

`Ideal aim → larger execution variance → head, torso, limb, near miss, or complete miss`

The green soldier can therefore produce a leg hit without the game ever deciding:

> "This soldier has a 17% chance to hit the leg."

The leg hit is the physical consequence of the Gesture being executed slightly away from its ideal position.

This same principle applies to:

- weapon handling;
- aiming;
- throwing;
- driving;
- aircraft control;
- climbing;
- loading equipment;
- medical procedures;
- construction;
- maintenance;
- formation movement;
- communications procedures;
- emergency actions;
- tool operation.

### Experience narrows the fuzzy range

Practice does not replace the ideal Gesture.

Practice changes the soldier's ability to reproduce it.

Conceptually:

`Green → large variance`

`Trained → reduced variance`

`Experienced → small variance`

`Expert → very small variance`

There should still be bounded variation at every level.

An expert is not a perfectly identical animation or a mathematically perfect machine.

The experienced soldier simply executes the intended action much more consistently.

### Skill therefore becomes visible

This gives the simulation a powerful visual consequence.

Two soldiers can receive the same order, use the same weapon, possess the same equipment, and attempt the same Gesture.

Yet they can produce different physical outcomes because their execution variance differs.

The player can therefore **see skill** rather than merely reading it from a character sheet.

A green squad may:

- take longer to settle into firing positions;
- produce visibly wider aim movement;
- make larger corrections;
- occasionally miss;
- occasionally strike less desirable portions of a target;
- recover more slowly from mistakes.

A practiced squad converges toward the same ideal actions with tighter, faster, more repeatable motion.

### Skill is therefore not another simulation layer

The architecture remains:

`Intent → Procedure → Ideal Gesture → Execution Variance → Physical Result`

not:

`Intent → Skill Stat → Dice Roll → Result`

The first model lets the existing Gesture system carry the meaning of skill.

The same mechanism can be used by soldiers, vehicle crews, construction workers, technicians, pilots, medical personnel, and any other qualified actor.

> **The ideal Gesture is what perfect execution looks like. Skill is how tightly the actor can reproduce it.**

## 7. PT tooling

Physical training and drilling require physical infrastructure.

A barracks can expose training interfaces/tooling.

Example:

### PT1

**Individual/squad physical training and basic movement**

Capabilities may include:

- marching;
- spacing;
- basic formations;
- physical conditioning;
- basic commands;
- synchronized movement.

### PT2

**Combined squad training**

A PT2 capability allows two trained squads to practice operating as a combined formation.

The important consequence is:

`2 trained squads ≠ 1 cohesive two-squad unit`

until they have practiced together.

PT2 therefore consumes:

- facility capacity;
- qualified instructors;
- training time;
- soldier availability;
- equipment;
- energy;
- scheduling.

## 8. Cohesion progression

A conceptual progression:

`Individual → Squad → Two-Squad Formation → Three-Squad Formation → Larger Formation`

Each transition requires additional collective practice.

Cohesion should affect:

- response to commands;
- formation integrity;
- movement coordination;
- target distribution;
- mutual support;
- casualty response;
- resupply behavior;
- communications;
- reaction to unexpected events;
- ability to execute complex procedures under stress.

Cohesion is not a damage multiplier.

It is an organizational capability.

## 9. Training is work

Training consumes the same scarce resources as other organizational activities.

A training session requires:

- soldiers;
- instructors;
- barracks/training spaces;
- training tooling;
- equipment;
- ammunition where applicable;
- power;
- food/water;
- maintenance;
- scheduling;
- time.

A commander who commits every soldier to operations can therefore reduce future readiness by starving training.

Conversely, a commander who maintains a training pipeline can build a force that becomes increasingly capable.

## 10. Drilling while idle

The world should visibly demonstrate this system.

When soldiers have no higher-priority work, they can be assigned to:

- physical training;
- marching;
- formation practice;
- weapons practice;
- maintenance drills;
- emergency drills;
- loading/unloading practice;
- vehicle embarkation;
- disembarkation;
- communications exercises.

The player should occasionally see imperfect behavior because the soldiers are learning.

Later, the same formation should visibly move with much greater confidence and precision.

This makes training a visible part of the living world rather than a hidden percentage.

## 11. Formation command

A formation command is itself a procedure.

For example:

`Commander Order → Formation Procedure → Squad Leaders → Soldiers`

The quality of execution depends on:

- communication;
- command authority;
- individual qualification;
- squad cohesion;
- inter-squad cohesion;
- environmental conditions;
- fatigue;
- casualties;
- equipment state;
- current mission;
- practice.

A two-squad formation therefore becomes more than eight soldiers standing nearby.

It is eight soldiers who have practiced responding to the same command structure.

## 12. Vehicle and formation interaction

A formation can have a mobility configuration.

Examples:

- 4 soldiers on foot;
- 2 motorcycles + 2 soldiers;
- 1 quad + 2 soldiers;
- 1 APC + 4 soldiers;
- 2 APCs + 8 soldiers;
- large carrier + multiple squads;
- carrier + motorcycles/quads internally transported.

This allows the organization to compose mobility and personnel capabilities.

A vehicle can also change how soldiers are deployed.

For example:

`APC arrives → doors open → squad dismount procedure → formation establishes → APC relocates`

The individual soldier's Gesture/FSM handles the physical action.

The squad FSM coordinates the collective action.

The vehicle FSM handles the carrier.

The command FSM provides the intent.

## 13. Equipment progression

The same unit can gain capability through research and configuration.

For example:

`Motorcycle Scout`
→ improved communications
→ improved optics
→ heavier weapon
→ anti-air weapon
→ deployable SAM
→ networked air-defense scout

But each progression can require:

- research;
- production;
- vehicle modification;
- weapon production;
- ammunition;
- qualification;
- training;
- logistics;
- maintenance.

Research therefore does not instantly transform every soldier.

## 14. FSM composition

Unit capability can be composed from:

- Individual Soldier FSM;
- Qualification FSM;
- Training FSM;
- Squad FSM;
- Formation FSM;
- Vehicle FSM;
- Vehicle Crew FSM;
- Weapon FSM;
- Communications FSM;
- Command FSM;
- Logistics FSM;
- Maintenance FSM;
- Deployment FSM;
- Cohesion FSM.

This is exactly where the centralized FSM architecture becomes valuable.

The game does not need a special monolithic "squad system."

The squad is the emergent result of soldiers, procedures, equipment, command, and shared training.

## 15. Commander-facing view

The commander should see consequences.

Example:

**2nd Scout Squadron**

- Personnel: 8
- Formation: 2 squads
- Cohesion: developing
- Mobility: 4 motorcycles
- Heavy weapons: 0
- Air defense: 0
- Readiness: 82%
- Combined training: 6/20 hours
- Next capability: coordinated two-squad maneuver

The deep dive can show exactly why those values exist.

## 16. Strategic consequence

The player should eventually realize:

> **You cannot manufacture cohesion by declaring it.**

You have to house people.

Train them.

Drill them.

Equip them.

Give them instructors.

Give them time.

Practice together.

Then deploy them.

That makes barracks among the most important buildings in the civilization.

## 17. Barracks as organizational infrastructure

Barracks are not merely beds.

They provide:

- personnel accommodation;
- equipment storage;
- scheduling;
- physical training;
- instruction;
- qualification;
- drilling;
- recovery;
- squad formation;
- leadership;
- readiness management.

A large military therefore requires a large training/accommodation infrastructure.

That infrastructure itself consumes:

- land;
- construction;
- power;
- water;
- food;
- maintenance;
- instructors;
- equipment.

## 18. Power is infrastructure

Buildings require energy.

Power production therefore belongs in the same physical dependency network as housing, factories, refineries, communications, sensors, and logistics.

Conceptual chain:

`Resource → Power Generation → Distribution → Facility → Equipment → Procedure → Capability`

A barracks without sufficient power may still house soldiers while losing portions of its training, communications, environmental control, or support capability.

A factory without sufficient power may exist physically while production slows or stops.

A refinery without sufficient power may have the right tooling and resource but be unable to process.

Power is therefore not merely an economy number.

It is infrastructure.

## 19. Power production facilities

Power generation should be modeled as physical facilities with:

- generation technology;
- resource/fuel input;
- generation capacity;
- storage;
- distribution interfaces;
- maintenance;
- qualified operators;
- environmental constraints;
- redundancy;
- failure states;
- construction requirements.

Different generation technologies can produce different strategic tradeoffs.

The player may build:

- small distributed generators;
- centralized power plants;
- redundant generation;
- storage systems;
- dedicated industrial power;
- mobile generation;
- orbital or remote generation where appropriate.

## 20. The living military

The intended visual result is important.

A base should not contain:

> "100 soldiers: morale 72."

It should contain people.

Some are training.

Some are eating.

Some are maintaining vehicles.

Some are marching badly.

Some are learning to operate together.

Some are recovering.

Some are waiting for orders.

Some are preparing equipment.

And as training progresses, the same people become visibly more capable.

> **The numbers describe the people. They do not replace them.**
