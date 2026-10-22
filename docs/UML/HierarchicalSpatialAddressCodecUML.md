# HierarchicalSpatialAddressCodec — class diagram

```mermaid
classDiagram
    class HierarchicalSpatialAddressCodec {
      <<static>>
      +byte CurrentVersion
      +Encode(HierarchicalSpatialAddress) byte[]
      +EncodeV1(HierarchicalSpatialAddress) byte[]
      +Decode(ReadOnlySpan~byte~) HierarchicalSpatialAddress
    }
    class HierarchicalSpatialAddress {
      +GalaxyCellAddress Root
      +int Depth
      +Child(x, y, z) HierarchicalSpatialAddress
    }
    class GalaxyCellAddress {
      +int Ordinal
      +FromOrdinal(int) GalaxyCellAddress
    }
    HierarchicalSpatialAddressCodec ..> HierarchicalSpatialAddress : encodes and reconstructs
    HierarchicalSpatialAddressCodec ..> GalaxyCellAddress : validates root and child grid
```

## Use case

Encode a hierarchy using the explicit V1 layout, or decode and validate external bytes before using the reconstructed address.

## Boundary

Canonical identity bytes only; hashing and physical coordinate conversion are separate concerns.
