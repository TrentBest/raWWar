# raWWar — Design Documentation

This directory is the design and production knowledge base for the raWWar Experience.

## Authority

The **Game Design Bible** records established design truth and creator decisions.

The **Game Design Document (GDD)** turns that truth into a working description of the player experience and production scope.

System-specific documents expand the design without silently changing canon.

## Core design

- [Game Design Bible](GAME_DESIGN_BIBLE.md) — authoritative living design.
- [Game Design Document](GAME_DESIGN_DOCUMENT.md) — working production GDD.
- [Vision and Pillars](VISION_AND_PILLARS.md) — north star and non-negotiable principles.
- [Player Roles](PLAYER_ROLES.md) — occupations, roles, and qualification-driven participation.
- [Persistent Universes](PERSISTENT_UNIVERSES.md) — long-term persistent-world vision.
- [Open Questions](OPEN_QUESTIONS.md) — unresolved design questions requiring elicitation.

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
