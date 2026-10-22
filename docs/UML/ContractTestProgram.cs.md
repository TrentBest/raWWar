# raWWar.ContractTests — Program.cs

**User moniker:** Executable contract-test harness and AnyApp composition proof

## Purpose

Top-level contract-test runner for raWWar's coordinate transforms, canonical spatial addresses, event identity and frame formats, orbit behavior, event history, pilot-station interaction, journal recovery, and runtime-manifest composition through the FSM_COS package used by AnyApp.

## Source and evidence

- [Test runner source](../../tests/raWWar.ContractTests/Program.cs)
- [Candidate journal checks](CandidateFileEventJournalChecks.cs.md)
- [Runtime manifest bundle](../../src/raWWar/RaWWarMicroBundle.cs)
- [Local test catalog](SingleBundleCatalog.cs.md)
- [Dedicated diagram](ContractTestProgramUML.md)

## Class card

| Element | Contract |
|---|---|
| Type | Top-level executable test program |
| Assertion mechanism | Local `Check` and `Near` helpers accumulate failures |
| Child-process mode | `--journal-child-write <path>` commits a probe event and exits |
| Exit code | 0 when all assertions pass; 1 on accumulated failures; child mode returns 0 or 2 |
| Output | Total passed/total checks plus failure messages |

## Coverage slices visible in source

- Coordinate-frame identity, translation/rotation, composition order, inverse restrictions, and non-finite inputs.
- Fixed reference vectors for spatial address V1 bytes and SHA-256, event identity, and candidate frame bytes; malformed/truncated/checksum/version cases.
- Explicit-time Kepler orbit and boundary validation.
- FSM_COS composition of the actual raWWar bundle using the checked-in runtime manifest and a local one-bundle catalog.
- Fighter station state transitions, qualification, power/damage restrictions, valid and invalid control inputs, and emergency release.
- Delegated candidate file journal checks, including concurrent and process-restart scenarios.

## Use case

Run the contract-test project as a single executable verification pass. Fixed byte/hash vectors act as compatibility tripwires; the manifest check proves the current local composition boundary, not remote artifact retrieval.

## Evidence boundary

This harness is not a formal proof, exhaustive fuzzing campaign, distributed-systems test, or hardware durability test. Passing results apply to the checked-in assertions and environment of the executed workflow.
