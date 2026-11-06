<!-- The Singularity Workshop moniker -->
<p align="center">
  <a href="https://github.com/TrentBest">
    <img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated workshop moniker" width="720" />
  </a>
</p>

# Formation Assembly, Drill-Field Presentation, and Fall Out

**Status:** Candidate Experience design and implementation brief. This document records the direction for the first visible formation proof; it does not claim that the formation coordinator, movement, or scene is implemented.

> **A formation is not a group of soldiers placed near one another. It is an organization that assembles, occupies known positions, acts together, and can safely release those positions.**

## 1. The first scene: a flat drill field

The first presentation should be deliberately simple:

- a broad, flat, unobstructed drill field;
- an elevated isometric-feeling or orthographic camera;
- one clearly marked formation footprint;
- a staging lane behind the formation;
- a visible approach path into the footprint;
- a designated egress lane or lanes for falling out.

The camera is a presentation choice for this formation demonstration and commander-facing overview. It does **not** change raWWar's first-person player identity or imply that the entire Experience becomes an isometric game.

The field should make spatial relationships legible before adding terrain, combat, buildings, vehicles, or environmental complications. Those can become later validation cases.

## 2. Formation geometry and stable slots

A formation is defined relative to its own facing, not the global screen axes.

The coordinator needs a formation frame with:

- a forward direction;
- a right/left direction;
- a rear direction;
- a footprint and spacing rules;
- stable slot identities;
- an approach/staging region;
- one or more release/egress regions.

A slot is a place in the formation plan, not a soldier transform copied into a visual object. Slot identity should remain stable when the scene camera moves or the formation rotates.

For the first drill, use a simple rectangular line formation. The exact spacing and soldier dimensions should be data supplied by the relevant actor/profile and formation definition, not invented as universal constants in the renderer.

## 3. Fall In: fill the formation from rear to front

The first visible assembly sequence should be easy to read and deterministic.

1. Soldiers gather in a single-file staging queue behind the formation.
2. The first soldier approaches the rear-most rank.
3. The soldier moves laterally to the first available position in that rank, decelerates, turns to the formation facing, and settles.
4. The next soldier fills the next position across that same rear rank.
5. Once that rank is occupied, the queue advances to the next rank forward.
6. The sequence continues until the required slots are occupied or the order is interrupted.

For a four-person squad in a two-by-two drill, the illustrative order is:

```text
                 FRONT / DIRECTION OF FACING
                              ↑

             [ 3 ] [ 4 ]       ← second rank, filled second
             [ 1 ] [ 2 ]       ← rear rank, filled first
                  ↑
             approach lane
                  ↑
             single-file queue
```

The numbers illustrate assignment order only. The actual queue may contain people with different equipment, movement capability, or readiness.

The important visual beats are approach, lateral alignment, deceleration, orientation correction, settling, and confirmed occupancy. Do not snap a soldier directly into a slot and call the formation assembled.

A slot becomes occupied only when authoritative movement/position state confirms that the soldier is within the required position and facing tolerances. A blocked approach, unavailable soldier, or unfilled slot remains visible as such; it must not be silently counted as ready.

## 4. Fall Out: release positions through physical movement

Fall Out is a real organizational and physical transition, not deletion, despawning, or making the soldiers disappear.

The release procedure should:

1. accept a fall-out order and enter a releasing state;
2. select a safe release order for the current formation geometry;
3. reserve the required egress path/lane;
4. have each soldier leave their slot and move clear of the formation footprint;
5. mark the slot free only after authoritative state confirms that the soldier has physically cleared it;
6. route the soldier to the next assigned activity, staging area, or idle location.

For the first rectangular drill, a simple sequence can release the rear-most rank first through a designated flank or rear egress lane. This is an initial choreography, not a universal rule: the safest release order depends on the formation shape, obstacles, neighboring units, and destination.

A soldier who is still moving out continues to occupy or reserve the relevant space. Another soldier must not be assigned to that slot merely because the release command was issued.

## 5. Formation lifecycle and readiness

The visual sequence should correspond to explicit organizational states. Candidate lifecycle:

```text
Unformed
   ↓ Fall In order
Assembling → Formed → Operating
   ↑             ↓
   └── Re-form   Fall Out order
                    ↓
                 Releasing
                    ↓
                  Unformed
```

The coordinator owns membership, slot assignment, assembly/release order, and lifecycle. It should report separate facts rather than one misleading boolean:

- required members;
- assigned members;
- members approaching;
- members settled in slots;
- blocked or unavailable members;
- members releasing;
- slots physically clear;
- formation readiness.

A formation is ready only when its declared readiness conditions are satisfied. Issuing an order is not proof of completion.

Orders may be interrupted or superseded. The design must preserve the current physical truth when that happens: do not reset a soldier to a previous slot or free a slot that remains physically occupied.

## 6. Scale the same grammar recursively

The same idea should work at multiple organizational levels.

- **Soldier:** approaches and occupies an individual slot.
- **Squad:** coordinates its members and reports its own readiness.
- **Platoon:** assigns stable squad-level positions; each squad assembles internally.
- **Larger formations:** coordinate subordinate formations without requiring the parent to micromanage every individual pose.

A parent formation should not report ready merely because its child formations report ready. The parent must also validate the placement, spacing, facing, and readiness of those children relative to the parent formation.

This gives the player a comprehensible progression: watch four people form a squad, then watch squads take their positions in a platoon. The same visual grammar remains recognizable as numbers grow.

