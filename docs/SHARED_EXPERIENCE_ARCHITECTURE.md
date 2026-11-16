# raWWar Shared-Experience Architecture

**Status:** source-grounded architecture proposal; candidate design, not implemented runtime behavior.  
**Source baseline:** Experiences PR #51 merged at `f173d47e071cc2dd430cb86925eb58d36f4048cf`; Experiences PR #56 draft head `9a02899c5ccc853ccccefa396b79fc86e5560942`; raWWar design alignment at PR #10 head `4e973b8b5e71890f0557291e485ff0e5682b2301`.  
**Purpose:** reconcile base-centered life and Galactic War's wider-galaxy design as different ways to exist in one persistent Experience.

## 1. Architectural decision

The base and the wider galaxy are not separate universes and should not be treated as isolated games that happen to share lore. They are different ways to exist in the same Experience.

The same galaxy contains military personnel, civilians, independent travelers, workers, researchers, merchants, pirates, passengers, ship crews, organization owners, and people who have no direct involvement in a current conflict. War can be enormous and consequential without making every inhabitant a soldier or commander.

The distinction is about **where a person is, what they do, what they know, what responsibilities they hold, and at what scale they exercise agency**—not which universe they occupy.

This architecture preserves distinct activity loops and player-facing controls while requiring continuity of authoritative world facts, people, places, organizations, assets, infrastructure, events, history, and consequences.

## 2. One world, multiple dimensions of participation

These dimensions are independent and should not be collapsed into a single class or mode selector.

### 2.1 Place and circumstance

A person may be at a military base, station, ship, settlement, frontier route, research site, battlefield, or remote environment. A location can support several kinds of activity at once.

The military base remains a major raWWar setting and an important social and operational center. It is not the boundary of the universe. Stations, ships, ports, and settlements are inhabited places with their own routines, institutions, services, and local circumstances.

### 2.2 Occupation and activity

Candidate examples—not a finalized occupation catalogue—include:

- Military service: soldier, officer, commander, logistics, research, engineering, medical, defense, and support roles.
- Civilian work: freight pilot, passenger-ship captain or crew, station worker, builder, mechanic, researcher, educator, and specialist service provider.
- Independent life: travel, trade, exploration, archaeology, and piracy.
- Organizational work: employee, contractor, owner, executive, dispatcher, planner, or operator.
- Participation without ownership: passenger, customer, visitor, student, audience member, or local resident.

Occupations are ways to participate, not mutually exclusive game modes. A person may change roles over a lifetime.

### 2.3 Agency and responsibility

The same activity can be experienced at different levels:

1. **Direct participant:** personally pilots, repairs, serves, researches, negotiates, or performs the work.
2. **Crew or team member:** contributes within a coordinated group.
3. **Leader or supervisor:** directs people and makes operational decisions.
4. **Owner or executive:** sets organizational priorities, allocates resources, hires or contracts, and accepts responsibility for outcomes.
5. **Competitor or partner:** competes or cooperates with other organizations.
6. **Customer or affected participant:** travels, purchases, receives a service, or experiences the consequences.

These are relationships to activity, not fixed character archetypes. A player interested in competing cruise or freight lines may own or direct an organization, delegate vessel operation to crews, and compete on service, routes, reliability, price, or reputation without personally captaining every ship.

**Delegation changes the player's controls and responsibilities; it does not remove the delegated people, assets, or consequences from the world.** Organizational agency must not become a detached management game that silently substitutes abstract tokens for the living world.

## 3. Shared-world invariants

The following are architecture requirements for any implementation that spans base and wider-galaxy activity:

- **One authoritative identity:** the same person, vessel, organization, station, route, and event must not become duplicate entities merely because they appear through different activities or interfaces.
- **One event history:** military, civilian, commercial, and personal consequences belong to the same world chronology.
- **One physical and social context:** routes, infrastructure, resources, access, reputation, law, risk, and local conditions are shared facts where the domain defines them.
- **No omniscient player shortcut:** characters and organizations know only what their observation, communication, records, and inference justify.
- **Continuity across agency scales:** direct operation, crew leadership, organizational control, and customer participation refer to the same underlying operation.
- **Persistence is selective but honest:** the world need not simulate every person at full fidelity at every moment, but abstraction must preserve the authoritative outcomes and obligations that matter.
- **War is a world force, not a universal occupation:** conflicts may change routes, markets, safety, institutions, migration, and personal lives without forcing every player into combat.

