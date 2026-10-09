using TheSingularityWorkshop.raWWar.Spatiotemporal;

// Copyable usage example for raWWar's explicit-time, immutable orbit model.
// Units must be internally consistent; this example uses arbitrary distance/time units.
var orbit = new KeplerOrbit(
    SemiMajorAxis: 10,
    Eccentricity: 0,
    Inclination: 0,
    LongitudeOfAscendingNode: 0,
    ArgumentOfPeriapsis: 0,
    MeanAnomalyAtEpoch: 0,
    EpochTime: 0,
    GravitationalParameter: 1);

var start = orbit.PositionAt(time: 0);
var quarterPeriod = orbit.PositionAt(time: orbit.Period / 4);
var oneFullPeriod = orbit.PositionAt(time: orbit.Period);

Console.WriteLine($"Period: {orbit.Period:G6} time units");
Console.WriteLine($"t = 0:              ({start.X:G6}, {start.Y:G6}, {start.Z:G6})");
Console.WriteLine($"t = period / 4:     ({quarterPeriod.X:G6}, {quarterPeriod.Y:G6}, {quarterPeriod.Z:G6})");
Console.WriteLine($"t = period:         ({oneFullPeriod.X:G6}, {oneFullPeriod.Y:G6}, {oneFullPeriod.Z:G6})");

// A repeated query at the same logical time is stable; no mutable simulation clock is used.
var repeated = orbit.PositionAt(time: 0);
if ((repeated - start).Length > 1e-12 || (oneFullPeriod - start).Length > 1e-9)
{
    Console.Error.WriteLine("Orbit example failed its repeatability check.");
    return 1;
}

return 0;
