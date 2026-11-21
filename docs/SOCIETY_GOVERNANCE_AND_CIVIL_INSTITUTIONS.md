# Society, Governance, and Civil Institutions

**Status:** creator-directed design expansion; proposed domain architecture, not implemented runtime behavior or finalized canon.  
**Scope:** civic government, political participation, law, justice, media, scientific and cultural institutions, wildlife/specimen stewardship, and player-created conflict.  
**Parent architecture:** [RaWWar Shared-Experience Architecture](SHARED_EXPERIENCE_ARCHITECTURE.md).  
**First executable-design candidate:** [Cross-Domain Contracts and Event Lifecycle](CROSS_DOMAIN_CONTRACT_AND_EVENT_LIFECYCLE.md).

## 1. Foundational direction: a society, not a war lobby

raWWar's universe is intended to support many meaningful ways to live. Military service and warfare are available, consequential possibilities, but participation in war is not compulsory and war is not a required world-state event.

The universe supplies people, places, institutions, laws, resources, infrastructure, organizations, communication, and tools. Players may use those systems to cooperate, compete, govern, investigate, create, trade, explore, commit crimes, enforce laws, or wage war. Player actions and the autonomous inhabitants' responses produce the history of the universe.

**No universal campaign director should force a war simply because the product contains military systems.** A conflict may arise from player decisions, organization decisions, political action, resource competition, crime, security responses, or other authored and simulated causes. Whether non-player institutions may independently initiate major wars, and under what safeguards, remains an explicit design question.

This is a proposal for a broad social simulation, not a promise that every institution or occupation will be available at launch.

## 2. One society, many civic and professional lives

Candidate player perspectives include, but are not limited to:

- **Civic leadership:** resident, community organizer, city councillor, mayor, regional representative, senator, planetary executive, or another office defined by a society.
- **Public administration:** civil servant, planner, regulator, infrastructure operator, emergency coordinator, public-health or education administrator.
- **Law and justice:** lawyer, advocate, prosecutor, investigator, judge, court clerk, mediator, corrections or rehabilitation worker, and legal researcher.
- **Public safety and enforcement:** patrol officer, investigator, customs officer, station security, planetary enforcement, interstellar warrant agent, or independent bounty hunter operating under a recognized contract.
- **News and public information:** reporter, editor, correspondent, commentator, camera operator, presenter, producer, publisher, or independent broadcaster.
- **Science, education, and stewardship:** zoologist, xenobiologist, veterinarian, ecologist, curator, educator, conservationist, archaeologist, or field researcher.
- **Enterprise and industry:** founder, employee, contractor, ship operator, logistics provider, manufacturer, service provider, investor, or customer.
- **Military and conflict:** enlisted soldier, officer, commander, fleet operator, military supplier, strategist, or civilian affected by conflict.

These are examples, not a fixed class list. A character may hold multiple roles over time, subject to the world's qualification, eligibility, law, institutional and practical requirements. A player may also choose to remain a private citizen and never pursue office, crime, enforcement, or military service.

The same underlying person, organization, asset, place, event, and history must be visible through the appropriate perspectives. A mayor's city, a reporter's city, a resident's city, and an investigator's city are the same city.

## 3. Government and political agency

### 3.1 Government is a world institution

A government is not merely a menu or a player-owned management screen. It is an organization embedded in a population and territory, with defined offices, authorities, procedures, public obligations, resources, records, and limits.

Potential scales include:
- neighborhood, district, town, and city;
- province, state, territory, or regional authority;
- planetary and system-level government;
- interplanetary, interstellar, or other federated institutions;
- factional and supranational bodies where the setting supports them.

These are candidate scales. Not every society must use the same constitutional model, and the existence of an office at one scale does not imply a universal hierarchy above it.

### 3.2 Elections and public office

A player could campaign for and become mayor of a city, seek a seat in a senate, or run for the highest elected office on a planet. Political play should be a complete perspective rather than a temporary combat-adjacent quest line.

