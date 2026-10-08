# raWWar Contract Tests

This dependency-free .NET 8 console project exercises mathematical and architectural invariants without introducing a test-framework package dependency.

Run from the repository root:

```sh
dotnet run --project tests/raWWar.ContractTests/raWWar.ContractTests.csproj
```

The initial checks cover:

- circular-orbit position at explicit logical times;
- period repeatability;
- elliptical periapsis and apoapsis;
- inclination into three dimensions;
- pure, order-independent time queries;
- explicit rejection of unsupported eccentricity.

These checks are an initial executable slice, not proof of a complete galaxy simulator or a full n-body model. The Kepler evaluator assumes an isolated two-body elliptic orbit with fixed elements. Perturbations, maneuvers, collisions, authoritative event history, observer transforms, textures, and GUI remain separate capabilities to model and test.
