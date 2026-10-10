namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// One integer coordinate in a subdivided parent cell's 10×10×10 child grid.
/// It is not a globally unique cell ordinal.
/// </summary>
public readonly record struct ChildCellCoordinate
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public ChildCellCoordinate(int x, int y, int z)
    {
        if (x is < 0 or >= GalaxyCellAddress.DimensionX)
            throw new ArgumentOutOfRangeException(nameof(x), x, "Child X must be in [0, 9].");
        if (y is < 0 or >= GalaxyCellAddress.DimensionY)
            throw new ArgumentOutOfRangeException(nameof(y), y, "Child Y must be in [0, 9].");
        if (z is < 0 or >= GalaxyCellAddress.DimensionZ)
            throw new ArgumentOutOfRangeException(nameof(z), z, "Child Z must be in [0, 9].");

        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString() => $"({X},{Y},{Z})";
}

/// <summary>
/// An in-memory hierarchical spatial address: one top-level galaxy cell followed
/// by zero or more child-grid coordinates, one for each subdivision depth.
/// </summary>
/// <remarks>
/// This type defines hierarchy and value equality, not canonical serialization,
/// stable hashing, physical coordinates, or random generation. Do not use
/// <see cref="object.GetHashCode"/> as a persistent or cross-process identity.
/// The wire encoding and byte order remain a separate versioned contract decision.
/// </remarks>
public sealed class HierarchicalSpatialAddress : IEquatable<HierarchicalSpatialAddress>
{
    private readonly ChildCellCoordinate[] _children;
    private readonly IReadOnlyList<ChildCellCoordinate> _readOnlyChildren;

    public GalaxyCellAddress Root { get; }
    public int Depth => _children.Length;

    /// <summary>Child coordinates from depth 1 through <see cref="Depth"/>.</summary>
    public IReadOnlyList<ChildCellCoordinate> Children => _readOnlyChildren;

    private HierarchicalSpatialAddress(GalaxyCellAddress root, ChildCellCoordinate[] children)
    {
        Root = root;
        _children = children;
        _readOnlyChildren = Array.AsReadOnly(_children);
    }

    public static HierarchicalSpatialAddress At(GalaxyCellAddress root) =>
        new(root, Array.Empty<ChildCellCoordinate>());

    /// <summary>Returns a new address one subdivision level below this address.</summary>
    public HierarchicalSpatialAddress Child(int x, int y, int z)
    {
        var next = new ChildCellCoordinate[_children.Length + 1];
        Array.Copy(_children, next, _children.Length);
        next[^1] = new ChildCellCoordinate(x, y, z);
        return new HierarchicalSpatialAddress(Root, next);
    }

    public bool Equals(HierarchicalSpatialAddress? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null || Root != other.Root || _children.Length != other._children.Length)
            return false;

        return _children.AsSpan().SequenceEqual(other._children);
    }

    public override bool Equals(object? obj) =>
        obj is HierarchicalSpatialAddress other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Root);
        foreach (var child in _children) hash.Add(child);
        return hash.ToHashCode();
    }

    /// <summary>Diagnostic display only; this is not a canonical serialized form.</summary>
    public override string ToString() =>
        _children.Length == 0
            ? Root.ToString()
            : $"{Root}/{string.Join("/", _children.Select(child => child.ToString()))}";
}
