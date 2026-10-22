# CandidateEventFrameCodec — class diagram

```mermaid
classDiagram
    class CandidateEventFrameCodec {
      <<internal static>>
      +int HeaderLength
      +int DigestLength
      +int MaxFrameBytes
      +Encode(SimulationEvent) byte[]
      +Decode(ReadOnlySpan~byte~) SimulationEvent
      -ValidateFields(...)
      -WriteField(...)
    }
    class SimulationEvent {
      +SimulationEventId Id
      +ulong WorldSeed
      +string StreamKey
      +ReadOnlyMemory~byte~ Payload
    }
    class SHA256 {
      <<cryptographic primitive>>
    }
    CandidateEventFrameCodec ..> SimulationEvent : serializes and reconstructs
    CandidateEventFrameCodec ..> SHA256 : frame digest
```

## Use case

The file journal uses `Encode` for appends and `Decode` during recovery. Decode validates framing, bounds, text, checksum and recomputed event identity before returning an event.

## Boundary

Internal and provisional; no public format stability, authenticity, or cross-version compatibility guarantee is established.
