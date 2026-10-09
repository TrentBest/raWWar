namespace TheSingularityWorkshop.raWWar.Interaction;

/// <summary>
/// Narrow raWWar prototype for one fighter-pilot station. It models station/occupant
/// preconditions and the resulting control request; it does not animate a rig or move a fighter.
/// </summary>
/// <remarks>
/// Generic physical-interaction resolution belongs in a reusable Workshop capability.
/// This Experience-owned type exists only to make the first pilot-station contract executable
/// while that capability's verified API is unavailable.
/// </remarks>
public sealed record FighterPilotStation
{
    private FighterPilotStation(
        ulong stationId,
        ulong? occupantId,
        bool occupantQualified,
        bool restraintsSecured,
        bool interfaceConnected,
        bool controlRigRaised,
        bool powered,
        bool controlsIntact)
    {
        StationId = stationId;
        OccupantId = occupantId;
        OccupantQualified = occupantQualified;
        RestraintsSecured = restraintsSecured;
        InterfaceConnected = interfaceConnected;
        ControlRigRaised = controlRigRaised;
        Powered = powered;
        ControlsIntact = controlsIntact;
    }

    public ulong StationId { get; private init; }
    public ulong? OccupantId { get; private init; }
    public bool OccupantQualified { get; private init; }
    public bool RestraintsSecured { get; private init; }
    public bool InterfaceConnected { get; private init; }
    public bool ControlRigRaised { get; private init; }
    public bool Powered { get; private init; }
    public bool ControlsIntact { get; private init; }

    /// <summary>True only when the physical and authority preconditions permit pilot input.</summary>
    public bool ControlsReady =>
        OccupantId is not null &&
        OccupantQualified &&
        RestraintsSecured &&
        InterfaceConnected &&
        ControlRigRaised &&
        Powered &&
        ControlsIntact;

    public static FighterPilotStation Create(
        ulong stationId,
        bool powered = true,
        bool controlsIntact = true)
    {
        if (stationId == 0)
            throw new ArgumentOutOfRangeException(nameof(stationId), "Station identity must be non-zero.");

        return new FighterPilotStation(
            stationId, null, false, false, false, false, powered, controlsIntact);
    }

    /// <summary>Seats a person; it does not silently grant qualification or enable controls.</summary>
    public PilotStationTransition TrySeat(ulong occupantId, bool fighterPilotQualified)
    {
        if (occupantId == 0)
            throw new ArgumentOutOfRangeException(nameof(occupantId), "Occupant identity must be non-zero.");

        if (OccupantId is not null)
            return Block(PilotStationAction.Seat, PilotStationBlockReason.AlreadyOccupied);

        return Accept(
            PilotStationAction.Seat,
            this with { OccupantId = occupantId, OccupantQualified = fighterPilotQualified });
    }

    /// <summary>Engages the station's physical restraints around the seated occupant.</summary>
    public PilotStationTransition TrySecureOccupant()
    {
        if (OccupantId is null)
            return Block(PilotStationAction.SecureOccupant, PilotStationBlockReason.NoOccupant);
        if (RestraintsSecured)
            return Block(PilotStationAction.SecureOccupant, PilotStationBlockReason.AlreadySecured);

        return Accept(
            PilotStationAction.SecureOccupant,
            this with { RestraintsSecured = true });
    }

    /// <summary>Connects the station interface after the occupant is physically secured.</summary>
    public PilotStationTransition TryConnectInterface()
    {
        if (OccupantId is null)
            return Block(PilotStationAction.ConnectInterface, PilotStationBlockReason.NoOccupant);
        if (!RestraintsSecured)
            return Block(PilotStationAction.ConnectInterface, PilotStationBlockReason.RestraintsNotSecured);
        if (!ControlsIntact)
            return Block(PilotStationAction.ConnectInterface, PilotStationBlockReason.ControlsDamaged);
        if (InterfaceConnected)
            return Block(PilotStationAction.ConnectInterface, PilotStationBlockReason.AlreadyConnected);

        return Accept(
            PilotStationAction.ConnectInterface,
            this with { InterfaceConnected = true });
    }

    /// <summary>
    /// Raises the physical control rig into operating position. Visual hands/grips are a
    /// presentation of this state, not a substitute for it.
    /// </summary>
    public PilotStationTransition TryRaiseControlRig()
    {
        if (OccupantId is null)
            return Block(PilotStationAction.RaiseControlRig, PilotStationBlockReason.NoOccupant);
        if (!RestraintsSecured)
            return Block(PilotStationAction.RaiseControlRig, PilotStationBlockReason.RestraintsNotSecured);
        if (!InterfaceConnected)
            return Block(PilotStationAction.RaiseControlRig, PilotStationBlockReason.InterfaceNotConnected);
        if (!ControlsIntact)
            return Block(PilotStationAction.RaiseControlRig, PilotStationBlockReason.ControlsDamaged);
        if (ControlRigRaised)
            return Block(PilotStationAction.RaiseControlRig, PilotStationBlockReason.RigAlreadyRaised);

        return Accept(
            PilotStationAction.RaiseControlRig,
            this with { ControlRigRaised = true });
    }