The architecture should eventually support the full institutional lifecycle:
1. A jurisdiction and office exist with defined eligibility and authority.
2. Candidates become eligible under that society's rules.
3. Candidates communicate positions, build coalitions, campaign, debate, and seek public support.
4. Eligible residents participate in a defined election or selection process.
5. Results are recorded as authoritative institutional events.
6. The successful candidate assumes a bounded office with explicit powers, term or tenure rules, duties, and constraints.
7. Decisions are enacted through the government and its agencies, not by silently rewriting the world.
8. Performance, controversy, legal review, public response, and future succession remain part of the shared history.

The sequence is a proposed capability map, not an approved election mechanic. Voting eligibility, campaign finance, election integrity, terms, recall, succession, constitutional limits, and NPC voting behavior require deliberate design. We must not assume that every world uses one-person-one-vote democracy, that every office is elected, or that a player can acquire unlimited authority by winning an election.

### 3.3 Office is authority with boundaries

An office should grant enumerated powers within a jurisdiction and period, not omnipotence. A mayor may direct city services within a charter but should not automatically command planetary fleets, rewrite private ownership, or overrule a court outside lawful authority.

Government actions should be represented as attributable decisions and events:
- who or what institution acted;
- under which office, mandate, law, or procedure;
- within which jurisdiction;
- against which entities or resources;
- at what effective time;
- with what stated basis and observable outcome;
- whether the decision is contested, appealed, suspended, reversed, or expired.

The model must distinguish legal authority, practical influence, public legitimacy, and coercive capability. They can diverge. A government may have lawful authority but lack capacity; an occupying power may have capacity but contested legitimacy; a popular leader may lack legal authority to perform a particular act.

## 4. Law, jurisdiction, and public safety

Law is contextual. The same conduct may be permitted, regulated, prohibited, or unresolved in different jurisdictions or at different times. A station, city, planet, faction, and interstellar authority may have overlapping or conflicting rules.

The legal domain should model:
- jurisdiction and its geographic, organizational, and temporal scope;
- the authority that enacted or recognizes a rule;
- the rule's effective interval, subjects, conditions, and exceptions;
- alleged conduct and its legal classification;
- evidence, witnesses, provenance, uncertainty, and chain of custody;
- investigation, warrants or equivalent authorizations, hearings, judgments, and appeals;
- enforcement powers and their limits;
- consequences such as restitution, fines, restrictions, custody, rehabilitation, asset seizure, or other society-specific outcomes.

These are domain concepts, not a commitment to reproduce any one real-world legal system. Each society may have different institutions and due process. The game must nevertheless make the applicable rules and consequences legible enough for meaningful agency.

Crime is not a special disconnected mode. It is conduct that an applicable authority treats as violating a rule. Piracy, smuggling, theft, fraud, trafficking, sabotage, and other examples may be represented where appropriate, but their legal status depends on the applicable rule set and facts. Military action, blockade, occupation, private security, bounty hunting, and resistance may intersect with law without being automatically classified as ordinary crime.

Enforcement is not guaranteed success. Investigators can lack evidence, witnesses can be mistaken, suspects can evade capture, institutions can be corrupt or constrained, and judgments can be appealed. Equally, the system must not presume every accused person guilty or every authority legitimate.

## 5. Trials as playable civic events

A trial can be a real event in the shared world, not a scripted cutscene. Depending on the society and case, players may participate as counsel, prosecutor, judge, witness, investigator, clerk, accused person, or public observer.

A trial-capable domain will need:
- a case identity linked to the alleged events and applicable jurisdiction;
- parties and their roles;
- admissible evidence and its provenance;
- disclosure and procedural milestones;
- testimony, examination, argument, and response;
- a decision by the authorized decision-maker or institution;
- a recorded outcome, rationale, and available appeal or review;
- public/private information boundaries appropriate to the society and case.

A simulated trial should be governed by explicit procedures and evidence rules. The system must not let presentation, audience popularity, or a news headline become the authoritative verdict. The exact procedural model, degree of player improvisation, and how NPC participants behave remain open design work.

