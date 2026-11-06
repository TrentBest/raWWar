<!-- The Singularity Workshop moniker -->
<p align="center">
  <a href="https://github.com/TrentBest">
    <img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated workshop moniker" width="720" />
  </a>
</p>

# Formation Coordinator: Pure Planning and Lifecycle Contract

**Status:** Candidate domain contract with a first executable, host-independent coordinator slice on `development`. This is organizational state and a test seam, not an implemented movement or physical-observation integration.

> **The coordinator decides who should occupy which slot and what organizational transition is pending. It never decides that a soldier has physically arrived.**

## Readiness reporting

The executable coordinator exposes a deterministic `FormationReadiness` snapshot: total capacity, assigned members, occupied/settled, approaching, blocked, releasing, interrupted, unavailable, clear, available slots, and approach/release reservations. `IsFormed` is true only when at least one member is assigned and every assigned member is settled; unassigned capacity does not prevent a partially filled unit from being formed. These counts describe coordinator state only and do not estimate simulation or presentation population.

## Implementation status — first executable coordinator slice

The development branch now contains:

- `FormationSlotPlanner`: deterministic rear-most-rank-first, left-to-right assignments.
- `FormationCoordinator`: immutable, host-independent member/slot lifecycle bookkeeping.
- Executable checks in `FormationSlotPlannerChecks.cs` and `FormationCoordinatorChecks.cs`.

The coordinator issues a Fall In assignment as `ReservedForApproach`; command issuance alone never occupies a slot. A member becomes `Occupied/Settled` only after the host supplies an observation matching that member's current order revision and confirming both slot position and facing. Blocked approach remains explicit and can be retried with a new per-member revision. Approach and release can be interrupted and resumed; interruption advances the member revision while preserving its slot reservation, and resume advances it again. Thus pre-interruption observations cannot complete a resumed order. An approaching or blocked member can be marked unavailable with a retained reason; this cannot erase an already occupied or releasing physical state.

Fall Out changes an occupied slot to `ReservedForRelease` and requires a named egress target. The slot becomes `Available` only after an explicit physical-clearance observation matching the current member revision. Stale observations are rejected. These DTOs are a host-neutral test seam; they do not validate the source or quality of physical evidence and do not implement locomotion, pathfinding, collision, or a Gesture/Physics adapter.

The solution build, local package-content verification, executable contract checks, event-frame reference verification, and checked-in usage examples passed on commit `c125f90a8a7cdeb09faf6fd5cf2b1d6217d08878`. Interruption/resume checks are the next CI validation.

## 1. Scope

The first coordinator should be a deterministic, host-independent component. It owns:

- formation membership for one formation;
- stable slot identity and slot assignment;
- deterministic Fall In ordering;
- reservation of approach and egress intent;
- organizational lifecycle and readiness;
- interpretation of explicit physical observations supplied by its host.

It does not own:

- pathfinding, locomotion, steering, pose interpolation, or collision response;
- authoritative world transforms or physical clearance;
- Gesture evaluation or Physics prediction;
- camera coordinates, rendering, or visual LOD;
- squad/platoon hierarchy in the first vertical slice.

The first proof should use one soldier and one slot. A four-person two-by-two squad is the next coordinator scale test, not a prerequisite for proving the lifecycle.

## 2. Identity and frame rules

Every member and slot has a stable caller-supplied identity. Identity must not be derived from list position, object hash, camera orientation, or a transient display index.

Each formation definition names its own local frame. Slot coordinates and formation-relative targets are expressed in that frame. The contract does not choose raWWar's universal world axes, handedness, units, or frame graph.

A slot definition contains at least:

- stable slot ID;
- rank index, where rank zero is the rear-most rank for this drill;
- lateral index within the rank, ordered left-to-right relative to formation facing;
- formation-local target position and facing;
- actor/profile compatibility requirements supplied by the formation definition.

Slot spacing and actor dimensions are data, not renderer constants or universal soldier assumptions.

## 3. Deterministic Fall In plan

For the initial rectangular drill, order slots by:

1. rear-most rank first;
2. within that rank, lateral index from left to right;
3. then the next rank forward, again left to right.

The plan is an ordered sequence of assignments, not a movement trajectory. For each eligible member, it selects the next compatible available slot and emits an approach intent in the formation-local frame.

The plan must be reproducible for identical formation definition, member set, compatibility data, and order revision. Inputs must be normalized with an explicit stable ordering before assignment. It must not depend on dictionary enumeration, wall-clock time, random values, or presentation state.

If a member is unavailable or a slot is blocked/incompatible, the planner reports the reason and continues only according to a declared policy. It must not silently skip a required member and then report the formation ready.

## 4. Slot and member state are separate

Do not compress the whole lifecycle into one boolean or use a single enum to describe both a person and a slot.

Candidate slot states:

| Slot state | Meaning |
|---|---|
| Available | No member owns or reserves this slot |
| ReservedForApproach | A member has been assigned; physical occupancy is not established |
| Occupied | An accepted physical observation confirms the member is within the slot tolerances |
| ReservedForRelease | Release is ordered; the slot remains unavailable while the member clears it |
| Blocked | The slot cannot currently be approached or safely used |
| Unavailable | The definition or required capability makes this slot unusable for this plan |