    /// <summary>
    /// Resolves a pilot's intent into a bounded control request. It never moves the aircraft
    /// directly; the aircraft control law and authoritative vehicle state must resolve the request.
    /// </summary>
    public PilotControlAttempt TryApplyControl(
        ulong actorId,
        PilotInputSource inputSource,
        PilotControlAxes axes)
    {
        var blockReason = GetControlBlockReason(actorId);
        if (blockReason != PilotStationBlockReason.None)
            return new PilotControlAttempt(false, blockReason, null);

        if (!Enum.IsDefined(inputSource) || !axes.IsValid)
            return new PilotControlAttempt(false, PilotStationBlockReason.InvalidControlInput, null);

        return new PilotControlAttempt(
            true,
            PilotStationBlockReason.None,
            new PilotControlRequest(StationId, actorId, inputSource, axes));
    }

    /// <summary>
    /// Emergency egress is independent of station power. It drops the rig, disconnects the
    /// interface, releases restraints, and clears occupancy in one explicit emergency transition.
    /// </summary>
    public PilotStationTransition TryEmergencyRelease()
    {
        if (OccupantId is null)
            return Block(PilotStationAction.EmergencyRelease, PilotStationBlockReason.NoOccupant);

        return Accept(
            PilotStationAction.EmergencyRelease,
            this with
            {
                OccupantId = null,
                OccupantQualified = false,
                RestraintsSecured = false,
                InterfaceConnected = false,
                ControlRigRaised = false
            });
    }

    private PilotStationBlockReason GetControlBlockReason(ulong actorId)
    {
        if (OccupantId is null)
            return PilotStationBlockReason.NoOccupant;
        if (OccupantId != actorId)
            return PilotStationBlockReason.WrongOccupant;
        if (!OccupantQualified)
            return PilotStationBlockReason.QualificationRequired;
        if (!RestraintsSecured)
            return PilotStationBlockReason.RestraintsNotSecured;
        if (!InterfaceConnected)
            return PilotStationBlockReason.InterfaceNotConnected;
        if (!ControlRigRaised)
            return PilotStationBlockReason.RigNotRaised;
        if (!Powered)
            return PilotStationBlockReason.StationUnpowered;
        if (!ControlsIntact)
            return PilotStationBlockReason.ControlsDamaged;

        return PilotStationBlockReason.None;
    }

    private PilotStationTransition Accept(PilotStationAction action, FighterPilotStation next) =>
        new(true, action, PilotStationBlockReason.None, this, next);

    private PilotStationTransition Block(PilotStationAction action, PilotStationBlockReason reason) =>
        new(false, action, reason, this, this);
}

public enum PilotStationAction
{
    Seat,
    SecureOccupant,
    ConnectInterface,
    RaiseControlRig,
    EmergencyRelease
}

public enum PilotStationBlockReason
{
    None,
    AlreadyOccupied,
    NoOccupant,
    AlreadySecured,
    RestraintsNotSecured,
    ControlsDamaged,
    AlreadyConnected,
    InterfaceNotConnected,
    RigAlreadyRaised,
    RigNotRaised,
    WrongOccupant,
    QualificationRequired,
    StationUnpowered,
    InvalidControlInput
}

public enum PilotInputSource
{
    Desktop,
    Controller,
    VirtualReality,
    Accessibility,
    NonPlayerCharacter
}

/// <summary>Normalized pilot intent: pitch, roll, and yaw in [-1, 1], throttle in [0, 1].</summary>
public readonly record struct PilotControlAxes(double Pitch, double Roll, double Yaw, double Throttle)
{
    public bool IsValid =>
        double.IsFinite(Pitch) && Pitch is >= -1 and <= 1 &&
        double.IsFinite(Roll) && Roll is >= -1 and <= 1 &&
        double.IsFinite(Yaw) && Yaw is >= -1 and <= 1 &&
        double.IsFinite(Throttle) && Throttle is >= 0 and <= 1;
}

public sealed record PilotControlRequest(
    ulong StationId,
    ulong ActorId,
    PilotInputSource InputSource,
    PilotControlAxes Axes);

public sealed record PilotControlAttempt(
    bool Accepted,
    PilotStationBlockReason BlockReason,
    PilotControlRequest? Request);

public sealed record PilotStationTransition(
    bool Accepted,
    PilotStationAction Action,
    PilotStationBlockReason BlockReason,
    FighterPilotStation Before,
    FighterPilotStation After);