## 6. News, broadcasting, and public knowledge

Players may work as reporters, correspondents, editors, presenters, producers, camera operators, or independent publishers. Trials, elections, public debates, discoveries, emergencies, commercial events, and conflicts may become broadcast material.

News is a perspective and an information product—not world truth. Preserve the existing architecture's separation between:
1. the event that occurred;
2. evidence and sources available to an observer;
3. the account a journalist or institution constructs;
4. editorial decisions and publication;
5. delivery to an audience;
6. what each recipient actually receives, notices, believes, or remembers;
7. later corrections, retractions, competing accounts, and investigations.

A broadcast can be inaccurate, incomplete, delayed, biased, or deliberately deceptive. Its existence proves that a publication was made, not that every statement in it is true. A correction should add to the public record rather than erase the original publication. Different audiences may receive different coverage based on access, channels, attention, and local circumstances.

This should reuse upstream StoryBuilder/Experience contracts where applicable; raWWar must not create a competing narrative-authoring or observer-knowledge engine.

## 7. Zoological networks, specimen acquisition, and intergalactic research

A player may establish or operate a network of zoos, sanctuaries, research facilities, or other institutions devoted to life forms from across the galaxy. This can become a major enterprise with public, scientific, educational, logistical, and conservation dimensions.

A network might:
- discover or commission the search for a species;
- publish a procurement or research request;
- contract qualified field teams, bounty hunters, capture specialists, transporters, veterinarians, and researchers;
- receive and verify a specimen or artifact;
- arrange safe custody, quarantine, care, transport, study, breeding where appropriate, exhibition, or release;
- coordinate facilities across planets and stations;
- share research and educational material;
- establish provenance and custody records;
- compete or collaborate with other institutions.

### 7.1 Acquisition is a chain of accountable work

The zoo owner does not simply click a species icon and cause it to appear. The request creates an opportunity in the shared world. Other players—or autonomous agents when no suitable player accepts the work—may undertake the expedition and capture.

A specimen's lifecycle should distinguish:
- species identity and the uncertainty of identification;
- individual specimen identity where individuals are distinguishable;
- wild location and ecological context;
- discovery and observation records;
- capture authorization and method;
- physical condition, injuries, stress, and care requirements;
- custody and each transfer;
- transport conditions and route;
- quarantine and veterinary assessment;
- current facility, habitat, and responsible caretaker;
- research, exhibition, breeding, rehabilitation, or release history.

An artifact has a different domain lifecycle from a living specimen. Do not collapse biological custody, legal title, physical possession, location, and scientific provenance into one generic ownership flag.

### 7.2 Living subjects are not ordinary cargo

Safe capture and transport are essential design constraints for this domain. A capture contract should communicate the required expertise, equipment, permits, target condition, and acceptable handling constraints. A team that delivers a dead or severely harmed specimen when the contract required safe live capture has not fulfilled the objective.

The universe should permit ethical disagreement and competing institutional approaches, but it should not silently treat sentient or living beings as interchangeable freight. Which life forms are sentient, what rights or protections apply, and how conservation law works are major setting and governance questions—not assumptions to bury in code.

### 7.3 Player work and autonomous fulfillment

The preferred opportunity order is to expose worthwhile work to players and allow them to accept, compete, specialize, or collaborate. Autonomous inhabitants can fill a vacancy when no player is available or willing, subject to explicit world rules.

AI fulfillment must not fabricate a player, impersonate a player, claim a player-earned reward, or erase the fact that an autonomous agent performed the work. Contracts, provenance, payment, quality, risk, and outcomes must remain attributable.

The exact waiting period, priority policy, compensation, and NPC capability model are unresolved.

## 8. Organizations, contracts, and opportunity

Political parties, courts, news organizations, zoos, research institutes, capture companies, security firms, unions, schools, manufacturers, shipping lines, and criminal organizations can all be modeled as organizations with different mandates.

