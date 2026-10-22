# GalaxyCellAddress.cs

**User moniker:** Validated top-level galaxy-grid coordinate

## Purpose

Identify one cell in the current 10×10×10 top-level galaxy grid and convert between zero-based coordinates and a one-based linear ordinal.

## Source and evidence

- Implementation: [GalaxyCellAddress.cs](../../src/raWWar/Spatiotemporal/GalaxyCellAddress.cs)
- Hierarchical child-address model: [HierarchicalSpatialAddress.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs)
- Binary serialization is separate: [HierarchicalSpatialAddressCodec.cs](../../src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs)
- Dedicated diagram: [GalaxyCellAddressUML.md](GalaxyCellAddressUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `public readonly record struct GalaxyCellAddress` |
| Grid | 10 cells per axis; 1,000 total |
| Coordinates | X, Y, Z are each in [0, 9] |
| Ordinal | One-based; X varies fastest, then Y, then Z |
| Reverse mapping | `FromOrdinal(1..1000)` |

## Validation

Construction rejects any coordinate outside the grid. `FromOrdinal` rejects values outside 1 through 1,000. `ToString` emits the coordinate tuple as `(X,Y,Z)`.

## Use case

Store a top-level cell as validated integer coordinates, or convert it to/from the stable one-based ordinal required by the current generation contract.

## Evidence boundary

This type maps only the top-level grid. It does not serialize recursive child paths, hash addresses, generate procedural features, or define physical dimensions.
