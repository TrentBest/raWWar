# Diegetic Interaction and Physical Interfaces

The world is the interface. Buttons, levers, displays, instruments, and vehicle controls are physical objects with state, access rules, dependencies, damage modes, and repair procedures.

Desktop and VR must operate the same underlying world objects. A screen can be stale, damaged, disconnected, or unpowered. A lever can jam. A projectile resolves against local construction and may damage a faceplate, control board, cable, power feed, or connected system depending on the modeled impact. Damage creates diagnosis, isolation, repair, calibration, and acceptance work.

Implementation sequence: button and lever state transitions; mounted screen with power/network/staleness; damage-created work order; reuse in vehicle and mech cockpits; desktop and VR mappings to the same semantic actions.

## First executable slice

The first input-independent control transition is implemented in [`PhysicalControlResolver.cs`](../src/raWWar/Interaction/PhysicalControlResolver.cs). It is intentionally small and renderer-agnostic: keyboard, mouse, controller, VR-tracked hand, and NPC input sources can submit the same semantic command against the same stable control identity. The transition checks power requirements, authorization, interlock state, integrity, and jamming; it returns the new authoritative control state, an auditable event, and the actor-motion cue that a presentation adapter can animate.

The executable contract tests compare keyboard and VR commands against the same starting state and verify that open interlocks and destroyed controls reject input without silently moving the world control.

**Scope boundary:** this proves the shared semantic-action seam, not a finished FPS/VR controller, physical animation, projectile damage model, mech locomotion controller, or GPU renderer. Those must be integrated and profiled separately. In particular, the actor-motion cue is a request for the presentation layer; it is not proof that a visible hand animation has rendered.
