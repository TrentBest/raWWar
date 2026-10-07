# raWWar — Gestures Design

Status: Initial design; emerging from creator architecture.

## Purpose

Gestures define how raWWar expresses physical motion without turning every soldier into a separately authored animation program.

A Gesture is a compact description of how an entity moves through a sequence of poses. The pose sequence is the visible motion; the mathematical motion rule describes how to progress from one pose toward the next.

Gestures are therefore closer to a motion flipbook plus transition mathematics than to a conventional library of baked animations.

## Core idea

An ideal Gesture defines:

- a sequence of poses;
- the mathematical path between poses;
- progression/rate along that path;
- completion conditions;
- required attachment points or spatial relationships.

The same ideal Gesture can be executed by thousands of soldiers. That alone would make the soldiers mechanically identical.

raWWar therefore applies a second layer: **fuzzy individualization**.

## Fuzzy motion

Each soldier has a stable seed and individual state/statistics.

The ideal Gesture is perturbed using that identity so that soldiers can differ while still performing the same intended action.

Possible variations include:

- slightly different movement rate;
- small lateral or positional offsets;
- different stride timing;
- different pose timing;
- head direction;
- posture;
- reaction timing;
- hesitation;
- recovery;
- occasional stumble or correction;
- other statistically bounded imperfections.

The goal is not random noise. The goal is a believable distribution around an ideal military action.

Ten thousand soldiers should therefore perform the same march without appearing to be ten thousand copies of one animation.

## Determinism

Fuzziness should be derived from stable identity and relevant state rather than uncontrolled randomness.

A soldier's seed can produce repeatable variation for a given context while allowing the same soldier to behave differently when the context changes.

The exact seed inputs and temporal model remain to be specified.

## Gesture ownership

Gestures belong to the capability that knows how an interaction should physically occur.

For example, a vehicle MicroBundle can expose a Gesture Provider capable of answering a request such as:

**Get enter cockpit gesture**

The soldier does not need to know the vehicle's internal climbing procedure.

The soldier needs to know:

1. it needs to enter the cockpit;
2. where to move to begin the interaction;
3. that the vehicle provides the appropriate Gesture;
4. when the Gesture is complete.

The vehicle supplies the domain-specific physical procedure.

The same model applies to:

- climbing into a tank;
- entering a hatch;
- boarding an aircraft;
- opening a building entrance;
- operating a machine;
- sitting at a workstation;
- using specialized equipment;
- driving or operating a vehicle.

## Gesture Providers

A Gesture Provider is a capability interface through which an entity exposes physical procedures.

A request should be semantic rather than an animation asset lookup.

Conceptually:

**Need: enter cockpit**
→ locate compatible interaction point
→ query vehicle Gesture Provider
→ receive the appropriate Gesture/FSM
→ move into the required starting position
→ execute the Gesture
→ continue with the resulting state.

This allows the soldier and vehicle to remain independently meaningful.

## FSM relationship

Gestures are FSMs applied to entities.

The FSM controls progression through the physical procedure. The Gesture describes the physical transition represented by that procedure.

A soldier therefore does not contain a giant hard-coded table of every way a soldier can enter every vehicle.

Instead:

- the soldier has capabilities and needs;
- the environment exposes interaction opportunities;
- the relevant object provides the procedure;
- FSM_API governs the state progression;
- the Renderer presents the resulting state.

This is a major example of the raWWar data-driven architecture.

## Movement before the Gesture

A Gesture does not necessarily begin where the soldier currently stands.

A common interaction is:

1. discover a need;
2. query the environment;
3. find the required interaction point;
4. move to that position;
5. transition into the Gesture;
6. execute the physical procedure;
7. emerge in the resulting state.

Walking to the tank is ordinary movement.

Climbing the tank and entering its hatch is a vehicle-provided Gesture.

The boundary matters because the vehicle owns the knowledge of how its own physical interfaces work.

## GPU realization

The long-term Renderer is expected to represent large populations through compact state data.

One candidate representation stores four independent 8-bit state values in each 32-bit pixel:

- channel R: state 0–255;
- channel G: state 0–255;
- channel B: state 0–255;
- channel A: state 0–255.

This permits four compact state channels per pixel while allowing compute shaders to operate only on the channels relevant to a particular operation.

This representation is an implementation direction, not yet a frozen GPU data contract.

Gesture evaluation should exploit this structure rather than requiring one conventional CPU animation object per soldier.

## Event horizons

Gesture fidelity should follow the Renderer event-horizon model.

Nearby soldiers can receive more detailed pose evaluation and richer individual variation.

Farther soldiers can use progressively cheaper representations while preserving the visual properties that matter at that distance.

The objective is not to calculate every joint of every soldier equally. The objective is to make the observed world convincing at the level at which it is observed.

## Design principle

> **The ideal Gesture defines what the soldier is trying to do. The soldier's identity and state determine how that soldier does it.**

The result should be coordinated behavior without mechanical sameness.

## Open questions

- What is the canonical pose representation?
- How is the mathematical transition represented?
- How are pose chains authored and validated?
- How are attachment points represented?
- How are collisions and physical constraints handled during a Gesture?
- How are interruptions represented?
- How are injuries, equipment failures, and environmental obstructions incorporated?
- How are Gesture Providers discovered through MicroBundles?
- How much Gesture evaluation occurs on CPU versus GPU?
- What exact texture/state layout is used?
- How does the Renderer transition between event-horizon fidelity levels without visible popping?
- How are deterministic seeds combined with temporary state?
