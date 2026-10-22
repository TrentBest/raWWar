# CandidateEventFrameCodec.cs

**User moniker:** Bounded experimental event-frame encoder/decoder

## Purpose

Encode and decode the internal Candidate V1 journal frame used by the provisional file journal. The XML documentation explicitly says the format is not frozen and is not a durable event-store contract.

## Source and executable evidence

- Implementation: [CandidateEventFrameCodec.cs](../../src/raWWar/History/CandidateEventFrameCodec.cs)
- Event model and identity: [SimulationEvent.cs](../../src/raWWar/History/SimulationEvent.cs)
- Journal consumer and recovery checks: [CandidateFileEventJournal.cs](../../src/raWWar/History/CandidateFileEventJournal.cs) and [contract checks](../../tests/raWWar.ContractTests/CandidateFileEventJournalChecks.cs)
- Dedicated diagram: [CandidateEventFrameCodecUML.md](CandidateEventFrameCodecUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | `internal static class CandidateEventFrameCodec` |
| Frame marker | ASCII `RWEJ`, version byte 1, reserved flags byte 0 |
| Header | 10 bytes; total frame length is unsigned 32-bit big-endian |
| Integrity | SHA-256 trailer over the preceding frame bytes |
| Bounds | Model/domain/address/logical-time fields each capped at 4 KiB; payload at 1 MiB; frame at 1,100,000 bytes |

## Encoding and validation

The body includes world seed, length-prefixed model version/address/domain, event ordinal, length-prefixed logical-time key and payload, and the 32-byte event ID. Integer values and field lengths are big-endian. Text fields use strict UTF-8 on decode. Decode rejects invalid magic/version/flags, length mismatch, oversized frames or fields, truncated fields, malformed UTF-8, unexpected body bytes, checksum mismatch, invalid event fields, and event-ID mismatch.

## Use case

The internal journal encodes a candidate event before appending it. Recovery bounds the declared frame size before allocation, reads the complete frame, then decodes and validates it before indexing the event.

## Evidence boundary

This is an internal experimental frame format. SHA-256 here detects accidental or adversarial byte changes relative to the stored digest but does not authenticate the writer. Do not treat the current layout as a frozen public wire format or durable compatibility promise.
