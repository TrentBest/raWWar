# Durable Event Commit Contract

> **A successful commit must survive the failure model it claims to survive. A retry must not create a second occurrence. A collision must never masquerade as a retry.**

**Status:** Design contract / Candidate. No durable event store is implemented by this document.  
**Owner:** raWWar world-model implementation.  
**Related:** [Event History, Checkpoints, and Deterministic Replay](EVENT_HISTORY_AND_REPLAY_CONTRACT.md).

## 1. Purpose and boundary

The current `InMemoryEventHistory` is a process-local reference ledger. It is useful for exercising identity, retry, conflict, and deterministic-read behavior, but its lock and dictionary are volatile. They do not provide persistence transactions, restart-safe deduplication, multi-process coordination, or durable world state.

This contract defines the minimum behavior a future durable adapter must prove before raWWar relies on it. It does **not** select a file format, database, storage package, public interface, transaction technology, or multi-host consistency model. Those choices require an implementation proposal grounded in the repository's supported dependencies and deployment needs.

The event ledger records accepted event occurrences. It does not by itself apply event payloads to authoritative world state. A successful ledger commit and a state-reduction operation must not be described as one atomic world transaction unless an implementation proves that stronger guarantee.

## 2. Commit outcomes

A durable commit attempt receives one immutable `SimulationEvent` candidate and produces one of these semantic outcomes:

- **Committed:** this event identity and content became durable according to the adapter's documented durability boundary.
- **AlreadyCommitted:** the same identity with identical semantic content was already durable. The retry has no additional effect.
- **IdentityConflict:** the identity is already associated with different semantic content. Preserve the original record and surface the conflict.
- **Rejected:** validation or a declared domain policy rejected the candidate before it became accepted history.
- **Indeterminate:** the caller cannot tell whether a durable commit completed (for example, a connection failed after the storage system accepted the write). The caller may retry the *same immutable event*; the adapter must resolve the retry to a committed duplicate or a new commit without creating a second occurrence.

The implementation may use a different concrete result type, but it must preserve these distinctions. A timeout or I/O exception must not be reported as proof that the event was not committed.

## 3. Atomic identity and content binding

For one event identity, the durable store must atomically bind the ID to the semantic event content used by the repository contract: world seed, simulation-model version, canonical address stream, event domain, logical-time key, event ordinal, and payload bytes.

- A first valid candidate inserts exactly one durable identity/content association.
- A concurrent or later retry with identical content resolves to `AlreadyCommitted`.
- A candidate reusing that ID with different content resolves to `IdentityConflict`; it must not overwrite the original.
- Competing first commits for the same identity must converge on one durable association, even across process restarts and across all concurrency scopes the adapter claims to support.
- Payload and address bytes must be copied or otherwise made immutable across the persistence boundary.
- A reported `Committed` result means the record meets the adapter's explicitly documented durability guarantee; merely entering an in-memory buffer is not enough.

The current event identity derives from metadata rather than payload. Therefore the content comparison is essential: stable identity alone does not detect a payload collision.

## 4. Crash and restart behavior

The adapter must define the point at which a write is considered committed and test failure at the boundaries around that point.

At minimum, a testable implementation must answer:

1. What remains after termination before the write starts?
2. What remains if termination occurs during a partial write?
3. What remains if the durable write completes but the process dies before the caller receives the result?
4. Can reopening storage reconstruct the identity index without accepting a truncated or corrupt record as valid?
5. Can a retry after reopening distinguish identical content from an identity conflict?
6. How are unsupported schema versions, invalid lengths, corrupt records, and incompatible model versions reported?
7. Does recovery preserve the same accepted records and deterministic ordering as before termination?

Recovery must not silently skip malformed authoritative history and continue as if the world were complete. It must fail closed, quarantine/recover according to an explicit policy, or report a clearly scoped recoverable condition. Any repair policy must preserve evidence and be covered by tests.

## 5. Ordering and read guarantees

Storage order, arrival order, and simulation order are different concepts. The adapter must not infer world causality from physical append order or database-generated identifiers.

- Event identity and domain-defined logical ordering remain governed by [the event-history contract](EVENT_HISTORY_AND_REPLAY_CONTRACT.md).
- Reads used for replay must return a stable snapshot for their documented scope, with the caller applying the domain's explicit ordering rule.
- A read must not expose a half-committed record.
- If the adapter supports concurrent readers and writers, document whether each read is snapshot-consistent and test the promised behavior.
- Cross-address, cross-domain, cross-seed, or cross-model queries must not accidentally be presented as one replayable stream.
- Causal prerequisite validation remains domain-owned; the store must not claim that sorting alone proves causal completeness.

## 6. Scope of guarantees

Do not use “durable,” “atomic,” “exactly once,” or “distributed” without specifying the boundary.

| Claim | Minimum evidence |
|---|---|
| Process-restart safe | Close/reopen or terminate/restart tests prove committed records and identity bindings survive. |
| Atomic single-event commit | Fault injection or equivalent tests show no partial accepted event is observable. |
| Restart-safe idempotency | Identical retry after restart has no second record/effect; conflicting content is detected. |
| Thread-safe | Concurrent same-ID and distinct-ID commits preserve invariants within one process. |
| Multi-process safe | Independent processes competing on the same storage preserve one identity/content binding. |
| Multi-host / distributed safe | The stated deployment and partition model is tested; local file locking is not sufficient evidence. |
| Authoritative state recovery | A separately specified reducer/checkpoint protocol reconstructs state and handles missing/corrupt history. |

Do not claim exactly-once *effects* merely because event insertion is deduplicated. Effects outside the same atomic boundary need their own idempotency or transaction contract.

## 7. Minimum acceptance suite for a first durable adapter

A first implementation should include executable tests for:

- first commit, identical retry, and same-ID/different-content conflict;
- concurrent duplicate attempts and concurrent distinct events;
- closing and reopening the store, then retrying the same event;
- restart after a committed write whose acknowledgement is intentionally lost or simulated as indeterminate;
- failure during write and detection/rejection of incomplete records;
- corruption, invalid lengths, unknown schema versions, and unsupported event encoding;
- deterministic ordered reads after reopening, including equal logical-time keys and tie-breaks;
- isolation by event domain and canonical address stream;
- preservation of original content when a conflict occurs;
- clear distinction between a committed ledger record and any downstream state-reduction effect.

Tests must state what the chosen fault model actually simulates. A unit test that throws before a write is not proof of crash consistency. If a test cannot reproduce a real process crash, label it a fault-injection approximation and supplement it with a process-level recovery test where feasible.

## 8. Implementation decision gate

Before adding a public durable-store abstraction or choosing a storage technology, inspect and record:

- current raWWar target frameworks and package constraints;
- existing persistence capabilities available from the approved Workshop ecosystem and their verified versions/contracts;
- expected deployment shape for the first release (single process, local desktop, server, or multiple hosts);
- required durability boundary and acceptable recovery behavior;
- migration and compatibility policy for event/schema versions;
- whether state mutation can be kept separate from ledger insertion or needs a future transactional outbox/reducer protocol.

**Current decision:** unresolved. This is a design contract, not permission to introduce a new dependency or claim production durability. Start with the narrowest deployment the project actually needs, prove its failure model, and expand guarantees only with corresponding tests.
