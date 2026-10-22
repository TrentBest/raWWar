# CartesianTransform3d — class diagram

```mermaid
classDiagram
    class CartesianTransform3d {
      <<readonly record struct>>
      +Identity CartesianTransform3d
      +TransformPosition(Vector3d) Vector3d
      +TransformDirection(Vector3d) Vector3d
      +Inverse() CartesianTransform3d
      +Compose(CartesianTransform3d) CartesianTransform3d
    }
    class Vector3d {
      <<readonly record struct>>
      +double X
      +double Y
      +double Z
      +double Length
    }
    CartesianTransform3d ..> Vector3d : maps points and directions
```

## Use case

Compose coordinate mappings or convert a point between explicit frames while keeping translation out of direction transforms.

## Boundary

No global axes or units are selected; inverse is restricted to orthonormal linear matrices.
