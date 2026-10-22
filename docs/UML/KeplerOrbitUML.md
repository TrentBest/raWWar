# KeplerOrbit — class diagram

```mermaid
classDiagram
    class KeplerOrbit {
      <<sealed record>>
      +double SemiMajorAxis
      +double Eccentricity
      +double Inclination
      +double LongitudeOfAscendingNode
      +double ArgumentOfPeriapsis
      +double MeanAnomalyAtEpoch
      +double EpochTime
      +double GravitationalParameter
      +PositionAt(double) Vector3d
      +Period double
      -SolveEccentricAnomaly(...)
      -Validate()
    }
    class Vector3d {
      <<readonly record struct>>
      +double X
      +double Y
      +double Z
      +double Length
    }
    KeplerOrbit ..> Vector3d : returns inertial position
```

## Use case

Evaluate an orbit at an explicit logical time, returning a framework-independent 3D vector.

## Boundary

Elliptic two-body model only; no frame-rate dependence or renderer integration.
