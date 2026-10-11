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

## Fuzzy motion: parameterize the law, not the finished animation

Each soldier has a stable seed and individual state/statistics. The ideal movement is expressed as a parameterized procedure. The seed selects stable individual values from safe ranges around the ideal parameters; it must not introduce fresh random noise every frame.

This distinction is central. We do not independently distort finished animation frames. We parameterize the mathematical law that governs transitions between the ideal poses, then evaluate that law for each individual.

The initial teaching example is experience:

- **Green soldiers** use wider permitted ranges for relevant motion parameters. Their march follows the same ideal procedure, but timing, stride, arm swing, body response, and coordination can diverge more noticeably.
- **Veteran soldiers** use tighter ranges around the ideal. They look more consistent and coordinated, yet remain individually distinct rather than becoming identical copies.
- **Experience changes the distribution, not the identity of the Gesture.** A veteran and a green soldier can execute the same march definition with different parameter distributions.

The range should be specific to each parameter family. It is not safe to blindly multiply every parameter by one skill scalar: mass-like terms, stiffness, damping, stride, cadence, and timing have different units and constraints. Some parameters should be varied additively; positive quantities may need multiplicative or transformed sampling. Correlated parameters may need to be sampled together to avoid physically incoherent combinations.

Possible variations include:

- slightly different movement rate or phase;
- stride and arm-swing amplitude;
- timing and coordination between body parts;
- effective inertia, stiffness, and damping;
- torso sway and posture;
- head direction and reaction timing;
- hesitation, recovery, and occasional correction when the domain state calls for it.

The goal is not arbitrary imperfection. It is a believable, bounded distribution around an ideal physical procedure.

## The motion equation deserves its own exploration

The motion model began conceptually with Newton's second law, \(\mathbf{F}=m\mathbf{a}\), then explored how spring-like response and mass-dependent ease-in/ease-out could describe the way a body moves between desired poses.

A useful candidate family is a driven mass-spring-damper:

\[
m\ddot{x}=F_{\text{drive}}(t)-c\dot{x}-k\bigl(x-x_{\text{target}}(t)\bigr)
\]

Here, effective mass supplies inertia; drive represents the intended movement; damping resists velocity; stiffness pulls the motion toward the changing target path. Acceleration and deceleration can emerge from the dynamic response instead of being supplied solely by a hand-authored easing curve.

This equation is a starting point for exploration, not yet the canonical raWWar or Gestures implementation. Translation and rotation have different physical quantities, and a pose path may need direct interpolation, dynamic response, or an explicit combination. Numerical integration, timestep policy, stable parameter bounds, and deterministic evaluation need to be tested before a public contract is frozen.

The standalone [Gestures motion-equation exploration](https://github.com/TrentBest/TheSingularityWorkshop.Gestures/blob/development/docs/MOTION_EQUATION_EXPLORATION.md) documents the candidate model, green-versus-veteran parameter ranges, determinism requirements, and open experiments.

## Determinism

Fuzziness should be derived from stable identity and relevant state rather than uncontrolled randomness.

A soldier's seed can produce repeatable variation for a given context while allowing the same soldier to behave differently when the context changes. The procedure instance, formation slot, equipment, load, fatigue, or terrain may be mixed into the seed/context when that distinction is meaningful.

Parameters should be sampled once per defined identity/context/procedure, or otherwise at a declared deterministic boundary—not re-sampled on each frame. This gives repeatable individuality rather than temporal jitter.

The exact seed inputs, parameter distributions, and temporal model remain to be specified and validated.

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
- Which transitions use direct interpolation versus a dynamic spring/damper response?
- How are pose chains authored and validated?
- How are attachment points represented?
- How are collisions and physical constraints handled during a Gesture?
- How are interruptions represented?
- How are injuries, equipment failures, fatigue, and environmental obstructions incorporated?
- How are Gesture Providers discovered through MicroBundles?
- How much Gesture evaluation occurs on CPU versus GPU?
- What exact texture/state layout is used?
- How does the Renderer transition between event-horizon fidelity levels without visible popping?
- How are deterministic seeds combined with temporary state?
- Which numerical integration and timestep rules produce stable, testable results?

## Visualize and the reusable Gestures package

The Workshop's Gesture model must not be reduced to a conventional baked animation clip. A Gesture is a physical procedure described by poses, mathematical transitions, progression/completion behavior, spatial anchors or constraints, and bounded individual variation. The capability that understands an interaction—such as a vehicle—may provide the relevant Gesture.

The standalone [TheSingularityWorkshop.Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures) repository is now established. Its initial .NET 8 alpha foundation provides normalized pose keyframes, provider-owned semantic anchors, engine-neutral transforms, a deterministic CPU reference evaluator, and bounded per-identity variation. It intentionally has no dependency on Visualize, Renderer, WPF, Unity, or any GPU API. Its first pull request is open for review; its public API and data layout are not yet frozen.

The [TheSingularityWorkshop.Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) package is the GPU-facing presentation layer. It consumes evaluated pose/instance data and executes the representation selected by Renderer. It does not define what a Gesture means, decide whether an interaction is valid, or own authoritative FSM transitions. GPU results remain disposable presentation data.

The current four-channel 8-bit state-texture concept is only one possible GPU layout. The canonical pose schema and data layout must be selected from actual Gesture math, equipment attachment needs, precision constraints, and measurements—not frozen before those requirements are understood.
