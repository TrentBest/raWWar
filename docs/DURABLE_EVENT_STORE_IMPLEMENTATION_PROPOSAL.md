# Durable Event Store — First Adapter Proposal

> **Candidate, not canon:** prove restart-safe event recording in one process before claiming a distributed world ledger.

**Status:** Candidate implementation proposal; an internal prototype now exists, but this document still requires review before the format or provider becomes a committed architecture decision.  
**Scope:** The first durable adapter for the existing event-history slice only.  
**Related contracts:** [Event History, Checkpoints, and Deterministic Replay](EVENT_HISTORY_AND_REPLAY_CONTRACT.md), [Durable Event Commit Contract](DURABLE_EVENT_COMMIT_CONTRACT.md), and [Durable Event Record Format — Candidate V1](DURABLE_EVENT_RECORD_FORMAT_CANDIDATE.md).

## Prototype status (2026-10-10)

An internal `CandidateFileEventJournal` now exercises the narrow local-file proposal. It uses the Candidate RWEJ frame codec, opens the journal with an exclusive file handle, appends frames and calls `Flush(true)` before reporting `Committed`, rebuilds an identity index on open, suppresses identical retries, reports same-ID/different-content conflicts, and refuses further operations on a live instance after a write/flush exception until it is reopened. Recovery fails closed on truncated/corrupt records and duplicate identities.

