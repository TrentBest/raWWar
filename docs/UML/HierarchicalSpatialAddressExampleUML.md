# HierarchicalSpatialAddress example — diagram

```mermaid
classDiagram
    class HierarchicalSpatialAddressExample {
      <<top-level program>>
      +Build nested address
      +Encode and decode V1
      +Compare stable digest
      +Reject malformed input
    }
    class GalaxyCellAddress {
      +FromOrdinal(int)
    }
    class HierarchicalSpatialAddress {
      +At(root)
      +Child(x, y, z)
    }
    class HierarchicalSpatialAddressCodec {
      +EncodeV1(address) byte[]
      +Decode(bytes)
    }
    class HierarchicalSpatialAddressHasher {
      +ComputeV1(address) byte[]
    }
    HierarchicalSpatialAddressExample ..> GalaxyCellAddress : creates root
    HierarchicalSpatialAddressExample ..> HierarchicalSpatialAddress : builds levels
    HierarchicalSpatialAddressExample ..> HierarchicalSpatialAddressCodec : round-trips canonical bytes
    HierarchicalSpatialAddressExample ..> HierarchicalSpatialAddressHasher : checks stable digest
```

## Use case

Validate that hierarchical addresses have explicit, versioned serialization and hashing.

## Boundary

This is a console proof of the current V1 contract, not a claim about future codec versions.
