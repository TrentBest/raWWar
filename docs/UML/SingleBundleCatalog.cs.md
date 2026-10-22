# SingleBundleCatalog.cs

**User moniker:** Minimal in-process MicroBundle catalog for composition tests

## Purpose

Provide an internal, single-entry implementation of `IMicroBundleCatalog` so contract tests can resolve the actual raWWar bundle by ID, with or without an exact version constraint.

## Source and evidence

- [Catalog source](../../tests/raWWar.ContractTests/SingleBundleCatalog.cs)
- [Contract-test runner](Program.cs)
- [raWWar bundle](../../src/raWWar/RaWWarMicroBundle.cs)
- [Dedicated diagram](SingleBundleCatalogUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Internal sealed class implementing `IMicroBundleCatalog` |
| Constructor | Requires a non-null `IMicroBundle` |
| Versioned resolution | ID must match and descriptor version must match with ordinal string comparison |
| ID-only resolution | ID must match |
| Miss behavior | Returns `false` and sets output bundle to `null` |

## Use case

Supply a deterministic local catalog to the FSM_COS composition check without depending on a remote repository, network lookup, or artifact materialization.

## Evidence boundary

This is a test double with exactly one candidate bundle. It is not a production catalog, package resolver, repository client, or dependency graph implementation.