## 4.1 First executable shared-entity contract

The first implementation slice is a stable `WorldEntityId` derived from immutable creation coordinates: world seed, simulation-model version, entity kind, canonical origin address, and a domain-assigned creation ordinal. Current location is deliberately not an identity input. Nor are observer, presentation, occupation, control scheme, or agency scale.

This gives a pilot-facing view and an organization-facing view the same vessel identity by construction. A later domain contract can attach current location, crew, ownership, operational authority, and observation permissions to that identity without creating a second vessel for the second perspective.

The identity primitive does **not** create or persist vessels, prove ownership, grant control authority, implement delegation, or establish a complete entity lifecycle. Those remain separate contracts. Its acceptance tests establish reproducible identity and separation across world seed, entity kind, and creation ordinal; they do not claim that the full two-perspective/two-agency-scale experience is implemented.

### 4.2 Explicit agency and bounded delegation

The next executable slice keeps agency attached to the same `WorldEntityId`:

- A **direct operator assignment** grants only `Operate` for that entity.
- An **organization mandate** grants only the enumerated capabilities for that entity. The current vocabulary separates `ManageOperations` from `DelegateOperation`.
- A **delegation** names the same entity, the mandating organization, one delegate, and one capability. The delegate receives `Operate` only; delegation does not transfer management authority.
- A revoked delegation is no longer effective. A delegation without a matching organization mandate, a different entity, an unknown capability, or a composite capability request is denied.

These records are inputs to a deterministic policy evaluator. The mandate must originate from the authoritative world domain; the evaluator does not authenticate the issuer, prove legal title, persist grants, or provide a multiplayer security boundary. Those concerns require a later trusted authority and durable-history contract. In particular, an organization is not assumed to own or control an asset merely because it appears in an organizational view.

This is deliberately narrower than an economy or corporate simulator. It proves that a pilot's direct operation and an organization's bounded delegation can refer to one vessel identity while retaining separate responsibilities. It does not yet implement crew scheduling, contracts, succession, offline operation, or ownership transfer.
### 4.3 First inhabited-place contract

The raWWar integration branch now contains a deliberately bounded InhabitedPlaceSnapshot contract. A place is identified by the same stable WorldEntityId primitive used for vessels and other world entities. Its immutable snapshot carries references to residents, available services, and background-event occurrences.

Observer projections retain the exact same PlaceId while carrying a distinct observer identity and explicitly selected resident/service/event references. The projection rejects references that are absent from the authoritative snapshot, and the snapshot copies caller-owned collections so later mutation cannot silently rewrite its facts.

This is a referential-integrity and perspective-shape contract—not a resident simulator, service executor, persistence layer, visibility policy, authorization system, or proof that a character actually acquired information. The caller supplies the observed references; an authoritative observation/knowledge contract remains future work. No StoryBuilder authoring records or package reader are duplicated here.

The initial contract checks cover:
- two observers projecting the same place identity with different fact subsets;
- rejection of a service reference that does not exist in the place snapshot;
- defensive copying of caller-owned resident collections.

The next proof should bind an actual background occurrence and a resident activity to the same place through the existing event-history boundary, then demonstrate that a player may ignore the activity without changing its authoritative occurrence. Durable storage, scheduling, and NPC simulation remain separate gates.

## 4. Perspective is not world state

The Experiences Galactic War design makes a critical separation between:

1. **World state:** authoritative physical, social, and institutional facts.
2. **Perception:** what a particular entity can detect from its body, location, instruments, channels, attention, and condition.
3. **Interpretation:** what that observer believes the detected evidence means.
4. **Presentation:** how the Experience communicates a view through sound, image, text, spatial structure, haptics, or interfaces.

The same event can be objectively consequential but perceived differently, interpreted incorrectly, or remembered incompletely. A military sensor view, a civilian news report, a pilot's cockpit, a station resident's conversation, and an executive's route-risk dashboard may all describe the same event from different perspectives.

A view, report, dashboard, character statement, or UI projection is not itself authoritative world state. GUI, Renderer, and Visualization present perspectives; they do not own the domain facts.

## 5. Activity loops over shared entities

