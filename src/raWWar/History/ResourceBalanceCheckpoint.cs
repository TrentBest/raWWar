namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Immutable checkpoint for the narrow resource-balance replay example only.
/// It is not a durable file format or a general world checkpoint.
/// </summary>
public sealed record ResourceBalanceCheckpoint(
    int SchemaVersion,
    long Balance,
    ulong WorldSeed,
    string SimulationModelVersion,
    string StreamKey,
    string LastEventId,
    string LastLogicalTimeKey,
    ulong LastEventOrdinal)
{
    public const int CurrentSchemaVersion = 1;

    /// <summary>Creates a checkpoint after replaying a non-empty, already ordered event prefix.</summary>
    public static ResourceBalanceCheckpoint Capture(long initialBalance, IEnumerable<SimulationEvent> orderedPrefix)
    {
        ArgumentNullException.ThrowIfNull(orderedPrefix);
        var events = orderedPrefix.ToArray();
        if (events.Length == 0)
            throw new ArgumentException("A checkpoint needs a non-empty event prefix so its history boundary is explicit.", nameof(orderedPrefix));

        var balance = ResourceBalanceReplay.Replay(initialBalance, events);
        var last = events[^1];
        return new ResourceBalanceCheckpoint(
            CurrentSchemaVersion,
            balance,
            last.WorldSeed,
            last.SimulationModelVersion,
            last.StreamKey,
            last.Id.Value,
            last.LogicalTimeKey,
            last.EventOrdinal);
    }

    /// <summary>Replays only a tail strictly after this checkpoint's exact ordering boundary.</summary>
    public long ReplayTail(IEnumerable<SimulationEvent> orderedTail)
    {
        ArgumentNullException.ThrowIfNull(orderedTail);
        if (SchemaVersion != CurrentSchemaVersion)
            throw new InvalidOperationException($"Unsupported resource checkpoint schema version {SchemaVersion}.");

        var tail = orderedTail.ToArray();
        SimulationEvent? previous = null;
        foreach (var item in tail)
        {
            ArgumentNullException.ThrowIfNull(item);
            if (item.StreamKey != StreamKey || item.WorldSeed != WorldSeed
                || item.SimulationModelVersion != SimulationModelVersion
                || item.EventDomain != ResourceBalanceReplay.EventDomain)
                throw new ArgumentException("Checkpoint tail does not belong to the checkpoint's domain, address, seed, and model version.", nameof(orderedTail));

            if (CompareToBoundary(item) <= 0)
                throw new ArgumentException("Checkpoint tail contains an event at or before the checkpoint boundary.", nameof(orderedTail));

            if (previous is not null && Compare(previous, item) >= 0)
                throw new ArgumentException("Checkpoint tail is not strictly ordered.", nameof(orderedTail));

            previous = item;
        }

        return ResourceBalanceReplay.Replay(Balance, tail);
    }

    private int CompareToBoundary(SimulationEvent item)
    {
        var time = StringComparer.Ordinal.Compare(item.LogicalTimeKey, LastLogicalTimeKey);
        if (time != 0) return time;
        var ordinal = item.EventOrdinal.CompareTo(LastEventOrdinal);
        if (ordinal != 0) return ordinal;
        return StringComparer.Ordinal.Compare(item.Id.Value, LastEventId);
    }

    private static int Compare(SimulationEvent left, SimulationEvent right)
    {
        var time = StringComparer.Ordinal.Compare(left.LogicalTimeKey, right.LogicalTimeKey);
        if (time != 0) return time;
        var ordinal = left.EventOrdinal.CompareTo(right.EventOrdinal);
        if (ordinal != 0) return ordinal;
        return StringComparer.Ordinal.Compare(left.Id.Value, right.Id.Value);
    }
}
