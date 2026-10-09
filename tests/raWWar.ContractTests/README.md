# raWWar Contract Tests

This dependency-free .NET 8 console project checks executable mathematical, data-integrity, and architectural contracts without adding a test-framework package dependency.

Run from the repository root:

```sh
dotnet run --project tests/raWWar.ContractTests/raWWar.ContractTests.csproj
```

## Current coverage

The executable suite currently reports **171/171 checks passed** on the last verified CI run. The checks include:

- explicit-time circular and elliptic Kepler-orbit positions, period repeatability, 3D inclination, query purity/order independence, and explicit rejection of unsupported eccentricity;
- uniqueness and cross-catalogue joins for resources, technologies, vehicles, chassis, electronic systems, facilities, assemblies, security systems, upgrades, installation packages, and power distribution;
- advisor appointments/candidates, eligibility, evidence-backed bonuses, warning/override history, personnel loss, succession, and knowledge-transfer boundaries;
- work-kanban transitions, explicit prerequisites, durable event history, and physical resource/labor constraints;
- ship part identity, mount/dependency joins, six-direction damage semantics, causal cascades, and provenance-aware salvage;
- candidate faction ship-style profiles and their visible/operational trade-offs;
- campaign and galaxy-generation policies, civilization lineage, human history, campaign discoveries, and astronomical reference/provenance contracts;
- renderer and Event-Horizon boundaries, embodied physical interaction, control outcomes and interlocks, manned mech component identity, and engineering watch/maintenance/lockout-tagout procedures;
- Workshop composition ownership, FSM_COS manifest-boundary assumptions, and the separation of authoring metadata from the machine-oriented `runtime-manifest.json`.

The suite is a growing set of contract checks, not proof that every catalogue is complete or that every described capability is implemented. In particular, architecture-contract assertions do not substitute for an end-to-end host loading a real composed Experience.

## Known implementation limits

The executable orbital model currently represents an isolated two-body elliptic orbit with fixed elements. It is not a complete n-body galaxy simulator. Perturbations, maneuvers, collisions, authoritative event-history reconstruction, observer transforms, rendering and GUI remain separate capabilities to model and test.

The raWWar Experience root is also still a composition scaffold: `RaWWarMicroBundle.Load` does not yet compose a real capability, and `Arbitrate` returns `false`. The suite currently checks manifest structure and the intended Workshop boundary, but does not prove end-to-end loading through FSM_COS and AnyApp.

The check count should be updated from actual CI output when the suite changes. Do not treat an earlier green run as evidence that a later commit passes.
