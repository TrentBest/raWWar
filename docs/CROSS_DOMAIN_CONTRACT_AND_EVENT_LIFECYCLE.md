# Cross-Domain Contracts and Event Lifecycle

**Status:** Proposed shared-world domain contract and first vertical-slice specification. Not finalized setting canon and not implemented runtime behavior.  
**Parent design:** [Society, Governance, and Civil Institutions](SOCIETY_GOVERNANCE_AND_CIVIL_INSTITUTIONS.md) and [Shared-Experience Architecture](SHARED_EXPERIENCE_ARCHITECTURE.md).  
**Purpose:** Define the smallest credible contract that lets different occupations act on the same world facts, with attributable outcomes and deterministic acceptance rules.

## 1. Why this slice comes first

The society vision spans government, law, media, science, logistics, enterprise, and independent work. Implementing each as a separate activity would create disconnected minigames. The first proof should instead exercise one shared chain of obligations:

1. A zoological institution requests a live specimen under stated care and legal constraints.
2. A qualified player crew may accept and perform the field work.
3. A government authority may issue, deny, or condition a permit within its jurisdiction.
4. A transport operator may accept custody and deliver the specimen under declared conditions.
5. A receiving institution verifies delivery and care requirements.
6. A journalist may report on the events using evidence available to them.

This is a proposed vertical slice, not a claim that these systems already exist. It is deliberately smaller than a complete economy, legal simulator, zoological simulator, or persistent multiplayer service.

## 2. Shared identity: references, not duplicated entities

Every domain record refers to stable identities owned by the appropriate domain. A contract must not create private copies of the people, organizations, places, species, specimens, vessels, or legal authorities it references.

Candidate identity categories:

| Identity | Owning domain | Examples |
|---|---|---|
| Person / agent | World population / agent identity | player character, named NPC |
| Organization | Organization domain | zoo, capture company, news service, public authority |
| Place | World geography | habitat, city, port, station, facility |
| Species / specimen | Biology and knowledge domains | species concept, individual living specimen |
| Asset / vessel | Asset and operations domains | capture equipment, transport vessel |
| Jurisdiction / authority | Government and law domains | protected habitat, permit office |
| Contract | Contract domain | specimen acquisition obligation |
| Case / permit | Legal and government domains | review, authorization, appeal |
| Event | Shared event history | offer, acceptance, capture, custody transfer, publication |

IDs are references, not proof of ownership, legal title, permission, custody, qualification, or truth. Those are separate facts with separate authorities.

## 3. Contract record and authority

A contract is an explicit set of obligations between identified parties. It is not a mission marker that directly causes the desired outcome.

A minimal contract record should identify:

- stable contract ID and revision;
- issuing organization and authorized issuer;
- offer publication time and expiry;
- requested outcome and target identity or discovery criteria;
- contracting parties and accepted role assignments;
- jurisdiction and required permits or authorizations;
- qualification and equipment requirements;
- safety, condition, handling, and environmental constraints;
- milestones, deadlines, and permitted route or destination constraints;
- compensation, expenses, and settlement conditions;
- evidence required to verify each milestone;
- cancellation, dispute, breach, and appeal procedures;
- current lifecycle state and the event sequence that produced it.

The contract domain owns the obligation and its lifecycle. It does not own the truth of a capture, the legal validity of a permit, the biological condition of a specimen, or the physical location of a vessel. Those facts are supplied by their authoritative domains and referenced with provenance.

## 4. Lifecycle: explicit transitions only

Candidate lifecycle:

`Draft → Offered → Accepted → InProgress → FulfillmentSubmitted → Verified → Settled`

Alternative terminal or suspended states include `Expired`, `Cancelled`, `Disputed`, `Breached`, and `Suspended`. Their legal and commercial meanings must be specified before implementation.

| Transition | Required authority / evidence | Must not imply |
|---|---|---|
| Draft → Offered | Authorized issuer; complete offer; valid expiry | That anyone has accepted |
| Offered → Accepted | Eligible party explicitly accepts; role and capacity checks pass | That work has begun or permits exist |
| Accepted → InProgress | Required prerequisites satisfied; start event recorded | That all later milestones will succeed |
| InProgress → FulfillmentSubmitted | Performer submits milestone evidence | That evidence is true or sufficient |
| FulfillmentSubmitted → Verified | Contract verifier applies published acceptance criteria against authoritative facts | That payment has already settled |
| Verified → Settled | Settlement authority records the agreed transfer | That unrelated obligations are discharged |
| Any eligible active state → Disputed | Authorized party raises a supported dispute | That either party is guilty or correct |
| Active state → Cancelled / Expired | Contract rules and authorized event | That incurred costs or custody obligations vanish |

