# raWWar Experience

raWWar is a manifest-oriented, first-person war Experience from The Singularity Workshop. It defines game-specific meaning—soldiers, factions, procedures, equipment, world history, and consequences—while consuming reusable Workshop capabilities.

## Package contents

This package contains the raWWar domain assembly plus two distinct manifest files:

- `manifest.json` — Experience authoring metadata, including the path to the runtime manifest.
- `runtime-manifest.json` — machine-oriented runtime identity and declared MicroBundle roots.

The manifests are packaged as content files. Their presence does **not** by itself prove that a host can resolve all artifacts or launch the Experience.

## Current implementation scope

The repository includes a narrow explicit-time elliptic-orbit model, versioned event identity, a process-local in-memory event ledger, a resource-balance replay example, and a fighter-pilot station domain prototype. These are reference implementation slices, not a complete playable game.

The MicroBundle root remains a scaffold; complete capability composition, durable event storage, checkpoint reconstruction, and end-to-end AnyApp loading are not yet established. Consult the [repository README](../../README.md), [Technical Design](../../docs/TECHNICAL_DESIGN.md), and [maintained work queue](../../TODO.md) for current boundaries and verification evidence.

## Build and verify

From the repository root with the .NET 8 SDK:

```bash
dotnet build raWWar.sln --configuration Release
dotnet run --project tests/raWWar.ContractTests/raWWar.ContractTests.csproj --configuration Release
dotnet run --project examples/KeplerOrbit/raWWar.KeplerOrbitExample.csproj --configuration Release
```

These commands verify the checked-in solution, executable contract checks, and orbit example. They do not prove that AnyApp can retrieve and execute the full Experience.

Package creation is local verification only. NuGet publication is not authorized by this repository's build workflow.
