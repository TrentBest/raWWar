# Request from raWWar: AnyApp immutable artifact dependency closure

- **Requesting repository:** [TrentBest/raWWar](https://github.com/TrentBest/raWWar)
- **Receiving repository:** [TrentBest/AnyApp](https://github.com/TrentBest/AnyApp)
- **Status:** Ready for receiving-agent review; not an implementation assignment
- **Priority:** Blocking for a verified repository-backed raWWar load
- **Related raWWar integration contract:** [AnyApp manifest bridge](../integration/ANYAPP_MANIFEST_BRIDGE.md)

## Problem and observed behavior

raWWar must be loadable as an Experience through AnyApp's repository-backed composition path without pretending its authoring manifest, runtime manifest, and AnyApp publication manifest are interchangeable.

The raWWar integration investigation found that the repository-backed catalog uses artifact addresses supplied by the publication manifest, while composition queues dependencies declared by materialized bundles. If a queued dependency has no registered artifact address, preload fails with a missing repository address. Dependency discovery alone does not establish the immutable address needed to retrieve that dependency.

Each artifact address must be grounded in a verified identity, including the required version and content hash. raWWar must not invent hashes or treat its authoring version as proof of a published artifact.

## Desired outcome

A documented and testable AnyApp contract that can resolve the complete required dependency artifact closure using verified immutable identities while preserving the distinction between:

- **Runtime roots:** bundles intentionally requested for composition.
- **Dependency closure:** additional artifacts needed to materialize those roots and their transitive dependencies.
- **Artifact identity:** the verified version/hash used to retrieve a specific artifact.

The AnyApp agent should determine the appropriate host/catalog/repository design. This request does not prescribe whether closure resolution is manifest-driven, repository-index-driven, or implemented through another verified mechanism.

## Acceptance evidence

A test fixture or fake repository should demonstrate that:

1. A root bundle can declare transitive dependencies without requiring raWWar to silently promote every dependency to a runtime root.
2. Every retrieved artifact has a verified immutable identity; missing identity fails clearly rather than guessing.
3. The catalog can resolve the full required dependency closure, or produces a deterministic, actionable error identifying the missing address.
4. Version/hash mismatch, missing artifacts, and malformed identities fail before an invalid runtime is treated as successfully composed.
5. Cancellation and repository/materialization failures produce clear outcomes.
6. A host-level test demonstrates that the composed raWWar Experience is actually consumed/presented, not merely assembled.

Tests should follow the actual AnyApp contract and make root-versus-closure semantics explicit.

## Constraints and non-goals

- AnyApp owns host lifecycle, publication manifest semantics, repository retrieval, and artifact resolution.
- raWWar owns game/world behavior and must not duplicate a generic artifact catalog or host lifecycle.
- FSM_COS root-request semantics must not be changed or bypassed implicitly by treating dependency closure entries as roots.
- Do not claim raWWar is host-loadable until the end-to-end behavior is verified.
- This document records a requirement; it does not authorize changes in AnyApp, publishing, or merging.

## Evidence still needed

The current raWWar tests do not prove repository artifact retrieval or end-to-end AnyApp presentation. Recheck the receiving repository's actual branch/API and link the specific tests or commits when the contract is implemented and verified.
