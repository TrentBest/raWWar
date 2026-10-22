# KeplerOrbit example — diagram

```mermaid
classDiagram
    class KeplerOrbitExample {
      <<top-level program>>
      +Construct circular orbit
      +Query epoch and period fractions
      +Check repeatability
      +Check unsupported boundaries
    }
    class KeplerOrbit {
      +Period double
      +PositionAt(time) Vector3d
    }
    class Vector3d {
      <<readonly record struct>>
      +X
      +Y
      +Z
      +Length
    }
    KeplerOrbitExample ..> KeplerOrbit : constructs and queries
    KeplerOrbit ..> Vector3d : returns position
    KeplerOrbitExample ..> Vector3d : compares positions
```

## Use case

Make the explicit logical-time orbit API easy to copy and verify.

## Boundary

An orbital model example only; no renderer or simulation loop is implied.
