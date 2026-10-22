# HierarchicalSpatialAddressHasher.cs

**User moniker:** Stable digest for canonical spatial identity

## Purpose

Compute a versioned SHA-256 digest of the canonical V1 binary encoding of a hierarchical spatial address.

## Source and evidence

- Implementation: [HierarchicalSpatialAddressHasher.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs)
- Canonical bytes: [HierarchicalSpatialAddressCodec.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs)
- Address model: [HierarchicalSpatialAddress.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs)
- Dedicated diagram: [HierarchicalSpatialAddressHasherUML.md](HierarchicalSpatialAddressHasherUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public static class HierarchicalSpatialAddressHasher` |
| Version | `CurrentVersion = 1` |
| Entry point | `ComputeV1(HierarchicalSpatialAddress)` |
| Output | 32-byte SHA-256 digest |
| Input | Exact canonical V1 address bytes |

## Observed constraints

The method rejects a null address and hashes `HierarchicalSpatialAddressCodec.EncodeV1(address)`. Stable digest compatibility therefore depends on both the immutable V1 encoding and SHA-256 definition.

## Use case

Compute a deterministic digest to use as identity material for the same hierarchical address across processes. Keep the raw canonical encoding available when the serialized address itself is needed.

## Evidence boundary

The digest is not encryption, a digital signature, writer authentication, or proof of authorization. Changing the encoding or hash definition requires a separately versioned contract and test vectors.
