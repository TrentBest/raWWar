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
- [Sourcebook catalogue](docs/books/index.md).
- [Editorial collection and roadmap](docs/books/README.md).
- [SURPS participation issue](https://github.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/issues/4).
- [raWWar sourcebook programme issue](https://github.com/TrentBest/raWWar/issues/12).

Build next: The Art of War; Greek Warfare; Persian Warfare (separate Achaemenid/Parthian/Sasanian treatments); Roman Warfare; Piracy and Maritime Raiding; Outlaws, Bandits and Popular Resistance. Then deepen into campaigns, battles, leaders, formations, equipment, doctrine, logistics, civilian experience, and playable scenarios.

## Resume procedure

1. Read this file, `docs/books/README.md`, and `docs/books/index.md`.
2. Inspect current `development` head and working tree/branch list. Do not assume SHAs in this document are current.
3. Check recent GitHub Actions runs and inspect failures. The build-and-contract run for commit `e87cdb05666e8b7938834083087e978885513885` succeeded; the subsequent fortifications commit `9d55c3288c89caec7427e19e4ebaede20e41435b` had its build workflow pending at last handoff, while Sith Policy failed because an extra branch existed.
4. Inspect open issues and recent comments before creating duplicate coordination work.
5. Continue on `development`; refresh file SHAs before updates.
6. Add meaningful content and keep all catalogue/navigation documents synchronized.
7. Run/inspect CI. Report actual status, not intended status.
8. Update this onboarding file whenever architecture, priorities, branch state, key commits, workflow outcomes, or coordination blockers materially change.
9. End each session with a short resume note: latest commit, changed books, validation, next steps, and unresolved blockers.

## Creator's north star

Build a living picture of warfare across time, place, cultures, institutions, and imagination. Treat the subject seriously: people wage war in the name of peace, security, power, survival, faith, wealth, freedom, revenge, and many conflicting claims. Show the human costs and contradictions without reducing the domain to either glorification or condemnation. Make the books useful, deep, interconnected, and alive.
