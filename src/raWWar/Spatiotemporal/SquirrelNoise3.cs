namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// Versioned SquirrelNoise3 integer hash used as a stateless, random-access
/// pseudo-random primitive. This is not a cryptographic hash and does not
/// define canonical spatial-address serialization.
/// </summary>
/// <remarks>
/// Arithmetic intentionally wraps modulo 2^32. Keep the algorithm and constants
/// stable for version 3; changing them requires a new generator/primitive version.
/// The original GDC 2017 algorithm adds BIT_NOISE2 after the first xor. Some
/// third-party transcriptions incorrectly multiply by that constant.
/// </remarks>
public static class SquirrelNoise3
{
    private const uint BitNoise1 = 0xB5297A4D;
    private const uint BitNoise2 = 0x68E31DA4;
    private const uint BitNoise3 = 0x1B56C4E9;

    /// <summary>
    /// Produces a deterministic unsigned 32-bit value from a signed 32-bit
    /// position and unsigned 32-bit seed.
    /// </summary>
    public static uint Hash(int position, uint seed = 0)
    {
        unchecked
        {
            var mangled = (uint)position;
            mangled *= BitNoise1;
            mangled += seed;
            mangled ^= mangled >> 8;
            mangled += BitNoise2;
            mangled ^= mangled << 8;
            mangled *= BitNoise3;
            mangled ^= mangled >> 8;
            return mangled;
        }
    }
}
