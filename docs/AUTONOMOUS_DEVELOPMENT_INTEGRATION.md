# raWWar Autonomous Development Pipeline Integration

**Status:** Initial consumer reconnaissance; advisory/read-only integration is the first gate.  
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

The current test harness has a useful deterministic contract surface. However, it is a console harness rather than a conventional test-runner project, and its current output is a summary plus failure text. Do not assume each individual `Check` is already emitted as a structured test result. A controlled-failure mechanism and structured evidence producer must be designed without weakening the normal test run.

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

## 6. Known gaps and questions to resolve

- No raWWar-specific CiAi consumer configuration or evidence adapter has been verified.
- The current workflow has no structured per-check TRX/JUnit output. Determine whether CiAi's existing provider/run/artifact contracts are sufficient for the first advisory slice before adding a producer.
- The current console contract harness aggregates individual checks. A controlled failure must identify a stable check and bounded diagnostic without changing normal pass/fail semantics.
- The exact supported HeadlessAi input/output contract and the Foundry host adapter integration point must be verified from their current source revisions before implementation.
- The human authorization boundary must remain in the host adapter; do not invent an approval receipt or allow a model response to authorize a write.
- The workflow currently uses version tags for several GitHub Actions. Action pinning and workflow hardening may be worthwhile, but must be assessed as a separate scoped change rather than mixed into the first consumer proof.
- Existing raWWar runtime/manifest integration gaps remain separate from CI evidence integration.

## 7. Explicit exclusions

This integration does not authorize or imply NuGet publication, merge, release, deployment, canonical branch mutation, branch deletion, privileged workflow dispatch, autonomous writes, or any weakening of CiAi/HeadlessAi/Foundry authorization gates. The current branch policy remains exactly `master` and `development`; no cleanup is part of this task.

It also does not supersede [issue #7](https://github.com/TrentBest/raWWar/issues/7) (formation lifecycle) or [issue #8](https://github.com/TrentBest/raWWar/issues/8) (shared-universe mode architecture).

## 8. Acceptance evidence

The integration is not complete until there is a reproducible evidence chain containing:

- one real raWWar run observed by CiAi at an exact SHA and attempt;
- one controlled failure represented without fabricating provider evidence;
- one HeadlessAi diagnosis/proposal tied to that evidence and constrained by the task;
- explicit host-side authorization before any protected write;
- one rerun against the resulting exact SHA and attempt;
- a complete, terminal, successful evidence verdict for all required checks;
- documentation distinguishing implemented, tested, observed, proposed, and unavailable capabilities.

Until those artifacts exist, raWWar is an **integration candidate**, not a proven Autonomous Development Pipeline consumer.
