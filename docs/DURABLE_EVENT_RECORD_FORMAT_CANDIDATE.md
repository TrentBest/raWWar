# Durable Event Record Format — Candidate V1

> **Status: Candidate for review. Not frozen, not implemented, and not a public compatibility promise.**

**Owner:** raWWar engineering  
**Scope:** A proposed byte-level format for the single-process local event journal described in [the durable-store proposal](DURABLE_EVENT_STORE_IMPLEMENTATION_PROPOSAL.md).  
**Purpose:** Make the format review concrete enough to expose ambiguities before storage code makes them expensive to change.

## 1. Design constraints from current code

The current SimulationEventId V1 algorithm hashes the ASCII domain tag RWEI, one identity-version byte, big-endian UInt64 world seed, length-prefixed UTF-8 simulation-model version, length-prefixed canonical address bytes, length-prefixed UTF-8 event domain, big-endian UInt64 event ordinal, and length-prefixed UTF-8 logical-time key. The SHA-256 result is exposed as 64 uppercase hexadecimal characters.

Payload bytes are deliberately excluded from identity. A durable reader must therefore recompute the event ID from identity fields and separately compare payload and every other semantic field when deciding whether an ID is an identical retry or a conflict.

This document does not redefine event identity or logical ordering. It only proposes how a journal stores an event.

## 2. Candidate frame layout

All integers are unsigned or signed as specified and encoded in **big-endian** order. Lengths count bytes, not characters. UTF-8 must be strict: malformed sequences are rejected rather than silently replaced.

| Offset / field order | Type | Candidate meaning |
|---|---|---|
| 0 | 4 bytes | Frame magic RWEJ |
| 4 | UInt8 | Journal format version; candidate value 1 |
| 5 | UInt8 | Flags; must be zero in V1 |
| 6 | UInt32 | Total frame byte length, including header and checksum |
| 10 | UInt64 | World seed |
| 18 | UInt32 + bytes | UTF-8 simulation-model version |
| variable | UInt32 + bytes | Canonical spatial-address bytes |
| variable | UInt32 + bytes | UTF-8 event domain |
| variable | UInt64 | Event ordinal |
| variable | UInt32 + bytes | UTF-8 logical-time key |
| variable | UInt32 + bytes | Opaque event payload |
| variable | 32 bytes | Stored event ID, raw SHA-256 digest bytes |
| final 32 bytes | 32 bytes | SHA-256 integrity checksum over every preceding frame byte |

The length field includes the checksum itself. A parser first reads the fixed 10-byte header, validates magic/version/flags and the total length against the maximum, then reads the remainder into a bounded buffer. It must not allocate based on an unvalidated or unbounded length.

The stored ID is redundant by design: recovery recomputes it from the identity fields and compares the result with the stored digest. The checksum detects accidental frame corruption; it is not a signature, authentication mechanism, or defense against an attacker who can rewrite the file and recompute hashes.

No padding, native struct layout, delimiter-based serialization, or implicit platform endianness is permitted.

## 3. Provisional resource bounds

These limits are proposed safety rails for a first reference implementation, not validated game-domain limits. Review them before code freezes them.

| Field | Candidate maximum |
|---|---:|
| Simulation-model version UTF-8 bytes | 4 KiB |
| Canonical address bytes | 4 KiB |
| Event-domain UTF-8 bytes | 4 KiB |
| Logical-time-key UTF-8 bytes | 4 KiB |
| Event payload bytes | 1 MiB |
| Total encoded frame bytes | 1,100,000 bytes |

The parser must check each field limit and checked arithmetic for cumulative length before allocating or slicing. It must reject zero-length required identity fields consistently with the current constructor contract; the canonical address must be non-empty. The logical-time key may be empty under the current API, so the durable codec must not silently tighten that contract without an explicit API decision.

A single payload limit is a storage-safety bound, not a recommendation to place large world snapshots into events. Larger data should be considered only through a separately specified, versioned design.

## 4. Candidate recovery semantics

- EOF exactly between frames is a clean end of journal.
- EOF inside the fixed header or inside a declared frame is an incomplete tail and fails closed.
- Invalid magic, unsupported format version, nonzero reserved flags, impossible lengths, malformed UTF-8, checksum mismatch, invalid event fields, or recomputed-ID mismatch fail closed.
- Duplicate event IDs in the journal fail recovery, even when their semantic content matches. The initial writer should never append a retry; retry resolution happens against the recovered index before writing.
- Same ID with different semantic content is an identity conflict and must never be accepted as a second event.
- Recovery exposes no partially recovered store. It builds a private index and publishes the store only after the entire file validates.
- No automatic truncation, tail repair, or “skip bad record and continue” behavior is allowed in ordinary open.

A future repair utility, if ever needed, must preserve original bytes, record the repair decision, and remain separate from normal open. It is not part of Candidate V1.

## 5. Identity and content comparison

The codec should centralize event encoding/decoding and should not create a second interpretation of identity. Tests must prove that decode followed by identity recomputation preserves the existing fixed SimulationEventId vector.

For retry comparison, compare world seed, model version, canonical address bytes, event domain, logical-time key, ordinal, and payload bytes. Comparing only the stored ID is insufficient because payload is excluded from identity.