Candidate member activity states:

| Member state | Meaning |
|---|---|
| AwaitingAssignment | Eligible but not yet assigned |
| Approaching | Has an active approach assignment |
| Settled | Physical observation confirms the assigned slot |
| Releasing | Has an active release/egress assignment |
| Clear | Physical observation confirms the member has cleared the slot |
| Unavailable | Cannot currently participate; reason is retained |
| Interrupted | An approach or release order is paused; the existing slot reservation is retained |

The exact implementation may use value objects or a smaller state representation, but it must preserve these distinctions.

## 5. Commands are not observations

Coordinator outputs are requests/intents:

- assign member to slot;
- request approach target;
- request release through an egress target;
- report blocked or unavailable work;
- report organizational readiness.

They are not evidence that movement occurred.

The host later supplies observations with enough identity and provenance to validate that they refer to the current assignment. At minimum, an observation needs member identity, assignment/order revision, logical observation time, the relevant frame identity, and an explicit outcome. Position/facing measurements and tolerances must be interpreted under a separately validated host/Physics contract.

The coordinator may transition ReservedForApproach to Occupied only after an accepted achieved-state observation confirms the required tolerances. A predicted contact, requested target, Gesture pose, elapsed timeout, or visual placement is insufficient.

Similarly, ReservedForRelease remains reserved until an accepted achieved-state/clearance observation confirms the member has physically cleared the slot and egress condition. Issuing Fall Out never frees the slot.

The precise observation DTO and adapter are deliberately not frozen here. Gestures #33 and the Physics motion-driven pipeline are still establishing the motion semantics and evidence boundary.

## 6. Interruption, retries, and stale observations

Every formation order has a monotonically increasing revision (or equivalent stable generation identity). Assignment intents and observations are correlated to that revision.

When an order is interrupted or superseded:

- do not erase the last accepted physical observation;
- do not teleport members to their prior or newly assigned slots;
- do not free a slot solely because its command was cancelled;
- preserve any physical occupancy or in-progress clearance until new evidence resolves it;
- reject stale observations that refer to superseded assignments, while retaining them for diagnostics if useful;
- make retry/replan decisions explicit and deterministic.

A blocked approach leaves the slot reserved or explicitly transitions it to a blocked/recovery state according to policy; it cannot be counted as occupied. A blocked release cannot free the slot. An interrupted approach or release retains its current reservation; resuming it issues a new member revision, invalidating observations from the interrupted order.

## 7. Readiness is derived

For the first slice, Formed means every required member has an accepted settled observation for its assigned slot and every required slot is occupied by its assigned member. Optional members and optional slots must be declared as such in the formation definition; do not infer optionality from failure.

Released means every released member has an accepted clear observation and no slot or egress reservation remains active.

Readiness should expose counts and reasons, not just a boolean: required, assigned, approaching, settled, blocked, unavailable, releasing, and clear. Parent formation readiness must eventually include child placement relative to the parent; child readiness alone will not suffice.

## 8. Minimal deterministic test matrix

The pure coordinator can be tested without pretending to validate locomotion or collision physics.

| Case | Required assertion |
|---|---|
| One member / one slot Fall In | Assignment is stable; command alone does not occupy |
| Accepted achieved observation | Slot becomes occupied only after matching revision and tolerance result |
| Rear-to-front two-by-two plan | Stable rear rank, left-to-right ordering, then forward rank |
| Reordered input collections | Same plan and assignments |
| Duplicate member or slot identity | Explicit rejection |
| Unavailable member / blocked slot | Reason retained; readiness remains false |
| Stale approach observation | Cannot occupy a superseded assignment |
| Fall Out command | Slot remains reserved |
| Accepted clearance observation | Slot becomes available only after confirmed clearance |
| Interrupted approach/release | No silent teleport or premature slot release |

These tests prove deterministic planning and lifecycle bookkeeping only. They do not prove a working Gesture adapter, path planner, physical solver, or large-population simulation.

## 9. Cross-repository gate

- **raWWar:** formation membership, slot identity, assignment, order, readiness, and lifecycle.
- **Gestures:** individual motion definitions and time-dependent motion description.
- **Host adapter:** translates motion intent into physical requests and maps observations back to the coordinator's host-neutral seam.
- **Physics:** independently evaluates physical consequences and reports achieved state/contact/clearance evidence.
- **Visualize / Renderer / GUI:** present state; they do not authoritatively commit it.

Do not add a direct raWWar-to-Gestures or raWWar-to-Physics dependency merely to make the first coordinator test compile. The adapter seam remains host-owned until the motion contract is stable.

Related work: [Formation Assembly and Release](FORMATION_ASSEMBLY_AND_RELEASE.md), [Coordinate Frames and Transform Contracts](COORDINATE_FRAME_CONTRACT.md), [Gestures issue #33](https://github.com/TrentBest/TheSingularityWorkshop.Gestures/issues/33), and the Physics [motion-driven interaction pipeline](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/MOTION_DRIVEN_INTERACTION_PIPELINE.md).

---

<p align="center">
  <img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="280" />
</p>
