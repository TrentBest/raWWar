using TheSingularityWorkshop.raWWar.Spatiotemporal;

var failures = new List<string>();
var checks = 0;

void Check(bool condition, string message)
{
    checks++;
    if (!condition) failures.Add(message);
}

void Near(double actual, double expected, double tolerance, string message) =>
    Check(Math.Abs(actual - expected) <= tolerance, $"{message}: expected {expected}, got {actual}");

var circular = new KeplerOrbit(
    SemiMajorAxis: 10,
    Eccentricity: 0,
    Inclination: 0,
    LongitudeOfAscendingNode: 0,
    ArgumentOfPeriapsis: 0,
    MeanAnomalyAtEpoch: 0,
    EpochTime: 0,
    GravitationalParameter: 1);

var period = circular.Period;
var start = circular.PositionAt(0);
var quarter = circular.PositionAt(period / 4);
var halfway = circular.PositionAt(period / 2);
var afterPeriod = circular.PositionAt(period);

Near(start.X, 10, 1e-10, "Circular orbit starts at positive X");
Near(start.Y, 0, 1e-10, "Circular orbit starts at zero Y");
Near(quarter.X, 0, 1e-9, "Quarter-period X");
Near(quarter.Y, 10, 1e-9, "Quarter-period Y");
Near(halfway.X, -10, 1e-9, "Half-period X");
Near(halfway.Y, 0, 1e-9, "Half-period Y");
Near((afterPeriod - start).Length, 0, 1e-9, "Orbit repeats after one period");

// Queries are pure: asking for times in a different order must not change any result.
var laterFirst = circular.PositionAt(period * 0.37);
_ = circular.PositionAt(period * 0.91);
var laterAgain = circular.PositionAt(period * 0.37);
Near((laterAgain - laterFirst).Length, 0, 1e-12, "Repeated time query is stable");

// Elliptical orbit: periapsis and apoapsis radii are a(1-e) and a(1+e).
var elliptical = circular with { SemiMajorAxis = 20, Eccentricity = 0.5 };
Near(elliptical.PositionAt(0).Length, 10, 1e-9, "Elliptical periapsis radius");
Near(elliptical.PositionAt(elliptical.Period / 2).Length, 30, 1e-8, "Elliptical apoapsis radius");

// Inclination rotates the orbit out of the reference plane.
var inclined = circular with { Inclination = Math.PI / 2 };
var inclinedQuarter = inclined.PositionAt(inclined.Period / 4);
Near(inclinedQuarter.X, 0, 1e-9, "Inclined quarter-period X");
Near(inclinedQuarter.Y, 0, 1e-9, "Inclined quarter-period Y");
Near(inclinedQuarter.Z, 10, 1e-9, "Inclined quarter-period Z");

var rejectedInvalid = false;
try { _ = circular with { Eccentricity = 1 }.PositionAt(0); }
catch (ArgumentOutOfRangeException) { rejectedInvalid = true; }
Check(rejectedInvalid, "Parabolic/hyperbolic eccentricity must be rejected explicitly");

Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");
foreach (var failure in failures) Console.Error.WriteLine($"FAIL: {failure}");
return failures.Count == 0 ? 0 : 1;
