# HierarchicalSpatialAddressHasher — class diagram

```mermaid
classDiagram
    class HierarchicalSpatialAddressHasher {
      <<static>>
      +int CurrentVersion
      +ComputeV1(HierarchicalSpatialAddress) byte[]
    }
    class HierarchicalSpatialAddressCodec {
      <<static>>
      +EncodeV1(HierarchicalSpatialAddress) byte[]
    }
    class SHA256 {
      <<cryptographic primitive>>
    }
    HierarchicalSpatialAddressHasher ..> HierarchicalSpatialAddressCodec : canonical bytes
    HierarchicalSpatialAddressHasher ..> SHA256 : 32-byte digest
```

## Use case

Hash canonical V1 address bytes when a compact deterministic identity digest is useful.

## Boundary

Digest only; no encryption, authentication, or coordinate conversion.
