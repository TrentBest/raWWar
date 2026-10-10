# Fighter Pilot Action Slice — Station to World Consequence

**Status:** Design contract / next implementation slice  
**Scope:** raWWar only; no changes to Workshop dependencies or APIs  
**Host direction:** AnyApp first; desktop and VR are manifestations of the same semantic procedure

## Purpose

The existing `FighterPilotStation` prototype establishes a narrow readiness sequence: occupy the station, secure the pilot, connect the interface, raise the rig, and accept bounded control intent. It does not yet cause an aircraft to act in the authoritative world.

The next slice must close that gap without pretending the station is a complete flight simulator. It should prove one small causal chain from an authorized physical interaction to a world-state change, with a durable explanation of what happened.

## The causal chain

```text
Station + occupant
    → restraint and interface readiness
    → qualification / aircraft access checks
    → physical rig input
    → bounded semantic flight-control request
    → aircraft capability + current world conditions
    → validated state transition
    → event/history record
    → observer-visible feedback
```

The station is not the aircraft, the rig is not a keyboard mapping, and the rendered motion is not authoritative state. A successful input is a request to perform a domain action; it is not proof that the requested motion occurred.

## Minimum vertical slice

Implement one deliberately small outcome first:

1. A pilot occupies, secures, connects to, and raises a powered, undamaged station rig.
2. The occupant must have the required pilot qualification and access to the assigned aircraft.
3. A bounded input produces a semantic control request. Desktop and VR inputs must converge on the same request shape; device-specific gestures remain outside the domain rule.
4. The aircraft-side rule validates the request against current aircraft state and prerequisites. The first outcome may be a limited, explicit change to a candidate flight-control state (for example, a commanded control-surface target), **not** an unvalidated claim that a complete flight model exists.
5. Acceptance or rejection produces a domain result with a reason. An accepted result updates authoritative state and emits a causally linked event; a rejected result must not mutate aircraft state.
6. The result can be queried without a camera or render loop and can be shown to the player as feedback.

The exact axis mapping, control limits, aircraft capability model, and flight-control law remain **Candidate** until grounded in the existing raWWar vehicle data and resolved by the creator where they express game design rather than implementation detail.

## Required contracts

### Request

A request needs enough identity to reject stale or misdirected control:

- pilot identity;
- station identity;
- assigned aircraft identity;
- control domain and bounded semantic values;
- logical time or domain-defined time key;
- a stable request/causal identity suitable for deduplication when the event-history contract supports it.

Do not trust the caller to assert qualification, station readiness, aircraft availability, or successful execution. Resolve these against authoritative domain state.

### Result

A result must distinguish at least:

- accepted and applied;
- rejected because the station is not ready;
- rejected because qualification/access is missing;
- rejected because the aircraft is unavailable or incompatible;
- rejected because input is outside the declared bounds;
- rejected because the request is stale, duplicated with conflicting content, or otherwise invalid.

Use existing repository types and conventions where available. Do not create a parallel generic interaction framework.

### State and history

The first implementation must state exactly which aircraft field changes and under what preconditions. A successful transition should record the initiating pilot, station, aircraft, logical time, request identity, accepted semantic command, and resulting state/event identity. A rejection should be inspectable too, but must not masquerade as a committed world-state mutation.

The current in-memory event ledger is a reference implementation, not durable restart-safe storage. Do not claim crash-safe or distributed exactly-once behavior until the storage contract actually provides it.

### Interruption and release

The securing procedure must eventually record partial progress and interruption. Emergency release should return the station to a safe, explicit state and revoke its ability to submit further commands. Do not silently complete a partially interrupted procedure or erase its history.

## Contract checks

Add executable checks alongside implementation for:

- valid, qualified pilot controlling the assigned aircraft;
- no control before the rig is raised and connected;
- no control after emergency release or loss of power;
- damaged/unavailable aircraft rejects commands without state mutation;
- invalid and out-of-range values reject deterministically;
- a successful request changes only the declared authoritative field(s);
- rejected requests leave aircraft state unchanged;
- duplicate retry does not apply the same state transition twice within the supported history contract;
- desktop and VR adapters produce equivalent semantic requests for equivalent physical intent;
- event/history data identifies the cause and outcome;
- query results are independent of camera/render order.

Build and run the actual suite after each implementation step. A checked-in design document or passing unit contract does not prove that AnyApp loads the Experience end to end.

## Explicit non-goals for this slice

- a complete aerodynamic model or flight-control law;
- a generic cockpit/interaction framework;
- rendered hands, animations, sound, or visual rig construction;
- a new API in FSM_COS, FSM_UserIO, AnyApp, or another repository;
- persistent/distributed event storage beyond verified existing capability;
- a claim that raWWar is already playable.

## Completion criterion

The slice is complete when one valid pilot input produces one bounded, testable, authoritative aircraft-state transition with an inspectable cause/outcome record, invalid requests do not mutate the world, and the semantic procedure is independent of desktop/VR presentation. AnyApp end-to-end hosting remains a separate milestone.
