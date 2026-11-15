using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace TheSingularityWorkshop.raWWar.History;

/// <summary>
/// Stable identity of one world entity, independent of its current location, observer,
/// presentation, activity, or agency scale.
/// </summary>
/// <remarks>
/// V1 is derived from immutable creation coordinates: world seed, simulation model version,
/// entity kind, canonical origin address, and a domain-assigned creation ordinal.
/// The origin address is an identity input, not a claim about the entity's current location.
/// This is an identity primitive only; it does not create, persist, own, or authorize an entity.
/// </remarks>
public readonly record struct WorldEntityId(string Value)
{
    public const byte CurrentVersion = 1;

    public static WorldEntityId Create(
        ulong worldSeed,
        string simulationModelVersion,
        string entityKind,
        ReadOnlySpan<byte> canonicalOriginAddress,
        ulong creationOrdinal)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(simulationModelVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(entityKind);
        if (canonicalOriginAddress.IsEmpty)
            throw new ArgumentException("A canonical origin address is required.", nameof(canonicalOriginAddress));

        using var stream = new MemoryStream();
        stream.Write("RWEY"u8);
        stream.WriteByte(CurrentVersion);
        WriteUInt64(stream, worldSeed);
        WriteUtf8(stream, simulationModelVersion);
        WriteUtf8(stream, entityKind);
        WriteBytes(stream, canonicalOriginAddress);
        WriteUInt64(stream, creationOrdinal);

        return new WorldEntityId(Convert.ToHexString(SHA256.HashData(stream.ToArray())));
    }

    private static void WriteUtf8(Stream stream, string value) =>
        WriteBytes(stream, Encoding.UTF8.GetBytes(value));

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