Shared organizational primitives may eventually include:
- membership, roles, qualifications, and employment;
- authority, mandates, delegation, and succession;
- assets, facilities, and custody;
- contracts, obligations, milestones, and settlement;
- reputation, public standing, and recorded history;
- competition, cooperation, disputes, and alliances.

The shared organization model must not imply that every organization is a corporation or that every relationship is commercial. Public offices, voluntary associations, households, research collectives, and informal groups have different purposes and authority structures.

An opportunity board or contract system can connect players who need work with players who want to perform it. It should represent real obligations and outcomes, not merely issue isolated mission prompts.

## 9. Conflict is possible; war is not compulsory

The universe provides the tools to wage war: people, organizations, command structures, ships, weapons, logistics, industry, intelligence, law, territory, and resources. It is the inhabitants—players and autonomous actors under the world's rules—who make decisions that can lead to conflict.

A peaceful player should be able to build a life and enterprise without being conscripted into a mandatory war storyline. A political player might prevent a conflict through diplomacy, public policy, or negotiated settlement. A journalist might expose preparations. A freight operator might reroute around instability. A zoo might suspend expeditions in a dangerous region. A soldier might choose to serve. A player organization might deliberately initiate hostilities.

War can still have enormous systemic consequences once caused: trade disruption, casualties, displacement, shortages, legal disputes, propaganda, political change, reconstruction, and long-lived historical memory. The world must not reset those consequences merely because an individual player changes activity or leaves the area.

Important unresolved distinction: whether autonomous factions can independently initiate wars, and how to prevent background simulation from manufacturing compulsory conflict, needs creator direction. The current design intent is that wars are not forced on players and should emerge from inhabitants' agency.

## 10. Cross-domain example: one event, many lives

A capture contract is issued by a planetary zoological institution for a rare life form reported on a remote moon.

- A player expedition team investigates the report and finds the species.
- A qualified capture crew accepts the contract and requests the necessary local authorization.
- A local government disputes the institution's right to remove specimens from a protected habitat.
- A lawyer represents the institution; another represents the local conservation authority.
- A judge issues a temporary order pending a hearing.
- A reporter covers the dispute, interviews residents, and publishes an account.
- The zoo's director must decide whether to pause the contract, challenge the order, or redirect the research.
- A rival institution may offer a non-invasive study proposal.
- If the matter is resolved, the capture team may proceed under the resulting terms—or the expedition may be cancelled.
- Players can attend the hearing, work on the expedition, report the story, or ignore the whole affair and continue their own lives.

The same moon, species, contract, institutions, people, legal record, and news history persist across every perspective. This is the intended character of the universe.

## 11. Domain boundaries and authority

This proposal identifies domain ownership candidates; it does not prescribe one giant monolithic system.

| Domain | Owns | Must not silently own |
|---|---|---|
| Government | offices, mandates, jurisdictions, public decisions, institutional succession | all private assets or every other institution's authority |
| Law and justice | rules, cases, procedure, evidence evaluation, judgments, appeals | objective world events or news presentation |
| Enforcement | authorized investigations, custody, patrols, warrants/contracts, enforcement outcomes | guilt by accusation or unlimited coercive authority |
| Media | reporting, editorial accounts, publications, delivery and corrections | the truth of the event being reported |
| Zoology and stewardship | species knowledge, care standards, specimen records, facility operations, research | legal permission, physical custody, or provenance without explicit linked records |
| Contracts and organizations | obligations, roles, delegation, fulfillment, settlement | automatic control over entities outside the mandate |
| Shared world/event history | attributable occurrences and durable consequences | observer interpretation or presentation |

Every cross-domain action should reference the relevant authority and source records. For example, a capture operation may require a zoo contract, a jurisdictional permit, a qualified crew, and a custody record. No single record should pretend to prove all four.

## 12. Deterministic contracts and acceptance direction

When these systems are implemented, initial tests should be small, deterministic, and domain-specific. Candidate invariants include:

