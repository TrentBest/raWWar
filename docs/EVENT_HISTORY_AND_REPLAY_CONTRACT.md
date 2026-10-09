# Event History, Checkpoints, and Deterministic Replay

> **A query observes the world. A committed event changes it. A checkpoint accelerates reconstruction; it does not replace the history that explains the state.**

**Status:** Partial reference implementation; durable storage, authoritative state application, checkpoints, and replay reconstruction are not implemented.  
**Owner:** raWWar world-model implementation.  
**Audience:** Simulation, persistence, networking, testing, and content-tooling contributors.  
**Evidence rule:** The repository implements stable V1 event identity and a thread-safe in-memory reference ledger. This is not durable storage, a world-state reducer, checkpoint support, or proof of cross-platform deterministic simulation.

## 1. Separate identity, ordering, and consequences

An event has three distinct concerns:

1. **Identity:** which logical occurrence this is, independent of when or how often it is processed.
2. **Ordering:** where it belongs relative to other applicable events in the relevant simulation domain.
3. **Consequences:** the authoritative state changes accepted when the event is committed.

Do not use a database row ID, arrival order, render frame, current wall clock, or process-local hash as the event's semantic identity. Do not assume a unique ID alone establishes causal order.

The world-model contract already identifies these event-key inputs:

- world seed;
- simulation-model version;
- entity or region address;
- event domain;
- event ordinal;
- domain-defined logical-time key.

The exact canonical encoding, uniqueness constraints, and domain-specific time representation remain implementation decisions. Until those are specified and tested, do not claim cross-process event IDs are stable or derive them from ad hoc string formatting.

## 2. A committed event is applied at most once

A simulation may discover, evaluate, queue, retry, or replay a candidate event multiple times. Those operations must not multiply its authoritative consequences.

- **Candidate:** a proposed occurrence; not yet world history.
- **Accepted/committed:** validated and recorded as part of authoritative history.
- **Rejected:** not part of authoritative history; preserve a diagnostic reason when useful.
- **Replayed:** re-evaluation of already recorded history to reconstruct state; not a second commit.

Commit and deduplication must be atomic at the persistence boundary. A process-local “seen IDs” set is not sufficient after restart or across replicas. Reprocessing a committed event with the same identity must not apply its effects twice. Reusing one identity for different payloads is a contract violation and must be surfaced, not silently treated as a harmless duplicate.

## 3. Order is explicit and domain-owned

Every replayable domain must define how to order events that can affect the same state. A suitable domain contract may include logical time, a domain-owned sequence, causal dependencies, and a deterministic tie-break rule. The order must not depend on whichever thread, network packet, collection iteration, or rendering callback happens to run first.

Do not impose one universal time unit or universal total order on every world subsystem. Different domains may have different valid time ranges and resolution. Where events are causally dependent, replay must respect that dependency. Where events are concurrent, the domain must define whether they commute, need deterministic conflict resolution, or are invalid together.

An event whose required causal predecessor is absent must be deferred, rejected, or handled under an explicit domain policy. It must not be silently applied as if its prerequisites had occurred.

## 4. Replay is versioned reconstruction

Reconstruction starts from a known seed-derived initial state or a compatible checkpoint, then applies the relevant committed events under declared model versions.

A checkpoint should identify at least:

- the world/entity scope it covers;
- the simulation-model and state-schema versions;
- the logical time represented;
- the history position or event identities included;
- the compatibility/migration information needed to decide whether it can be loaded.

A checkpoint without a trustworthy history boundary risks applying an event twice or skipping it. A checkpoint from an incompatible model version must not be silently interpreted as current. The implementation must choose and test a migration, replay-from-earlier-state, or explicit incompatibility path.

Regenerable initial conditions and authoritative consequences remain distinct: seed and generator versions may recreate unchanged initial properties; discoveries, ownership, casualties, construction, destruction, resource depletion, and other consequences of play must remain recoverable from persisted history or a validated checkpoint plus the history after it.

## 5. Observation must not commit history

A world query at a requested valid time may reconstruct or evaluate a state. Rendering, camera movement, GUI updates, cache misses, texture generation, and repeated reads must not commit events or mutate authoritative history.

If evaluating a model discovers that an event should occur, the implementation must cross an explicit simulation/commit boundary. The read path cannot quietly turn an observation into a write.

## 6. Determinism has a declared scope

“Deterministic replay” is meaningful only relative to a stated contract. For each domain, document:

- model and schema versions;
- valid logical-time range and units;
- event identity and ordering rules;
- initial-state inputs and random sampling keys;
- numerical solver, precision, and tolerance where applicable;
- whether determinism is guaranteed within one build/platform, across supported platforms, or only within a tolerance;
- checkpoint compatibility and migration behavior.

Do not promise bit-for-bit replay across platforms merely because event ordering is stable. Floating-point math, solver changes, and model-version changes may affect results. If exact replay is required, specify the implementation strategy and test it against fixed reference scenarios.

## 7. Implemented reference slice

The core now contains `SimulationEventId`, `SimulationEvent`, and `InMemoryEventHistory` under `src/raWWar/History/`.

- Event identity V1 hashes a canonical binary encoding with the `RWEI` magic/version prefix, big-endian unsigned integers, length-prefixed UTF-8 strings and length-prefixed canonical address bytes. SHA-256 output is represented as uppercase hexadecimal.
- Identity inputs are world seed, model version, canonical address bytes, event domain, event ordinal, and the domain-defined logical-time key. Fixed vectors guard against accidental encoding drift.
- Event construction copies payload and canonical address bytes and derives the ID from the same immutable metadata. The event retains a canonical address stream key so history can be read per entity/region rather than mixing every entity in a domain. Constructing an event does not commit anything.
- The in-memory ledger serializes commits under a lock. Identical retries return `AlreadyCommitted`; a reused ID with different event content returns `IdentityConflict`.
- Reads can filter by both domain and canonical address, then sort by ordinal string comparison of the logical-time key, event ordinal, and stable ID. A domain-wide read remains available for diagnostics. Lexical ordering is only appropriate when the domain explicitly chooses that key policy; the type does not infer numeric time.
- Executable contract checks cover a fixed event-ID vector, repeatability, changed identity inputs, payload isolation, idempotent retry, payload conflict, deterministic domain ordering, domain isolation, and concurrent duplicate commits.

**Known boundary:** the ledger is process-local and volatile. It neither persists events nor applies their payloads to authoritative world state. Its lock is not a persistence transaction. Restart-safe idempotency, causal prerequisite policy, durable ordering, state reduction, checkpoint boundaries, and replay equivalence remain future implementation work. Do not use this reference ledger as a multiplayer or production persistence guarantee.

## 8. Minimum acceptance tests

A future event/history implementation should demonstrate:

- replaying the same seed, versions, checkpoint, and ordered history reconstructs the same defined state;
- retrying a committed event does not duplicate its consequences;
- the same event identity with a different payload is detected as a conflict;
- different thread scheduling, packet arrival order, render frame rate, and camera movement do not change the committed result when the domain contract says they are irrelevant;
- missing causal prerequisites follow a declared policy;
- checkpoint-plus-tail replay matches full replay from the same compatible origin;
- incompatible checkpoint/model versions are rejected or migrated explicitly;
- an observation-only query does not append events or mutate authoritative state;
- unsupported times and out-of-order events are handled by the domain's declared policy.

## 9. Implementation boundary

This document is a design contract, not an event-store API. Do not invent public types, package dependencies, event encodings, or storage providers solely to make the document appear implemented. The next implementation slice should begin with one small domain, fixed event vectors, explicit ordering, and executable replay/idempotency tests before generalizing the contract.
