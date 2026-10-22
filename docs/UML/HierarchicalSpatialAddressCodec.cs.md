# HierarchicalSpatialAddressCodec.cs

**User moniker:** Versioned canonical spatial-address bytes

## Purpose

Encode and decode hierarchical spatial addresses using the canonical V1 binary layout. This is identity serialization, not physical coordinates or a hash.

## Source and evidence

- Implementation: [HierarchicalSpatialAddressCodec.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs)
- Address model: [HierarchicalSpatialAddress.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs)
- Root grid mapping: [GalaxyCellAddress.cs](GalaxyCellAddress.cs.md)
- Stable digest consumer: [HierarchicalSpatialAddressHasher.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs)
- Dedicated diagram: [HierarchicalSpatialAddressCodecUML.md](HierarchicalSpatialAddressCodecUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public static class HierarchicalSpatialAddressCodec` |
| Version | `CurrentVersion = 1` |
| Header | `RWSA`, version byte, 16-bit big-endian root ordinal, 32-bit big-endian depth |
| Children | Three bytes per child, X/Y/Z each in [0, 9] |
| Length | Exactly 11 + 3 × depth bytes; trailing bytes are rejected |

## Validation

Decode rejects short headers, wrong magic, unsupported version, depth beyond supported in-memory range, length mismatch, invalid root ordinal, and child coordinates outside the 10×10×10 grid. The codec uses checked length calculations and validates declared length before constructing the recursive address.

## Use case

Persist or transmit the canonical bytes for a hierarchical address, then decode those bytes into the corresponding address structure. Existing V1 bytes are documented as immutable; an incompatible layout requires a new version.

## Evidence boundary

The codec does not select a hash algorithm, generate spatial features, or map addresses to physical positions. Versioned byte stability is a compatibility contract, not a cryptographic authenticity guarantee.
