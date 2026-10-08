# raWWar — Skill as Gesture Execution

Status: Living design model.

> **Skill is not a number added to an outcome. Skill is the ability to reproduce an ideal action with less physical variance.**

## 1. The ideal action

Every meaningful physical action has an ideal execution.

Imagine a soldier who is perfectly trained, perfectly positioned, perfectly stable, and perfectly executing an action.

That is the **Ideal Gesture**.

For a weapon engagement, the ideal Gesture includes the sequence of movement required to establish the correct stance, position the weapon, align the sights, control the weapon, and produce the intended shot.

The ideal is not a scripted result.

It is the target that physical execution approaches.

## 2. Execution is where skill lives

The actual actor does not necessarily reproduce the ideal exactly.

Instead:

`Ideal Gesture + Actor State + Experience → Executed Gesture`

Experience controls the size and character of the permitted execution variance.

A green soldier has a larger fuzzy range around the ideal.

An experienced soldier has a smaller fuzzy range.

An expert still has bounded variation rather than becoming a perfectly identical machine.

## 3. Example: aiming at Soldier B

Suppose Soldier A is ordered to engage Soldier B.

The ideal execution places the weapon's aim exactly on Soldier B's head.

A highly experienced soldier may reproduce that positioning within a very small range.

A green soldier may reproduce the same intended action with substantially more positional variance.

The green soldier might therefore:

- hit the head;
- hit the torso;
- strike an arm or leg;
- pass close to the target;
- miss entirely.

Those outcomes emerge from the executed physical position.

The simulation does not need a rule such as:

`17% chance of head shot`

or:

`8% chance of leg shot`

Instead:

`Ideal aim → execution variance → actual aim → physical result`

The result is a consequence of the action.

## 4. Skill narrows variance

A useful conceptual progression is:

`Green → large variance`

`Trained → reduced variance`

`Experienced → small variance`

`Expert → very small variance`

Training therefore improves repeatability.

It does not replace the Gesture.

It does not secretly change the target.

It does not manufacture arbitrary bonuses.

It makes the actor better at reproducing the intended action.

## 5. The same model applies everywhere

This is not a weapon-specific mechanic.

The same model can describe:

- weapon handling;
- aiming;
- throwing;
- reloading;
- driving;
- piloting;
- climbing;
- entering vehicles;
- loading equipment;
- medical procedures;
- construction;
- maintenance;
- communications procedures;
- formation movement;
- emergency response;
- tool operation.

For example:

`Ideal loading Gesture + inexperienced loader → wider movement variance → slower/messier loading`

while:

`Ideal loading Gesture + experienced loader → tighter movement variance → faster/repeatable loading`

The physical action remains the same.

## 6. Qualification and skill are different

A soldier may be **qualified** to perform an action without being highly skilled at performing it.

Qualification answers:

> **Can this person legally/organizationally perform this procedure?**

Gesture execution answers:

> **How precisely can this person reproduce the procedure?**

Training connects them.

A newly qualified operator may have permission and knowledge but still execute with substantial variance.

Repeated practice narrows that variance.

## 7. Cohesion is the collective version

The same principle extends from the individual to the unit.

Individual Gesture:

`Intent → Ideal Gesture → Execution Variance → Physical Result`

Formation procedure:

`Command → Ideal Formation Procedure → Collective Execution Variance → Formation Result`

A newly assembled squad may understand the same command but execute it with larger timing, spacing, and movement variance.

A practiced squad converges more tightly on the ideal collective procedure.

This gives individual skill and unit cohesion a common foundation without reducing either to a simple percentage.

## 8. Skill becomes visible

The player should be able to see experience.

A green soldier can look uncertain because the physical execution contains larger corrections and greater deviation.

A practiced soldier settles into position more confidently.

An expert performs the same action with very little wasted movement.

Likewise, a green squad can march awkwardly while an experienced squad moves with tight spacing and synchronized timing.

The UI may expose summaries such as qualification, training hours, or readiness, but the world itself demonstrates the underlying capability.

## 9. Deterministic simulation

Execution variance should be bounded and reproducible under the simulation's deterministic rules.

The same:

- actor state;
- Gesture;
- equipment;
- environment;
- command;
- experience;
- simulation conditions

should produce the same result.

Designed variation can prevent identical actors from looking mechanically cloned while remaining controlled by the simulation.

## 10. Architecture

The resulting architecture is deliberately small:

`Intent → Procedure → Ideal Gesture → Execution Variance → Physical Result`

Skill is therefore not another monolithic simulation system.

The Gesture system already provides the ideal action.

Training changes the actor's ability to reproduce that action.

Experience narrows execution variance.

The physical world determines the consequence.

> **The ideal Gesture defines what perfect execution looks like. Skill is how tightly the actor can reproduce it.**

That means the numbers describe the person.

They do not replace the person.
