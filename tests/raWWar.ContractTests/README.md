# raWWar Contract Tests

This .NET 8 console project checks executable mathematical, data-integrity, and architectural contracts without adding a test-framework dependency. It references the current AnyApp FSM_COS package only for a small composition contract proof.

Run from the repository root:

```sh
dotnet run --project tests/raWWar.ContractTests/raWWar.ContractTests.csproj
```

## Current coverage

The historical executable suite reported **195/195 checks passed** on [CI run 38001820718](https://github.com/TrentBest/raWWar/actions/runs/38001820718) at code/test head `7ec40343f15a15639951a096165169cf991d7920`. That implementation and its tests were subsequently reverted, so this historical result does not describe the current branch or a runnable suite here. The checks at that revision included:

- stable event-identity V1 reference encoding, immutable event metadata/payloads, in-memory idempotent commits, conflicting-payload detection, address-scoped stream reads, domain-local ordering, domain isolation, and concurrent duplicate retries;
- explicit-time circular and elliptic Kepler-orbit positions, period repeatability, 3D inclination, query purity/order independence, and explicit rejection of unsupported eccentricity;
- uniqueness and cross-catalogue joins for resources, technologies, vehicles, chassis, electronic systems, facilities, assemblies, security systems, upgrades, installation packages, and power distribution;
- advisor appointments/candidates, eligibility, evidence-backed bonuses, warning/override history, personnel loss, succession, and knowledge-transfer boundaries;
- work-kanban transitions, explicit prerequisites, durable event history, and physical resource/labor constraints;
- ship part identity, mount/dependency joins, six-direction damage semantics, causal cascades, and provenance-aware salvage;
- candidate faction ship-style profiles and their visible/operational trade-offs;
- campaign and galaxy-generation policies, civilization lineage, human history, campaign discoveries, and astronomical reference/provenance contracts;
- renderer and Event-Horizon boundaries, embodied physical interaction, control outcomes and interlocks, manned mech component identity, and engineering watch/maintenance/lockout-tagout procedures;
- fighter-pilot station occupancy, restraint/connect/raise ordering, qualification, bounded control intent, desktop/VR parity, invalid input rejection, damaged/unpowered blocking, and emergency release;
- Workshop composition ownership, FSM_COS manifest-boundary assumptions, and the separation of authoring metadata from the machine-oriented `runtime-manifest.json`;
- a test-only in-memory composition of the raWWar root through the same published FSM_COS `0.1.0-alpha.5` package currently referenced by AnyApp, producing a `RuntimeAssembly` and checking runtime/root identity.

The suite is a growing set of contract checks, not proof that every catalogue is complete or that every described capability is implemented. In particular, architecture-contract assertions do not substitute for an end-to-end host loading a real composed Experience.

## Known implementation limits

The executable orbital model currently represents an isolated two-body elliptic orbit with fixed elements. It is not a complete n-body galaxy simulator. Perturbations, maneuvers, collisions, durable event-history reconstruction, checkpointing, observer frame graphs, rendering and GUI remain separate capabilities to model and test. The event-history reference ledger is volatile and does not apply events to authoritative world state.

The raWWar Experience root is also still a composition scaffold: `RaWWarMicroBundle.Load` does not yet compose a domain capability, and `Arbitrate` returns `false`. The suite now proves that the root can be assembled through FSM_COS using a test-only in-memory catalog, but it does not prove repository artifact retrieval, direct deserialization of `runtime-manifest.json` by the host, or end-to-end loading/execution through AnyApp. The published FSM_COS `0.1.0-alpha.5` used by AnyApp accepts unversioned `MicroBundleDependencyRequest` roots; the current FSM_COS `development` source has a newer versioned `MicroBundleManifestEntry` contract. That boundary must be reconciled rather than silently treating the two APIs as identical.

The check count should be updated from actual CI output when the suite changes. Do not treat an earlier green run as evidence that a later commit passes.
