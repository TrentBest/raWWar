using TheSingularityWorkshop.raWWar.Spatiotemporal;

// The root is a top-level galaxy-cell ordinal. Each Child call appends one
// coordinate in that parent's 10x10x10 subdivision; it does not replace prior levels.
var address = HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(42))
    .Child(3, 4, 5)
    .Child(9, 0, 1);

var encoded = HierarchicalSpatialAddressCodec.EncodeV1(address);
var decoded = HierarchicalSpatialAddressCodec.Decode(encoded);
var stableDigest = HierarchicalSpatialAddressHasher.ComputeV1(address);

Console.WriteLine($"Address: {address}");
Console.WriteLine($"Canonical V1 bytes: {Convert.ToHexString(encoded)}");
Console.WriteLine($"SHA-256 V1: {Convert.ToHexString(stableDigest)}");

if (!decoded.Equals(address)
    || !HierarchicalSpatialAddressHasher.ComputeV1(decoded).SequenceEqual(stableDigest))
{
    Console.Error.WriteLine("Spatial-address round-trip or digest check failed.");
    return 1;
}

// Diagnostic ToString/GetHashCode are not the wire format or cross-process identity.
// The explicit codec and versioned SHA-256 definition provide those contracts.
var malformed = encoded.Concat(new byte[] { 0xFF }).ToArray();
try
{
    _ = HierarchicalSpatialAddressCodec.Decode(malformed);
    Console.Error.WriteLine("Expected trailing bytes to be rejected.");
    return 1;
}
catch (FormatException)
{
    Console.WriteLine("Boundary check passed: trailing bytes are rejected.");
}

try
{
    _ = address.Child(10, 0, 0);
    Console.Error.WriteLine("Expected child coordinate 10 to be rejected.");
    return 1;
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("Boundary check passed: child coordinates must be in [0, 9].");
}

Console.WriteLine("Hierarchical spatial-address example passed.");
return 0;
