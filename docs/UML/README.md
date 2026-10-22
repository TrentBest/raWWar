# UML source documentation

This folder records source-backed UML per C# file. Pages describe checked-in implementation and executable examples, not intended future behavior. The repository contains 21 C# files across runtime source, examples, and contract tests.

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
| `src/raWWar/History/ResourceBalanceCheckpoint.cs` | [ResourceBalanceCheckpoint.cs.md](ResourceBalanceCheckpoint.cs.md) · [ResourceBalanceCheckpointUML.md](ResourceBalanceCheckpointUML.md) | Documented |
| `src/raWWar/Spatiotemporal/CartesianTransform3d.cs` | [CartesianTransform3d.cs.md](CartesianTransform3d.cs.md) · [CartesianTransform3dUML.md](CartesianTransform3dUML.md) | Documented |
| `src/raWWar/Spatiotemporal/GalaxyCellAddress.cs` | [GalaxyCellAddress.cs.md](GalaxyCellAddress.cs.md) · [GalaxyCellAddressUML.md](GalaxyCellAddressUML.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressCodec.cs` | [HierarchicalSpatialAddressCodec.cs.md](HierarchicalSpatialAddressCodec.cs.md) · [HierarchicalSpatialAddressCodecUML.md](HierarchicalSpatialAddressCodecUML.md) | Documented |
| `src/raWWar/Spatiotemporal/HierarchicalSpatialAddressHasher.cs` | [HierarchicalSpatialAddressHasher.cs.md](HierarchicalSpatialAddressHasher.cs.md) · [HierarchicalSpatialAddressHasherUML.md](HierarchicalSpatialAddressHasherUML.md) | Documented |
| `src/raWWar/Spatiotemporal/KeplerOrbit.cs` | [KeplerOrbit.cs.md](KeplerOrbit.cs.md) · [KeplerOrbitUML.md](KeplerOrbitUML.md) | Documented |
| `src/raWWar/Interaction/FighterPilotStation.cs` | [FighterPilotStation.cs.md](FighterPilotStation.cs.md) · [FighterPilotStationUML.md](FighterPilotStationUML.md) | Documented |
| All runtime `.cs` files | Source companions and dedicated class diagrams indexed above | Complete |

## Example source coverage

| Source file | Companion | Diagram | Coverage |
|---|---|---|---|
| `examples/CartesianTransform/Program.cs` | [CartesianTransformExample.cs.md](CartesianTransformExample.cs.md) | [CartesianTransformExampleUML.md](CartesianTransformExampleUML.md) | Documented |
| `examples/EventHistoryReplay/Program.cs` | [EventHistoryReplayExample.cs.md](EventHistoryReplayExample.cs.md) | [EventHistoryReplayExampleUML.md](EventHistoryReplayExampleUML.md) | Documented |
| `examples/HierarchicalSpatialAddress/Program.cs` | [HierarchicalSpatialAddressExample.cs.md](HierarchicalSpatialAddressExample.cs.md) | [HierarchicalSpatialAddressExampleUML.md](HierarchicalSpatialAddressExampleUML.md) | Documented |
| `examples/KeplerOrbit/Program.cs` | [KeplerOrbitExample.cs.md](KeplerOrbitExample.cs.md) | [KeplerOrbitExampleUML.md](KeplerOrbitExampleUML.md) | Documented |

## Contract-test source coverage

| Source file | Companion | Diagram | Coverage |
|---|---|---|---|
| `tests/raWWar.ContractTests/CandidateFileEventJournalChecks.cs` | [CandidateFileEventJournalChecks.cs.md](CandidateFileEventJournalChecks.cs.md) | [CandidateFileEventJournalChecksUML.md](CandidateFileEventJournalChecksUML.md) | Documented |
| `tests/raWWar.ContractTests/Program.cs` | [ContractTestProgram.cs.md](ContractTestProgram.cs.md) | [ContractTestProgramUML.md](ContractTestProgramUML.md) | Documented |
| `tests/raWWar.ContractTests/SingleBundleCatalog.cs` | [SingleBundleCatalog.cs.md](SingleBundleCatalog.cs.md) | [SingleBundleCatalogUML.md](SingleBundleCatalogUML.md) | Documented |

## Coverage summary

- Runtime source companions and UML diagrams: complete for the 14 runtime source files.
- Example source companions and UML diagrams: complete for all 4 example `.cs` files.
- Contract-test source companions and UML diagrams: complete for all 3 test `.cs` files.
- Total indexed C# source files: 21 of 21.

## Rules

- One source companion and a dedicated UML page per `.cs` file; top-level programs are documented as executable orchestration diagrams.
- Include responsibility, public/test contracts, validation and lifecycle semantics where relevant.
- Cite source and tests where available; distinguish deterministic assertions from system-level proof.
- Record unresolved contracts as open questions rather than silently choosing a wire format or behavior.
- Coverage means documentation is present and indexed, not that every contract has been independently audited or that the release is ready.
