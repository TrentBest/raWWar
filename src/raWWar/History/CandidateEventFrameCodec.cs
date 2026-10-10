using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Experimental byte codec for Candidate V1 journal frames. Internal by design: the format
/// is not frozen and this is not a durable event store.
/// </summary>
internal static class CandidateEventFrameCodec
{
    internal const int HeaderLength = 10;
    internal const int DigestLength = 32;
    internal const int MaxModelVersionBytes = 4 * 1024;
    internal const int MaxAddressBytes = 4 * 1024;
    internal const int MaxEventDomainBytes = 4 * 1024;
    internal const int MaxLogicalTimeKeyBytes = 4 * 1024;
    internal const int MaxPayloadBytes = 1024 * 1024;
    internal const int MaxFrameBytes = 1_100_000;

    private static readonly byte[] Magic = "RWEJ"u8.ToArray();
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);

    internal static byte[] Encode(SimulationEvent simulationEvent)
    {
        ArgumentNullException.ThrowIfNull(simulationEvent);

        var model = StrictUtf8.GetBytes(simulationEvent.SimulationModelVersion);
        var address = Convert.FromHexString(simulationEvent.StreamKey);
        var domain = StrictUtf8.GetBytes(simulationEvent.EventDomain);
        var logicalTime = StrictUtf8.GetBytes(simulationEvent.LogicalTimeKey);
        var payload = simulationEvent.Payload.ToArray();

        ValidateFields(model, address, domain, logicalTime, payload);
        var length = checked(HeaderLength + sizeof(ulong)
            + FieldSize(model) + FieldSize(address) + FieldSize(domain)
            + sizeof(ulong) + FieldSize(logicalTime) + FieldSize(payload)
            + DigestLength + DigestLength);
        if (length > MaxFrameBytes)
            throw new FormatException("Candidate event frame exceeds the maximum frame size.");

        using var stream = new MemoryStream(length);
        stream.Write(Magic);
        stream.WriteByte(1);
        stream.WriteByte(0);
        WriteUInt32(stream, checked((uint)length));
        WriteUInt64(stream, simulationEvent.WorldSeed);
        WriteField(stream, model);
        WriteField(stream, address);
        WriteField(stream, domain);
        WriteUInt64(stream, simulationEvent.EventOrdinal);
        WriteField(stream, logicalTime);
        WriteField(stream, payload);
        stream.Write(Convert.FromHexString(simulationEvent.Id.Value));

        var prefix = stream.ToArray();
        stream.Write(SHA256.HashData(prefix));
        return stream.ToArray();
    }

    internal static SimulationEvent Decode(ReadOnlySpan<byte> frame)
    {
        if (frame.Length < HeaderLength)
            throw new FormatException("Candidate event frame has a truncated header.");
        if (frame.Length > MaxFrameBytes)
            throw new FormatException("Candidate event frame exceeds the maximum frame size.");
        if (!frame[..4].SequenceEqual(Magic))
            throw new FormatException("Candidate event frame has invalid magic.");
        if (frame[4] != 1)
            throw new FormatException("Candidate event frame version is unsupported.");
        if (frame[5] != 0)
            throw new FormatException("Candidate event frame reserved flags must be zero.");

        var declaredLength = BinaryPrimitives.ReadUInt32BigEndian(frame.Slice(6, 4));
        if (declaredLength != frame.Length || declaredLength > MaxFrameBytes)
            throw new FormatException("Candidate event frame length does not match its bounded input.");

        var bodyLength = frame.Length - DigestLength;
        if (bodyLength < HeaderLength)
            throw new FormatException("Candidate event frame is too short.");
        var expectedChecksum = SHA256.HashData(frame[..bodyLength]);
        if (!CryptographicOperations.FixedTimeEquals(expectedChecksum, frame[bodyLength..]))
            throw new FormatException("Candidate event frame checksum does not match.");

        var reader = new FrameReader(frame.Slice(HeaderLength, bodyLength - HeaderLength));
        var worldSeed = reader.ReadUInt64();
        var modelBytes = reader.ReadField(MaxModelVersionBytes, "simulation-model version");
        var address = reader.ReadField(MaxAddressBytes, "canonical address");
        var domainBytes = reader.ReadField(MaxEventDomainBytes, "event domain");
        var ordinal = reader.ReadUInt64();
        var logicalTimeBytes = reader.ReadField(MaxLogicalTimeKeyBytes, "logical-time key");
        var payload = reader.ReadField(MaxPayloadBytes, "payload");
        var storedId = reader.ReadBytes(DigestLength, "event ID");
        reader.RequireEnd();

        string model;
        string domain;
        string logicalTime;
        try
        {
            model = StrictUtf8.GetString(modelBytes);
            domain = StrictUtf8.GetString(domainBytes);
            logicalTime = StrictUtf8.GetString(logicalTimeBytes);
        }
        catch (DecoderFallbackException ex)
        {
            throw new FormatException("Candidate event frame contains malformed UTF-8.", ex);
        }

        SimulationEvent simulationEvent;
        try
        {
            simulationEvent = SimulationEvent.Create(
                worldSeed, model, address, domain, ordinal, logicalTime, payload);
        }
        catch (ArgumentException ex)
        {
            throw new FormatException("Candidate event frame contains invalid event fields.", ex);
        }

        var computedId = Convert.FromHexString(simulationEvent.Id.Value);
        if (!CryptographicOperations.FixedTimeEquals(computedId, storedId))
            throw new FormatException("Candidate event frame's stored event ID does not match its identity fields.");

        return simulationEvent;
    }

    private static void ValidateFields(byte[] model, byte[] address, byte[] domain, byte[] logicalTime, byte[] payload)
    {
        if (model.Length == 0 || StrictUtf8.GetString(model).AsSpan().Trim().IsEmpty)
            throw new FormatException("Simulation-model version must not be empty or whitespace.");
        if (address.Length == 0)
            throw new FormatException("Canonical address must not be empty.");
        if (domain.Length == 0 || StrictUtf8.GetString(domain).AsSpan().Trim().IsEmpty)
            throw new FormatException("Event domain must not be empty or whitespace.");
        if (model.Length > MaxModelVersionBytes || address.Length > MaxAddressBytes
            || domain.Length > MaxEventDomainBytes || logicalTime.Length > MaxLogicalTimeKeyBytes
            || payload.Length > MaxPayloadBytes)
            throw new FormatException("Candidate event frame field exceeds its configured bound.");
    }

    private static int FieldSize(byte[] bytes) => checked(sizeof(uint) + bytes.Length);

    private static void WriteField(Stream stream, byte[] bytes)
    {
        WriteUInt32(stream, checked((uint)bytes.Length));
        stream.Write(bytes);
    }

    private static void WriteUInt32(Stream stream, uint value)
    {
        Span<byte> bytes = stackalloc byte[sizeof(uint)];
        BinaryPrimitives.WriteUInt32BigEndian(bytes, value);
        stream.Write(bytes);
    }

    private static void WriteUInt64(Stream stream, ulong value)
    {
        Span<byte> bytes = stackalloc byte[sizeof(ulong)];
        BinaryPrimitives.WriteUInt64BigEndian(bytes, value);
        stream.Write(bytes);
    }

    private ref struct FrameReader
    {
        private ReadOnlySpan<byte> _remaining;

        internal FrameReader(ReadOnlySpan<byte> bytes) => _remaining = bytes;

        internal byte[] ReadField(int maximum, string label)
        {
            var length = ReadUInt32();
            if (length > maximum)
                throw new FormatException($"Candidate event frame {label} exceeds its configured bound.");
            return ReadBytes(checked((int)length), label);
        }

        internal byte[] ReadBytes(int length, string label)
        {
            if (length < 0 || length > _remaining.Length)
                throw new FormatException($"Candidate event frame has a truncated {label} field.");
            var value = _remaining[..length].ToArray();
            _remaining = _remaining[length..];
            return value;
        }

        internal uint ReadUInt32()
        {
            var bytes = ReadBytes(sizeof(uint), "length");
            return BinaryPrimitives.ReadUInt32BigEndian(bytes);
        }

        internal ulong ReadUInt64()
        {
            var bytes = ReadBytes(sizeof(ulong), "integer");
            return BinaryPrimitives.ReadUInt64BigEndian(bytes);
        }

        internal void RequireEnd()
        {
            if (!_remaining.IsEmpty)
                throw new FormatException("Candidate event frame contains unexpected bytes before its checksum.");
        }
    }
}
