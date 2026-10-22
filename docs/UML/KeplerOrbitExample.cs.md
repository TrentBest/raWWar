# KeplerOrbit example — Program.cs

**User moniker:** Explicit-time orbital position demonstration

## Purpose

Show how to construct an immutable circular Kepler orbit and query its position at epoch, quarter period, and one period. The example also asserts repeatability and rejects non-finite time and eccentricity at the parabolic boundary.

## Source and evidence

- [Example source](../../examples/KeplerOrbit/Program.cs)
- [Runtime implementation](../../src/raWWar/Spatiotemporal/KeplerOrbit.cs)
- [Runtime class companion](KeplerOrbit.cs.md)
- [Dedicated diagram](KeplerOrbitExampleUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Top-level console program |
| Model | Circular orbit with semi-major axis 10 and gravitational parameter 1 |
| Time | Explicit logical time; consistent caller-selected units |
| Failure | Returns exit code 1 when repeatability or boundary assertions fail |

## Use case

Run the sample to inspect the period and positions at selected logical times. Querying the same time twice should return the same position; one period should return to the starting point within the sample's tolerance.

## Evidence boundary

The runtime supports elliptic two-body orbits only (`0 <= e < 1`). This example does not integrate forces, perturbations, authoritative world state, or rendering.
