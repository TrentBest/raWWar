# CartesianTransform3d.cs

**User moniker:** Explicit Cartesian-frame transform

## Purpose

Represent an affine mapping between caller-defined three-dimensional Cartesian frames without imposing global axes, handedness, or units. It transforms positions and directions, composes mappings, and computes an inverse only when the linear matrix is orthonormal.

## Source and evidence

- Implementation: [CartesianTransform3d.cs](../../src/raWWar/Spatiotemporal/CartesianTransform3d.cs)
- Value type: `Vector3d`, declared in the same source file
- Dedicated diagram: [CartesianTransform3dUML.md](CartesianTransform3dUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public readonly record struct CartesianTransform3d` |
| Matrix | Row-major 3×3 linear transform |
| Translation | Expressed in destination-frame units |
| Position | Applies linear transform and translation |
| Direction | Applies linear transform only |
| Inverse | Requires orthonormal matrix within tolerance `1e-10` |
| Composition | Applies the supplied source-to-middle transform first, then this transform |

## Validation and limits

Matrix, translation, and input vector components must be finite. `Inverse` rejects scale and shear because it uses transpose as the inverse of an orthonormal matrix. This is a coordinate-representation operation, not an authoritative simulation-state mutation.

## Use case

Define a transform between two explicitly named frames, transform a point or displacement, and compose transforms when converting through an intermediate frame. Use consistent units at the call site; the type cannot infer them.

## Evidence boundary

This type does not define a global coordinate convention, units, physics integration, camera transform, or entity-state ownership.
