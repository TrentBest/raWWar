using System.Security.Cryptography;

namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// Stable cross-process digest of a hierarchical spatial address's canonical V1 encoding.
/// </summary>
/// <remarks>
/// V1 is SHA-256 over the exact bytes returned by <see cref="HierarchicalSpatialAddressCodec.Encode"/>.
/// The digest is identity material, not encryption or authentication. Keep this definition immutable;
/// an incompatible encoding or hash definition requires a separately versioned API and test vectors.
/// </remarks>
public static class HierarchicalSpatialAddressHasher
{
    /// <summary>The stable hash definition implemented by <see cref="ComputeV1"/>.</summary>
    public const int CurrentVersion = 1;

    /// <summary>
    /// Computes the 32-byte SHA-256 digest of the canonical hierarchical-address V1 bytes.
    /// </summary>
    public static byte[] ComputeV1(HierarchicalSpatialAddress address)
    {
        ArgumentNullException.ThrowIfNull(address);
        return SHA256.HashData(HierarchicalSpatialAddressCodec.EncodeV1(address));
    }
}
