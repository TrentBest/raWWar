namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// Identifies one of the 1,000 top-level galaxy cells using the current v1
/// galaxy-generation contract. Coordinates are zero-based; the public ordinal
/// is one-based and linearized with X changing fastest, then Y, then Z.
/// </summary>
/// <remarks>
/// This type only implements the top-level grid mapping. It is not the canonical
/// recursive spatial-address serialization and does not hash addresses or
/// generate random features; those remain separate versioned contract work.
/// </remarks>
public readonly record struct GalaxyCellAddress
{
    public const int DimensionX = 10;
    public const int DimensionY = 10;
    public const int DimensionZ = 10;
    public const int CellCount = DimensionX * DimensionY * DimensionZ;

    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public int Ordinal => checked(1 + X + DimensionX * Y + DimensionX * DimensionY * Z);

    public GalaxyCellAddress(int x, int y, int z)
    {
        if (x < 0 || x >= DimensionX)
            throw new ArgumentOutOfRangeException(nameof(x), x, $"X must be in [0, {DimensionX - 1}].");
        if (y < 0 || y >= DimensionY)
            throw new ArgumentOutOfRangeException(nameof(y), y, $"Y must be in [0, {DimensionY - 1}].");
        if (z < 0 || z >= DimensionZ)
            throw new ArgumentOutOfRangeException(nameof(z), z, $"Z must be in [0, {DimensionZ - 1}].");

        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>Returns the address represented by a one-based top-level cell ordinal.</summary>
    public static GalaxyCellAddress FromOrdinal(int ordinal)
    {
        if (ordinal < 1 || ordinal > CellCount)
            throw new ArgumentOutOfRangeException(nameof(ordinal), ordinal, $"Ordinal must be in [1, {CellCount}].");

        var zeroBased = ordinal - 1;
        return new GalaxyCellAddress(
            zeroBased % DimensionX,
            (zeroBased / DimensionX) % DimensionY,
            zeroBased / (DimensionX * DimensionY));
    }

    public override string ToString() => $"({X},{Y},{Z})";
}
