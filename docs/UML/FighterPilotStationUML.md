# FighterPilotStation — class diagram

```mermaid
classDiagram
    class FighterPilotStation {
      <<sealed record>>
      +ControlsReady bool
      +Create(stationId, powered, controlsIntact) FighterPilotStation
      +TrySeat(occupantId, qualified) PilotStationTransition
      +TrySecureOccupant() PilotStationTransition
      +TryConnectInterface() PilotStationTransition
      +TryRaiseControlRig() PilotStationTransition
      +TryApplyControl(actorId, inputSource, axes) PilotControlAttempt
      +TryEmergencyRelease() PilotStationTransition
    }
    class PilotStationTransition {
      +bool Accepted
      +PilotStationAction Action
      +PilotStationBlockReason BlockReason
      +FighterPilotStation Before
      +FighterPilotStation After
    }
    class PilotControlAttempt {
      +bool Accepted
      +PilotStationBlockReason BlockReason
      +PilotControlRequest Request
    }
    class PilotControlRequest {
      +ulong StationId
      +ulong ActorId
      +PilotInputSource InputSource
      +PilotControlAxes Axes
    }
    class PilotControlAxes {
      +double Pitch
      +double Roll
      +double Yaw
      +double Throttle
      +bool IsValid
    }
    FighterPilotStation --> PilotStationTransition : state transition
    FighterPilotStation --> PilotControlAttempt : resolves pilot intent
    PilotControlAttempt --> PilotControlRequest : optional accepted request
    PilotControlRequest --> PilotControlAxes : normalized intent
```

## Use case

The station validates readiness and actor authority, returning either a block reason or a control request for a separate aircraft-control system.

## Boundary

State contract only; no animation, vehicle movement, or final control-law semantics.
