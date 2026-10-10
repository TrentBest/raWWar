# raWWar Examples

This directory is the **usage-example source of truth** for raWWar-owned code and consumed Workshop contracts. Examples should be practical, copyable, and traceable to the implementation or dependency version they demonstrate.

## Examples

| Example | What it demonstrates | Verification boundary |
|---|---|---|
| [KeplerOrbit](KeplerOrbit/Program.cs) | Constructing an immutable elliptic orbit, querying explicit logical times, checking repeatability, and rejecting non-finite logical time and eccentricity outside the supported elliptic domain. | The project is in the solution and references the real raWWar project. Matching numerical contract checks live in [the contract-test program](../tests/raWWar.ContractTests/Program.cs). Run the example with the command below. |
| [Authoring manifest](../manifest.json) and [runtime manifest](../runtime-manifest.json) | Separating Experience metadata from FSM_COS root bundle IDs and requested versions. | Manifest-shape and identity checks are in the contract-test program. This is not AnyApp publication-manifest or end-to-end host-loading proof. |
| [Workshop composition contract](../data/workshop-composition-contract.json) | Recording ownership boundaries, current verified FSM_COS contract, proposed reusable capabilities, and explicit non-publication status as data. | Architecture assertions are in the contract-test program. Proposed capabilities are not implemented capabilities. |

## Implemented contracts that still need dedicated examples

The following raWWar-owned contracts have executable checks in [the contract-test program](../tests/raWWar.ContractTests/Program.cs), but are not yet presented as dedicated, copyable usage examples. Treat the tests as current verification evidence—not as a substitute for reader-facing examples.

| Contract | Current evidence | Example gap |
|---|---|---|
| [Hierarchical spatial address V1](../src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs) | Canonical encoding/decoding and stable SHA-256 vectors are checked by the contract suite. | Add a focused example showing address construction, encoding, decoding, and validation boundaries. |
| [Cartesian transform](../src/raWWar/Spatiotemporal/CartesianTransform3d.cs) | Identity, rotation/translation, composition order, inverse restrictions, and invalid inputs are checked by the contract suite. | Add a focused example distinguishing point transforms from direction transforms and explaining the inverse precondition. |
| [Event history and resource replay](../docs/EVENT_HISTORY_AND_REPLAY_CONTRACT.md) | The contract suite checks stable event identity, immutable payload handling, in-memory commit semantics, ordering, stream boundaries, malformed events, and checked resource arithmetic. | Add a focused example of event creation, commit, and replay; explicitly label the ledger as process-local and replay as a narrow resource-balance reducer. |

These contracts are useful implemented slices, not evidence of a complete galaxy simulation, durable event store, distributed exactly-once processing, checkpoint migration, or host-level world advancement.

## In-progress first-person slice

- [Fighter Pilot Action Slice](../docs/FIGHTER_PILOT_ACTION_SLICE.md) — a **candidate** design sketch for connecting a future station interaction to an authoritative aircraft-state transition, causal history, and observable feedback. It is not evidence of an existing station implementation, not a selected implementation commitment, and not a runnable example. Reconcile it with canonical game-design decisions before using it to drive code.

## Run the executable example

From the repository root:

```sh
dotnet run --project examples/KeplerOrbit/raWWar.KeplerOrbitExample.csproj
```

## Rules for future examples

When raWWar consumes another Workshop package, API, schema, data catalogue, or capability:

1. Add a checked-in example **from raWWar's perspective** showing the actual intended use. Prefer a runnable example; if the contract cannot yet run, label it explicitly as a proposal or fixture.
2. Record the exact dependency/package version or source commit the example was verified against. Do not make development-branch APIs look available in a published package.
3. Include realistic success, boundary, and failure cases where applicable—not just the shortest happy path.
4. Link the example from this index and the relevant architecture/API documentation. Documentation should draw from the full relevant example set, not repeat a lone illustrative snippet.
5. Keep examples aligned with executable contract tests. Where practical, build/run examples in CI; do not claim verification merely because a file exists.
6. State what the example does **not** prove, especially for external hosting, persistence, networking, rendering, or release readiness.

The example is also a handoff artifact: another agent should be able to inspect how raWWar expects to consume a capability without reverse-engineering intent from prose alone. Examples demonstrate usage; the owning package's source and published contract remain authoritative.
