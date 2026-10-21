# ResourceBalanceCheckpoint — class diagram

```mermaid
classDiagram
    class ResourceBalanceCheckpoint {
      <<sealed record>>
      +int SchemaVersion
      +long Balance
      +ulong WorldSeed
      +string SimulationModelVersion
      +string StreamKey
      +string LastEventId
      +string LastLogicalTimeKey
      +ulong LastEventOrdinal
      +Capture(long initialBalance, IEnumerable~SimulationEvent~ orderedPrefix) ResourceBalanceCheckpoint
      +ReplayTail(IEnumerable~SimulationEvent~ orderedTail) long
      -CompareToBoundary(SimulationEvent item) int
      -Compare(SimulationEvent left, SimulationEvent right) int
    }
    ResourceBalanceCheckpoint ..> ResourceBalanceReplay : captures prefix balance and replays tail
    ResourceBalanceCheckpoint ..> SimulationEvent : records exact ordering boundary
```

## Use case

A caller captures a checkpoint after replaying a non-empty prefix and later applies only a validated tail whose events belong to the same stream and lie strictly after the saved boundary.

## Boundary

The record does not serialize itself, persist to disk, discover history, or provide atomic world-state snapshots.