No domain may silently advance another domain's state. A submitted completion claim is not a verified completion. A verified obligation is not a settled payment. A cancellation does not erase a specimen, custody duty, permit, injury, expense, or historical event already created.

## 5. Event history and provenance

Each consequential transition should append an immutable event. Corrections and reversals append further events; they do not rewrite history.

A candidate event envelope contains:

- event ID and schema version;
- event type;
- effective world time and recorded time;
- actor identity and actor kind (player, autonomous agent, institution, or system process);
- acting organization and delegated authority, if any;
- subject and related entity references;
- jurisdiction and applicable rule/contract revision;
- causal predecessor or correlation reference;
- evidence references and provenance;
- outcome, uncertainty, and validation status.

The distinction between effective time and recorded time matters. A witness statement may be recorded after an event; a permit may be issued at one time and become effective later; a news report may be published after the underlying event.

An event record proves that the world recorded an occurrence under its rules. It does not automatically prove every claim made by an actor. Evidence may be incomplete, disputed, forged, mistaken, or superseded by later findings.

## 6. The specimen chain: custody is not ownership

For the first slice, model the specimen as a living entity with a stable individual identity once identified. Its current location, condition, caretaker, legal status, and custodian are distinct, time-varying facts.

Candidate custody sequence:

`Wild / Uncollected → Captured → FieldCustody → InTransit → Received → Quarantined → AcceptedIntoCare`

Possible outcomes include `Released`, `Transferred`, `Escaped`, `Injured`, `Deceased`, or `Unlocated`. These are not all interchangeable terminal states; biological and legal domain rules must define them.

Required invariants for the proof:

- At a given effective time, a specimen has at most one authoritative physical location.
- A custody transfer identifies both releasing and receiving parties, the time, location, and acceptance evidence.
- A transfer does not by itself change legal title or permit status.
- A contract cannot authorize capture where the applicable authority says capture is prohibited.
- A permit does not prove that capture occurred or that handling was safe.
- A delivery receipt does not prove the specimen meets its required condition.
- A living specimen cannot be marked as successfully delivered alive if authoritative condition evidence says it died before receipt.
- If a specimen is injured, lost, escapes, or dies, that fact remains in history and triggers the applicable contract and care procedures.

The first implementation should use one explicitly defined specimen type and a small, authored set of care constraints. It should not pretend to solve alien biology in general.

## 7. Player and autonomous fulfillment

The opportunity is visible to eligible players according to the contract's publication and access rules. A player acceptance must identify the actual player-controlled character and any organization or crew acting under delegated authority.

If no eligible player accepts, the world may eventually assign work to an autonomous agent, but only under an explicit fallback policy. That policy is not yet decided.

For any autonomous fulfillment:

- record the autonomous agent or institution as the performer;
- never fabricate a player identity or player acceptance;
- apply the same published acceptance criteria unless the contract explicitly defines a different service tier;
- record the reason and time the fallback was selected;
- account for autonomous agent qualifications, equipment, risk, and capacity;
- do not award player-earned reputation, achievements, or attribution for NPC work;
- expose the fact of autonomous fulfillment to the parties entitled to know it.

The fallback delay, player priority window, compensation differences, and NPC capability limits remain open questions. Do not implement a hidden NPC that instantly completes every unwanted task.

## 8. Cross-domain authority boundaries

| Fact or decision | Authoritative owner | Contract may do |
|---|---|---|
| Offer, obligations, milestones, breach under contract terms | Contract domain | Define and evaluate obligations |
| Person identity and player/NPC classification | Identity / population domain | Reference performers |
| Qualification and current capacity | Training / organization domains | Require and check prerequisites |
| Permit, legal restriction, jurisdiction | Government / law domains | Require valid authorization |
| Species identity and specimen condition | Biology / research domain | Specify target and acceptance thresholds |
| Physical location and custody | World / logistics domains | Require custody milestones |
| Payment and settlement | Economy domain | Define consideration and settlement trigger |
| News account and publication | Media domain | Provide publishable event/evidence references |
| Event history | Shared event-history capability | Correlate events; never rewrite them |

A contract verifier may combine authoritative facts, but must not become the authority for those facts. A UI, news report, AI-generated summary, or client-side simulation is a projection, never the source of truth.

## 9. Deterministic acceptance rules

The first proof must use deterministic rules that can be evaluated from a fixed event/evidence set and fixed contract revision.

A submitted milestone is accepted only if all required predicates pass. Example for live specimen delivery:

