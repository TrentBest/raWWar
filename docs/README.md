# raWWar — Design Documentation

This directory is the design and production knowledge base for the raWWar Experience.

## Documentation standard

- [Documentation Conformance](DOCUMENTATION_CONFORMANCE.md) — how raWWar applies the shared Workshop section identifiers, visual markers, evidence rules, architecture boundaries, and example requirements. The referenced [FSM_COS Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/docs/ecosystem-documentation-standard/DOCUMENTATION_STANDARD.md) is currently a proposal under review; raWWar's conformance work is incremental, not a claim that every companion document has already been audited.

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
- [Mapping the Milky Way](MAPPING_THE_MILKY_WAY.md) — evidence-backed galactic reference, Solar System placement, procedural population expansion, motion across time, and truthful navigation presentation.
- [Navigation Frames and Chart Evolution](NAVIGATION_FRAMES_AND_CHART_EVOLUTION.md) — how Earth-origin, Solar-System, stellar, and galactocentric chart frames evolve with navigation equipment, standards, uncertainty, and historical knowledge.
- [Contested History, Betrayal, and the Politics of Memory](CONTESTED_HISTORY_BETRAYAL_AND_MEMORY.md) — victor-written narratives, conflicting battle accounts, evidence provenance, betrayal, propaganda, cover-ups, and player-led historical investigation.
- [Embodied Interaction, Mechs, and Engineering Operations](EMBODIED_INTERACTION_MECHS_AND_ENGINEERING_OPERATIONS.md) — integrated overview of physical in-world UI, shared FPS/VR/input semantics, breathing Gesture guidance, componentized mechs, engineering logs, lockout/tagout, and measured renderer performance.
- [Diegetic Interaction and Physical Interfaces](DIEGETIC_INTERACTION_AND_PHYSICAL_INTERFACES.md) — physical controls, mounted screens, desktop/VR semantic parity, damageable interfaces, and repair work.
- [Mechs, Machines, and the Evolution of Warfare](MECHS_MACHINES_AND_WARFARE_EVOLUTION.md) — pilotable battle mechs and branching combat, industrial, research, extraction, and exotic-environment platforms.
- [Mech Cockpit and Pilot Interaction](MECH_COCKPIT_AND_PILOT_INTERACTION.md) — boarding, physical controls, optional motion-capture harness, interface failures, and shared desktop/VR actions.
- [Rendering Performance and GPU Budgets](RENDERING_PERFORMANCE_AND_GPU_BUDGETS.md) — mesh joining versus instancing, GPU budgets, VR frame deadlines, and reproducible benchmark requirements.
- [Humanity's Long History and the Discoverable Past](HUMANITYS_LONG_HISTORY_AND_DISCOVERABLE_PAST.md) — speculative history from Earth to the galactic Empire, science-fiction guardrails, and deep discoverable archives.
- [Open Questions](OPEN_QUESTIONS.md) — unresolved design questions requiring elicitation.

## Spatiotemporal and runtime contracts

These documents define the world-model rules and implementation gaps. A design contract is not evidence that the runtime feature is already implemented.

- [Spatiotemporal World Model and Observer-Relative Presentation](SPATIOTEMPORAL_WORLD_MODEL_AND_OBSERVATION.md) — stable identity, explicit-time motion, observation-only queries, authoritative history, and rendering as a projection.
- [Coordinate Frames and Transform Contracts](COORDINATE_FRAME_CONTRACT.md) — required frame metadata, transform conventions, logical-time validity, and tests; canonical world-wide axes and handedness remain open.
- [Event History, Checkpoints, and Deterministic Replay](EVENT_HISTORY_AND_REPLAY_CONTRACT.md) — event identity versus ordering, idempotent commits, checkpoint boundaries, and replay acceptance tests.
- [Machine-readable galaxy generation contract](../data/galaxy-generation-contract.json) — executable-contract metadata, including implemented spatial address V1/hash vectors and explicitly unimplemented transform/history requirements.

## Visual atlas

- [Visual Asset Catalogue](images/README.md)
- [Embodied mech control and engineering](images/rawwar-embodied-mech-control.svg) — shared action semantics, pilot control loop, physical engineering procedures, and performance limits.
- [Milky Way reference layers](images/rawwar-milky-way-reference-layers.svg) — observed data, inferred galactic structure, generated populations, and player-facing representation kept separate.
- [Advisor agency and consequences](images/rawwar-advisor-agency.svg) — how an advisor's agency, earned experience, warnings, physical outcomes, and persistent history connect.
- [Work kanban lifecycle](images/rawwar-work-kanban.svg) — the board as a projection of real work, with evidence-based transitions and blockers.
- [Science floor construction](images/rawwar-science-floor-construction.svg) — the physical prerequisites between proposing a lab and operating it.
- [Ship damage and salvage](images/rawwar-ship-damage-and-salvage.svg) — part hierarchy, directional effects, causal system failures, and surviving wreck components.
- [Faction ship design languages](images/rawwar-faction-ship-languages.svg) — candidate ship silhouettes showing how doctrine changes engineering, not just paint.
- [Authored campaign and civilization lineage](images/rawwar-authored-campaign-lineage.svg) — curated single-player and generated multiplayer creation paths, converging on one world contract and a branching human lineage graph.
- [Campaign history discoveries](../data/campaign-history-discoveries.json) — a data contract for physical evidence, archive access, contradictory records, and layered discovery.
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

- [Workshop Composition Boundary](WORKSHOP_COMPOSITION_BOUNDARY.md) — ownership rules for reusable Workshop MicroBundles versus raWWar domain content.
