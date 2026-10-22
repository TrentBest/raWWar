# KeplerOrbit.cs

**User moniker:** Immutable logical-time elliptic orbit evaluator

## Purpose

Evaluate the inertial-frame position and orbital period of a three-dimensional Keplerian two-body ellipse at an explicit logical time, without relying on frame rate, camera state, or mutable simulation clocks.

## Source and evidence

- Implementation and vector type: [KeplerOrbit.cs](../../src/raWWar/Spatiotemporal/KeplerOrbit.cs)
- Example project: [KeplerOrbit example](../../examples/KeplerOrbit/)
- Dedicated diagram: [KeplerOrbitUML.md](KeplerOrbitUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public sealed record KeplerOrbit` |
| Output | `Vector3d` position at requested time |
| Supported eccentricity | `0 <= e < 1` (elliptic only) |
| Angles | Radians |
| Solver | Newton iteration, maximum 64 iterations, convergence tolerance `1e-13` |
| Period | Same time units as the supplied model |

## Validation and calculation

Semi-major axis and gravitational parameter must be finite and positive. Eccentricity must be finite and in the elliptic range. Angles, epoch, and requested time must be finite. The evaluator advances mean anomaly using mean motion, solves Kepler's equation for eccentric anomaly, computes the orbital-plane position, and rotates it into the inertial frame. Failure to converge throws `InvalidOperationException`.

All distance, time, and gravitational-parameter units must be mutually consistent; the implementation cannot infer or convert units.

## Use case

Construct an orbit from explicit orbital elements and evaluate `PositionAt(time)` for a deterministic logical timestamp. Use `Period` to obtain the orbital period in the same chosen time units.

## Evidence boundary

This is a two-body elliptic evaluator. It does not model perturbations, multi-body gravity, collisions, integration with authoritative entity state, or rendering. No accuracy claim beyond the implementation's stated numerical procedure is implied.
