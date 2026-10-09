# raWWar — Mech Cockpit and Pilot Interaction

> **The player boards a machine, learns its systems, and operates the actual equipment represented in the world.**

Status: Candidate interaction design. This describes the intended experience, not a claim that the runtime is already implemented.

## Boarding is a sequence, not a camera toggle

The player approaches a mech, inspects its condition and access state, opens the hatch, boards, secures the seat and restraints, connects the pilot interface, closes and seals the hatch, powers up, runs a self-test, checks the system panels, verifies control response, and only then releases safeties.

The machine has a physical cockpit: seat and restraints, hatch and seal, displays, status lamps, switches, guarded controls, levers, grips, pedals, communications, environmental controls, maintenance access, manual isolation controls, and emergency egress. Those are objects in the same world as the player, not a separate menu that pretends to be a cockpit.

## One physical control, one semantic action

Every control maps to a semantic action against the authoritative world object. Desktop mouse and keyboard, VR hand/controller, and accessibility inputs can invoke the same action. The input method changes; the world rules do not.

A button press is a request, not proof of success. Power, connectivity, interlocks, authorization, physical reach, damage, and mechanism condition determine whether the action is accepted and what happens next. The player receives feedback grounded in actual system state.

## Motion capture is an option, not magic

An instrumented motion-capture harness or control frame is a candidate pilot interface. It needs calibration, a neutral pose, safe motion limits, latency measurement, invalid-signal detection, and a defined safe response if tracking is lost. Manual controls need an explicit precedence and fallback policy.

The harness supplies intent; it does not force the machine to copy the pilot's pose exactly. The control system must still account for actuator limits, joint torque, balance, traction, payload, terrain, damage, and collision. A mech may be unable to complete a commanded gesture even when the pilot performs it perfectly.

## The cockpit can fail in meaningful ways

- A screen surface can be damaged while its data remains available elsewhere.
- A display controller or power feed can fail.
- A networked panel can show stale values after a data-bus fault.
- A sensor can report incorrectly while the display itself remains intact.
- A switch or lever can jam or lose its electrical connection.
- A damaged feedback channel can make a functioning actuator unsafe to command.
- A hatch interlock can prevent normal opening and require a separate emergency procedure.

These are different faults with different symptoms and recovery procedures. Repair should create work: identify the fault, isolate the relevant system, obtain parts and qualified labor, replace or repair, calibrate, test, and record acceptance. Physical impacts resolve against the actual construction and occlusion; no universal hit point instantly disables an entire cockpit.

## Performance does not dictate the design in advance

A cockpit combines moving controls, independently meaningful equipment, displays, and surrounding geometry. Keep components independently addressable where animation, interaction, damage, repair, or replacement needs it. Batch or join static pieces where doing so helps without erasing meaningful boundaries.

Compare separate meshes, material batching, instancing, selectively joined static groups, and GPU-driven submission on actual target hardware. GPU-side processing can greatly reduce per-object CPU overhead, but it is not unlimited: memory capacity, bandwidth, cache behavior, geometry, shaded pixels, overdraw, synchronization, command overhead, uploads, thermal limits, and frame deadlines still matter. VR comfort requires measuring missed refresh deadlines and latency, not just average throughput.

See [Rendering Performance and GPU Budgets](RENDERING_PERFORMANCE_AND_GPU_BUDGETS.md) for the benchmark matrix and reporting contract. See [Mechs, Machines, and the Evolution of Warfare](MECHS_MACHINES_AND_WARFARE_EVOLUTION.md) for machine families and warfare's branching evolution.