1. The accepted contract revision names the specimen or valid discovery criteria.
2. The performer held the required qualification and capacity at the relevant time.
3. Every required permit was valid for the relevant place, actor, activity, and time.
4. Capture and custody events form a valid sequence with no unexplained gap or overlapping custodian.
5. The specimen's authoritative condition at receipt satisfies the contract's explicit threshold.
6. The receiving facility and responsible caretaker satisfy the required capability constraints.
7. Evidence references resolve to records accepted by the relevant authority.
8. No active injunction, suspension, or unresolved blocking dispute applies.

Failure must produce a specific failed predicate and preserve the submitted evidence. It must not silently coerce the state to success. Where evidence is insufficient rather than contradictory, the result should be `NeedsEvidence`, not an invented pass or an automatic accusation of breach.

The initial implementation should avoid probabilistic adjudication. Any later AI assistance may organize evidence or propose an assessment, but the authoritative decision must remain traceable to explicit rules and authorized actors.

## 10. News and public knowledge are projections

A reporter may discover an event through observation, testimony, records, or an authorized feed. The media domain records the sources available to the reporter, the account they produce, editorial review, publication, distribution, and corrections.

The contract and event domains must not grant universal knowledge to every player. A public broadcast is a publication event; it does not mean every resident saw it or believes it. A confidential permit or protected specimen location must not become public merely because a related contract exists.

## 11. First vertical-slice acceptance tests

The first proof should be executable as deterministic domain tests, even before a full 3D occupation experience exists.

| Test | Expected invariant |
|---|---|
| Two crews accept the same exclusive offer | At most one acceptance succeeds under the contract's concurrency rule |
| Acceptance occurs after expiry | Rejected; no active obligation created |
| Crew lacks a required qualification | Acceptance or start is rejected according to the published rule |
| Permit is denied | Capture cannot be authorized by the contract |
| Permit expires before capture | Capture fails the authorization predicate |
| Capture succeeds but condition threshold fails | Fulfillment is not verified |
| Custody transfer omits receiver acceptance | Chain remains incomplete |
| NPC fulfills after the player window | NPC is recorded as performer; no player attribution is created |
| Evidence is submitted twice | Idempotent handling; no duplicate settlement |
| A verified event is later challenged | New dispute/review events; original history retained |
| News account contradicts verified evidence | Publication remains a media fact, not a replacement world fact |
| Contract is cancelled after capture | Existing specimen and care obligations remain represented |

Concurrency, idempotency, replay, and deterministic ordering are required even in a single-process proof. Multiplayer scale can come later; the domain model should not depend on an unsafe assumption that only one request can ever arrive.

## 12. Proposed implementation order

1. Define stable references and event envelope using existing upstream capabilities where they genuinely fit; do not duplicate StoryBuilder or other source-owned systems.
2. Implement the contract lifecycle as a pure, deterministic state transition model.
3. Add one specimen identity and custody lifecycle with explicit authoritative ownership.
4. Add a minimal permit decision and jurisdiction check.
5. Add player acceptance and a clearly attributed autonomous fallback in tests.
6. Add a receiving-facility verification and settlement boundary.
7. Add a media projection test proving publication does not mutate event truth.
8. Only then build embodied occupation interactions and persistent multiplayer integration.

Each step needs source revision pins, contract tests, failure-path tests, and explicit limits. Renderer and Visualization remain presentation/experience MicroBundles, not authoritative domain stores or alternate sources of world truth.

## 13. Decisions deliberately left open

- Which government and legal model governs the first specimen location?
- Which species are sentient, protected, domesticated, or otherwise subject to special treatment?
- What exact care/condition scale is meaningful for the first specimen?
- Who may issue capture permits, and what appeal or review exists?
- How long is a player opportunity protected before autonomous fulfillment?
- Can players subcontract, form temporary crews, or transfer obligations?
- What compensation and insurance apply to injury, loss, escape, or death?
- Which contract and event facts are public, private, or restricted?
- What happens when a receiving institution loses capacity during transit?
- Which upstream identity, event, authorization, and narrative contracts already exist and must be reused?

These questions must be answered by design evidence or creator direction, not guessed by implementation.

## 14. Acceptance boundary

This document defines a proposed first shared-world contract proof. It does not claim an implemented contract engine, economy, legal system, zoological simulator, autonomous labor market, media runtime, durable event store, or multiplayer service.

The slice succeeds when multiple domains can contribute authoritative facts to one contract outcome; every consequential action is attributable; failed or disputed work remains visible; NPC fulfillment is honestly identified; and no presentation layer can rewrite the underlying world.