Different activities can have distinct loops without creating separate worlds.

| Activity | Example loop | Shared dependencies |
|---|---|---|
| Military service | qualify → train → receive orders → prepare → deploy → resolve → recover | people, command, facilities, equipment, logistics, events |
| Freight operation | accept contract → plan route → crew/load → travel → deliver → settle | ships, crews, cargo, routes, stations, markets, risk |
| Passenger service | schedule → provision → board → travel → serve → arrive | vessels, passengers, crew, ports, safety, reputation |
| Piracy | identify opportunity → prepare → intercept → resolve encounter → escape or face response | ships, law, security, cargo, witnesses, factions |
| Organization management | establish or join → set priorities → allocate → delegate → monitor → adapt | people, assets, contracts, finance, institutions, competitors |
| Exploration/research | observe → form hypothesis → prepare → investigate → validate → publish/share | instruments, knowledge, locations, sources, observers |

These loops are illustrative design candidates, not implemented systems or approved economic rules. The architecture requires shared dependencies and continuity; detailed mechanics remain open until their domain contracts are illuminated.

## 6. Places and vessels are inhabited systems

The Galactic War source design treats stations as places rather than service menus and ships as places as well as vehicles.

A station can have residents, work, services, learning, commerce, local events, public information, routines, and circumstances that continue when the player leaves. Docking is a transition into an inhabited place, scaled to the vessel: fighter clamp, shuttle hangar, freighter cargo collar, or capital-ship cradle.

A ship can progress from viewport-first presence to inspectable systems and walkable interiors. A vessel may continue traveling while occupants move through its spaces and interact with its crew and systems.

These ideas are compatible with raWWar's military base and logistics focus. They broaden the same world rather than defining a separate civilian layer.

## 7. Learning and capability are not a military funnel

The Galactic War design treats learning as a way into the world and qualification as scoped, evidenced, and contextual. Education should open many futures, not funnel every person toward military service or a universal character level.

A person may learn engineering, navigation, medicine, research, diplomacy, logistics, piloting, construction, or military disciplines. Qualifications should establish capability and responsibility for defined work; they should not become arbitrary global power levels.

This supports raWWar's training and career systems while making them part of a wider society.

## 8. Authored possibility, runtime occurrence, and knowledge

The source design establishes several important ownership boundaries:

- StoryBuilder authors story-domain records, sources, claims, chronology, observer-knowledge constraints, scenes, and authored possibilities.
- The Experience/runtime owns campaign-specific occurrence, participating runtime actors, media scheduling and delivery, player presentation, and playthrough receipts, subject to the actual upstream contracts.
- An authored event possibility is not proof that an event occurred in a particular playthrough.
- A report is not the event it describes. A correction creates a new account state; it does not erase the earlier publication.
- A character who hears a bulletin knows that the bulletin was issued; that does not make every claim in it true.
- A character who has not received a report or correction must not be granted that knowledge.

The first playable proof, “The Signal Nobody Owns,” is deliberately modest: a visitor in an inhabited station may watch a public report, hear two people interpret it differently, inspect competing accounts, notice a correction, investigate, or ignore the story and continue ordinary activity. It demonstrates a living world without forcing the player to become the cause of every event.

Do not create a parallel narrative database or observer-knowledge authoring system inside raWWar. If an upstream contract cannot express a required distinction, document the exact gap and coordinate with its owner.

## 9. Source crosswalk and confidence

The following classification distinguishes design intent from implementation. “Candidate” means the source explicitly treats the idea as exploratory or not approved canon. It does not mean the idea is implemented.

