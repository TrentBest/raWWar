# UML source documentation

This folder records source-backed UML per C# file. Pages must describe checked-in implementation, not intended future behavior. The repository currently contains 21 C# files across runtime source, examples, and contract tests; this is the initial coverage slice, not a complete audit.

## Runtime source coverage

| Source file | UML page | Coverage |
|---|---|---|
| `src/raWWar/RaWWarMicroBundle.cs` | [RaWWarMicroBundle.cs.md](RaWWarMicroBundle.cs.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddress.cs` | [HierarchicalSpatialAddress.cs.md](HierarchicalSpatialAddress.cs.md) | Documented |
| `src/raWWar/Spatiotemporal/SquirrelNoise3.cs` | [SquirrelNoise3.cs.md](SquirrelNoise3.cs.md) | Documented |
| `src/raWWar/History/ResourceBalanceCheckpoint.cs` | [ResourceBalanceCheckpoint.cs.md](ResourceBalanceCheckpoint.cs.md) · [ResourceBalanceCheckpointUML.md](ResourceBalanceCheckpointUML.md) | Documented |
| `src/raWWar/History/ResourceBalanceReplay.cs` | [ResourceBalanceReplay.cs.md](ResourceBalanceReplay.cs.md) · [ResourceBalanceReplayUML.md](ResourceBalanceReplayUML.md) | Documented |
| `src/raWWar/History/SimulationEvent.cs` | [SimulationEvent.cs.md](SimulationEvent.cs.md) · [SimulationEventUML.md](SimulationEventUML.md) | Documented |
| `src/raWWar/History/CandidateEventFrameCodec.cs` | [CandidateEventFrameCodec.cs.md](CandidateEventFrameCodec.cs.md) · [CandidateEventFrameCodecUML.md](CandidateEventFrameCodecUML.md) | Documented |
| `src/raWWar/History/CandidateFileEventJournal.cs` | [CandidateFileEventJournal.cs.md](CandidateFileEventJournal.cs.md) · [CandidateFileEventJournalUML.md](CandidateFileEventJournalUML.md) | Documented |
| `src/raWWar/Spatiotemporal/CartesianTransform3d.cs` | [CartesianTransform3d.cs.md](CartesianTransform3d.cs.md) · [CartesianTransform3dUML.md](CartesianTransform3dUML.md) | Documented |
| `src/raWWar/Spatiotemporal/GalaxyCellAddress.cs` | [GalaxyCellAddress.cs.md](GalaxyCellAddress.cs.md) · [GalaxyCellAddressUML.md](GalaxyCellAddressUML.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs` | [HierarchicalSpatialAddressCodec.cs.md](HierarchicalSpatialAddressCodec.cs.md) · [HierarchicalSpatialAddressCodecUML.md](HierarchicalSpatialAddressCodecUML.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs` | [HierarchicalSpatialAddressHasher.cs.md](HierarchicalSpatialAddressHasher.cs.md) · [HierarchicalSpatialAddressHasherUML.md](HierarchicalSpatialAddressHasherUML.md) | Documented |
| `src/raWWar/Spatiotemporal/KeplerOrbit.cs` | [KeplerOrbit.cs.md](KeplerOrbit.cs.md) · [KeplerOrbitUML.md](KeplerOrbitUML.md) | Documented |
| `src/raWWar/Interaction/FighterPilotStation.cs` | [FighterPilotStation.cs.md](FighterPilotStation.cs.md) · [FighterPilotStationUML.md](FighterPilotStationUML.md) | Documented |
| Other runtime `.cs` files | Not yet documented | Pending |
| Example `.cs` files | Not yet documented | Pending |
| Contract-test `.cs` files | Not yet documented | Pending |

## Remaining runtime files

- `src/raWWar/Spatiotemporal/CartesianTransform3d.cs` is documented above; remaining unlisted runtime files are tracked in the pending category.

## Rules

- One page per `.cs` file, named after the source path.
- Include a class/type diagram, responsibility, public contracts, validation and lifecycle semantics.
- Cite source and tests where available; distinguish deterministic unit tests from system-level proof.
- Record unresolved contracts as open questions rather than silently choosing a wire format or behavior.
