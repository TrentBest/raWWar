# raWWar ADP build evidence

**Status:** The first structured provider-evidence slice is implemented on `development`. CiAi's independent exact-source observation and trust adjudication are still a separate gate.

## What the provider now emits

The raWWar build workflow runs on pushes and pull requests to `master` and `development`, and supports a manually invoked controlled-failure fixture. It now:

1. Records the repository, workflow reference and SHA, event identity, run ID/attempt, and actual checkout SHA in a `ciai.workflow-provenance.v1` payload.
2. Uploads that provenance as `rawwar-workflow-provenance-{run_id}-{attempt}`.
3. Executes the real raWWar contract harness and writes one TRX result per actual assertion, including failed assertions and their diagnostics.
4. Packages `results.trx` with `provenance.json` in `rawwar-contract-results-{run_id}-{attempt}`.
5. Uploads both artifacts and retains the existing generated Soldier Field preview artifact.
6. Fails the workflow when any required preview, Node test, build, local package validation, contract, event-frame, or example check fails.

The TRX writer's own controlled pass/fail fixtures run only in a temporary file and are deleted. They never enter the real provider artifact. The optional workflow-dispatch failure fixture is deliberately opt-in and must not be enabled for ordinary evidence runs.

## First verified run

The first complete development-branch run after the evidence implementation:

- Commit: `8869a8b2ad6fed97b4e6e347896c784b813a9883`
- Workflow run: [45546590828](https://github.com/TrentBest/raWWar/actions/runs/45546590828)
- Contract harness: **406/406 passed**
- Uploaded artifacts:
  - `rawwar-workflow-provenance-45546590828-1`
  - `rawwar-contract-results-45546590828-1`
  - `soldier-field-preview-svg`

This proves the workflow generated and uploaded the structured evidence artifacts for that exact run. It does not prove that CiAi has independently accepted them or that the raWWar Experience is playable or launchable through AnyApp.

## CiAi enrollment

`.ciai/repository.json` now declares:

- repository identity `TrentBest/raWWar`;
- built-in observer profile `rawwar-contracts-v1`;
- exact workflow path `.github/workflows/build.yml`;
- artifact naming and entry names defined by the CiAi profile;
- the direct MicroBundleDomain capability dependency.

The manifest is declarative. It does not grant write authority or activate autonomous repair.

## Trust boundary

The workflow-side provenance file is a claim made by the workflow, not a self-authenticating attestation. Before using it for an ADP decision, CiAi and the trusted host must independently verify:

- provider repository, workflow run, attempt, and exact checked-out source revision;
- the trusted workflow definition and action/reusable-workflow revisions under the configured control-plane policy;
- provider-reported artifact identity, association, expiry, and archive digest;
- the extracted provenance/TRX payload digests and exact run/attempt correlation;
- the required job's terminal outcome and every required test result.

A green provider workflow is evidence, not authority. A passing TRX is not authorization to modify a repository. LLM diagnosis is not evidence authentication. Host authorization remains independently enforced for each exact proposed operation; merge, publication, release, and deployment remain separate gates.

## Next integration

The next CiAi slice is to consume this profile and these artifacts through the existing exact-source observer, then map accepted evidence to one bounded raWWar ADP work group. A controlled failure must demonstrate the full red → diagnostic → bounded proposal → host authorization → isolated change → new exact-SHA green evidence loop before we call autonomous development operational.
