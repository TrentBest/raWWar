using System.Buffers.Binary;

namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// Stable, versioned binary encoding for <see cref="HierarchicalSpatialAddress"/>.
/// This encoding is for identity serialization, not a hash and not physical coordinates.
/// </summary>
/// <remarks>
/// V1 bytes are: ASCII "RWSA", version byte 1, root ordinal as unsigned 16-bit
/// big-endian, depth as unsigned 32-bit big-endian, then X/Y/Z bytes for each child
/// from root outward. Child coordinates are each in [0,9]. No trailing bytes are allowed.
/// Once persisted or distributed, this version's layout must not be changed; add a new
/// version instead. This does not select a stable hash algorithm.
/// </remarks>
public static class HierarchicalSpatialAddressCodec
{
    private const byte MagicR = (byte)'R';
    private const byte MagicW = (byte)'W';
    private const byte MagicS = (byte)'S';
    private const byte MagicA = (byte)'A';
    public const byte CurrentVersion = 1;
    private const int HeaderLength = 11;

    public static byte[] Encode(HierarchicalSpatialAddress address)
    {
        ArgumentNullException.ThrowIfNull(address);

        var depth = address.Depth;
        var length = checked(HeaderLength + checked(depth * 3));
        var bytes = new byte[length];
        bytes[0] = MagicR;
        bytes[1] = MagicW;
        bytes[2] = MagicS;
        bytes[3] = MagicA;
        bytes[4] = CurrentVersion;
        BinaryPrimitives.WriteUInt16BigEndian(bytes.AsSpan(5, 2), checked((ushort)address.Root.Ordinal);
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(7, 4), checked((uint)depth));

        var offset = HeaderLength;
        foreach (var child in address.Children)
        {
            bytes[offset++] = checked((byte)child.X);
            bytes[offset++] = checked((byte)child.Y);
            bytes[offset++] = checked((byte)child.Z);
        }

        return bytes;
    }

    public static HierarchicalSpatialAddress Decode(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length < HeaderLength)
            throw new FormatException("Spatial address encoding is shorter than the V1 header.");
        if (bytes[0] != MagicR || bytes[1] != MagicW || bytes[2] != MagicS || bytes[3] != MagicA)
            throw new FormatException("Spatial address encoding has an invalid magic prefix.");
        if (bytes[4] != CurrentVersion)
            throw new FormatException($"Unsupported spatial address encoding version: {bytes[4]}.");

        var rootOrdinal = BinaryPrimitives.ReadUInt16BigEndian(bytes.Slice(5, 2));
        var depthValue = BinaryPrimitives.ReadUInt32BigEndian(bytes.Slice(7, 4));
        if (depthValue > int.MaxValue)
            throw new FormatException("Spatial address depth exceeds the supported in-memory range.");

        var depth = (int)depthValue;
        var expectedLength = (long)HeaderLength + (long)depth * 3;
        if (expectedLength != bytes.Length)
            throw new FormatException("Spatial address encoding length does not match its declared depth.");

        GalaxyCellAddress root;
        try
        {
            root = GalaxyCellAddress.FromOrdinal(rootOrdinal);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new FormatException("Spatial address root ordinal is outside the defined galaxy grid.", ex);
        }

        var address = HierarchicalSpatialAddress.At(root);
        var offset = HeaderLength;
        for (var i = 0; i < depth; i++)
        {
            var x = bytes[offset++];
            var y = bytes[offset++];
            var z = bytes[offset++];
            if (x >= GalaxyCellAddress.DimensionX ||
                y >= GalaxyCellAddress.DimensionY ||
                z >= GalaxyCellAddress.DimensionZ)
                throw new FormatException($"Child coordinate at depth {i + 1} is outside the defined 10x10x10 grid.");

            address = address.Child(x, y, z);
        }

        return address;
    }
}
