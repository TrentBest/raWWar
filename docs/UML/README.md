# UML source documentation

This folder records source-backed UML per C# file. Pages must describe checked-in implementation, not intended future behavior. The repository currently contains 21 C# files across runtime source, examples, and contract tests; this is the initial coverage slice, not a complete audit.

## Runtime source coverage

| Source file | UML page | Coverage |
|---|---|---|
| `src/raWWar/RaWWarMicroBundle.cs` | [RaWWarMicroBundle.cs.md](RaWWarMicroBundle.cs.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs` | [HierarchicalSpatialAddress.cs.md](HierarchicalSpatialAddress.cs.md) | Documented |
| `src/raWWar/Spatiotemporal/SquirrelNoise3.cs` | [SquirrelNoise3.cs.md](SquirrelNoise3.cs.md) | Documented |
| `src/raWWar/History/ResourceBalanceCheckpoint.cs` | [ResourceBalanceCheckpoint.cs.md](ResourceBalanceCheckpoint.cs.md) · [ResourceBalanceCheckpointUML.md](ResourceBalanceCheckpointUML.md) | Documented |\n| `src/raWWar/History/ResourceBalanceReplay.cs` | [ResourceBalanceReplay.cs.md](ResourceBalanceReplay.cs.md) · [ResourceBalanceReplayUML.md](ResourceBalanceReplayUML.md) | Documented |\n| Other runtime `.cs` files | Not yet documented | Pending |
| Example `.cs` files | Not yet documented | Pending |
| Contract-test `.cs` files | Not yet documented | Pending |

## Remaining runtime files

- `src/raWWar/History/CandidateEventFrameCodec.cs`
- `src/raWWar/History/CandidateFileEventJournal.cs`
- `src/raWWar/History/SimulationEvent.cs`
- `src/raWWar/Interaction/FighterPilotStation.cs`
- `src/raWWar/Spatiotemporal/CartesianTransform3d.cs`
- `src/raWWar/Spatiotemporal/GalaxyCellAddress.cs`
- `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs`
- `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs`
- `src/raWWar/Spatiotemporal/KeplerOrbit.cs`

## Rules

- One page per `.cs` file, named after the source path.
- Include a class/type diagram, responsibility, public contracts, validation and lifecycle semantics.
- Cite source and tests where available; distinguish deterministic unit tests from system-level proof.
- Record unresolved contracts as open questions rather than silently choosing a wire format or behavior.
