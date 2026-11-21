**Status (2026-11-21):** Latest fully validated PR source head `c0ed299859c540433b7616a794271672cab129f2` passed GitHub Actions run [#1240](https://github.com/TrentBest/raWWar/actions/runs/44887261843), attempt 1. The single build-and-contracts job completed successfully; solution build, local package-content check, executable contract checks, TRX/provenance packaging and uploads, Soldier Field checks, event-frame verification, and KeplerOrbit example all succeeded. The run uploaded the contract/provenance archive (artifact `14700004729`, SHA-256 `61eac724b655e19a166a4b2a06d36f4ea8bac09caffb292ff8d2b5b21922e57d`) and workflow provenance (artifact `14700660832`, SHA-256 `07f2a649e0e452b2ae0f9ce58710476bb2529c58bfa34eb3a873d3c1c076212f`). This is candidate-controlled `pull_request` workflow evidence, so it remains structurally useful but untrusted/non-actionable until CiAi's independent workflow-definition trust and provider-identity checks are implemented. The authenticated human-decision surface remains a proposed capability request; no live inbox or authorized repair cycle is proven. A subsequent documentation-only status refresh is now at PR head `1590489a60018f6a49c923db90be2da0ba072438`; its PR workflow run #1242 is in progress, so that newer head is not yet claimed validated. Full end-to-end CODE RED repair remains unproven.  
**Related task:** [raWWar issue #9](https://github.com/TrentBest/raWWar/issues/9)  
**Scope:** Make raWWar a flagship Experience developed through the Workshop Autonomous Development Pipeline. This document distinguishes source-backed implementation, tested behavior, proposed capabilities, and unavailable integration; it does not claim the pipeline is connected or autonomous repair is implemented.

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

The current test harness is a console executable rather than a conventional test-runner project. It now records each actual `Check` invocation (including checks routed through formation/journal helper suites) and emits a genuine per-check TRX file at `TestResults/results.trx`. The workflow also emits a `ciai.workflow-provenance.v1` record from the actual GitHub event/workflow/checkout context, uploads it separately as `rawwar-workflow-provenance-{attempt}`, and packages it beside the TRX as `provenance.json` in `rawwar-contract-results-{run_id}-{attempt}`. On run #1199, the provider reported workflow/event/checkout SHA `7d738cd70e59cbe452771171960083779d049623` (the PR merge ref), while the record separately preserved PR head SHA `fcac6e2e3297985bf6988171a1fe2f9a2d8ad87e` and base SHA `ddd4c0b5ce34d4a1291b76824b265701cb32e436`. Check identities are unique within a run, pass/fail outcomes come directly from the executed boolean assertions, and failed checks retain their diagnostic text. The existing console summary and nonzero exit behavior are preserved. This is real harness output, not fabricated per-check evidence. It is **not yet a complete CiAi evidence packet**: trusted workflow provenance, provider-authenticated artifact-to-job association, and the raWWar-specific observer/host composition remain separate requirements. A separate temporary writer self-test exercises both pass and controlled-failure serialization, validates counters/IDs/diagnostics, and deletes its fixture. That synthetic fixture never enters the real uploaded result file. The provenance payload is a workflow-side claim, not self-authenticating authority: CiAi/host must independently verify the workflow definition, exact provider run/attempt, job, artifact metadata/digest, event identity, and checkout before trusting it. The run produced 383/383 passing checks; the downloaded source artifact contained exactly `results.trx` and `provenance.json`, with 383 unique result rows/IDs and matching counters. The separate provenance artifact contained exactly one `workflow-provenance.json` entry. The PR workflow ref is `refs/pull/10/merge`, so its workflow definition is still candidate-controlled; these artifacts are structurally CiAi-compatible but must remain untrusted until the host independently verifies the workflow definition against an approved control-plane revision. The TRX artifact must not be represented as a complete or trusted CiAi verdict by itself.

The README's current architecture/status statements remain authoritative: the Experience is actively designed and engineered, but a complete playable slice and end-to-end AnyApp host launch have not been delivered.

## 3. Responsibility map

| Component | Owns | Must not be treated as |
|---|---|---|
| GitHub Actions in raWWar | Authoritative checkout, build, test, workflow execution, run/attempt/job/step records | An LLM verdict or proof that every artifact was consumed |
| CiAi | Read-only CI observation, exact provenance, evidence normalization, completeness/policy evaluation | A CI runner, permission grant, or source of game-domain truth |
| HeadlessAi | Bounded interpretation of supplied evidence and structured diagnostic/repair proposals | An authority to write, merge, publish, release, or deploy |
| FSM_Execution / authenticated host adapter | Exact-SHA task admission, durable task lifecycle, bounded execution orchestration, and capability-gated repository operations; the deployed host retains authentication, credentials, and explicit human authorization | A second CI verdict engine or an alternate approval ledger |
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
| FSM_Execution host and lifecycle | [FSM_Execution PR #40](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Execution/pull/40), validated candidate head `42e5cff724f1944e0939b5a3360848ed971f3973` | The renamed execution substrate replaces FSM_Foundry. PR #40 combines exact-development-SHA AgentOnboarding admission, persisted task lifecycle/restart revalidation, capability-gated GitHub repository operations, and bounded HeadlessAi coordination. The exact candidate head passed 222/222 workflow runtime tests and six builds; live privileged dispatch, production credential provisioning, and a complete CODE RED repair cycle remain unproven. PR remains draft/unmerged. |
| HeadlessAi invocation | [HeadlessAi source](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) | Core input is provider-neutral `HeadlessAiInput(Content, Metadata)`; output is normalized `HeadlessAiOutput(Content, StatusCode, MediaType, Metadata)`. The host supplies task planning, role/permissions/budgets, output-schema validation, tool mediation, and consequential actions. Model output is not an authority grant. |
| Host authorization and execution | [FSM_Execution](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Execution) | FSM_Execution is the current execution substrate and owns its task/execution contracts. The deployed host remains responsible for sender authentication, credentials, secure transport, provider operations, and explicit human approval. CiAi owns the CI verdict; FSM_Execution must not recreate a second CI evaluator. |

The source-artifact identity, outer report-artifact identity, notice identity, source run/attempt, observer run, and candidate SHA are distinct. Never collapse them into one generic run ID or digest. The FSM_Execution/host path must validate authenticated notices and persisted task/dispatch mappings, retrieve exact bounded report bytes, verify digest/size/expiry, and preserve nested source-artifact provenance. Candidate tests establish contract behavior, not production ingress/network security or a live provider round trip.

### RaWWar-specific integration gaps

- No raWWar-specific CiAi provider profile/observer configuration or production host composition has been verified. CiAi PR #65's current observer is not automatically a general-purpose observer for arbitrary Experience repositories.
- raWWar now emits actual per-check TRX from the executable harness and uploads it from the ordinary build workflow. The artifact is not yet accompanied by trusted workflow provenance or consumed by a raWWar-specific CiAi observer; do not treat it as a complete CiAi evidence packet.
- The current raWWar workflow runs on pull requests and pushes to canonical branches. A candidate-repair loop needs a trusted, exact-SHA workflow/checkout policy that can validate an isolated candidate branch without granting that candidate control over trusted workflow definitions. Do not add `workflow_dispatch` or broad write permissions as a shortcut.
- The concrete deployed host still needs to compose CiAi report publication/notice delivery, FSM_Execution authenticated ingress and retrieval, durable task correlation, HeadlessAi invocation, and the existing host-owned approval boundary.
- The controlled-failure fixture must be opt-in/test-only and must never be mistaken for a genuine GitHub provider failure or a real source-artifact observation.
- The workflow currently uses version tags for several GitHub Actions. Action pinning and workflow hardening may be worthwhile, but assess them as a separate scoped change rather than mixing them into the first consumer proof.
- Existing raWWar runtime/manifest integration gaps remain separate from CI evidence integration.

## 7. Next implementation sequence

1. Keep raWWar's current build workflow authoritative for its ordinary pull-request and canonical-branch validation.
2. Define the trusted candidate-checkout workflow boundary for isolated repair candidates. The trusted workflow revision must be independently registered/approved and bound to the exact candidate SHA; a candidate branch must not be allowed to redefine the workflow that judges it.
3. **Completed and CI-verified:** record actual per-check assertions, emit TRX, self-test pass/failure serialization, and upload the real result artifact. The remaining evidence work is independent workflow-definition trust, exact provider artifact-to-job binding, and a raWWar-specific CiAi observer policy. The current CiAi sample observer hard-codes `.github/workflows/ci.yml`, the `contract` job, CiAi-specific artifact names, and a trusted-workflow assertion; it cannot be pointed at raWWar unchanged. This upstream work is tracked in [CiAi issue #70 — generalize exact-run evidence observer for external Experience repositories](https://github.com/TrentBest/TheSingularityWorkshop.CiAi/issues/70). A second PR-specific identity gap is now documented there: GitHub's workflow-run API reports the PR source `head_sha`, while the workflow's event/checkout SHA is the merge commit. The current CiAi evidence packet equates those values, which is valid for push runs but not PR merge runs. Do not treat PR #10's run as actionable; either CiAi must independently verify PR head/base/merge-parent identities or the first actionable pilot must observe a trusted `push` run on `development` after any merge is explicitly authorized. Preserve fail-closed trust before attempting report/HeadlessAi handoff.
4. Connect the exact provider observations and report bytes to the CiAi v1 producer contract. Keep the source workflow run distinct from the CiAi observer run and the outer report artifact.
5. Compose the authenticated host path into FSM_Execution's current task/execution contracts; bind it to a durable raWWar task and isolated candidate ref/SHA.
6. Invoke HeadlessAi only with accepted, provenance-bound evidence and bounded task instructions. Validate the response as an untrusted proposal.
7. Require explicit host-side human authorization before applying any repository change, then rerun the trusted candidate validation against the exact resulting SHA.

Do not create a second scheduler, CI verdict engine, evidence schema, HTTP transport, or approval ledger inside raWWar.

## 8. Explicit exclusions

This integration does not authorize or imply NuGet publication, merge, release, deployment, canonical branch mutation, branch deletion, privileged workflow dispatch, autonomous writes, or any weakening of CiAi/HeadlessAi/FSM_Execution authorization gates. The current branch policy remains exactly `master` and `development`; no cleanup is part of this task.

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


## 10. Human decision and reporting surface

The raWWar proving path now includes a product requirement beyond CI evidence: CODE RED must be able to stop at a genuine human decision, explain why it stopped, and resume only after an authenticated, auditable response. A green/red CI badge is useful repository status, but it is not a human decision system.

### 10.1 The user experience

The human-facing surface is an authenticated, cross-repository inbox. It should provide:

- a repository map showing the repositories the signed-in person is authorized to review;
- a concise overview for each repository, including task state, current evidence status, and relevant policy/badge state;
- a pending-decision queue with the question, why the model cannot safely decide, available choices, recommendation, consequences, and supporting evidence;
- an aggregation of similar decisions, with every member individually inspectable;
- a durable history of answers, reusable rules, and the exact policy version applied;
- a rich-text response path when the ambiguity cannot be expressed adequately as structured choices.

A decision should not be hidden in email, chat, or a transient notification. The inbox is authoritative and durable. Notifications merely bring the person back to it.

### 10.2 LLM-composed, constrained GUI

The LLM should compose the decision presentation from the decision context, not author an arbitrary web page. It should choose the smallest semantic interaction that makes the ambiguity understandable:

- single choice when one mutually exclusive answer is needed;
- multi-select when multiple options may apply;
- structured fields when the answer has a known schema;
- rich-text input when the problem needs explanation or new information;
- grouped review when multiple decisions appear to share a rule.

The model may supply the question, explanation, options, rationale, and a proposed semantic control tree. The renderer must validate that proposal against a supported GUI vocabulary and bind controls to a typed decision payload. Do not execute model-authored HTML, JavaScript, callbacks, or arbitrary components. A model recommendation is not an approval.

The current GUI architecture points to GUI.Core for platform-neutral semantics and GUI.Blazor for browser manifestation. Its architecture document also lists semantic input as not yet a stable runtime contract. The required decision/input contract is therefore a capability request, not a claim that the complete surface already exists. See [the GUI decision-surface request](requests/REQUEST-FROM-raWWar-GUI-LLM-ComposedDecisionSurface.md).

### 10.3 One-time answers versus reusable policy

Every decision must distinguish:

1. **Answer this instance:** a one-time response tied to one decision ID, task, repository, exact candidate SHA, and evidence identity.
2. **Approve a reusable rule:** a human-approved policy with a visible match scope, stable policy ID/version, rationale, approver, and revocation path.

The LLM may suggest that decisions are similar. It must not silently merge them or infer that one answer authorizes all of them. Before approving a reusable rule, the human must be able to inspect the grouped decisions and see the repository, task, exact SHA, evidence, proposed match criteria, and consequence for each member. The system records which policy version applied to each decision. Cases outside the declared scope, or cases with ambiguous similarity, return to a human.

A free-form response is preserved as human input. The host may ask an LLM to interpret it into a structured proposal, but the user must confirm any consequential interpretation that is not already unambiguous. The host remains responsible for validation, authorization, and execution.

### 10.4 Browser access and notifications

Initial technology reconnaissance identifies [WebPage](https://github.com/TrentBest/WebPage) as the strongest existing browser starting point:

- Blazor WebAssembly client in TheSingularityWorkshop;
- Azure Static Web Apps authentication via StaticWebAppAuthenticationStateProvider and /.auth/me;
- Azure Functions v4 API project in PollingAPI;
- an existing Azure Table Storage usage example in VoteFunction.cs.

This is a starting foundation, not a completed secure notification system. The inspected WebPage tree contains no service-worker/Web Push implementation or durable decision inbox. The current vote endpoints are anonymous and are not suitable for approvals. Client-side identity display does not replace server-side authentication and authorization.

The proposed browser notification mechanism is standards-based Web Push (service worker, Push API, Notification API, and server-managed VAPID keys), with explicit browser opt-in. Email may serve as an invitation or fallback where recipients are explicitly configured. Push and email payloads should carry only an opaque decision reference; the person signs in and retrieves protected details from the inbox.

**The durable inbox is authoritative; push/email delivery is best effort.** A missed, blocked, expired, or unsupported notification must never lose the decision. The backend must authenticate every read and write, resolve repository membership itself, and never trust client-supplied user identity, repository scope, or approval authority.

The existing ServerApp repository is currently a minimal shell, not an implemented notification backend. Its eventual role should be evaluated with the ServerApp owner rather than presumed. See [the WebPage inbox and notification request](requests/REQUEST-FROM-raWWar-WebPage-AuthenticatedDecisionInboxAndNotifications.md).

### 10.5 Minimum decision record and audit identity

A decision record should preserve, at minimum:

- immutable decision ID and current state;
- task ID and task lifecycle identity;
- repository full name and authorized recipient identity;
- exact source/candidate commit SHA;
- CiAi report identity and digest, including the source workflow/run/attempt and any observer/report artifact identities;
- the question, ambiguity, options, recommendation, and model-produced explanation;
- the answer payload and whether it is one-time or a reusable policy;
- policy ID/version and the exact set of decisions to which it was applied;
- timestamps, actor identity, and audit events.

Do not collapse task ID, decision ID, GitHub run ID, workflow attempt, artifact ID, evidence digest, or policy ID into one generic correlation token. Every transition must be idempotent and attributable. A notification being delivered or opened does not change decision state.

### 10.6 Human-loop acceptance slice for raWWar

The first proof should use a deliberately bounded raWWar engineering decision, not a gameplay-critical or irreversible operation. Demonstrate the following sequence:

1. A real raWWar task reaches a declared decision point and records the exact task/repository/SHA/evidence identity.
2. HeadlessAi explains the ambiguity and proposes a small semantic decision interface; malformed or unsupported GUI output is rejected.
3. The authenticated inbox shows the decision with evidence and meaningful options.
4. The human submits a one-time answer; the host validates the answer and records the actor and exact decision identity.
5. A second, deliberately similar decision is grouped as a candidate reusable rule. The human inspects the group and explicitly approves or rejects the proposed scope.
6. The audit view shows which decisions the policy version affected and which decisions remained outside its scope.
7. A case with insufficient match confidence returns to the human rather than inheriting the rule.
8. A rich-text response can be submitted for a genuinely ambiguous case without treating model interpretation as authorization.
9. Browser notification failure is simulated; the decision remains available in the inbox.

This slice is not complete until it is tested against authenticated users and repository boundaries. It does not authorize autonomous writes, merge, release, publication, deployment, or any weakening of the existing host approval gate.
