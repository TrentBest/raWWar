using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Stable identity for one logical event occurrence. Identity is independent of payload,
/// arrival order, thread scheduling, and process-local state.
/// </summary>
/// <remarks>
/// V1 hashes an explicit binary encoding of seed, model version, canonical spatial-address
/// bytes, domain, event ordinal, and domain-defined logical-time key. It does not prescribe
/// a universal time unit or ordering policy.
/// </remarks>
public readonly record struct SimulationEventId(string Value)
{
    public const byte CurrentVersion = 1;

    public static SimulationEventId Create(
        ulong worldSeed,
        string simulationModelVersion,
        ReadOnlySpan<byte> canonicalAddress,
        string eventDomain,
        ulong eventOrdinal,
        string logicalTimeKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(simulationModelVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(eventDomain);
        ArgumentNullException.ThrowIfNull(logicalTimeKey);
        if (canonicalAddress.IsEmpty)
            throw new ArgumentException("A canonical entity or region address is required.", nameof(canonicalAddress));

        using var stream = new MemoryStream();
        stream.Write("RWEI"u8);
        stream.WriteByte(CurrentVersion);
        WriteUInt64(stream, worldSeed);
        WriteUtf8(stream, simulationModelVersion);
        WriteBytes(stream, canonicalAddress);
        WriteUtf8(stream, eventDomain);
        WriteUInt64(stream, eventOrdinal);
        WriteUtf8(stream, logicalTimeKey);

        return new SimulationEventId(Convert.ToHexString(SHA256.HashData(stream.ToArray())));
    }

    private static void WriteUtf8(Stream stream, string value) => WriteBytes(stream, Encoding.UTF8.GetBytes(value));

    private static void WriteBytes(Stream stream, ReadOnlySpan<byte> value)
    {
        Span<byte> length = stackalloc byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(length, checked((uint)value.Length));
        stream.Write(length);
        stream.Write(value);
    }

    private static void WriteUInt64(Stream stream, ulong value)
    {
        Span<byte> bytes = stackalloc byte[8];
        BinaryPrimitives.WriteUInt64BigEndian(bytes, value);
        stream.Write(bytes);
    }
}

/// <summary>A proposed immutable event occurrence. Constructing it does not commit history.</summary>
public sealed class SimulationEvent
{
    private readonly byte[] _canonicalAddress;
    private readonly byte[] _payload;

    private SimulationEvent(
        ulong worldSeed,
        string simulationModelVersion,
        ReadOnlySpan<byte> canonicalAddress,
        string eventDomain,
        ulong eventOrdinal,
        string logicalTimeKey,
        ReadOnlySpan<byte> payload)
    {
        WorldSeed = worldSeed;
        SimulationModelVersion = simulationModelVersion;
        _canonicalAddress = canonicalAddress.ToArray();
        EventDomain = eventDomain;
        EventOrdinal = eventOrdinal;
        LogicalTimeKey = logicalTimeKey;
        _payload = payload.ToArray();
        Id = SimulationEventId.Create(worldSeed, simulationModelVersion, _canonicalAddress,
            eventDomain, eventOrdinal, logicalTimeKey);
    }

    public static SimulationEvent Create(
        ulong worldSeed,
        string simulationModelVersion,
        ReadOnlySpan<byte> canonicalAddress,
        string eventDomain,
        ulong eventOrdinal,
        string logicalTimeKey,
        ReadOnlySpan<byte> payload)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(simulationModelVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(eventDomain);
        ArgumentNullException.ThrowIfNull(logicalTimeKey);
        if (canonicalAddress.IsEmpty)
            throw new ArgumentException("A canonical entity or region address is required.", nameof(canonicalAddress));
        return new SimulationEvent(worldSeed, simulationModelVersion, canonicalAddress,
            eventDomain, eventOrdinal, logicalTimeKey, payload);
    }

    public SimulationEventId Id { get; }
    public ulong WorldSeed { get; }
    public string SimulationModelVersion { get; }
    public string EventDomain { get; }
    /// <summary>Canonical address bytes as uppercase hexadecimal for stream selection.</summary>
    public string StreamKey => Convert.ToHexString(_canonicalAddress);
    /// <summary>Opaque, domain-defined ordering key; this type assigns no units or numeric meaning.</summary>
    public string LogicalTimeKey { get; }
    public ulong EventOrdinal { get; }
    public ReadOnlyMemory<byte> Payload => _payload.ToArray();

    internal bool HasSameContent(SimulationEvent other) =>
        WorldSeed == other.WorldSeed
        && SimulationModelVersion == other.SimulationModelVersion
        && StreamKey == other.StreamKey
        && EventDomain == other.EventDomain
        && LogicalTimeKey == other.LogicalTimeKey
        && EventOrdinal == other.EventOrdinal
        && _payload.AsSpan().SequenceEqual(other._payload);
}

/// <summary>Outcome of attempting to commit an event to this process-local reference ledger.</summary>
public enum EventCommitResult
{
    Committed,
    AlreadyCommitted,
    IdentityConflict
}

/// <summary>
/// Thread-safe in-memory reference ledger for event identity, idempotency, and deterministic
/// domain-local ordering. It is not durable storage and does not apply payloads to world state.
/// </summary>
/// <remarks>
/// Concurrent commits are serialized under one lock. A repeated ID with identical content is
/// an idempotent retry; the same ID with different content is a conflict. Ordered reads sort
/// by ordinal logical-time key, then event ordinal, then stable event ID. Domains must ensure
/// that this lexical key policy matches their declared semantics before using it for replay.
/// </remarks>
public sealed class InMemoryEventHistory
{
    private readonly object _gate = new();
    private readonly Dictionary<SimulationEventId, SimulationEvent> _events = new();

    public int Count
    {
        get { lock (_gate) return _events.Count; }
    }

    public EventCommitResult Commit(SimulationEvent candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate);
        lock (_gate)
        {
            if (_events.TryGetValue(candidate.Id, out var existing))
                return existing.HasSameContent(candidate)
                    ? EventCommitResult.AlreadyCommitted
                    : EventCommitResult.IdentityConflict;

            _events.Add(candidate.Id, candidate);
            return EventCommitResult.Committed;
        }
    }

    /// <summary>Returns a deterministic snapshot for one domain and canonical entity/region address.</summary>
    public IReadOnlyList<SimulationEvent> ReadOrdered(string eventDomain, ReadOnlySpan<byte> canonicalAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventDomain);
        if (canonicalAddress.IsEmpty)
            throw new ArgumentException("A canonical entity or region address is required.", nameof(canonicalAddress));
        var streamKey = Convert.ToHexString(canonicalAddress);
        lock (_gate)
        {
            return _events.Values
                .Where(e => StringComparer.Ordinal.Equals(e.EventDomain, eventDomain)
                    && StringComparer.Ordinal.Equals(e.StreamKey, streamKey))
                .OrderBy(e => e.LogicalTimeKey, StringComparer.Ordinal)
                .ThenBy(e => e.EventOrdinal)
                .ThenBy(e => e.Id.Value, StringComparer.Ordinal)
                .ToArray();
        }
    }

    /// <summary>Returns a domain-wide deterministic snapshot; use the address-scoped overload for replay.</summary>
    public IReadOnlyList<SimulationEvent> ReadOrdered(string eventDomain)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventDomain);
        lock (_gate)
        {
            return _events.Values
                .Where(e => StringComparer.Ordinal.Equals(e.EventDomain, eventDomain))
                .OrderBy(e => e.LogicalTimeKey, StringComparer.Ordinal)
                .ThenBy(e => e.EventOrdinal)
                .ThenBy(e => e.Id.Value, StringComparer.Ordinal)
                .ToArray();
        }
    }
}
