namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// An explicit affine transform between two caller-defined Cartesian frames.
/// The 3x3 matrix is row-major and maps source-frame column coordinates into
/// destination-frame coordinates; translation is expressed in destination units.
/// This type deliberately does not choose raWWar-wide axes, handedness, or units.
/// </summary>
/// <remarks>
/// Inverse is available only for orthonormal rotation/reflection matrices.
/// Scale, shear, and singular matrices are therefore not accepted by <see cref="Inverse"/>.
/// Transforming a coordinate is a representation change, not an authoritative state mutation.
/// </remarks>
public readonly record struct CartesianTransform3d(
    double M11, double M12, double M13,
    double M21, double M22, double M23,
    double M31, double M32, double M33,
    double Tx, double Ty, double Tz)
{
    private const double OrthonormalTolerance = 1e-10;

    /// <summary>The identity mapping in one explicitly chosen coordinate convention.</summary>
    public static CartesianTransform3d Identity => new(
        1, 0, 0,
        0, 1, 0,
        0, 0, 1,
        0, 0, 0);

    /// <summary>Maps a point, including translation, from source to destination coordinates.</summary>
    public Vector3d TransformPosition(Vector3d position)
    {
        ValidateFinite();
        ValidateFinite(position, nameof(position));
        return new Vector3d(
            M11 * position.X + M12 * position.Y + M13 * position.Z + Tx,
            M21 * position.X + M22 * position.Y + M23 * position.Z + Ty,
            M31 * position.X + M32 * position.Y + M33 * position.Z + Tz);
    }

    /// <summary>Maps a direction/displacement; translation is intentionally ignored.</summary>
    public Vector3d TransformDirection(Vector3d direction)
    {
        ValidateFinite();
        ValidateFinite(direction, nameof(direction));
        return new Vector3d(
            M11 * direction.X + M12 * direction.Y + M13 * direction.Z,
            M21 * direction.X + M22 * direction.Y + M23 * direction.Z,
            M31 * direction.X + M32 * direction.Y + M33 * direction.Z);
    }

    /// <summary>Returns the inverse mapping when the linear part is orthonormal.</summary>
    public CartesianTransform3d Inverse()
    {
        ValidateFinite();
        if (!IsOrthonormal())
            throw new InvalidOperationException(
                "Inverse requires an orthonormal 3x3 matrix; scale and shear are not supported.");

        // For an orthonormal matrix, inverse(R) = transpose(R).
        var i11 = M11; var i12 = M21; var i13 = M31;
        var i21 = M12; var i22 = M22; var i23 = M32;
        var i31 = M13; var i32 = M23; var i33 = M33;

        return new CartesianTransform3d(
            i11, i12, i13,
            i21, i22, i23,
            i31, i32, i33,
            -(i11 * Tx + i12 * Ty + i13 * Tz),
            -(i21 * Tx + i22 * Ty + i23 * Tz),
            -(i31 * Tx + i32 * Ty + i33 * Tz));
    }

    /// <summary>
    /// Composes source-to-middle with middle-to-destination, returning source-to-destination.
    /// The result applies <paramref name="sourceToMiddle"/> first, then this transform.
    /// </summary>
    public CartesianTransform3d Compose(CartesianTransform3d sourceToMiddle)
    {
        ValidateFinite();
        sourceToMiddle.ValidateFinite();

        var a = this;
        var b = sourceToMiddle;
        return new CartesianTransform3d(
            a.M11*b.M11 + a.M12*b.M21 + a.M13*b.M31,
            a.M11*b.M12 + a.M12*b.M22 + a.M13*b.M32,
            a.M11*b.M13 + a.M12*b.M23 + a.M13*b.M33,
            a.M21*b.M11 + a.M22*b.M21 + a.M23*b.M31,
            a.M21*b.M12 + a.M22*b.M22 + a.M23*b.M32,
            a.M21*b.M13 + a.M22*b.M23 + a.M23*b.M33,
            a.M31*b.M11 + a.M32*b.M21 + a.M33*b.M31,
            a.M31*b.M12 + a.M32*b.M22 + a.M33*b.M32,
            a.M31*b.M13 + a.M32*b.M23 + a.M33*b.M33,
            a.M11*b.Tx + a.M12*b.Ty + a.M13*b.Tz + a.Tx,
            a.M21*b.Tx + a.M22*b.Ty + a.M23*b.Tz + a.Ty,
            a.M31*b.Tx + a.M32*b.Ty + a.M33*b.Tz + a.Tz);
    }

    private bool IsOrthonormal()
    {
        var r1 = M11*M11 + M12*M12 + M13*M13;
        var r2 = M21*M21 + M22*M22 + M23*M23;
        var r3 = M31*M31 + M32*M32 + M33*M33;
        var d12 = M11*M21 + M12*M22 + M13*M23;
        var d13 = M11*M31 + M12*M32 + M13*M33;
        var d23 = M21*M31 + M22*M32 + M23*M33;
        return Math.Abs(r1 - 1) <= OrthonormalTolerance
            && Math.Abs(r2 - 1) <= OrthonormalTolerance
            && Math.Abs(r3 - 1) <= OrthonormalTolerance
            && Math.Abs(d12) <= OrthonormalTolerance
            && Math.Abs(d13) <= OrthonormalTolerance
            && Math.Abs(d23) <= OrthonormalTolerance;
    }

    private void ValidateFinite()
    {
        if (!double.IsFinite(M11) || !double.IsFinite(M12) || !double.IsFinite(M13)
            || !double.IsFinite(M21) || !double.IsFinite(M22) || !double.IsFinite(M23)
            || !double.IsFinite(M31) || !double.IsFinite(M32) || !double.IsFinite(M33)
            || !double.IsFinite(Tx) || !double.IsFinite(Ty) || !double.IsFinite(Tz))
            throw new ArgumentOutOfRangeException(nameof(CartesianTransform3d),
                "Transform matrix and translation components must all be finite.");
    }

    private static void ValidateFinite(Vector3d value, string parameterName)
    {
        if (!double.IsFinite(value.X) || !double.IsFinite(value.Y) || !double.IsFinite(value.Z))
            throw new ArgumentOutOfRangeException(parameterName, "Vector components must all be finite.");
    }
}