| Source concept | Classification | raWWar architectural treatment |
|---|---|---|
| One connected living galaxy; people, learning, social life, missions, ships, factions, consequences | Shared foundation | Adopt as the shared-Experience frame; keep specific lore candidate until approved |
| First-person embodied presence | Shared foundation | Preserve; organizational agency does not remove embodiment |
| Person → exoskeleton → craft → ship → capital ship → fleet → alliance → galaxy | Shared scale model | Reuse as a scale relationship, not a mandatory progression ladder |
| Military service and combat | Activity-specific expression | Preserve raWWar's military campaign and base life as a major path, not the only identity |
| Stations as inhabited ports/cities/markets/schools/research/political places | Shared foundation / candidate | Use as station design direction; simulation scope and canon remain open |
| Lived-in ships and vessel-scaled docking | Activity-specific expression / candidate | Shared vessel and place model; staged implementation required |
| Learning, schools, mentors, apprenticeships, qualifications | Shared foundation / candidate | Support multiple pathways; qualification semantics need domain contracts |
| First-contact detection, attribution, interpretation, diplomacy | Activity-specific expression / candidate | Shared world-event and observer model; Galactic War-specific story content remains source-owned |
| Different civilization perception profiles | Shared perspective architecture / candidate | Keep world state separate from perception, interpretation, and presentation |
| Finite trophy instances and provenance | Cross-cutting domain candidate | Do not conflate ownership, custody, location, and condition; require authoritative event history |
| Station news and observer knowledge | Shared information architecture | Preserve source, claim, event, acquisition, and presentation distinctions |
| StoryBuilder authoring contracts | Upstream capability mapping | Reuse only confirmed contracts; never duplicate them in raWWar |
| StationMediaRuntime prototype in PR #56 | Experimental implementation | Treat as a narrow in-memory prototype with documented persistence/provenance gaps, not a complete station or durable campaign service |

## 10. Source inventory and exact revisions

### Experiences PR #51 — merged vision baseline

