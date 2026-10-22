# HierarchicalSpatialAddress example — Program.cs

**User moniker:** Canonical hierarchical address and stable digest walkthrough

## Purpose

Demonstrate root-cell addressing, nested 10×10×10 child coordinates, V1 encode/decode round-trip, versioned SHA-256 identity, and malformed-input validation.

## Source and evidence

- [Example source](../../examples/HierarchicalSpatialAddress/Program.cs)
- [Address model](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs)
- [V1 codec](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs)
- [V1 hasher](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs)
- [Dedicated diagram](HierarchicalSpatialAddressExampleUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Top-level console program |
| Input | Root ordinal 42 and two child levels |
| Checks | Decode equality, digest stability, trailing-byte rejection, coordinate range |
| Failure | Returns exit code 1 when a check fails |

## Use case

Run the sample to inspect the address, canonical V1 bytes, and SHA-256 digest. Appending an unexpected byte must cause `FormatException`; child coordinate 10 must cause `ArgumentOutOfRangeException`.

## Evidence boundary

The example explicitly treats `ToString()` and `GetHashCode()` as diagnostic/runtime facilities, not as a wire format or cross-process identity. The versioned codec and digest define those contracts.