## 7. What the player should see

The scene should communicate state without requiring a wall of text.

| State | Visual language |
|---|---|
| Available slot | Subtle ground marker or outline |
| Assigned / approaching | Slot marker reserved; approach cue visible |
| Settling | Soldier slows and makes small alignment corrections |
| Occupied / confirmed | Marker resolves into a quiet occupied state |
| Blocked / unavailable | Distinct warning marker and reason in detail view |
| Releasing | Egress cue and visible movement out of the slot |
| Clear | Slot marker returns to available |

At overview distance, show formation footprint, rank/slot occupancy, movement flow, and readiness. At closer inspection, allow a drill-down to individual soldiers and their current physical state. Do not require every soldier to receive full pose/joint evaluation merely to show that a large formation is assembling.

Keep population counts semantically separate: commanded, materialized, actively simulated, Gesture-evaluated, and currently presented are different quantities. An overview may represent many soldiers compactly, but it must not imply that every represented person is being fully simulated and rendered.

## 8. Ownership boundaries

This is an Experience-level behavior composed from reusable capabilities.

| Concern | Owner |
|---|---|
| Formation membership, stable slots, assembly/release order, readiness | raWWar formation/command domain |
| Desired slot assignment, staging/egress intent, and formation-relative targets | raWWar formation/command domain |
| Individual motion procedure and time-dependent motion description | Gestures |
| Physical prediction, collision/contact, achieved state, and clearance evidence | Physics |
| Translation between motion descriptions and physical simulation inputs/observations | Explicit adapter / host integration |
| Isometric/orthographic scene, markers, instancing, visual LOD | Visualize |
| Observer-relative detail selection | Renderer |
| Player-facing controls and command presentation | GUI / host presentation |

Gestures does not own formation membership or decide when the squad is ready. Physics does not own the squad's organizational structure or slot assignment. Visualize does not decide whether a slot is physically occupied. Renderer does not own world state. GUI does not become the formation simulation.

The intended physical interaction boundary is important: a Gesture motion description is not automatically an achieved physical trajectory. Physics evaluates/predicts physical consequences and reports achieved state; an explicit host-side adapter connects the two. The proposed contract is documented in the Physics [motion-driven interaction pipeline](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/MOTION_DRIVEN_INTERACTION_PIPELINE.md) and coordinated with Gestures [issue #33](https://github.com/TrentBest/TheSingularityWorkshop.Gestures/issues/33). This remains a research/design boundary, not an implemented cross-package integration. Physics itself must not depend on Gestures.

## 9. First executable proof

Build the smallest inspectable lifecycle before attempting large armies:

1. one soldier with a stable identity;
2. one formation with one defined slot in a formation-local frame;
3. one staging point and one formation-issued approach target;
4. a narrow adapter contract through which a Gesture motion description can be supplied to physical simulation;
5. a coordinator contract test using a deterministic motion/physics test double, clearly labeled as a coordinator test rather than a physical-motion proof;
6. authoritative Physics/world observations before the slot is marked occupied;
7. a Fall Out order with a reserved egress target;
8. authoritative confirmation that the soldier has physically cleared the slot before it is released.

Then expand the coordinator to four soldiers and a two-by-two squad. Only after the individual movement/physical interaction pipeline is demonstrated should the work expand to multiple squads and platoon assembly.

The first success criterion is not a crowd screenshot. It is a repeatable, inspectable lifecycle in which membership, physical position, visual state, and readiness agree.

## 10. Explicit non-claims and open design work

This document is a candidate design and implementation brief. It does not establish that raWWar currently has:

- a formation coordinator or formation-slot runtime;
- a soldier navigation or locomotion implementation;
- authoritative collision/clearance integration for formation assembly;
- an isometric drill-field scene;
- a squad/platoon command hierarchy;
- large-population simulation or rendering performance.

Before implementation, the formation domain should be reconciled with the existing soldier, Gesture motion, Physics interaction, command, and MicroBundle contracts. The Physics [motion-driven interaction pipeline](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/MOTION_DRIVEN_INTERACTION_PIPELINE.md), paired with Gestures [issue #33](https://github.com/TrentBest/TheSingularityWorkshop.Gestures/issues/33), describes the proposed host-side handoff: Gestures supplies a versioned motion description; the host adapter translates it; Physics evaluates physical consequences and reports authoritative observations. This is still a research/design proposal, not an implemented integration. Physics remains independent of Gestures. In particular, slot dimensions, spacing, approach target generation, interruption behavior, blocked-slot recovery, and safe release ordering need executable contracts and tests. The first coordinator test must not masquerade as proof of a working locomotion or collision pipeline.

Coordinator contract: [Formation Coordinator: Pure Planning and Lifecycle Contract](FORMATION_COORDINATOR_CONTRACT.md) defines deterministic slot planning, separate member/slot states, command-versus-observation semantics, revision-safe interruption handling, and the minimum coordinator test matrix. It remains a candidate contract, not an implemented API.

Related design: [Unit Formation, Training, Drilling, and Cohesion](UNIT_FORMATION_TRAINING_AND_COHESION.md), [Gestures Design](GESTURES_DESIGN.md), [Simulation Scale and Memory](SIMULATION_SCALE_AND_MEMORY.md), and [Renderer Integration](RENDERER_INTEGRATION.md).

---

<p align="center">
  <img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="280" />
</p>
