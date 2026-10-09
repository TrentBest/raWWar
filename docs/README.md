# raWWar — Design Documentation

This directory is the design and production knowledge base for the raWWar Experience.

## Authority

The **Game Design Bible** records established design truth and creator decisions.

The **Game Design Document (GDD)** turns that truth into a working description of the player experience and production scope.

System-specific documents expand the design without silently changing canon.

## Core design

- [Game Design Bible](GAME_DESIGN_BIBLE.md) — authoritative living design.
- [Game Design Document](GAME_DESIGN_DOCUMENT.md) — comprehensive living master GDD.
- [Public Design Hub](DESIGN_HUB.md) — future WebPage presentation of the raWWar design.
- [Experience Architecture](EXPERIENCE_ARCHITECTURE.md) — how raWWar fits into FSM_API, MicroBundles, FSM_COS, AnyApp, UserIO, and the Workshop Renderer, including what raWWar deliberately does not reinvent.
- [Vision and Pillars](VISION_AND_PILLARS.md) — north star and non-negotiable principles.
- [Player Roles](PLAYER_ROLES.md) — occupations, roles, and qualification-driven participation.
- [Persistent Universes](PERSISTENT_UNIVERSES.md) — long-term persistent-world vision.
- [Authored Galaxies, Campaigns, and Lineage](AUTHORED_GALAXIES_CAMPAIGNS_AND_LINEAGE.md) — deliberate single-player campaign construction, reproducible multiplayer generation, civilization ancestry, validation, and distribution.
- [Open Questions](OPEN_QUESTIONS.md) — unresolved design questions requiring elicitation.

## Visual atlas

- [Visual Asset Catalogue](images/README.md) — an illustrated map of the current visual language, system diagrams, and where each image belongs.
- [Advisor agency and consequences](images/rawwar-advisor-agency.svg) — how an advisor's agency, earned experience, warnings, physical outcomes, and persistent history connect.
- [Work kanban lifecycle](images/rawwar-work-kanban.svg) — the board as a projection of real work, with evidence-based transitions and blockers.
- [Science floor construction](images/rawwar-science-floor-construction.svg) — the physical prerequisites between proposing a lab and operating it.
- [Ship damage and salvage](images/rawwar-ship-damage-and-salvage.svg) — part hierarchy, directional effects, causal system failures, and surviving wreck components.
- [Faction ship design languages](images/rawwar-faction-ship-languages.svg) — candidate ship silhouettes showing how doctrine changes engineering, not just paint.
- [Starship sizing, damage, and salvage](STARSHIP_SIZING_DAMAGE_AND_SALVAGE.md) — candidate reference ship, what is sized now, what remains unknown, and how to model persistent destruction.
- [Faction ship design languages](FACTION_SHIP_DESIGN_LANGUAGES.md) — how shared human missions produce recognizable faction-specific ship architecture, industrial signatures, tradeoffs, refits, and repair histories.
- [Experience architecture](images/rawwar-experience-architecture.svg) — what raWWar owns versus reusable Workshop machinery.
- [Engineering base cutaway](images/rawwar-base-cutaway.svg) — facilities, staffing, logistics, and infrastructure as one system.
- [Gesture sequence](images/rawwar-gesture-sequence.svg) — semantic intent becoming physical procedure and visible action.

## Production design

- [Systems Design](SYSTEMS_DESIGN.md) — systemic behavior and simulation concepts.
- [Narrative Design](NARRATIVE_DESIGN.md) — story, factions, environmental storytelling, and ambient life.
- [Art Direction](ART_DIRECTION.md) — original visual language, soldiers, equipment, vehicles, and environments.
- [Audio Direction](AUDIO_DIRECTION.md) — sound and music direction.
- [UX and Interaction](UX_AND_INTERACTION.md) — player interaction and information presentation.
- [Multiplayer Design](MULTIPLAYER_DESIGN.md) — cooperative, competitive, and persistent multiplayer.
- [Technical Design](TECHNICAL_DESIGN.md) — architecture, simulation, renderer, data, and persistence boundaries.
- [Production Plan](PRODUCTION_PLAN.md) — workstreams, capability milestones, and definition of done.

## Design discipline

Every document should distinguish:

- **Canon** — directly established by the creator.
- **Candidate** — proposed design awaiting confirmation.
- **Experiment** — something being tested.
- **Open question** — deliberately unresolved.

The purpose of this layer is to **edify, not mystify**: someone new to the project should be able to understand what raWWar is, why it works this way, and what remains to be discovered before reading implementation code.

## Working rule

The design should lead the implementation.

If implementation exposes a better possibility, record the possibility here first rather than allowing an implementation convenience to silently become game design.
