# CartesianTransform example — diagram

```mermaid
classDiagram
    class CartesianTransformExample {
      <<top-level program>>
      +TransformPosition()
      +TransformDirection()
      +Inverse round-trip assertion
      +Scaled inverse rejection
    }
    class CartesianTransform3d {
      +TransformPosition(Vector3d) Vector3d
      +TransformDirection(Vector3d) Vector3d
      +Inverse() CartesianTransform3d
    }
    class Vector3d {
      <<readonly record struct>>
      +X
      +Y
      +Z
      +Length
    }
    CartesianTransformExample ..> CartesianTransform3d : exercises
    CartesianTransformExample ..> Vector3d : supplies and checks vectors
    CartesianTransform3d ..> Vector3d : transforms
```

## Use case

Demonstrate coordinate conversion and assert the contract from a copyable executable.

## Boundary

This diagram describes example orchestration, not a production application or universal coordinate convention.
