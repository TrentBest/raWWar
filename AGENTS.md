# raWWar Agent Onboarding

> Read this file at the beginning of every new agent session before changing the repository. It is the durable handoff for domain ownership, current architecture, editorial rules, and operating boundaries.

## Mission

Build raWWar into a deep, accurate, cross-linked living library and simulation domain for warfare and organized force. The library serves tabletop roleplaying, campaign creation, worldbuilding, historical learning, and simulation design. It must be much more than a list of units, weapons, and battles: cover the people, institutions, economies, environments, technologies, strategies, logistics, lived experiences, political causes, consequences, and memories that surround organized force.

The creator has explicitly authorized substantial expansion of the book library. Continue producing many useful, substantive books without waiting for approval on each book. Keep the creator informed through concise progress updates and repository issues when coordination is required.

## Current branch and safety boundaries

- Work on `development`.
- Long-term repository branch policy: exactly `master` and `development`.
- The branch `integration/autonomous-development-pipeline` was observed alongside those two branches on 2026-11-23. It is a reconciliation candidate, **not approved for deletion**. Inspect ancestry and unique commits, report it as ready to delete only when proven safe, and wait for explicit approval before deleting.
- Do not merge to `master`, publish packages, create releases, deploy, or dispatch privileged workflows without explicit authorization.
- The creator has authorized broad sourcebook creation; this does not authorize destructive Git operations or protected release actions.
- Keep changes focused, validate them, and commit coherent increments to `development`.
- Never overwrite concurrent development work: refresh the branch head and file SHA immediately before each write.
- If the linked GitHub credential is unavailable or expires, stop writes and ask the creator to reconnect it. Never ask the creator to paste a token into chat.

## Domain definition

Warfare is broader than declared wars and national armies, but not every act of violence is warfare. Study organized force, armed contest, coercion, protection, defense, raiding, piracy, privateering, corsairing, outlawry, brigandage, resistance, insurgency, rebellion, occupation, mercenary activity, militias, peacekeeping, military institutions, and adjacent forms of organized coercive power. Classify carefully by organization, authority, legal status, purpose, methods, relationship to surrounding communities, and historical perspective.

Piracy belongs in the library. Robin Hood-like outlawry belongs in the library. Do not automatically call every pirate a navy, every outlaw a freedom fighter, every rebellion a war, or every form of violence warfare. Explain contested classifications.

## Required historical breadth

Build distinct, cross-linked books for (among many others):
- Greek worlds: archaic and classical poleis, Persian Wars, Peloponnesian War, Macedon and Alexander, Hellenistic kingdoms, successor conflicts, Greek mythic and legendary traditions, and later receptions.
- Persian worlds: Achaemenid Empire, Parthian Empire, Sasanian Empire, their predecessor/successor contexts, military institutions, logistics, diplomacy, and conflicts with Greek, Roman, steppe, and neighboring powers.
- Roman worlds: Kingdom/Republic/Empire and later eastern Roman/Byzantine continuities treated with clear periodization; legions and auxiliaries; civil wars; frontier systems; naval forces; logistics; engineering; campaigns and battles.
- Other major civilizations, polities, periods, theaters, coalitions, and communities across the world; avoid a Eurocentric or state-only catalogue.
- Maritime raiding and piracy; privateering and corsairing; outlaws, bandits, brigands, Robin Hood traditions, popular resistance, insurgencies, and local defense.
- Major strategic and military texts. **Sun Tzu's The Art of War is a priority standalone sourcebook**, with textual history, attribution questions, chapter-by-chapter analysis, strategic themes, reception, and careful modern application.

Greek myth and other traditional imaginative material can be developed creatively because ancient mythic motifs and ideas are not themselves modern copyrighted works. However, do not copy protected modern translations, novels, films, games, artwork, or distinctive modern adaptations. Clearly distinguish ancient source material, later folklore, historical evidence, and original Workshop invention. Prefer original prose and commissioned/generated original covers.

## SURPS integration

SURPS is optional. A host/Experience with SURPS disabled must remain valid and must not acquire an accidental SURPS dependency. A simple opt-in belongs at the host/Experience configuration boundary. Detailed book selection and precedence are separate editor concerns.

raWWar owns warfare-domain meaning, historical content, operational context, and simulation requirements. SURPS owns shared rulebook identity, LOD conventions, catalogue/composition contracts, and the opt-in contract. Do not invent or freeze shared APIs in raWWar. Coordinate with SURPS issues when shared conventions need decisions.

