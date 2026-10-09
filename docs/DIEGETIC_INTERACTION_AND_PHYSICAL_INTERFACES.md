# Diegetic Interaction and Physical Interfaces

The world is the interface. Buttons, levers, displays, instruments, and vehicle controls are physical objects with state, access rules, dependencies, damage modes, and repair procedures.

Desktop and VR must operate the same underlying world objects. A screen can be stale, damaged, disconnected, or unpowered. A lever can jam. A projectile resolves against local construction and may damage a faceplate, control board, cable, power feed, or connected system depending on the modeled impact. Damage creates diagnosis, isolation, repair, calibration, and acceptance work.

Implementation sequence: button and lever state transitions; mounted screen with power/network/staleness; damage-created work order; reuse in vehicle and mech cockpits; desktop and VR mappings to the same semantic actions.

## Implementation status

**The first narrow executable station slice now exists in source and contract checks; end-to-end interaction remains unimplemented.** `src/raWWar/Interaction/FighterPilotStation.cs` models one fighter-pilot station's immutable occupancy, restraint, interface, rig, qualification, power, and control-integrity state. The executable contract suite exercises the intended transition order, blocked actions, bounded input, desktop/VR semantic parity, and emergency release. The current code does not contain a generic `PhysicalControlResolver`, and no Workshop physical-interaction API is assumed.

CI must be checked after the current changes before the slice is reported as verified. Source presence and test assertions are not themselves proof that the build passed.

## First executable slice

The first raWWar-facing slice is deliberately a **fighter-pilot station**, not a reusable interaction framework. The sequence is seat occupant → secure restraints → connect interface → raise control rig → accept bounded pilot intent. The station checks occupant identity and qualification, physical readiness, power, and control integrity. Desktop and VR are two input sources for the same semantic pilot request; neither moves the aircraft directly.

A successful or blocked attempt returns a structured result and before/after station states. Durable event storage and replay are still outstanding; a returned result is not yet a claim that a persistent event has been recorded. The Workshop remains the intended owner of reusable physical-interaction, actor-performance, event-history, and presentation capabilities.

Required proofs for the first slice:
- [x] A valid transition changes only the intended immutable station state.
- [x] Missing occupant, wrong occupant, missing qualification, unsecured restraints, disconnected interface, unraised rig, unpowered station, damaged controls, and invalid control values have explicit blocked outcomes.
- [x] Desktop and VR input produce the same bounded pilot intent from the same initial state.
- [x] Emergency release clears occupancy, restraints, interface connection, and rig state without requiring station power.
- [ ] Interrupted/partial securing and connection procedures produce durable event history.
- [ ] Actual aircraft control laws consume the request and resolve movement against authoritative vehicle state.
- [ ] AnyApp hosts the station interaction and presents the physical rig/hands/grips.

**Scope boundary:** this prototype proves only a narrow domain contract. It does not prove a finished FPS/VR controller, visible hand animation, physical restraint forces, aircraft movement, damage propagation, durable event storage, or a functioning GPU renderer.