# raWWar Autonomous Development Pipeline Integration

**Status:** Genuine per-check TRX production and workflow artifact upload are implemented and passing on the current PR head. Full CiAi observation and authorized repair remain unproven; the next gate is trusted provenance and provider-artifact correlation.  
**Related task:** [raWWar issue #9](https://github.com/TrentBest/raWWar/issues/9)  
**Scope:** Make raWWar a flagship Experience developed through the Workshop Autonomous Development Pipeline. This document describes verified repository facts and a proposed integration path; it does not claim that the pipeline is connected or that autonomous repair is implemented.

## 1. Intent

raWWar should demonstrate that the Workshop can help develop a substantial, coherent Experience through bounded, evidence-backed engineering. “Developed” is deliberate: authored domain rules, deterministic contracts, reproducible builds, reviewable proposals, and verified changes. This is not uncontrolled random generation.

The game remains the authority for its own soldiers, organizations, facilities, equipment, simulation, missions, social life, and consequences. The Workshop supplies reusable development and runtime machinery; it must not become the source of raWWar's game design.

The product distinction remains:

- **raWWar / base mode:** the lives, careers, training, command, logistics, facilities, research, defense, relationships, and routines of people at and around the base.
- **Galactic War / beyond-base mode:** a complementary FPS starting point in the wider galaxy. War and conflict can remain a backdrop to ordinary life until an event, deployment, discovery, or encounter makes them immediate.
- **Shared universe:** compatible authoritative facts and consequences, with distinct entry points and play loops. See [issue #8](https://github.com/TrentBest/raWWar/issues/8); do not collapse the modes or duplicate world authority by assumption.

## 2. Verified raWWar repository baseline

The following facts were read from the `development` branch during initial reconnaissance on 2026-11-14:

| Area | Verified source | Observed contract |
|---|---|---|
| CI workflow | [`.github/workflows/build.yml`](https://github.com/TrentBest/raWWar/blob/development/.github/workflows/build.yml) — blob `4c19d4a39a37f38ad61e21a27fc8626955a02935` | Push and pull-request triggers for `master` and `development`; workflow_dispatch; read-only contents permission; Ubuntu runner; Node 22 and .NET 8. |
| Solution | [`raWWar.sln`](https://github.com/TrentBest/raWWar/blob/development/raWWar.sln) — blob `738954c4c72bc67d9d13d51be1f6154e30001d16` | Includes the Experience project, executable contract-check project, and KeplerOrbit example. |
| Contract test entry point | [`tests/raWWar.ContractTests/Program.cs`](https://github.com/TrentBest/raWWar/blob/development/tests/raWWar.ContractTests/Program.cs) — blob `bba3beeab0169967eceea29c138754cd59683065` | Executable checks aggregate failures, print passed/total counts, and return nonzero when checks fail. Includes deterministic spatial/event vectors, station interaction checks, formation planner/coordinator checks, and journal checks. |
| Contract test project | [`tests/raWWar.ContractTests/raWWar.ContractTests.csproj`](https://github.com/TrentBest/raWWar/blob/development/tests/raWWar.ContractTests/raWWar.ContractTests.csproj) — blob `b028deef18b41418873ab2256589eb5c26ca42fe` | .NET 8 executable; project-references raWWar; test-only FSM_COS package reference `0.1.0-alpha.5`. |
| Experience boundary | [`docs/EXPERIENCE_ARCHITECTURE.md`](https://github.com/TrentBest/raWWar/blob/development/docs/EXPERIENCE_ARCHITECTURE.md) — blob `e994a2c897b8a1caa3a6d2dd20238a346c6659d8` | raWWar owns domain meaning; Workshop capabilities provide reusable machinery; FSM_COS is composition boundary; Renderer is observer-relative and not authoritative. |
| Formation lifecycle | [`docs/FORMATION_ASSEMBLY_AND_RELEASE.md`](https://github.com/TrentBest/raWWar/blob/development/docs/FORMATION_ASSEMBLY_AND_RELEASE.md) — blob `4812adb563277087b3140ec094e94fe123fc4220` | Formation behavior remains a candidate contract and explicitly does not claim locomotion, collision, or a complete slot runtime. Physics/Gestures gates remain mandatory. |

The workflow currently performs Soldier Field preview generation and validation, Node tests, .NET build, local package-content validation, executable contract checks, candidate event-frame verification, and the KeplerOrbit example. It uploads the generated Soldier Field preview SVGs. The workflow does **not yet** publish a dedicated CiAi evidence packet, invoke HeadlessAi, or demonstrate an authorized repair cycle.

The current test harness is a console executable rather than a conventional test-runner project. It now records each actual `Check` invocation (including checks routed through formation/journal helper suites) and emits a genuine per-check TRX file at `TestResults/results.trx`; the workflow uploads that file as `raWWar-contract-results-{run_id}-{attempt}` even when a later step fails. Check identities are unique within a run, pass/fail outcomes come directly from the executed boolean assertions, and failed checks retain their diagnostic text. The existing console summary and nonzero exit behavior are preserved. This is real harness output, not fabricated per-check evidence. It is **not yet a complete CiAi evidence packet**: trusted workflow provenance, provider-authenticated artifact-to-job association, and the raWWar-specific observer/host composition remain separate requirements. A separate temporary writer self-test exercises both pass and controlled-failure serialization, validates counters/IDs/diagnostics, and deletes its fixture. That synthetic fixture never enters the real uploaded result file. The TRX artifact must not be represented as a complete or trusted CiAi verdict by itself.

The README's current architecture/status statements remain authoritative: the Experience is actively designed and engineered, but a complete playable slice and end-to-end AnyApp host launch have not been delivered.

## 3. Responsibility map

| Component | Owns | Must not be treated as |
|---|---|---|
| GitHub Actions in raWWar | Authoritative checkout, build, test, workflow execution, run/attempt/job/step records | An LLM verdict or proof that every artifact was consumed |
| CiAi | Read-only CI observation, exact provenance, evidence normalization, completeness/policy evaluation | A CI runner, permission grant, or source of game-domain truth |
| HeadlessAi | Bounded interpretation of supplied evidence and structured diagnostic/repair proposals | An authority to write, merge, publish, release, or deploy |
| FSM_Foundry / host adapter | Orchestration and the existing host-side human authorization boundary | A second CI system or an alternate approval ledger |
| raWWar | Domain contracts, expected behavior, test fixtures, and acceptance of changes | A place to reimplement generic Workshop infrastructure |

Model output is untrusted input. A proposal is not approval. A successful CI run must be tied to the exact resulting commit and attempt, and required evidence must be complete and terminal before it can be considered successful.

## 4. First vertical slice

Start read-only. Do not begin with combat, formation locomotion, persistence, multiplayer, or a large Galactic War feature.

1. **Observe a real run.** Select a pull-request or push run of `build.yml`; capture repository, workflow path, workflow/run ID, attempt, head SHA, jobs, steps, conclusion, and artifact metadata.
2. **Normalize through CiAi.** Use the existing CiAi contracts and observer. Require exact SHA and attempt correlation. Missing, duplicate, non-terminal, unsuccessful, or incomplete required evidence must remain incomplete/failed closed.
3. **Choose a small deterministic contract.** Use an existing isolated raWWar contract check or add a narrowly scoped check whose expected behavior is unambiguous.
4. **Create a controlled failure fixture.** Prefer a test-only opt-in fixture or a separate test invocation. Never make the ordinary development build fail, and never disguise synthetic fixture data as a real provider result.
5. **Interpret with HeadlessAi.** Supply the normalized evidence and explicit task scope. Require a structured diagnosis that references the failing check and exact source/run provenance.
6. **Propose, do not self-authorize.** Require a bounded proposal: target paths, rationale, expected behavior, test plan, exclusions, and evidence references. Any write/privileged operation stays behind the existing host-side approval boundary.
7. **Verify the authorized change.** Run CI again; correlate the successful run to the exact resulting commit and attempt. Retain both failure and success evidence so the cycle is auditable.

## 5. Evidence profile requirements

The initial raWWar profile should require the repository's actual build workflow path, not a guessed workflow name. At minimum, a future CiAi consumer policy must verify:

- exact repository identity and full 40-character commit SHA;
- exact workflow path and workflow identity;
- unique run identity and attempt;
- terminal run and job conclusions;
- required build, Node contract, .NET contract, and example steps;
- expected artifact metadata where the selected policy requires an artifact;
- no missing, duplicate, stale, or conflicting evidence;
- explicit distinction between **evidence complete** and **all required checks successful**.

Do not require an artifact merely because the current workflow uploads one. First establish whether the provider observer can retrieve the necessary metadata and whether the CiAi contracts represent it without adding a parallel evidence model. Do not treat the Soldier Field preview artifact as a test-result artifact.

## 6. Upstream contracts inspected (2026-11-14)

The upstream stack has progressed beyond the initial reconnaissance. The following are real candidate contracts, but each referenced PR remains draft/unmerged; they are not yet a released or canonical integration surface.

| Layer | Current candidate | Verified responsibility and boundary |
|---|---|---|
| CiAi producer/observer | [CiAi PR #65](https://github.com/TrentBest/TheSingularityWorkshop.CiAi/pull/65), head `3df707a35c53ac270a2d15d2e08139f263fedbb8` | Defines `ciai.workflow-provenance.v1` and `ciai.test-results.v1`; observes provider run/job/artifact metadata, verifies exact source artifact and provenance, parses genuine TRX, and emits a fail-closed evidence report. The current exact-commit observer workflow is configured around CiAi's own canonical push workflow; raWWar-specific provider/run policy and host composition are still required. |
| Foundry consumer and lifecycle | [Foundry PR #15](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Foundry/pull/15), head `813118ccb82d44d7c579590725ab0654591ffdd6` | Defines the v1 notice/report consumer, binds the report to persisted task/dispatch/run identity, applies CiAi's named verdict to the task lifecycle, and supports a same-task HeadlessAi repair-turn handoff. Its fake-host integration tests do not prove the deployed authenticated receiver or real raWWar report delivery. |
| HeadlessAi invocation | [HeadlessAi source](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) | Core input is provider-neutral `HeadlessAiInput(Content, Metadata)`; output is normalized `HeadlessAiOutput(Content, StatusCode, MediaType, Metadata)`. The host supplies task planning, role/permissions/budgets, output-schema validation, tool mediation, and consequential actions. Model output is not an authority grant. |
| Foundry authorization | [Foundry PR #15](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Foundry/pull/15) and [Foundry host-composition guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Foundry/blob/development/docs/CIAI_ARTIFACT_HOST_COMPOSITION.md) | Foundry owns durable task correlation/lifecycle and bounded turn orchestration; the deployed host owns sender authentication, credentials, secure transport, repository operations, and explicit human approval. CiAi owns the CI verdict; Foundry must not recreate a second CI evaluator. |

The source-artifact identity, outer report-artifact identity, notice identity, source run/attempt, observer run, and candidate SHA are distinct. Never collapse them into one generic run ID or digest. The Foundry contract requires the authenticated host to validate the notice and persisted dispatch mapping, retrieve exact bounded report bytes, verify digest/size/expiry, and preserve nested source-artifact provenance. The candidate's fake-host tests establish contract behavior, not production ingress/network security or a live provider round trip.

### RaWWar-specific integration gaps

- No raWWar-specific CiAi provider profile/observer configuration or production host composition has been verified. CiAi PR #65's current observer is not automatically a general-purpose observer for arbitrary Experience repositories.
- raWWar now emits actual per-check TRX from the executable harness and uploads it from the ordinary build workflow. The artifact is not yet accompanied by trusted workflow provenance or consumed by a raWWar-specific CiAi observer; do not treat it as a complete CiAi evidence packet.
- The current raWWar workflow runs on pull requests and pushes to canonical branches. A candidate-repair loop needs a trusted, exact-SHA workflow/checkout policy that can validate an isolated candidate branch without granting that candidate control over trusted workflow definitions. Do not add `workflow_dispatch` or broad write permissions as a shortcut.
- The concrete deployed host still needs to compose CiAi report publication/notice delivery, Foundry authenticated ingress and retrieval, durable task correlation, HeadlessAi invocation, and the existing host-owned approval boundary.
- The controlled-failure fixture must be opt-in/test-only and must never be mistaken for a genuine GitHub provider failure or a real source-artifact observation.
- The workflow currently uses version tags for several GitHub Actions. Action pinning and workflow hardening may be worthwhile, but assess them as a separate scoped change rather than mixing them into the first consumer proof.
- Existing raWWar runtime/manifest integration gaps remain separate from CI evidence integration.

## 7. Next implementation sequence

1. Keep raWWar's current build workflow authoritative for its ordinary pull-request and canonical-branch validation.
2. Define the trusted candidate-checkout workflow boundary for isolated repair candidates. The trusted workflow revision must be independently registered/approved and bound to the exact candidate SHA; a candidate branch must not be allowed to redefine the workflow that judges it.
3. **Completed on the current PR head:** record actual per-check assertions, emit TRX, self-test pass/failure serialization, and upload the real result artifact. The remaining evidence work is trusted workflow provenance, artifact-to-job binding, and a raWWar-specific CiAi observer policy.
4. Connect the exact provider observations and report bytes to the CiAi v1 producer contract. Keep the source workflow run distinct from the CiAi observer run and the outer report artifact.
5. Compose the authenticated host path into Foundry's v1 notice/retrieval/consumer contracts; bind it to a durable raWWar task and isolated candidate ref/SHA.
6. Invoke HeadlessAi only with accepted, provenance-bound evidence and bounded task instructions. Validate the response as an untrusted proposal.
7. Require explicit host-side human authorization before applying any repository change, then rerun the trusted candidate validation against the exact resulting SHA.

Do not create a second scheduler, CI verdict engine, evidence schema, HTTP transport, or approval ledger inside raWWar.

## 8. Explicit exclusions

This integration does not authorize or imply NuGet publication, merge, release, deployment, canonical branch mutation, branch deletion, privileged workflow dispatch, autonomous writes, or any weakening of CiAi/HeadlessAi/Foundry authorization gates. The current branch policy remains exactly `master` and `development`; no cleanup is part of this task.

It also does not supersede [issue #7](https://github.com/TrentBest/raWWar/issues/7) (formation lifecycle) or [issue #8](https://github.com/TrentBest/raWWar/issues/8) (shared-universe mode architecture).

## 9. Acceptance evidence

The integration is not complete until there is a reproducible evidence chain containing:

- one real raWWar run observed by CiAi at an exact SHA and attempt;
- one controlled failure represented without fabricating provider evidence;
- one HeadlessAi diagnosis/proposal tied to that evidence and constrained by the task;
- explicit host-side authorization before any protected write;
- one rerun against the resulting exact SHA and attempt;
- a complete, terminal, successful evidence verdict for all required checks;
- documentation distinguishing implemented, tested, observed, proposed, and unavailable capabilities.

Until those artifacts exist, raWWar is an **integration candidate**, not a proven Autonomous Development Pipeline consumer.
