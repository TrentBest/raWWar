# ResourceBalanceReplay — class diagram

```mermaid
classDiagram
    class ResourceBalanceReplay {
      <<static>>
      +string EventDomain
      +byte PayloadVersion
      +int PayloadLength
      +EncodeDelta(long delta) byte[]
      +DecodeDelta(ReadOnlySpan~byte~ payload) long
      +Replay(long initialBalance, IEnumerable~SimulationEvent~ orderedEvents) long
      -CompareOrder(SimulationEvent left, SimulationEvent right) int
    }
    ResourceBalanceReplay ..> SimulationEvent : validates domain, stream, seed, version and order
    ResourceBalanceReplay ..> BinaryPrimitives : big-endian Int64 payload
    ResourceBalanceReplay ..> HashSet~string~ : rejects repeated event IDs
```

## Use case

The caller supplies an already ordered, address-scoped event sequence. The reducer validates the stream contract, decodes each fixed-width delta, and applies checked arithmetic.

## Boundary

The reducer is not a journal, checkpoint store, scheduler, or world-state transaction coordinator.
