using System.Buffers.Binary;

namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Small example reducer for one address-scoped resource-balance stream.
/// Payload V1 is exactly one signed Int64 delta encoded in big-endian two's-complement form.
/// </summary>
/// <remarks>
/// This is a domain example, not a universal world-state reducer or durable replay service.
/// Pass the ordered events for one address and domain; the reducer does not query history,
/// commit events, infer time units, or implement checkpoints.
/// </remarks>
public static class ResourceBalanceReplay
{
    public const string EventDomain = "resource.balance.delta";
    public const byte PayloadVersion = 1;
    public const int PayloadLength = sizeof(long);

    public static byte[] EncodeDelta(long delta)
    {
        var payload = new byte[PayloadLength];
        BinaryPrimitives.WriteInt64BigEndian(payload, delta);
        return payload;
    }

    public static long DecodeDelta(ReadOnlySpan<byte> payload)
    {
        if (payload.Length != PayloadLength)
            throw new FormatException($"Resource balance delta V{PayloadVersion} payload must be exactly {PayloadLength} bytes.");
        return BinaryPrimitives.ReadInt64BigEndian(payload);
    }

    /// <summary>Reconstructs a balance by applying already ordered deltas with checked Int64 arithmetic.</summary>
    public static long Replay(long initialBalance, IEnumerable<SimulationEvent> orderedEvents)
    {
        ArgumentNullException.ThrowIfNull(orderedEvents);
        var balance = initialBalance;
        foreach (var simulationEvent in orderedEvents)
        {
            ArgumentNullException.ThrowIfNull(simulationEvent);
            if (!StringComparer.Ordinal.Equals(simulationEvent.EventDomain, EventDomain))
                throw new ArgumentException("Replay input contains an event from a different domain.", nameof(orderedEvents));

            var delta = DecodeDelta(simulationEvent.Payload.Span);
            balance = checked(balance + delta);
        }

        return balance;
    }
}
