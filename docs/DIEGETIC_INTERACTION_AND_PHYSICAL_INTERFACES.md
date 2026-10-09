# Diegetic Interaction and Physical Interfaces

The world is the interface. Buttons, levers, displays, instruments, and vehicle controls are physical objects with state, access rules, dependencies, damage modes, and repair procedures.

Desktop and VR must operate the same underlying world objects. A screen can be stale, damaged, disconnected, or unpowered. A lever can jam. A projectile resolves against local construction and may damage a faceplate, control board, cable, power feed, or connected system depending on the modeled impact. Damage creates diagnosis, isolation, repair, calibration, and acceptance work.

Implementation sequence: button and lever state transitions; mounted screen with power/network/staleness; damage-created work order; reuse in vehicle and mech cockpits; desktop and VR mappings to the same semantic actions.

## Implementation status

**The interaction model is currently design-level, not an implemented executable slice.** The current `src/raWWar` tree contains the raWWar MicroBundle scaffold and spatiotemporal types; it does not contain the previously referenced `Interaction/PhysicalControlResolver.cs`. The contract-test suite does not currently prove keyboard/VR equivalence for physical controls. Earlier wording that described that resolver and those tests as implemented was inaccurate and has been corrected.

## First executable slice

The first useful raWWar-facing slice should be one stable world-control identity with an explicit state and a semantic action. Resolve the action against the same authoritative control state regardless of whether the intent came from desktop input, a controller, VR-tracked hand, or an NPC. The resolver must check the actual control's power, authority, interlocks, integrity, and jammed/usable condition before changing state.

A successful or blocked attempt should return a structured outcome that the Experience can later connect to Workshop event-history, physical-interaction, and presentation capabilities. Do not implement a parallel generic interaction framework in raWWar if an appropriate Workshop capability becomes available. Until that capability and its API are verified, keep any raWWar-owned prototype narrow, domain-focused, and explicitly temporary.

Required proofs for the first slice:
- A valid action changes only the intended control state.
- Unpowered, unauthorized, interlocked, damaged, or jammed controls do not silently operate.
- A blocked attempt explains the blocking condition without granting hidden knowledge to the actor.
- Desktop and VR input express the same semantic action and produce the same authoritative result from the same initial state.
- An action attempt and its result can be represented as durable domain evidence; a presentation animation is not the authoritative result.

**Scope boundary:** this slice would prove only a shared semantic-action seam. It would not prove a finished FPS/VR controller, visible hand animation, projectile damage model, mech locomotion controller, or GPU renderer.