Existing starting points:
- [SURPS architecture](https://github.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/blob/development/docs/architecture.md)
- [SURPS rulebook identity and covers](https://github.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/blob/development/docs/rulebook-identity-and-covers.md)
- [SURPS book descriptor example](https://github.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/blob/development/docs/books/dungeons-and-underground.book.json)

Current descriptor pattern:
```json
{
  "schemaVersion": 1,
  "id": "surps.domain.example",
  "category": "Domain",
  "lod": 2,
  "title": "Example",
  "version": "0.1.0",
  "cover": "docs/books/covers/example.svg",
  "lodBadge": "https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg",
  "publisher": "The Singularity Workshop",
  "publisherMoniker": "https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg"
}
```
Use the badge matching the declared LOD. Confirm the current SURPS standard before assuming this pattern is still canonical.

## LOD guidance

LOD means depth and specificity, not chronology, quality, moral ranking, or a mandatory parent-child chain. A book may cross-reference several other books. The current raWWar editorial proposal is:
- LOD 0: core concepts and shared vocabulary.
- LOD 1: broad domain orientation.
- LOD 2: major families, eras, institutions, reusable manuals.
- LOD 3: focused histories, campaigns, organizations, theaters, named operations.
- LOD 4: bounded missions, encounters, and scenario instances.
- LOD 5+: specialist fidelity, archival, technical, experiential, or simulation-specific detail.

This is an editorial proposal until SURPS establishes the shared definition.

## Book quality bar

Every substantive book should:
1. Have a matching `.book.json` descriptor and unique original cover.
2. Declare scope, category, LOD, owner, status, version, geographic and temporal coverage where relevant.
3. Explain context and internal diversity rather than flattening centuries or peoples into stereotypes.
4. Separate evidence, scholarly interpretation, disputed claims, legend, fiction, and speculation.
5. Identify primary and secondary sources or a research bibliography; state source limitations.
6. Include worked examples, maps/diagrams where useful, and cross-links to adjacent books.
7. Support tabletop use and simulation interpretation where appropriate without pretending prose is executable code.
8. State what is outside scope and link to authoritative providers.
9. Keep titles, index, collection README, and `docs/README.md` navigation current.
10. Validate JSON, links/paths, SVG XML, and relevant repository CI.

Do not create empty placeholder books merely to inflate counts. Build a useful LOD 2 foundation, then progressively deepen it with focused LOD 3/4 books.

## Evidence and intellectual property

- Distinguish state, regime, government, military, faction, population, and individual.
- Avoid assigning timeless traits, collective guilt, or uniform motives to ethnicities or nationalities.
- Explain whose perspective a source represents and what it can establish.
- For contested history, preserve disagreement and cite the claims.
- Ancient texts may be public domain, but modern translations, annotations, introductions, and editions may be copyrighted. Quote only from verified public-domain translations or within applicable limits; otherwise summarize in original language.
- Mythic source traditions may be reimagined, but do not copy protected modern adaptations.
- Do not present speculative future conflicts such as WWIII as historical fact.

## Provider ownership

- **raWWar:** warfare domain, campaigns, command/doctrine as domain content, organizations, missions, military logistics and simulation requirements.
- **SURPS:** shared rulebook identity, LOD and composition contracts, opt-in model.
- **Physics:** authoritative physical laws, materials, forces, motion, and physical consequences.
- **Gestures:** embodied human movement, posture, and procedures.
- **FSM_Dungeon:** underground spatial topology and dungeon/fortress spaces where that repository owns them.
- **SingularityCity:** cities, civilian population, civic life, markets, and urban domain content.
- Other Workshop repositories own their own authoritative implementations. Link and coordinate; do not duplicate.

## Current library state (2026-11-23)

- [Warfare and Organized Force, LOD 1](docs/books/warfare-and-organized-force.md) — foundational domain map.
- [Fortifications and Strongholds, LOD 2](docs/books/fortifications-and-strongholds.md) — defensive places, threat models, siege endurance, engineering, sustainment, civilian life.
- [The Art of War, LOD 2](docs/books/the-art-of-war.md) — thirteen-chapter Sun Tzu tradition, textual history, strategic themes, translation discipline, and play/simulation applications.
- [Greek Warfare, LOD 2](docs/books/greek-warfare.md) — Mycenaean worlds, poleis, Persian Wars, Peloponnesian War, Macedon, Alexander, Hellenistic kingdoms, and myth/history boundaries.
- [Persian Warfare, LOD 2](docs/books/persian-warfare.md) — distinct Achaemenid, Parthian, and Sasanian imperial traditions.
- [Roman Warfare, LOD 2](docs/books/roman-warfare.md) — periodized Roman military history from early Rome through eastern Roman continuities.
- Original cover: [the-art-of-war.svg](docs/books/covers/the-art-of-war.svg).
- [Sourcebook catalogue](docs/books/index.md).
- [Editorial collection and roadmap](docs/books/README.md).
- [SURPS participation issue](https://github.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/issues/4).
- [raWWar sourcebook programme issue](https://github.com/TrentBest/raWWar/issues/12).

Build next: dedicated Achaemenid, Parthian, and Sasanian books; Greek/Persian conflict and campaign studies; Roman Republic/Imperial/Late Roman books; Piracy and Maritime Raiding; Outlaws, Bandits and Popular Resistance. Then deepen into campaigns, battles, leaders, formations, equipment, doctrine, logistics, civilian experience, and playable scenarios.

## Resume procedure

1. Read this file, `docs/books/README.md`, and `docs/books/index.md`.
2. Inspect current `development` head and working tree/branch list. Do not assume SHAs in this document are current.
3. Check recent GitHub Actions runs and inspect failures. The build-and-contract run for commit `e87cdb05666e8b7938834083087e978885513885` succeeded. The subsequent fortifications commit `9d55c3288c89caec7427e19e4ebaede20e41435b` also passed its full build-and-contract workflow (run `45089000651`); Sith Policy failed (run `45089000634`) because `integration/autonomous-development-pipeline` remains alongside `master` and `development`. No branch was deleted.
4. Inspect open issues and recent comments before creating duplicate coordination work.
5. Current sourcebook work since the fortifications milestone: `AGENTS.md` onboarding; The Art of War LOD 2 descriptor, original SVG cover, and sourcebook; catalogue and collection README updated. Latest known sourcebook milestone before the current historical expansion: `2cabc722b10e68f3ea43b34c78420c4902e0a637`. Greek, Persian, and Roman sourcebooks and their descriptors/covers/catalogue entries have since been added; verify the current branch head before resuming.
6. Continue on `development`; refresh file SHAs before updates.
7. Add meaningful content and keep all catalogue/navigation documents synchronized.
8. Run/inspect CI. Report actual status, not intended status.
9. Update this onboarding file whenever architecture, priorities, branch state, key commits, workflow outcomes, or coordination blockers materially change.
10. End each session with a short resume note: latest commit, changed books, validation, next steps, and unresolved blockers.

## Creator's north star

Build a living picture of warfare across time, place, cultures, institutions, and imagination. Treat the subject seriously: people wage war in the name of peace, security, power, survival, faith, wealth, freedom, revenge, and many conflicting claims. Show the human costs and contradictions without reducing the domain to either glorification or condemnation. Make the books useful, deep, interconnected, and alive.


## Science-fiction warfare is first-class

Science fiction is not an appendix or a prediction claim. It is a full raWWar domain because organized force, contested authority, protection, raiding, resistance, and consequence extend into imagined futures. Build books for near-future conflict, orbital and space warfare, planetary defense, habitats, autonomous systems, cyber/information conflict, interstellar logistics, alien polities, posthuman societies, and speculative campaigns.

Every future-facing claim must be labeled as one of:
- **Established:** supported by current evidence or deployed systems.
- **Extrapolated:** plausible extension of known principles, with uncertainty.
- **Speculative:** depends on unresolved science, engineering, economics, or politics.
- **Fictional:** an invented rule of a particular setting.

Do not treat space as a naval ocean with a new skin. Account for orbital mechanics, energy, thermal management, communication delay, radiation, debris, propellant, and life-support dependencies where the setting uses known physics. If the setting introduces faster-than-light travel, artificial gravity, or other fictional technology, define its rules, limitations, costs, and strategic consequences. Treat infrastructure and civilian survival as central to planetary/habitat warfare. Autonomous systems do not erase human or institutional responsibility.

## Expanded current library state (2026-11-23)

The current LOD 2 library now also includes:
- [Piracy and Maritime Raiding](docs/books/piracy-and-maritime-raiding.md) — maritime predation, privateering, corsairing, naval authority, and coastal communities.
- [Outlaws, Bandits, and Popular Resistance](docs/books/outlaws-bandits-and-popular-resistance.md) — outlawry, social banditry, rebellion, resistance, and Robin Hood folklore.
- [Chinese Warfare](docs/books/chinese-warfare.md) — periodized military history across changing Chinese states and dynasties.
- [Science Fiction Warfare](docs/books/science-fiction-warfare.md) — near-future through interstellar conflict, with explicit evidence/speculation labels.

Each has a `.book.json` descriptor and original SVG cover. The index and collection README have been extended.

## Next editorial priorities

Continue breadth without creating shallow stubs:
1. Japanese Warfare, with careful periodization and distinction among political regimes, military institutions, and populations.
2. Dedicated Achaemenid, Parthian, and Sasanian books, then focused Persian Wars and Roman-Persian conflict studies.
3. Mongol warfare and steppe polities; Viking-age warfare and maritime networks.
4. Roman Republic, imperial army, civil wars, late Roman and eastern Roman focused volumes.
5. Science-fiction subbooks: orbital warfare and space infrastructure; planetary defense and habitat conflict; autonomous systems and machine agency; interstellar logistics and strategy; alien contact, diplomacy, and conflict.
6. Deeper LOD 3 books for campaigns, battles, organizations, theaters, and scenarios.

Do not present speculative future wars as historical fact. Science-fiction sourcebooks should be useful for coherent worldbuilding and simulation while remaining clear about what is known, extrapolated, or invented.

## Latest verified session checkpoint (2026-11-23)

- Previous development head before this expansion: `9db483fca4031008f113e7556316f55556d77e3d`.
- Build-and-contract workflow for that head passed: [run 45091391781](https://github.com/TrentBest/raWWar/actions/runs/45091391781).
- Sith Policy for that head failed: [run 45091391753](https://github.com/TrentBest/raWWar/actions/runs/45091391753), because `integration/autonomous-development-pipeline` remains in addition to `master` and `development`. Do not delete without explicit approval.
- New books added during this session: Piracy and Maritime Raiding; Outlaws, Bandits, and Popular Resistance; Chinese Warfare; Science Fiction Warfare. Each has a descriptor and original SVG cover.
- Index and collection README updated. Re-run CI after the documentation batch and record the resulting commit/run IDs before closing the session.
- Continue only on `development`; no master merge, release, deployment, or branch deletion.


## Latest library expansion — 2026-11-23

Two additional LOD 2 books now extend the catalogue:
- [Japanese Warfare](docs/books/japanese-warfare.md) — periodized history from early state formation through warrior governments, Sengoku, unification, Tokugawa peace, modern transformation, imperial war, and postwar memory; avoids timeless samurai stereotypes.
- [Orbital Warfare and Space Infrastructure](docs/books/orbital-warfare-and-space-infrastructure.md) — space infrastructure, orbital constraints, access, resilience, governance, escalation, and civilian consequences; avoids treating space as an ocean and labels speculative assumptions.

Both have matching descriptors and original SVG covers. Catalogue and collection README have been extended. The current session's new book set is therefore: Piracy and Maritime Raiding; Outlaws, Bandits, and Popular Resistance; Chinese Warfare; Science Fiction Warfare; Japanese Warfare; and Orbital Warfare and Space Infrastructure.

Next, deepen the Persian and Roman families, then add Mongol and Viking-age studies. Continue the science-fiction family with planetary defense/habitat conflict, autonomous systems and machine agency, interstellar logistics, and alien contact/conflict. Each volume must be substantive, cross-linked, and explicit about evidence versus speculation.


## Latest expansion — dedicated Iranian imperial books (2026-11-23)

Three focused LOD 2 sourcebooks now deepen the Persian family:
- [Achaemenid Warfare](docs/books/achaemenid-warfare.md) — royal authority, provincial mobilization, multi-community forces, logistics, and campaign history.
- [Parthian Warfare](docs/books/parthian-warfare.md) — Arsacid political-military networks, mounted forces, horse logistics, Roman relations, and frontier diplomacy.
- [Sasanian Warfare](docs/books/sasanian-warfare.md) — military institutions, elite cavalry, frontier systems, siegecraft, imperial finance, and the late antique wars.

Each has a matching `.book.json` descriptor and original SVG cover. The Persian overview remains the comparative entry point; the three new books provide distinct period-specific depth.

Next priorities: Mongol warfare and steppe polities; Viking-age warfare and maritime networks; focused Roman Republic, imperial army, civil wars, and Late Roman/Eastern Roman volumes; then science-fiction subbooks for planetary defense and habitats, autonomous systems and machine agency, interstellar logistics, and alien diplomacy/conflict. Keep evidence and speculation distinct.


## Latest expansion — steppe and Viking-age sourcebooks (2026-11-23)

Two further LOD 2 books extend the historical catalogue:
- [Mongol Warfare](docs/books/mongol-warfare.md) — steppe political formation, ecology, mounted mobility, command, intelligence, siege adaptation, regional campaigns, and successor khanates.
- [Viking-Age Warfare](docs/books/viking-age-warfare.md) — maritime networks, ships, raiding and trade, settlement, armed groups, and regionally distinct political transformations.

Both have matching descriptors and original SVG covers. Index and collection README are current. Maintain the distinction between raiding, trade, settlement, mercenary service, and conquest; do not flatten regional actors into a single identity.


## Latest expansion — Roman and planetary settlement warfare (2026-11-23)

Three LOD 2 sourcebooks were added:
- [Roman Republican Warfare](docs/books/roman-republican-warfare.md) — civic and allied manpower, republican command, Mediterranean expansion, naval adaptation, and civil-war transformation.
- [The Roman Imperial Army](docs/books/roman-imperial-army.md) — imperial military institutions, frontier systems, recruitment, logistics, military society, and late antique change.
- [Planetary Defense and Habitat Conflict](docs/books/planetary-defense-and-habitat-conflict.md) — planetary and habitat resilience, governance, civilian protection, legal uncertainty, autonomy, and de-escalation.

Each has a matching LOD 2 descriptor and original SVG cover. The science-fiction volume must retain explicit evidence labels (established, extrapolated, speculative, fictional) and remain a high-level resilience and consequence framework, not a procedural guide to attacking real infrastructure.

Next priorities: Roman civil wars and late antique/Eastern Roman development; then science-fiction companion volumes on autonomous systems and machine agency, interstellar logistics, and alien contact/diplomacy/conflict. Continue to separate historical evidence from literary memory and speculative technology from established capability.


## Latest expansion — Roman civil wars, Eastern Rome, and machine agency (2026-11-23)

Three substantive LOD 2 sourcebooks have been added, each with a matching .book.json descriptor and original SVG cover:
- [Roman Civil Wars](docs/books/roman-civil-wars.md) — Social War through the Augustan settlement; legitimacy, coalition politics, military allegiance, finance, and civilian consequences.
- [Eastern Roman Warfare](docs/books/eastern-roman-warfare.md) — periodized eastern Roman military history from late antiquity through 1453; institutions, fiscal foundations, diplomacy, frontier systems, and regional strategy.
- [Autonomous Systems and Machine Agency](docs/books/autonomous-systems-and-machine-agency.md) — delegated authority, oversight, accountability, reliability, escalation, civilian protection, and speculative machine political agency.

Keep the distinction between the Roman Republic, Principate, late Roman state, and medieval Eastern Roman polity explicit. “Byzantine” is a useful modern label, but the polity understood itself as Roman. Do not reduce civil wars to personality contests or military allegiance to a single simplistic reform narrative.

For machine systems, separate assistance, supervised execution, conditional autonomy, coordinated autonomy, strategic agency, and political agency. Do not imply that technical capability confers legal personhood or removes human/institutional responsibility. Label all future-facing claims established, extrapolated, speculative, or fictional. Keep discussion at the level of governance, resilience, accountability, and consequences—not real-world attack procedures.

## Current resume checkpoint (2026-11-23)

- Continue on development; no merge to master, release, deployment, or NuGet publishing without explicit authorization.
- Branches currently include master, development, and integration/autonomous-development-pipeline. Sith Policy remains failed because the third branch violates the two-branch policy. Do not delete it; request explicit owner approval.
- The preceding content build for the Roman/planetary batch passed: [run 45103161930](https://github.com/TrentBest/raWWar/actions/runs/45103161930).
- New current batch: Roman Civil Wars; Eastern Roman Warfare; Autonomous Systems and Machine Agency. Each has a descriptor and original SVG cover. Index and collection README are updated.
- Run build/contract validation after catalogue synchronization and record the actual workflow outcome. Recheck Sith Policy separately; do not report it as passing while the extra branch remains.
- Next priorities: interstellar logistics and strategy; alien contact, diplomacy, and conflict; then LOD 3 campaign and institutional studies. Continue to coordinate with Physics, Gestures, and SURPS at their ownership boundaries.


## Latest expansion — interstellar strategy and first contact (2026-11-23)

Two LOD 2 science-fiction sourcebooks have been added, each with a matching .book.json descriptor and original SVG cover:
- [Interstellar Logistics and Strategy](docs/books/interstellar-logistics-and-strategy.md) — light-time, travel, energy, life support, industrial ecology, network topology, political reach, autonomy, and civilian sustainment.
- [Alien Contact, Diplomacy, and Conflict](docs/books/alien-contact-diplomacy-and-conflict.md) — evidence standards, translation uncertainty, biological boundaries, representation, diplomacy, security dilemmas, escalation, restraint, and machine intermediaries.

For interstellar settings, do not treat distance as a simple map measurement or space as an ocean. Separate travel from communication, account for energy and thermal constraints, and expose industrial and life-support dependencies. FTL, generation-scale industrial independence, and closed ecosystems require explicit setting assumptions. For first contact, extraterrestrial life remains unconfirmed; distinguish verified observation from interpretation, extrapolation, speculation, and fictional premises. Never assume one envoy speaks for an entire species or that contact inevitably leads to war.

## Current resume checkpoint (2026-11-23)

- Latest sourcebook batch: Interstellar Logistics and Strategy; Alien Contact, Diplomacy, and Conflict. Each has a substantive Markdown volume, LOD 2 descriptor, and original SVG cover.
- docs/books/index.md and docs/books/README.md updated for both books.
- Continue on development. No merge to master, release, deployment, NuGet publishing, or branch deletion without explicit authorization.
- Known extra branch remains integration/autonomous-development-pipeline; leave it untouched pending explicit owner approval. Sith Policy cannot pass the two-branch rule while it exists.
- Validate descriptors, SVG covers, catalogue references, and repository CI. Report actual workflow status; do not infer push-triggered CI from PR-only workflow queries.
- Next priorities: focused LOD 3 campaigns, institutions, and theaters; deepen cross-links between the historical and speculative sourcebook families.


## Latest LOD 3 expansion — alien civilizations (2026-11-23)

- [Alien Civilizations and Political Systems](docs/books/alien-civilizations-and-political-systems.md) is now a substantive LOD 3 sourcebook with a matching descriptor and original SVG cover.
- The volume provides a reusable method for constructing fictional civilization histories, political systems, legitimacy, institutions, factions, strategic culture, logistics, armed organizations, diplomacy, and campaign histories. It distinguishes populations, regimes, states, institutions, and armed forces; it rejects species-wide personality and inevitable-conflict assumptions.
- Cross-links connect it to Alien Contact, Diplomacy, and Conflict; Interstellar Logistics and Strategy; Science Fiction Warfare; Autonomous Systems and Machine Agency; and Planetary Defense and Habitat Conflict.
- The index and collection README have been updated. Validate descriptor, cover, catalogue paths, and CI after this batch.
- Continue LOD 3 with specific civilization dossiers and campaign histories only after giving each polity internal diversity, institutional history, resource constraints, and credible non-war political options.
- Continue on development. Leave integration/autonomous-development-pipeline untouched pending explicit branch-deletion approval. No merge to master, release, deployment, or NuGet publication without explicit authorization.


## Latest LOD 3 case study — Nacrean Compact (2026-11-23)

- [The Nacrean Compact and the Tidal Archive Crisis](docs/books/nacrean-compact-and-tidal-archive-crisis.md) is a new substantive fictional LOD 3 civilization dossier and campaign history, with a matching descriptor and original SVG cover.
- The setting defines Pelagos, a fictional ocean moon, its distributed settlements, political membership categories, constitutional history, overlapping institutions, five internally divided coalitions, and distinct protective services.
- The campaign follows a disputed archive model through safety containment, competing explanations, legislative negotiation, an unauthorized emergency order, judicial restraint, independent reconstruction, and constitutional repair. Its strategic outcome is not a decisive battle.
- The volume includes infrastructure and logistics analysis, civilian continuity, playable roles, alternative outcomes, abstract simulation variables, and research/design notes. It labels fictional premises and avoids treating an alien population as a single actor.
- Catalogue and collection README have been updated. Validate descriptor JSON, SVG XML, paths, and CI.
- Latest known content build-and-contract workflow passed: [run 45108845646](https://github.com/TrentBest/raWWar/actions/runs/45108845646). Sith Policy failed: [run 45108845657](https://github.com/TrentBest/raWWar/actions/runs/45108845657), because `integration/autonomous-development-pipeline` remains beside `master` and `development`. Do not delete it without explicit approval.
- Continue on `development`. Leave the extra branch untouched; no merge to `master`, release, deployment, privileged workflow dispatch, or NuGet publication without explicit authorization.


## Latest LOD 3 expansion — connected Pelagos regional theatre (2026-11-23)

- [The Serein Passage Treaties and the Three-Current Crisis](docs/books/serein-passage-treaties-and-three-current-crisis.md) is a new substantive fictional LOD 3 connected-theatre volume, with a matching descriptor and original SVG cover.
- It expands the Nacrean setting into a regional system: the Vey Shelf Assemblies, Orison Works League, Outer Drift Mutuals, and Serein Passage Commission, each with distinct institutions and internally competing constituencies.
- It establishes the Serein basin's three current systems and five geographic zones; layered rescue, marker, passage-access, thermal-works, and records treaties; trade and migration flows; and shared navigation, communications, refuge, fabrication, ecological-monitoring, and records services.
- Its regional history runs from informal rescue networks through treaty formation, marker disputes, industrial coordination, and the Three-Current Crisis. The crisis centers on route-model provenance, uncertainty, representation, and service continuity; battle is not the assumed resolution.
- The volume includes multiple campaign outcomes, ten playable role perspectives, five scenario frames, an abstract simulation-variable model, continuity guidance, and research/design safeguards. It explicitly labels the setting fictional and avoids treating any polity as a unified personality.
- Catalogue and collection README updated. Validate descriptor JSON, SVG XML, file paths, internal links, and CI before closing the milestone.
- Latest known content build-and-contract workflow before this batch passed: [run 45108845646](https://github.com/TrentBest/raWWar/actions/runs/45108845646). Sith Policy failed: [run 45108845657](https://github.com/TrentBest/raWWar/actions/runs/45108845657), because `integration/autonomous-development-pipeline` remains beside `master` and `development`. Check fresh workflow results; do not delete the extra branch without explicit approval.
- Next expansion: deepen one neighboring polity (recommended: Vey Shelf Assemblies) or create a focused LOD 4 scenario from the regional theatre. Keep cross-polity dependencies consistent with the Nacrean case study.
- Continue on `development`. Leave `integration/autonomous-development-pipeline` untouched; no merge to `master`, release, deployment, privileged workflow dispatch, or NuGet publication without explicit authorization.


## Resume checkpoint — Serein regional theatre (2026-11-23)

- Latest development commit: [`f2b4655127e87d389b890427281c519814d1a9a7`](https://github.com/TrentBest/raWWar/commit/f2b4655127e87d389b890427281c519814d1a9a7), which adds the reciprocal cross-link from the Nacrean case study to the new regional theatre. The immediately preceding handoff commit is [`870e7f9977945c9688d91ee3d5196405de9a8491`](https://github.com/TrentBest/raWWar/commit/870e7f9977945c9688d91ee3d5196405de9a8491).
- New volume: [The Serein Passage Treaties and the Three-Current Crisis](docs/books/serein-passage-treaties-and-three-current-crisis.md), 29,554 characters and 14 major sections. Matching descriptor and original SVG cover are present; both catalogue surfaces link to all three assets.
- Validation completed: descriptor parsed as JSON; descriptor ID, LOD 3, and cover path match; SVG has an opening root, closing tag, and title; book, index, README, and handoff were fetched from development and confirmed to contain the expected additions; reciprocal Nacrean cross-link committed.
- The commit-scoped workflow lookup returned no runs because the available connector wrapper only returns pull-request-triggered runs. This is **not** evidence that push-triggered build/contract CI passed or failed. Check GitHub Actions in a later session using a repository-wide run listing if available.
- Branches observed: `development`, `master`, and `integration/autonomous-development-pipeline`. The extra branch remains unapproved for deletion. No branch deletion, master merge, release, deployment, or package publication occurred.
- Next: deepen the Vey Shelf Assemblies as a distinct LOD 3 polity dossier, or build a focused LOD 4 scenario around the Expiring Emergency Order. Preserve treaty, geography, and timeline continuity; do not turn any polity into a monolithic actor.


## Latest sourcebook family — medicine and healing (2026-11-23)

- [Medicine and Healing: History, Practice, and Play](docs/books/medicine-and-healing.md) is the new LOD 2 foundation for a dedicated raWWar medicine series. It has a matching descriptor and original SVG cover; both sourcebook catalogue surfaces and the documentation navigation now link to all three.
- The volume covers historical healing traditions across eras and regions; military medicine and the casualty chain; triage under scarcity; medic qualifications and institutional trust; trauma, bleeding, mangled limbs, amputation, infection, rehabilitation, and psychological recovery; fantasy healing and resurrection design; and evidence-labeled future medicine.
- Player-medic design includes a layered HUD with observed versus inferred data, sensor provenance, confidence, timestamps, treatment history, triage state, battery reserve, evacuation, handoff, and reassessment. The HUD is decision support, not an oracle; sensor failure must never silently become a diagnosis or death declaration.
- The creator's hypersonic scalpel is recorded as a **Fictional** raWWar instrument with calibration, power, safety, qualification, and failure constraints. The book explicitly rejects the idea that extraordinary sharpness guarantees painless cutting or substitutes for anesthesia, diagnosis, or surgical judgment. Emergency amputation is rare and exceptional, not a routine player action.
- The book distinguishes **CPR** as circulatory support from **defibrillation** for eligible shockable rhythms; a flatline is not ordinarily corrected with a shock. The medic's battery powers regulated, purpose-built devices, not improvised direct electrical contact or a generic “restart the heart” action.
- Real-world clinical material is high-level and non-procedural. The book is educational/game-design material, not clinical training or a treatment protocol. Real medical decisions require qualified professionals and current authoritative guidance.
- Planned companion volumes: Healing Across the Centuries; Battlefield Medicine and the Casualty Chain; Surgery, Amputation, and Prosthetic Futures; Medicine at Sea, in Space, and on the Frontier; Fantasy Healing and the Laws of Restoration; Future Medicine and Human-Machine Care; Medical Ethics, Law, and Protected Care; Alien and Posthuman Physiology.
- Validation to perform after catalogue synchronization: parse descriptor JSON; verify descriptor ID, LOD, and cover path; inspect SVG root/title/closing tag; confirm sourcebook, descriptor, and cover links in both catalogue files and docs/README.md; confirm medical HUD and scalpel concepts remain explicitly marked as game design/Fictional; inspect CI results without inferring push-triggered success from a PR-only workflow query.
- Continue on development. Branches observed before this batch: master, development, and integration/autonomous-development-pipeline. The extra branch remains unapproved for deletion. No branch deletion, master merge, release, deployment, privileged workflow dispatch, or package publication was performed or authorized.
- Next recommended expansion: **Healing Across the Centuries**, built as a globally comparative, periodized history with regional companion volumes and a carefully sourced bibliography. Follow with **Battlefield Medicine and the Casualty Chain** to deepen the playable medic role and HUD contract.


## Medicine series expansion — Healing Across the Centuries (2026-11-23)

- [Healing Across the Centuries](docs/books/healing-across-the-centuries.md) is the first focused historical companion to [Medicine and Healing: History, Practice, and Play](docs/books/medicine-and-healing.md). It has a matching LOD 2 descriptor and original SVG cover.
- The volume surveys care before formal professions; early urban medical texts; South Asian, Chinese, Mediterranean, and other traditions; classical and late antique worlds; medieval institutions and translation networks; African, Indian Ocean, and Indigenous American histories; early modern exchange; nineteenth-century anesthesia, infection control, nursing, and public health; wartime casualty systems; disability, prosthetics, epidemics, and modern recovery.
- The method distinguishes material, textual, institutional, retrospective, and legendary evidence. It explicitly rejects a single linear ladder of medical progress, civilization-wide stereotypes, efficacy claims based only on written remedies, and erasure of informal caregivers and patients.
- The foundation book now links reciprocally to this historical companion. Both catalogue surfaces and docs/README.md have been updated for the new volume.
- Validate descriptor JSON, descriptor ID/LOD/cover path, SVG root/title/closing tag, sourcebook links in index and README, reciprocal cross-link, and current CI status. The earlier commit-scoped workflow wrapper only returned PR-triggered runs; empty results do not prove push CI status.
- Next recommended volume: **Battlefield Medicine and the Casualty Chain**, with a deeper, historically grounded treatment of evacuation, triage, trauma systems, field hospitals, medical neutrality, and the player-medic HUD. Keep procedural real-world medicine non-instructional and use current authoritative clinical sources for any specific claims.


## Medicine sourcebook CI result (2026-11-23)

- Build and contract checks passed for the medicine-series catalogue checkpoint: [GitHub Actions run 45117789795](https://github.com/TrentBest/raWWar/actions/runs/45117789795), commit `d5021f3bc92bdb2da3884af12ca6c2e06a6db237`.
- Sith Policy failed in [run 45117789786](https://github.com/TrentBest/raWWar/actions/runs/45117789786), because `integration/autonomous-development-pipeline` remains alongside `master` and `development`. This branch is still not approved for deletion; request explicit owner approval after ancestry and unique commits are inspected.
- The medicine books, descriptors, covers, reciprocal cross-link, catalogue entries, docs navigation, and this handoff are present on `development`. The latest handoff update follows the successful content build. Re-run build/contract checks after subsequent edits.


## New sourcebook family — formations, bases, resupply, and battlefield medicine (2026-11-23)

The creator clarified that raWWar should treat embodied instruction as a reusable game architecture: the HUD displays a ghost action or target arrangement; Gestures provides reusable embodied movement semantics; tracking observes the player's performance; haptics/visual/audio cues communicate alignment and timing; raWWar defines the task context and transparent scoring rubric. Never claim this integration is already implemented in runtime code merely because the sourcebooks define it.

New LOD 2 books on development:
- [Formations I: Land Forces](docs/books/formations-i-land-forces.md) — recursive teams/squads/platoons and larger formations, geometry, transitions, command, terrain, cohesion, ghost slots, and scoring.
- [Formations II: Naval Forces](docs/books/formations-ii-naval-forces.md) — fleet organization, station keeping, convoy, amphibious and mixed-domain coordination, sea-state and navigation constraints.
- [Formations III: Air Forces](docs/books/formations-iii-air-forces.md) — three-dimensional geometry, separation, weather, human factors, autonomy, and orbital mechanics distinctions.
- [Bases and Encampments](docs/books/bases-and-encampments.md) — small mobile camps, literary inspiration clearly labeled as fiction, Roman castra, larger bases, services, local impacts, resilience, and simulation data.
- [The Art of Resupply](docs/books/the-art-of-resupply.md) — demand, supply chain, transport, last-mile delivery, repair, medical logistics, naval/air sustainment, contested routes, ethics, and HUD/ghost logistics.
- [Battlefield Medicine and the Casualty Chain](docs/books/battlefield-medicine-and-the-casualty-chain.md) — casualty network, triage context, collection, evacuation, handoff, medic HUD, ghost surgical-line game interaction, scoring, and suit-integrated fictional cardiac support.

Each new volume has a .book.json descriptor and original SVG cover. Both sourcebook catalogue surfaces and docs/README.md have been updated. The Medicine and Healing foundation now cross-links the battlefield companion.

### Design boundaries
- Formations are not just diagrams: preserve identity, command provenance, physical constraints, transitions, and local agency.
- Naval formations are not land formations on water; air formations are 3D; orbital formations require orbital mechanics rather than aircraft metaphors.
- Literary camp references (including Wheel of Time examples) are inspiration only; distinguish fiction from historical evidence and do not reproduce protected prose or distinctive adaptations.
- Base layouts must include water, sanitation, medical care, maintenance, logistics, civilian relationships, and environmental consequences—not just defensive perimeter geometry.
- Resupply is an operation and network, not an inventory button. Track stock condition, capacity, transit, last mile, staffing, repair, and allocation.
- Ghost actions must expose their rubric. Separate target alignment, path deviation, timing, tool state, sequence, and safety. Gate scoring on tracking confidence and offer accessible alternatives to haptics.
- The exoskeleton's pre-positioned cardiac-support leads and dedicated controller are Fictional setting equipment. Do not portray a battery as directly wired to a heart or imply that any stopped heart can be restarted by a shock. Preserve the distinction between CPR and eligible defibrillation.
- Real clinical and aviation/naval operational procedures remain out of scope for player instruction; use qualified current sources and expert review for any real-world training claim.
- Physics owns physical simulation; Gestures owns reusable embodied movement; raWWar owns domain meaning, context, and evaluation; host/runtime owns execution. SURPS remains opt-in.

Validate all descriptors as JSON, SVG covers as XML, cover paths, catalogue links, cross-links, and current CI. Do not infer runtime implementation from documentation. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit approval.


## Validation checkpoint — formations and sustainment family (2026-11-23)

- Six new LOD 2 sourcebooks are present with descriptors and original SVG covers: Formations I: Land Forces; Formations II: Naval Forces; Formations III: Air Forces; Bases and Encampments; The Art of Resupply; Battlefield Medicine and the Casualty Chain.
- All six descriptors were fetched and parsed as JSON. Each descriptor ID and LOD 2 value were checked; each declared cover path matches a fetched SVG. Each SVG has an opening/closing root and title. All six Markdown files were fetched; section counts range from 16 to 21 major headings.
- Both book catalogue surfaces and docs/README.md were updated; the Medicine and Healing foundation cross-links to the battlefield companion. The new books cross-link within the family.
- The battlefield medicine companion explicitly specifies ghost cut-line guidance, tool alignment, emphatic but interpretable haptic cues, transparent scoring dimensions, tracking-confidence gating, and accessible non-haptic alternatives. It also records the fictional suit-integrated cardiac-support leads as predefined protected interfaces around the cardiac region, with a dedicated controller and reserved energy. This remains setting fiction, not clinical guidance.
- Earlier book-content checkpoint build passed: [run 45121085453](https://github.com/TrentBest/raWWar/actions/runs/45121085453), on the battlefield companion's initial commit. The later catalogue/handoff and haptic refinement commits triggered newer workflows; check the latest run before claiming final build/contract success.
- Latest source update before this handoff: 9badfb6cef3a3ecce25e7ef372c661ea76fe7ed0. The handoff commit that follows this note will become the new branch head.
- Branches observed: development, master, and integration/autonomous-development-pipeline. The extra branch remains unapproved for deletion. No branch deletion, master merge, release, deployment, privileged workflow dispatch, or package publication occurred.
## Mounted formations expansion — Formations IV (2026-11-23)

- [Formations IV: Mounted and Cavalry Forces](docs/books/formations-iv-mounted-and-cavalry.md) is a new LOD 2 companion with a matching descriptor and original SVG cover.
- The book distinguishes cavalry, mounted infantry, chariots, scouts, couriers, pack/draft animals, and mixed mounted/dismounted organizations. It treats rider and mount as distinct living participants with linked but non-identical identity, readiness, fatigue, health, and welfare.
- Coverage includes historical variation, steppe remount systems, terrain and route capacity, communication, dismount/remount transitions, animal welfare, veterinary support, feed/water/forage, command institutions, ghost-training concepts, transparent scoring, and simulation contracts.
- Cross-links connect Formations I–III, Bases and Encampments, The Art of Resupply, Mongol Warfare, Parthian Warfare, Sasanian Warfare, Roman Warfare, and Battlefield Medicine. The index and collection README plus docs navigation must stay synchronized.
- Do not present this sourcebook as real riding, animal handling, or military training. Do not reward animal exhaustion or abuse. Animal-specific medical expertise must not be conflated with human battlefield medicine. Physics owns physical simulation; Gestures owns embodied movement; raWWar owns domain context and evaluation; SURPS remains opt-in.
- Validation checkpoint: descriptor JSON, ID/LOD/cover path, SVG XML, Markdown links, catalogue entries, and build/contract CI.
- Latest known build/contract run for commit `ac76913ba3e7594896fd3340f70d95138778bd7c` passed: [run 45121253255](https://github.com/TrentBest/raWWar/actions/runs/45121253255). Sith Policy failed: [run 45121253325](https://github.com/TrentBest/raWWar/actions/runs/45121253325), because the extra branch `integration/autonomous-development-pipeline` remains. It is not approved for deletion; inspect ancestry and unique commits, report readiness, and wait for explicit approval.
- Continue on development. No branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit authorization.


## Mechanized formations expansion — Formations V (2026-11-23)

- [Formations V: Mechanized and Armored Forces](docs/books/formations-v-mechanized-and-armored-forces.md) is a new LOD 2 sourcebook with a matching descriptor and original SVG cover.
- It models crews, vehicles, dismounted personnel, support organizations, command, route and infrastructure constraints, fuel/energy, ammunition and stores, maintenance, recovery, industrial depth, medical support, civilian impacts, autonomous systems, and transparent simulation contracts.
- It distinguishes armored, mechanized, motorized, combined-arms, and support formations without projecting modern categories backward onto historical cases. It avoids real-world operational or vehicle-crew instruction.
- People and platforms retain separate identities and state. Dismounted personnel do not become cargo or disappear; disabled vehicles are not automatically destroyed; readiness is mission-specific and decomposed into visible dimensions.
- Cross-links connect Formations I–IV, Bases and Encampments, The Art of Resupply, Fortifications, Battlefield Medicine, Science Fiction Warfare, and Orbital Warfare. Keep the index, collection README, and docs navigation synchronized.
- Physics owns physical simulation; Gestures owns reusable embodied movement; raWWar owns domain meaning, history, scenario context, and evaluation; host/runtime owns execution. SURPS remains opt-in.
- Validate descriptor JSON, SVG XML, cover path, Markdown links, catalogue entries, and build/contract CI. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit approval.


## Orbital formations expansion — Formations VI (2026-11-23)

- [Formations VI: Orbital and Space Forces](docs/books/formations-vi-orbital-and-space-forces.md) is a new LOD 2 sourcebook with descriptor and original SVG cover.
- It complements, rather than duplicates, [Orbital Warfare and Space Infrastructure](docs/books/orbital-warfare-and-space-infrastructure.md): Formations VI owns organization and coordination of orbital assets, crews, stations, service craft, and support relationships; the broader orbital volume owns the wider security, governance, and infrastructure analysis.
- Preserve time-dependent orbital state, epoch, reference frame, uncertainty, relative motion, and communications latency. Spacecraft are not ships; orbital paths are not roads. Do not import naval or aircraft maneuver assumptions.
- Cover station keeping, rendezvous concepts at a non-procedural level, crew and civilian identity, autonomy boundaries, resource endurance, thermal/radiation/debris context, docking/service dependencies, rescue, law, escalation, and civilian impacts.
- Use evidence labels: Established, Extrapolated, Speculative, Fictional. Physics owns physical dynamics and environmental simulation; raWWar owns domain organization, historical/political context, scenarios, and evaluation; Gestures owns reusable embodied interaction; SURPS remains opt-in.
- Validate descriptor JSON, SVG XML, Markdown links, catalogues, docs navigation, reciprocal links, and build/contract CI. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit authorization.


## Amphibious and littoral formations expansion — Formations VII (2026-11-23)

- [Formations VII: Amphibious and Littoral Forces](docs/books/formations-vii-amphibious-and-littoral-forces.md) is a new LOD 2 sourcebook with descriptor and original SVG cover.
- Treat the littoral as a connected coastal system: sea, shore, rivers and estuaries, ports, beaches, infrastructure, civilian economies, and inland distribution. The core problem is cross-domain transition and handoff, not merely landing infantry.
- Preserve the identity of embarked people and organizations independently from transport platforms. Track transfer, custody, capacity, shore receiving services, command authority, and uncertainty.
- Cover naval and land interfaces, transfer assets, coastal geography, ports, riverine systems, engineering, sustainment throughput, casualty movement, civilian services, humanitarian missions, law, autonomy, and failure recovery.
- Avoid real-world landing plans, tactical procedures, targeting, sabotage, and infrastructure-harm instructions. Use evidence labels: Established, Extrapolated, Speculative, Fictional.
- Physics owns physical/environmental simulation; raWWar owns domain knowledge, organization, history, scenarios, and assessment; Gestures owns reusable embodied interaction; logistics and facilities remain with their providers; SURPS is opt-in.
- Keep both catalogues, docs navigation, reciprocal formation links, and this handoff synchronized. Validate descriptor JSON, SVG XML, local Markdown links, and build/contract CI. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit authorization.



## Specialized and irregular formations expansion — Formations VIII (2026-11-23)

- [Formations VIII: Special Operations and Irregular Forces](docs/books/formations-viii-special-operations-and-irregular-forces.md) is a new LOD 2 sourcebook with a matching descriptor and original SVG cover.
- Keep special operations as a category of military specialization distinct from irregularity as an institutional or political relationship. A specialized unit may be fully regular; an irregular organization is not automatically illegitimate, effective, or morally justified.
- Distinguish resistance movements, insurgencies, partisan organizations, militias, auxiliaries/proxies, paramilitary bodies, criminal armed groups, and clandestine civic networks. Do not automatically classify unarmed political or mutual-aid networks as military formations.
- Preserve the distinction between an organization and the population around it. Model consent, coercion, civilian protection, legal status, accountability, external sponsorship, fragmentation, and demobilization explicitly. Never infer allegiance from ethnicity, religion, language, residence, family, or appearance.
- This volume is historical, organizational, and simulation-design material. It excludes actionable clandestine tradecraft, raids, sabotage, evasion, targeting, weapons employment, and methods for defeating security systems. Prefer scenarios about evidence, oversight, negotiation, civilian protection, and institutional transition.
- raWWar owns domain vocabulary and context; Physics owns physical simulation; Gestures owns reusable embodied interaction; SURPS remains opt-in. Do not imply documentation creates runtime implementation.
- Keep both sourcebook catalogues, docs navigation, and reciprocal cross-links synchronized. Validate descriptor JSON, SVG XML, cover path, Markdown links, and build/contract CI. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit approval.


## Engineering, fleet, and planetary expeditionary expansion (2026-11-23)

- [Formations IX: Engineering and Construction Forces](docs/books/formations-ix-engineering-and-construction-forces.md) is a new LOD 2 sourcebook. It treats engineering as an organization and project lifecycle, covering bridge trains, temporary crossings, floating bridges, ferries, route and field infrastructure, utilities, repair, recovery, civilian labor, and handover. The bridge anecdote is presented as an illustrative principle, not asserted as a verified historical event: a crossing can be a temporary instrument of maneuver whose continued existence, removal, transfer, and civilian dependencies require explicit decisions.
- [Formations X: Armadas and Flotillas](docs/books/formations-x-armadas-and-flotillas.md) is a new LOD 2 sourcebook. It covers armadas, flotillas, squadrons, task forces, escorts, transports, auxiliaries, coalition authority, concentration and dispersion, ports, repair, maritime sustainment, and civilian shipping. Do not impose one universal hierarchy on historically variable naval terms.
- [Planetary Assault and Expeditionary Doctrine](docs/books/planetary-assault-and-expeditionary-doctrine.md) is a new LOD 2 science-fiction doctrine sourcebook. It distinguishes planetary assault from broader expeditionary missions and amphibious warfare. Orbital mechanics, atmospheric and environmental constraints, biology, local infrastructure, political authority, civilian survival, and escalation must remain first-class. Arrival is not control or legitimacy.
- All three books have matching .book.json descriptors and original SVG covers. Their catalogue entries and docs navigation are maintained in docs/books/index.md, docs/books/README.md, and docs/README.md.
- The books are historical, conceptual, and simulation-design references. They do not provide real-world bridge construction, demolition, sabotage, landing procedures, targeting, or weapons employment instructions. Use Established / Extrapolated / Speculative / Fictional labels for future technology. Physics owns authoritative physical models; Gestures owns reusable embodied interaction; raWWar owns domain organization, history, doctrine context, and simulation requirements; SURPS remains opt-in.
- Validate descriptor JSON, SVG XML, Markdown paths and links, reciprocal links, and build/contract CI. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit approval.


## Resource, industrial, and interstellar expansion (2026-11-23)

- [Military Resources and Sustainment](docs/books/military-resources-and-sustainment.md) — A lifecycle study of the resources that make organized forces possible: food, water, energy, materials, labor, finance, transport, maintenance, recovery, and the human and political consequences of scarcity.
- [Extraction, Materials, and Industrial Foundations](docs/books/extraction-materials-and-industrial-foundations.md) — A comparative study of resource extraction and industrial transformation, from terrestrial agriculture, mines, forests, and refineries to extraterrestrial prospecting, in-situ resource use, and the economics of materials across different worlds.
- [Orbital Industry and Strategic Resource Networks](docs/books/orbital-industry-and-strategic-resource-networks.md) — A systems-level study of off-world industrial infrastructure, asteroid prospecting and capture concepts, processing centers, depots, transport networks, and the physics, governance, and resilience constraints that shape a solar-system economy.
- [Interstellar Economies and Multi-System War](docs/books/interstellar-economies-and-multi-system-war.md) — A long-horizon framework for imperial and interstellar political economies, multi-star-system logistics, strategic autonomy, mobilization, communication delay, and conflicts across civilizations spanning millions of worlds.

These books establish the next connected domain family: military consumption and sustainment; terrestrial and extraterrestrial extraction and processing; orbital industrial networks and receiving infrastructure; and interstellar economies and multi-system war in a millennia-old empire with millions of inhabited worlds. “Rare earth elements” is a conventional element-group term, not a claim of Earth exclusivity; use contextual language such as “rare on Earth” for cross-world abundance comparisons. Mercury is a rocky metal-rich planet, not a planet-wide liquid-mercury ocean. Electric/ion propulsion uses electrical power to accelerate reaction mass; it does not create free braking or erase momentum/energy management. The asteroid catcher/processing-center concept is speculative and must account for mass, velocity, kinetic energy, capture loads, navigation, thermal rejection, throughput, governance, and failure recovery. Distinguish whole-body transport from local processing and standardized cargo. Interstellar communication delay, travel constraints, system autonomy, political legitimacy, and civilian survival are first-class. Physics owns authoritative physical models; raWWar owns military demand, doctrine context, history, and simulation requirements; SURPS remains opt-in. Continue on development; no branch deletion, merge to master, release, deployment, privileged workflow dispatch, or package publication without explicit authorization.


## Latest addition — military industry and wartime production (2026-11-23)

- [Military Industry and Wartime Production](docs/books/military-industry-and-wartime-production.md) — LOD 2 sourcebook covering historical arsenals, shipyards, aircraft and spacecraft production, orbital fabrication, civilian-to-military conversion, procurement, labor and skills, bottlenecks, surge capacity, repair, quality assurance, industrial geography, war economies, and the fictional industrial governance of a mature interstellar empire.
- Descriptor: `docs/books/military-industry-and-wartime-production.book.json`, id `surps.domain.military-industry-and-wartime-production`, category Domain, LOD 2, version 0.1.0.
- Original cover: `docs/books/covers/military-industry-and-wartime-production.svg`.
- Important modeling distinction: installed capacity != available capacity != throughput != accepted delivery != operational readiness. Production orders track each state separately.
- Future industrial claims use Established, Extrapolated, Speculative, and Fictional labels. Space elevators, large orbital yards, and interstellar industrial specialization are not presented as established capabilities.
- The new volume complements (does not replace) Military Resources and Sustainment (consumption and supply), Extraction, Materials, and Industrial Foundations (raw materials and processing), Orbital Industry and Strategic Resource Networks (space industrial geography), and Interstellar Economies and Multi-System War (multi-system political economy).
- Latest sourcebook files and catalogue/navigation updates are on `development`. Verify current HEAD and run status before further changes. Latest known build-and-contract run for prior checkpoint `ce7f5af339824e658b5273d541b0f69ea009312c` passed (run 45265378312); Sith Policy failed because branches are `master`, `development`, and `integration/autonomous-development-pipeline`. Do not delete branches without explicit authorization. No merge to master, release, deployment, privileged workflow dispatch, or package publication.


## Contemporary warfare sourcebook addition (2026-11-23)

- [The Russia–Ukraine War: Drone, Robotic, and Industrial Adaptation](docs/books/russia-ukraine-war-drone-robotic-and-industrial-adaptation.md) — LOD 2 living case study, covering 2014–present with emphasis on the full-scale invasion from 2022 and evidence current through 2026-11-23.
- Companion descriptor: [russia-ukraine-war-drone-robotic-and-industrial-adaptation.book.json](docs/books/russia-ukraine-war-drone-robotic-and-industrial-adaptation.book.json); original cover: [russia-ukraine-war-drone-robotic-and-industrial-adaptation.svg](docs/books/covers/russia-ukraine-war-drone-robotic-and-industrial-adaptation.svg).
- Treat this as an ongoing, evidence-sensitive history. Date claims; distinguish verified public record, attributed claims, corroborated assessments, disputed claims, and unknowns. Never present official wartime claims as independently verified by default.
- The sourcebook is analytical and human-centered, not a tactical manual. Do not add weapon construction, targeting, attack planning, evasion, jamming recipes, or actionable procedures for defeating systems.
- Link it with Autonomous Systems and Machine Agency, Formations I–III and VIII–X, Military Resources and Sustainment, Military Industry and Wartime Production, and the broader Science Fiction Warfare library. Keep Physics, Gestures, and SURPS ownership boundaries intact.