- A government decision is scoped to its issuing authority, mandate, jurisdiction, and effective time.
- Winning an office does not grant capabilities absent from that office's explicit mandate.
- A law's applicability is evaluated against the relevant jurisdiction and time; the same act can receive different classifications in different jurisdictions.
- An accusation is not a verdict; a publication is not proof; a verdict references the case and evidence considered.
- A correction appends a new publication state and does not erase the original.
- A specimen's identity, custody, location, condition, title, and provenance are distinct facts.
- A contract cannot be fulfilled by an actor lacking the required qualifications or authorization.
- An autonomous fulfillment is attributed to an autonomous actor, never represented as player performance.
- A player may decline all military activity without the simulation forcing their character into a war role.
- Player-caused conflict is represented by attributable initiating decisions and subsequent world events; no unexplained war flag is accepted as a cause.
- Different observer projections refer to the same authoritative event and entities while preserving their distinct information boundaries.

These are proposed test directions. They are not claims that the runtime currently implements these capabilities.

## 13. Questions requiring creator decisions

Keep these visible in the living elicitation process; do not fill them with generic defaults.

1. Which government forms and constitutional structures exist in the setting, and can different planets choose different forms?
2. Are offices elected, appointed, inherited, selected by councils, or some combination?
3. What are the eligibility, voting, campaigning, term, succession, recall, and removal rules?
4. Can a player hold public office while owning private enterprises or serving in other organizations? What conflict-of-interest rules apply?
5. What limits prevent an elected leader from unilaterally controlling every institution?
6. Which legal rights and procedural protections are universal, and which vary by society?
7. How are jurisdictions recognized when governments dispute borders, sovereignty, occupation, or legitimacy?
8. Which roles can players fill in trials, and how are impartiality, evidence standards, appeals, and unavailable participants handled?
9. What information is public, sealed, private, or restricted during investigations and trials?
10. Can news organizations be publicly owned, privately owned, independent, partisan, or all of these?
11. How are audience reach, broadcast infrastructure, editorial accountability, and corrections represented?
12. Which life forms are sentient, protected, dangerous, invasive, endangered, or legally ownable?
13. What constitutes ethical capture, custody, transport, exhibition, research, breeding, rehabilitation, and release?
14. How are species discovered and validated without turning every rare life form into a simple collectible?
15. When should an NPC fulfill a player-posted contract, and how should its work, risk, payment, and provenance compare with player work?
16. Can autonomous factions initiate major wars, or must major wars always trace to explicit player or institution decisions?
17. What simulation continues when no player is present, and what must be preserved when detail is abstracted?

## 14. Implementation sequence proposal

Do not attempt to implement the whole society at once. Suggested design and proof order:

1. **Institution and jurisdiction vocabulary:** define organization, office, mandate, jurisdiction, rule, and attributable decision without choosing every political system.
2. **One small local government:** demonstrate a bounded authority and a recorded decision over one public service.
3. **One election lifecycle:** eligibility, candidates, public communication, vote, result, office assumption, and bounded authority.
4. **One legal case:** allegation, evidence provenance, hearing, decision, and appeal boundary.
5. **One public report:** event, sources, account, publication, delivery, and correction with observer-specific knowledge.
6. **One specimen contract:** request, permit, qualified capture, custody transfer, safe transport, and facility receipt.
7. **One cross-domain event:** let government, law, media, commerce, and a zoological institution interact around the same event history.
8. **Player/NPC opportunity parity:** prove that unfilled work can be performed by autonomous actors without false player attribution.
9. **Persistent institutional consequences:** only after authority, event-history, concurrency, and recovery contracts are proven.

Each slice requires deterministic tests, explicit exclusions, exact-SHA CI evidence, and a clear distinction between implemented, tested, simulated, and proposed behavior.

## 15. Acceptance boundary

This document records a creator-directed expansion of the shared-Experience design. It does not establish finalized political canon, legal codes, a complete economy, a complete zoological simulator, a trial engine, a news runtime, or autonomous war behavior.

The success criterion is not the number of occupations on a menu. It is whether different people can pursue different purposes through shared institutions and shared facts, with their decisions producing coherent, attributable, persistent consequences—without requiring every player to participate in war.
