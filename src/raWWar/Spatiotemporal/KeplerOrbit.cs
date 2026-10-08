namespace TheSingularityWorkshop.raWWar.Spatiotemporal;

/// <summary>
/// A three-dimensional Keplerian two-body orbit evaluated at an explicit logical time.
/// All distance, time, and gravitational-parameter units must be mutually consistent.
/// Angles are in radians. The evaluator is immutable and has no dependency on frame rate,
/// camera state, random-call order, or mutable simulation clocks.
/// </summary>
public sealed record KeplerOrbit(
    double SemiMajorAxis,
    double Eccentricity,
    double Inclination,
    double LongitudeOfAscendingNode,
    double ArgumentOfPeriapsis,
    double MeanAnomalyAtEpoch,
    double EpochTime,
    double GravitationalParameter)
{
    private const int MaximumIterations = 64;
    private const double ConvergenceTolerance = 1e-13;

    /// <summary>Returns the inertial-frame position at the requested logical time.</summary>
    public Vector3d PositionAt(double time)
    {
        Validate();

        if (!double.IsFinite(time))
            throw new ArgumentOutOfRangeException(nameof(time), "Time must be finite.");

        var meanMotion = Math.Sqrt(GravitationalParameter / Math.Pow(SemiMajorAxis, 3));
        var meanAnomaly = NormalizeRadians(MeanAnomalyAtEpoch + meanMotion * (time - EpochTime));
        var eccentricAnomaly = SolveEccentricAnomaly(meanAnomaly, Eccentricity);

        var orbitalX = SemiMajorAxis * (Math.Cos(eccentricAnomaly) - Eccentricity);
        var orbitalY = SemiMajorAxis * Math.Sqrt(1 - Eccentricity * Eccentricity)
                       * Math.Sin(eccentricAnomaly);

        var cosNode = Math.Cos(LongitudeOfAscendingNode);
        var sinNode = Math.Sin(LongitudeOfAscendingNode);
        var cosInclination = Math.Cos(Inclination);
        var sinInclination = Math.Sin(Inclination);
        var cosPeriapsis = Math.Cos(ArgumentOfPeriapsis);
        var sinPeriapsis = Math.Sin(ArgumentOfPeriapsis);

        var x = (cosNode * cosPeriapsis - sinNode * sinPeriapsis * cosInclination) * orbitalX
              + (-cosNode * sinPeriapsis - sinNode * cosPeriapsis * cosInclination) * orbitalY;
        var y = (sinNode * cosPeriapsis + cosNode * sinPeriapsis * cosInclination) * orbitalX
              + (-sinNode * sinPeriapsis + cosNode * cosPeriapsis * cosInclination) * orbitalY;
        var z = (sinPeriapsis * sinInclination) * orbitalX
              + (cosPeriapsis * sinInclination) * orbitalY;

        return new Vector3d(x, y, z);
    }

    /// <summary>Returns the orbital period in the same time units used by the model.</summary>
    public double Period
    {
        get
        {
            Validate();
            return 2 * Math.PI * Math.Sqrt(Math.Pow(SemiMajorAxis, 3) / GravitationalParameter);
        }
    }

    private void Validate()
    {
        if (!double.IsFinite(SemiMajorAxis) || SemiMajorAxis <= 0)
            throw new ArgumentOutOfRangeException(nameof(SemiMajorAxis), "Semi-major axis must be finite and positive.");
        if (!double.IsFinite(Eccentricity) || Eccentricity < 0 || Eccentricity >= 1)
            throw new ArgumentOutOfRangeException(nameof(Eccentricity), "This evaluator supports elliptic orbits only (0 <= e < 1).");
        if (!double.IsFinite(Inclination) || !double.IsFinite(LongitudeOfAscendingNode)
            || !double.IsFinite(ArgumentOfPeriapsis) || !double.IsFinite(MeanAnomalyAtEpoch)
            || !double.IsFinite(EpochTime))
            throw new ArgumentOutOfRangeException(nameof(Inclination), "Orbital angles and epoch must be finite.");
        if (!double.IsFinite(GravitationalParameter) || GravitationalParameter <= 0)
            throw new ArgumentOutOfRangeException(nameof(GravitationalParameter), "Gravitational parameter must be finite and positive.");
    }

    private static double SolveEccentricAnomaly(double meanAnomaly, double eccentricity)
    {
        var estimate = eccentricity < 0.8 ? meanAnomaly : Math.PI;
        for (var iteration = 0; iteration < MaximumIterations; iteration++)
        {
            var residual = estimate - eccentricity * Math.Sin(estimate) - meanAnomaly;
            var derivative = 1 - eccentricity * Math.Cos(estimate);
            var delta = residual / derivative;
            estimate -= delta;
            if (Math.Abs(delta) <= ConvergenceTolerance)
                return estimate;
        }

        throw new InvalidOperationException("Kepler equation solver did not converge for this orbit.");
    }

    private static double NormalizeRadians(double angle)
    {
        angle %= 2 * Math.PI;
        if (angle > Math.PI) angle -= 2 * Math.PI;
        if (angle < -Math.PI) angle += 2 * Math.PI;
        return angle;
    }
}

/// <summary>A small immutable vector type that keeps the core model independent of rendering frameworks.</summary>
public readonly record struct Vector3d(double X, double Y, double Z)
{
    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static Vector3d operator -(Vector3d left, Vector3d right) =>
        new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
}
