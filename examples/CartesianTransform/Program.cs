using TheSingularityWorkshop.raWWar.Spatiotemporal;

// This example uses caller-defined frame conventions and arbitrary consistent units.
// CartesianTransform3d does not impose a raWWar-wide axis, handedness, or unit system.

// A point is rotated 90 degrees around Z, then translated into the destination frame.
var sourceToDestination = new CartesianTransform3d(
    0, -1, 0,
    1,  0, 0,
    0,  0, 1,
    5,  6, 7);

var sourcePoint = new Vector3d(1, 0, 0);
var destinationPoint = sourceToDestination.TransformPosition(sourcePoint);
var destinationDirection = sourceToDestination.TransformDirection(sourcePoint);

Console.WriteLine($"Point:     ({destinationPoint.X:G6}, {destinationPoint.Y:G6}, {destinationPoint.Z:G6})");
Console.WriteLine($"Direction: ({destinationDirection.X:G6}, {destinationDirection.Y:G6}, {destinationDirection.Z:G6})");

// Translation affects a point but deliberately does not affect a direction/displacement.
if ((destinationPoint - new Vector3d(5, 7, 7)).Length > 1e-12
    || (destinationDirection - new Vector3d(0, 1, 0)).Length > 1e-12)
{
    Console.Error.WriteLine("Point/direction transform example failed.");
    return 1;
}

// For an orthonormal matrix (rotation or reflection), inverse mapping is supported.
var recoveredPoint = sourceToDestination.Inverse().TransformPosition(destinationPoint);
if ((recoveredPoint - sourcePoint).Length > 1e-12)
{
    Console.Error.WriteLine("Inverse transform did not recover the source point.");
    return 1;
}

// Scale and shear are intentionally outside the current inverse contract.
var scaled = new CartesianTransform3d(
    2, 0, 0,
    0, 1, 0,
    0, 0, 1,
    0, 0, 0);

try
{
    _ = scaled.Inverse();
    Console.Error.WriteLine("Expected scaled inverse to be rejected.");
    return 1;
}
catch (InvalidOperationException)
{
    Console.WriteLine("Boundary check passed: scaled inverse is rejected.");
}

Console.WriteLine("Cartesian transform example passed.");
return 0;
