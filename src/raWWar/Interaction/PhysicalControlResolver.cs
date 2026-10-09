namespace TheSingularityWorkshop.raWWar.Interaction;

/// <summary>
/// Input-independent physical control transition. Desktop, VR, and NPC adapters submit the
/// same semantic command; the renderer is responsible for showing the actor's corresponding
/// reach/grip/hand motion, not for deciding whether the world action succeeds.
/// </summary>
public static class PhysicalControlResolver
{
    public static ControlResolution Apply(
        PhysicalControlDefinition definition,
        PhysicalControlState state,
        PhysicalControlCommand command)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.ActorId))
            throw new ArgumentException("An actor identity is required.", nameof(command));
        if (!double.IsFinite(command.Amount))
            throw new ArgumentOutOfRangeException(nameof(command), "Command amount must be finite.");
        if (!double.IsFinite(command.LogicalTime))
            throw new ArgumentOutOfRangeException(nameof(command), "Logical time must be finite.");
        if (definition.Minimum > definition.Maximum || definition.InitialPosition < definition.Minimum
            || definition.InitialPosition > definition.Maximum)
            throw new ArgumentException("Control definition has an invalid range.", nameof(definition));
        if (state.Position < definition.Minimum || state.Position > definition.Maximum)
            throw new ArgumentException("Control state is outside its declared range.", nameof(state));

        string? rejection = null;
        if (!state.Powered && definition.RequiresPower) rejection = "unpowered";
        else if (!state.Authorized) rejection = "unauthorized";
        else if (!state.InterlockSatisfied) rejection = "interlock-open";
        else if (state.Integrity <= 0) rejection = "destroyed";
        else if (state.Jammed) rejection = "jammed";

        if (rejection is not null)
        {
            var failedEvent = new PhysicalControlEvent(
                definition.Id, command.ActorId, command.Action, command.InputSource,
                command.LogicalTime, state.Position, state.Position, false, rejection);
            return new ControlResolution(state with { EventCount = state.EventCount + 1 },
                failedEvent, false, "none", rejection);
        }

        var next = command.Action switch
        {
            PhysicalControlAction.Press => Math.Clamp(state.Position + Math.Sign(command.Amount) * definition.PressStep,
                definition.Minimum, definition.Maximum),
            PhysicalControlAction.Move => Math.Clamp(state.Position + command.Amount,
                definition.Minimum, definition.Maximum),
            PhysicalControlAction.SetPosition => Math.Clamp(command.Amount,
                definition.Minimum, definition.Maximum),
            PhysicalControlAction.Release => state.Position,
            _ => throw new ArgumentOutOfRangeException(nameof(command), "Unsupported physical control action.")
        };

        var changed = Math.Abs(next - state.Position) > 1e-12;
        var updated = state with { Position = next, EventCount = state.EventCount + 1 };
        var physicalAction = command.Action == PhysicalControlAction.Release
            ? "release-grip"
            : changed ? definition.ActorMotion : "attempt-control";
        var evt = new PhysicalControlEvent(
            definition.Id, command.ActorId, command.Action, command.InputSource,
            command.LogicalTime, state.Position, next, true, changed ? "applied" : "at-limit");
        return new ControlResolution(updated, evt, changed, physicalAction, null);
    }
}

public sealed record PhysicalControlDefinition(
    string Id,
    string Kind,
    double Minimum,
    double Maximum,
    double InitialPosition,
    double PressStep,
    bool RequiresPower,
    string ActorMotion);

public sealed record PhysicalControlState(
    double Position,
    double Integrity = 1,
    bool Powered = true,
    bool Authorized = true,
    bool InterlockSatisfied = true,
    bool Jammed = false,
    long EventCount = 0);

public sealed record PhysicalControlCommand(
    string ActorId,
    PhysicalControlAction Action,
    double Amount,
    PhysicalInputSource InputSource,
    double LogicalTime);

public enum PhysicalControlAction { Press, Move, SetPosition, Release }
public enum PhysicalInputSource { Keyboard, Mouse, Controller, VrTrackedHand, NpcAgent }

public sealed record PhysicalControlEvent(
    string ControlId,
    string ActorId,
    PhysicalControlAction Action,
    PhysicalInputSource InputSource,
    double LogicalTime,
    double PreviousPosition,
    double ResultingPosition,
    bool Accepted,
    string Result);

public sealed record ControlResolution(
    PhysicalControlState State,
    PhysicalControlEvent Event,
    bool WorldStateChanged,
    string ActorMotion,
    string? RejectionReason);
