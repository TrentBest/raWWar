# raWWar ADP build evidence

**Status:** The provider evidence and exact-source CiAi observation are implemented on `development`. This is a read-only evidence lane; it does not authorize repository writes or release operations.

## What the provider emits

The raWWar build workflow runs on pushes and pull requests to `master` and `development`, and supports a manually invoked controlled-failure fixture. It now:

1. Records repository, workflow reference and SHA, event identity, run ID/attempt, and actual checkout SHA in a `ciai.workflow-provenance.v1` payload.
2. Uploads that provenance as `rawwar-workflow-provenance-{run_id}-{attempt}`.
3. Executes the real raWWar contract harness and writes one TRX result per actual assertion, including failed assertions and diagnostics.
4. Packages `results.trx` with `provenance.json` in `rawwar-contract-results-{run_id}-{attempt}`.
5. Uploads both artifacts and retains the generated Soldier Field preview artifact.
6. Fails when a required preview, Node test, build, local package validation, contract, event-frame, or example check fails.

The TRX writer's controlled pass/fail fixtures run only in temporary files and are deleted. They never enter provider evidence. The optional workflow-dispatch failure fixture is deliberately opt-in and must not be enabled for ordinary evidence runs.

## First independently reconciled run

| Evidence | Exact result |
|---|---|
| raWWar source commit | `eef1974a87d78eefe2802d77d7f2451f4f2b47a7` |
| raWWar build run | [45549019401](https://github.com/TrentBest/raWWar/actions/runs/45549019401) — succeeded |
| Source contract harness | **406/406 passed** |
| Source provenance artifact ID | `14992029354` |
| Source contract-results artifact ID | `14992453115` |
| CiAi observer run | [45549019399](https://github.com/TrentBest/raWWar/actions/runs/45549019399) — succeeded |
| CiAi observation artifact | `ciai-rawwar-commit-evidence-eef1974a87d78eefe2802d77d7f2451f4f2b47a7` |
| Required workflow result | `.github/workflows/build.yml#45549019401#1` — **passed** |
| Workflow-definition comparison | **trusted against the pinned approved baseline revision** |
| TRX verdict | **CompletePass** |

CiAi independently resolved source run **45549019401, attempt 1**, downloaded the two provider artifacts, matched their identities to that exact run, checked the workflow definition against the pinned baseline, and validated the 406 real TRX results. This is the first end-to-end read-only cross-repository evidence reconciliation for raWWar.

This proves the source build and evidence were reconciled for that exact commit. It does not prove the raWWar Experience is playable or launchable through AnyApp.

## CiAi enrollment

`.ciai/repository.json` declares repository identity `TrentBest/raWWar`, observer profile `rawwar-contracts-v1`, workflow path `.github/workflows/build.yml`, profile-specific artifact names/entries, and the direct MicroBundleDomain capability dependency.

The manifest is declarative. It does not grant write authority or activate autonomous repair.

## Trust boundary

The workflow-side provenance file is a claim made by the workflow, not a self-authenticating attestation. The pinned CiAi observer independently checks provider repository/run/attempt, exact source SHA, artifact identity and archive digest, provenance/TRX payloads, required job result, and workflow bytes against a pinned approved baseline.

The observer workflow itself is still a candidate-controlled repository workflow. The trusted host must independently verify its own pinned observer/workflow revision and retain the resulting decision. A green provider workflow is evidence, not authority. A passing TRX is not authorization to modify a repository. LLM diagnosis is not evidence authentication. Host authorization remains independently enforced for each exact proposed operation; merge, publication, release, and deployment remain separate gates.

## Next integration

The next CiAi slice is to persist the observed run and artifact identities as a durable evidence receipt, then map accepted evidence into one bounded raWWar ADP work group. A controlled failure must demonstrate the full red → diagnostic → bounded proposal → host authorization → isolated change → new exact-SHA green evidence loop before autonomous development is considered operational.
