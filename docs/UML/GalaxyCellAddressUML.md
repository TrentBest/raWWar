# GalaxyCellAddress — class diagram

```mermaid
classDiagram
    class GalaxyCellAddress {
      <<readonly record struct>>
      +int DimensionX
      +int DimensionY
      +int DimensionZ
      +int CellCount
      +int X
      +int Y
      +int Z
      +int Ordinal
      +GalaxyCellAddress(x, y, z)
      +FromOrdinal(int) GalaxyCellAddress
      +ToString() string
    }
    class HierarchicalSpatialAddress {
      +At(GalaxyCellAddress) HierarchicalSpatialAddress
    }
    HierarchicalSpatialAddress ..> GalaxyCellAddress : uses as root cell
```

## Use case

Convert the one-based galaxy-cell ordinal into coordinates, then use those coordinates as the root of a hierarchical spatial address.

## Boundary

Top-level address mapping only; recursive encoding and hashing are separate contracts.
