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