Ordering remains the current declared order: ordinal logical-time key using ordinal string comparison, then event ordinal, then stable event ID. The journal preserves append order; consumers that need deterministic replay must continue to validate the event-history ordering contract rather than assuming physical append order proves semantic order.

## 6. Required byte-level tests before adopting the format

1. A tiny event has a fixed complete-frame hexadecimal vector, including checksum.
2. The stored ID matches the current SimulationEventId fixed vector after encode/decode.
3. Each field boundary and the total frame length are exact and endian-stable.
4. Round-trip preserves every semantic field and payload byte.
5. Truncated header and truncated frame fail closed.
6. Unknown version, nonzero flags, too-small/too-large lengths, field-limit overflow, malformed UTF-8, bad checksum, and wrong stored ID fail closed.
7. Trailing garbage fails as a malformed next frame; it is not ignored.
8. Duplicate IDs in a journal fail recovery.
9. A same-ID/different-payload retry is detected as a conflict by the recovered store.
10. Tests include maximum accepted field sizes and checked-length arithmetic boundaries without allocating unbounded input.

## 7. Fixed reference frame — candidate vector only

This vector makes the proposed field order, lengths, byte order, event identity, and checksum independently reproducible. It is a review aid, **not approval to freeze the format**.

Input event:
- World seed: `123`
- Simulation-model version UTF-8: `v1`
- Canonical address bytes: `5257534101002A00000002030405090001`
- Event domain UTF-8: `resource.depleted`
- Event ordinal: `7`
- Logical-time key UTF-8: `tick:000042`
- Payload: `102030`

Expected current event identity (raw 32-byte digest):
`F223794D5176C58304EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1`

Expected candidate frame length: **160 bytes** (`0x000000A0`).

Expected complete frame, hexadecimal (line wrapping is presentation only):

```text
5257454A0100000000A0000000000000007B000000027631000000115257534101002A00000002030405090001000000117265736F757263652E6465706C6574656400000000000000070000000B7469636B3A30303030343200000003102030F223794D5176C58304EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1BCE9CCE3807A763076F756157C1D83F117AFFBFBC6831B3427F876FC901D6567
```

### Fixed-vector byte offsets

The following offsets are zero-based and apply to this 160-byte example only. Ranges are half-open, so `[22, 24)` contains two bytes.

| Byte range | Length | Field / sample |
|---|---:|---|
| `[0, 4)` | 4 | Magic: `RWEJ` |
| `[4, 5)` | 1 | Format version: `01` |
| `[5, 6)` | 1 | Flags: `00` |
| `[6, 10)` | 4 | Total length: `000000A0` |
| `[10, 18)` | 8 | World seed: `000000000000007B` |
| `[18, 22)` | 4 | Model byte length: `00000002` |
| `[22, 24)` | 2 | Model: `v1` |
| `[24, 28)` | 4 | Address byte length: `00000011` |
| `[28, 45)` | 17 | Canonical address bytes |
| `[45, 49)` | 4 | Event-domain byte length: `00000011` |
| `[49, 66)` | 17 | Event domain: `resource.depleted` |
| `[66, 74)` | 8 | Event ordinal: `0000000000000007` |
| `[74, 78)` | 4 | Logical-time byte length: `0000000B` |
| `[78, 89)` | 11 | Logical time: `tick:000042` |
| `[89, 93)` | 4 | Payload byte length: `00000003` |
| `[93, 96)` | 3 | Payload: `102030` |
| `[96, 128)` | 32 | Stored event ID |
| `[128, 160)` | 32 | SHA-256 checksum over `[0, 128)` |

A second, language-independent reference assembly is checked in at [`tools/verify_candidate_event_frame.py`](../tools/verify_candidate_event_frame.py). It uses only Python's standard library, independently assembles the frame from the documented fields, and asserts the complete vector, offsets, event-ID placement, and checksum. It treats the event-ID bytes as the already-established SimulationEventId V1 reference vector; it does **not** independently reimplement the event-ID algorithm. CI runs it alongside the C# contract suite. This is a reproducible cross-language layout check, not external review or format approval.

Expected final checksum (the final 32 bytes, SHA-256 over the preceding 128 bytes):
`BCE9CCE3807A763076F756157C1D83F117AFFBFBC6831B3427F876FC901D6567`

Before implementation relies on this vector, a second independent implementation should verify it and the field offsets. The executable contract suite must then pin the vector and include mutation tests for every field and checksum.

## 8. Decisions still required

- Approve or change the provisional bounds.
- Confirm the frame checksum algorithm and whether an accidental-corruption checksum is sufficient for the stated local threat model.
- Review whether storing the redundant 32-byte ID is worthwhile (it is recommended for corruption diagnosis).
- Confirm that strict UTF-8 and fail-closed incomplete-tail behavior are the desired V1 policies.
- Freeze the exact layout only after the reference byte vector is independently reviewed.

**Non-goal:** This format does not solve multi-process coordination, multi-host authority, atomic world-state mutation, general checkpoint persistence, or external side effects. Those remain outside this candidate.