Temporary-file contract checks currently cover first commit, no-growth retry, conflict, distinct append, close/reopen recovery, retry/conflict after reopen, deterministic address-scoped reads, truncated-tail rejection, and corruption rejection. CI passed on the code/test head `156831b62032df4c0f16211f62b621e547f8d4f7` (push run [38024000541](https://github.com/TrentBest/raWWar/actions/runs/38024000541); PR run [38024003689](https://github.com/TrentBest/raWWar/actions/runs/38024003689)). The follow-up documentation head `84b7da901be143b380d83f7952c80a519617d7ad` also passed both push and PR CI ([38024033477](https://github.com/TrentBest/raWWar/actions/runs/38024033477), [38024030861](https://github.com/TrentBest/raWWar/actions/runs/38024030861)).

This is **prototype evidence only**, not proof of process-crash or power-loss safety. The uncertain-outcome protocol check throws after `_stream.Write(frame)` and before `Flush(true)`, then verifies the caller receives an exception, the live instance blocks reads/commits, and reopen resolves the event as present so retry is idempotent. This is a controlled application-level exception with bytes still available to the OS; it does **not** simulate a real flush failure, OS crash, or power loss. A separate child-process check now commits an event, exits normally, and verifies a fresh process can reopen, recover, and idempotently retry it. Both push and PR workflows passed on code/test head `85d6b166509dc6974629b3f9393685d56da8f7cb` ([push CI](https://github.com/TrentBest/raWWar/actions/runs/38026095413), [PR CI](https://github.com/TrentBest/raWWar/actions/runs/38026096526)). A further contract test submits 32 concurrent identical commits to one journal instance and verifies exactly one `Committed`, 31 `AlreadyCommitted`, and one indexed event; both push CI [38026144191](https://github.com/TrentBest/raWWar/actions/runs/38026144191) and PR CI [38026147318](https://github.com/TrentBest/raWWar/actions/runs/38026147318) passed on head `40e520d0403c0395757e4a1ebde8bc1e82e48a94`. This checks serialized same-process retry behavior, not competing processes. Clean process restart is useful evidence, but not crash recovery. Supported OS/filesystem boundaries, real storage-fault/process-termination tests, and the durability claim remain unresolved. The candidate format is not frozen, and no public storage API or world-state transaction has been introduced.

## 1. Proposed first deployment boundary

**Candidate assumption:** the first adapter is a local, single-process development/reference store on .NET 8. It is not a multiplayer authority, multi-process database, or multi-host ledger. The repository currently targets .NET 8 and the event-history slice has no storage-provider dependency.

Under that assumption, prototype a small append-only journal using built-in .NET file I/O before adding a storage package. Keep the store behind a narrow history boundary so a later provider can implement the same semantic contract without making game code depend on file details.

This is a proposal, not a statement that local-file storage is sufficient for raWWar's eventual galaxy-scale deployment. If the first required runtime is a server with concurrent processes or multiple hosts, revisit the provider choice before implementation rather than stretching this candidate beyond its guarantee.

## 2. Candidate journal shape

Each record should be a framed, versioned binary record with:

- fixed magic and format version;
- bounded record length;
- all semantic event fields required to reconstruct the immutable `SimulationEvent`;
- the exact payload bytes;
- an integrity checksum over the canonical record content.

Use explicit endianness and length-prefixed UTF-8 for strings. Never serialize by delimiter-joined text, runtime object hash, or implicit platform layout. Establish fixed byte vectors before treating the encoding as a compatibility contract.

The journal must validate lengths against conservative documented limits before allocating memory. Unknown versions, invalid field encodings, mismatched checksums, and malformed complete records are corruption/incompatibility errors—not records to skip silently.

## 3. Commit algorithm candidate

1. Validate the event and encode the complete frame in memory.
2. Serialize writers within the process.
3. Resolve the event ID against the recovered in-memory index.
   - Identical semantic content: return `AlreadyCommitted` without appending.
   - Different content for the same ID: return `IdentityConflict` without appending.
4. For a new ID, append the complete frame and request an OS-level durable flush before reporting `Committed`.
5. Add the event to the in-memory index only after the durable flush succeeds.
6. If a write or flush fails and the result is uncertain, do not pretend the event definitely failed. Mark the live store as requiring recovery/reopen, then resolve the outcome from the journal before accepting more commits.

Opening the store should acquire an exclusive process-level file handle so a second process cannot silently write the same journal. This is a single-writer deployment restriction, not multi-process durability.

The event's existing identity intentionally excludes payload bytes. The recovered index must compare the complete semantic content, including payload, so a same-ID/different-payload candidate is a conflict rather than an idempotent retry.

## 4. Recovery and incomplete tail policy

**Candidate safety policy:** fail closed on malformed data, including an incomplete final frame. Do not silently truncate the tail during ordinary open; truncation could erase evidence about whether an event was committed. A separate, explicit repair tool/policy may be designed later with backups and diagnostics.

During recovery:

- scan frames in order, validating magic, version, bounded length, and checksum;
- reconstruct each event and recompute its ID from the semantic fields;
- reject a stored ID/content mismatch;
- rebuild the identity index;
- treat repeated IDs with identical content as corruption unless the format explicitly documents why duplicates can occur (the initial writer should never append retries);
- reject a repeated ID with conflicting content;
- expose the store only after the entire journal validates.

If a process dies after a durable flush but before the caller receives the result, reopening finds the event; retrying it returns `AlreadyCommitted`. If a crash leaves a partial frame, the journal is not automatically accepted as complete. A future repair workflow must preserve the original bytes and document the uncertainty.

## 5. Guarantees and explicit non-guarantees

If implemented and tested as proposed, the first adapter may claim only:

- one writer process at a time;
- durable append attempts subject to the documented .NET/OS/filesystem flush contract;
- restart reconstruction and same-ID deduplication;
- same-ID/different-content conflict detection;
- deterministic recovery of valid records.

It must **not** claim:

- multi-process or multi-host transactions;
- distributed consensus or multiplayer authority;
- atomic transaction between event recording and world-state mutation;
- automatic recovery from arbitrary storage corruption;
- general checkpoint persistence or state reconstruction;
- exactly-once external effects.

File flush semantics and storage hardware cannot make a universal promise against every controller, filesystem, power-loss, or device failure. Documentation must name the tested environment and failure model.

## 6. Acceptance tests before adoption

- first commit survives close/reopen and returns identical semantic content;
- same event retried after reopen is `AlreadyCommitted` and does not grow the journal;
- same ID with changed payload is `IdentityConflict` before and after reopen;
- multiple distinct events recover in stable order;
- competing same-ID commits in one process produce one stored record;
- a second process cannot open the same journal for writing;
- truncated header, truncated payload, impossible length, bad checksum, unknown version, invalid UTF-8, and recomputed-ID mismatch fail closed;
- injected write/flush failures prevent further commits until recovery establishes the durable outcome;
- a simulated lost acknowledgement followed by reopen and retry does not append a second record;
- an independent reader never observes a partial frame as an accepted event.

Use real temporary files and close/reopen tests. Unit-level exception injection alone is not evidence of process-crash or power-loss safety. Add process-level fault tests where feasible, and label the remaining limits honestly.

## 7. Open decisions before implementation

1. Confirm that a single-process local reference store is the right first deployment target.
2. Decide how to handle an incomplete final frame: fail-closed (recommended for the first version) or a separately invoked, evidence-preserving repair path.
3. Set maximum string, payload, and total-record lengths.
4. Choose and freeze the record schema only after fixed encoding vectors are reviewed.
5. Define supported OS/filesystem CI coverage and what durability claims those tests justify.
6. Decide when the first adapter should be replaced or supplemented for server/multi-host authority.

**Recommendation:** approve the narrow single-process prototype only as a stepping stone. Keep the semantic contract independent of the file format, and do not start general state persistence or claim multiplayer safety as part of this slice.
