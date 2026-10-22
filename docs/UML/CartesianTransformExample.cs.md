# CartesianTransform example — Program.cs

**User moniker:** Point-versus-direction coordinate-frame walkthrough

## Purpose

Runnable console example for the `CartesianTransform3d` contract: transform a point with rotation and translation, transform a direction without translation, invert an orthonormal transform, and reject a scaled matrix whose inverse is outside the current contract.

## Source and evidence

- [Example source](../../examples/CartesianTransform/Program.cs)
- [Runtime implementation](../../src/raWWar/Spatiotemporal/CartesianTransform3d.cs)
- [Runtime class companion](CartesianTransform3d.cs.md)
- [Dedicated diagram](CartesianTransformExampleUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Top-level console program |
| Input | Fixed transform and `Vector3d(1, 0, 0)` |
| Output | Transformed point/direction and boundary-check messages |
| Failure | Returns exit code 1 when a numeric or rejection assertion fails |

## Use case

Run the example to see that the point becomes (5, 7, 7), while its direction becomes (0, 1, 0). The inverse must recover the original point within tolerance (10^{-12}). A scale matrix must throw `InvalidOperationException` when inverted.

## Evidence boundary

The example uses caller-defined frame conventions and consistent arbitrary units. It does not define global axes, handedness, unit conversions, or general affine inversion.
