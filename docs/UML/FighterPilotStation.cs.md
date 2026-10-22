# FighterPilotStation.cs

**User moniker:** Explicit fighter-pilot station readiness contract

## Purpose

Model one prototype pilot station's occupant, restraints, interface, control rig, power, and control integrity. Accepted transitions return before/after states; pilot intent becomes a bounded request rather than directly moving an aircraft.

## Source and evidence

- Implementation and related contracts: [FighterPilotStation.cs](../../src/raWWar/Interaction/FighterPilotStation.cs)
- Dedicated diagram: [FighterPilotStationUML.md](FighterPilotStationUML.md)

## Class card

| Type | Contract |
|---|---|
| `FighterPilotStation` | Immutable-style record with explicit seating, securing, connection, rig, input, and emergency-release operations |
| `PilotStationTransition` | Accepted/blocked state transition with before and after states |
| `PilotControlAxes` | Candidate normalized pitch/roll/yaw in [-1, 1] and throttle in [0, 1] |
| `PilotControlRequest` | Station ID, actor ID, input source, and axes |
| `PilotControlAttempt` | Accepted flag, block reason, optional request |
| Enums | `PilotStationAction`, `PilotStationBlockReason`, `PilotInputSource` |

## Readiness and transition behavior

Controls are ready only when an occupant is present and qualified, restraints are secured, interface is connected, rig is raised, station is powered, and controls are intact. Seating does not grant qualification. Connection requires a seated occupant, secured restraints, and intact controls; raising the rig additionally requires a connected interface. Control attempts must come from the seated actor, pass readiness checks, use a defined input source, and have finite axes in the declared ranges. Emergency release clears occupancy, qualification, restraints, interface, and rig state regardless of station power.

## Use case

Drive the station through explicit transitions, present the returned state to the experience layer, and pass accepted `PilotControlRequest` values to a separate authoritative aircraft control law.

## Evidence boundary

The source comments explicitly identify this as a narrow raWWar prototype. It does not animate a rig, simulate physical restraints, move a fighter, define the final control axes, or replace a reusable Workshop physical-interaction capability. The normalized axes are candidate contract examples, not canon.