- PR: [#51 — Capture Galactic War Experience vision](https://github.com/TrentBest/TheSingularityWorkshop.Experiences/pull/51)
- Merged commit: `f173d47e071cc2dd430cb86925eb58d36f4048cf`
- PR head: `e06cf5e769ef86146a693dc31086a85e6f56baaf`
- Changed source paths:
  - `experiences/GalacticWar/README.md`
  - `experiences/GalacticWar/design/EXPERIENCE_VISION.md`
  - `experiences/GalacticWar/design/EMBODIED_COMBAT_AND_SPACEFLIGHT.md`
  - `experiences/GalacticWar/design/FIRST_STATION_CANDIDATE.md`
  - `experiences/GalacticWar/design/LEARNING_AND_SOCIAL_GALAXY.md`
  - `experiences/GalacticWar/design/LEARNING_PROGRESSION_AND_QUALIFICATION.md`
  - `experiences/GalacticWar/design/LIVED_IN_SHIPS_AND_VIEWPORTS.md`
  - `experiences/GalacticWar/design/MISSION_LOOP.md`
  - `experiences/GalacticWar/design/STATIONS_AND_DOCKING.md`
  - `experiences/GalacticWar/design/STATION_LIFE_AND_BACKGROUND_ACTIVITY.md`
  - `experiences/GalacticWar/design/STYLE_IDENTITY_AND_PROVENANCE.md`
  - root `README.md`

### Experiences PR #56 — expanded worldbuilding and first-contact design

- PR: [#56 — Expand Galactic War worldbuilding, first contact, and living campaign design](https://github.com/TrentBest/TheSingularityWorkshop.Experiences/pull/56)
- Status at review: open draft, not merged.
- Base: `development` at `51f4a9006930975967312e3a37baf63b1d88af7e`.
- Reviewed head: `9a02899c5ccc853ccccefa396b79fc86e5560942`.
- Relevant design and proof paths:
  - `experiences/GalacticWar/README.md`
  - `experiences/GalacticWar/design/CIVILIZATION_INTERFACE_AND_PERCEPTION.md`
  - `experiences/GalacticWar/design/FINITE_PROVENANCE_AND_TROPHY_OWNERSHIP.md`
  - `experiences/GalacticWar/design/FIRST_CONTACT_AND_DIPLOMACY.md`
  - `experiences/GalacticWar/design/FIRST_PLAYABLE_PROOF_THE_SIGNAL_NOBODY_OWNS.md`
  - `experiences/GalacticWar/design/FSM_API_TROPHY_LIFECYCLE_MAPPING.md`
  - `experiences/GalacticWar/design/GALACTIC_WAR_EXECUTABLE_SLICE.md`
  - `experiences/GalacticWar/design/GAME_DESIGN_FOUNDATION_AND_LIVING_EVENTS.md`
  - `experiences/GalacticWar/design/OBSERVER_KNOWLEDGE_TWO_NPC_PROOF.md`
  - `experiences/GalacticWar/design/STORYBUILDER_CAPABILITY_CROSSWALK.md`
  - `experiences/GalacticWar/design/STORYBUILDER_CONTRACT_RECHECK_2026-11-12.md`
  - plus workflow, sample, and runtime/test paths listed in the source PR.
- The source PR explicitly labels much of this work candidate design, implementation mapping, or proof specification. The presence of sample/runtime code does not establish durable campaign, persistence, multiplayer, or complete station behavior.

### raWWar design baseline

- raWWar PR #10 head: `4e973b8b5e71890f0557291e485ff0e5682b2301`.
- Aligned documents: `docs/GAME_DESIGN_DOCUMENT.md`, `docs/GAME_DESIGN_BIBLE.md`, `docs/VISION_AND_PILLARS.md`, `docs/FLAGSHIP_EXPERIENCE_STORY.md`, `docs/PLAYER_ROLES.md`, and README.
- Related tasks: [issue #7 — formation lifecycle](https://github.com/TrentBest/raWWar/issues/7), [issue #8 — shared-Experience source intake](https://github.com/TrentBest/raWWar/issues/8), [issue #9 — Autonomous Development Pipeline](https://github.com/TrentBest/raWWar/issues/9).

## 11. Decisions still requiring illumination

These are not silently decided by this architecture:

- Whether “base-centered” and “wider-galaxy” should be named starting circumstances, perspectives, careers, or activity bundles in player-facing UX.
- How a player changes occupation, joins/leaves organizations, inherits or transfers ownership, and retains personal identity/history.
- The authority model for organizations: delegation, succession, accountability, hiring, contracts, and limits on owner control.
- Which commercial mechanics exist: prices, wages, costs, scarcity, trade, credit, profit, insurance, and competition.
- How offline or absent organization members and delegated crews continue operating, and what level of simulation is required.
- How piracy, law, security, military intervention, insurance, and jurisdiction interact.
- Which world facts persist at full fidelity versus bounded simulation or summarized history.
- How a player's embodied presence is maintained when exercising organization-scale agency.
- How multiple players may share ownership, command, crew positions, and authority without contradictory state.
- What is canon, candidate setting, authored possibility, generated occurrence, or player-created history.
- How story publication, observer acquisition, delivery, and presentation are represented across upstream StoryBuilder and Experience runtime boundaries.
- What durable persistence and concurrency contracts are required for organizations, assets, ownership, and campaign events.

## 12. Proposed bounded implementation sequence

1. **Shared identity and event vocabulary:** define or map stable person, organization, place, vessel, activity, and event references. Tests must prove that different views resolve to the same entities.
2. **One inhabited place:** extend the existing station/base proof with a small set of residents, jobs, services, and a background event. Prove the place remains coherent when the player ignores its lead.
3. **One vessel as shared entity:** represent one vessel, crew, route, cargo/passenger responsibility, and location. Provide cockpit/crew and external/organizational projections over the same state.
4. **Direct operation and delegation:** prove one activity can be performed directly or delegated to an identified crew with observable responsibilities and outcomes.
5. **Organization-level agency:** add the smallest organization contract—membership, authority, one owned/operated asset, one obligation, and one result. Avoid a full economy until ownership and concurrency rules are specified.
6. **Cross-activity consequence:** introduce one bounded world event (for example, a conflict-related route disruption) and prove it affects military, freight, and passenger perspectives consistently.
7. **Persistence and multiplayer gates:** only after single-authority contracts pass, specify durable event history, concurrency, recovery, shared control, and adversarial tests.

Each slice must have deterministic domain tests, exact source/dependency revisions, and a clear statement of what is simulated versus physically or socially verified. A sample transcript or visualization is presentation evidence, not proof of durable world behavior.

## 13. Acceptance boundary

This document is an architecture proposal grounded in the cited source revisions. It does not claim that the shared universe, civilian occupations, organization management, economy, persistence, multiplayer, station simulation, or lived-in ship runtime is implemented.

A slice is accepted only when its authoritative domain contract, failure behavior, deterministic tests, and exact-SHA evidence exist. Presentation must be traceable to the same state and must not become a second authority.

The next design pass should resolve the agency/organization questions before implementing a management UI or a broad economy. The next engineering proof should be one shared entity with two perspectives and two valid agency scales—not a new parallel game mode.
