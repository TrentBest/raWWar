# raWWar — Universe Foundation and Campaign Zero

**Status:** Canonical boundary and campaign-authoring foundation. Creator-established facts are separated from candidate structure and unresolved canon.
**Owner:** raWWar Experience design.
**Purpose:** Give the Autonomous Development pipeline one coherent universe to build against, without allowing agents to invent the setting's defining truths.

> **The library explains warfare. The universe gives it somewhere to happen.**

This document connects the Game Design Bible, master GDD, authored-galaxy contract, historical framework, and machine-readable world data. It does not replace those authorities or claim that the universe is already implemented.

## 1. The actual objective

raWWar is one continuous, first-person war Experience set in a galaxy-scale human future. The player inhabits a person inside a military and political world that continues to exist outside the player's immediate view. Battles, missions, bases, ships, cities, worlds, factions, and campaigns are persistent parts of that world—not disconnected match maps.

The immediate production objective is not to simulate every world. It is to establish a causally coherent universe and a bounded first campaign whose content can be validated, rendered, simulated, and replayed by the Autonomous Development pipeline.

The pipeline must be able to answer, with evidence:

- What exists in the universe?
- Where and when does it exist?
- Who has authority, ownership, allegiance, or a claim?
- What happened, what caused it, and what changed?
- What does each actor know, believe, claim, or misunderstand?
- Which resources, people, infrastructure, and capabilities constrain action?
- What can the player observe or affect from their current role?
- Can the same authored starting world be reconstructed deterministically?

## 2. Creator-established universe facts

The following are treated as established design direction, not suggestions for an agent to replace:

1. **One galaxy.** The Experience takes place in a galaxy, not a collection of unrelated arenas.
2. **A long-lived Empire.** An Empire is in power in the campaign era and has persisted for multiple millennia at minimum. Its exact founding date, continuity, constitutional form, and succession history remain open.
3. **Millions of worlds.** The setting has millions of worlds from which the Empire, its factions, and the wider history can draw. Do not imply that every world is inhabited, equally developed, equally governed, or even fully known.
4. **An Empress.** An Empress is a real political actor in the campaign-era order. Her powers, legitimacy, succession, institutions, and relationship to the player are not yet fully defined.
5. **A continuing war.** The campaign occurs inside an active, ongoing galactic conflict. The war has consequences beyond any one operation.
6. **Warp capability.** Warp drives exist. Preparing and initializing a jump takes days. The underlying physics, travel duration, limitations, costs, and strategic exceptions still require definition.
7. **Factional politics.** Major factions exist and have their own beliefs, interests, institutions, and relationships with Imperial power. Their identities and the exact balance of the conflict remain to be authored.
8. **Two foundational campaign perspectives.** The Commander campaign begins from high authority; the Enlisted campaign begins at the bottom of the military hierarchy. Both inhabit the same living war rather than separate simulations.
9. **First-person presence.** The game is experienced from a person physically present in the world. Desktop and VR are presentation paths, not different world truths.
10. **The world remembers.** Ownership, resources, personnel, organizations, history, relationships, and consequences persist beyond a local mission.

These statements are the minimum canon envelope for world generation. A generated detail may elaborate them; it may not silently contradict them.

## 3. Existing campaign opening: Imperial Summons

The current Commander opening already provides the strongest authored entry point. Preserve its established sequence:

- The raWWar moniker appears; lightning and overexposure reveal a storm.
- The player stands high above a large, active square, looking through a massive window.
- An imposing Imperial palace dominates the opposite side of the square.
- Weather, people, and activity continue outside while the player observes.
- Airtight doors open; two Imperial soldiers approach and announce that the Empress wants the Commander immediately.
- The player recognizes the faction headquarters and can inspect faction offices, dogma, literature, recruiting material, and physical evidence.
- The summons is not optional indefinitely: refusal leads to an explicit execution consequence.

This is not merely an opening cutscene. It is the first small window into a much larger political and military system. The square needs ordinary civilian life; the palace needs an institution; the faction headquarters needs an identity and internal structure; the guards need an authority chain; the summons needs a political reason; and the player's office needs a history.

**Do not invent the missing answers.** The capital's name, the player's faction name, the Empress's name, the exact political crisis, and the reason for the summons remain illumination items until the creator establishes them.

## 4. Universe model: interlocking dimensions

The universe is not just a tree of map locations. It is a set of connected, time-dependent relationships.

### 4.1 Physical and spatial reality

- Galaxy, regions, stellar populations, star systems, stars, planets, moons, habitats, stations, ships, surface locations, and built environments.
- Every spatial entity has a stable semantic identity independent of rendering, load order, camera position, or discovery order.
- Real astronomical references, derived scientific models, generated systems, and fictional additions remain distinguishable.
- Travel, motion, environment, communications, and infrastructure use explicit models and declared validity limits.
- The 10×10×10 spatial address contract is an address-generation scheme, not a claim that the physical galaxy is a uniform cubic grid.

### 4.2 Historical reality

- Earth is humanity's origin reference in the current history framework; the future from that origin is branching, not predetermined.
- The Imperial era is the latest layer of a much longer history.
- Settlement, migration, trade, war, technological change, succession, reform, secession, reunification, and cultural exchange have causes and consequences.
- History is not a single clean official timeline. Events, surviving records, interpretations, public narratives, and propaganda are separate data.
- The Empire's multiple-millennia duration must be reconciled with the detailed chronology before dates are pinned.

### 4.3 Political reality

Represent institutions and authority explicitly: Empire, imperial offices, Empress, constituent or subordinate polities, factions, local governments, military commands, commercial bodies, and civilian institutions. Do not flatten a population into its government or a faction into one person's motives.

A polity may control a location, claim it, administer it, occupy it, defend it, or merely describe it as belonging to it. These are different relationships. Authority can be contested, delegated, overlapping, or absent.

### 4.4 Material and economic reality

A world has finite people, food, water, energy, materials, industrial capacity, transport, maintenance, and time. Military forces consume the same material world that sustains civilian life. Resource ownership, extraction, processing, manufacturing, transport, stockpiles, repair, and waste are connected.

A resource record must distinguish physical existence from accessible reserves, extracted quantity, refined output, usable inventory, and delivered supply. A technology unlock does not create physical equipment; research, tooling, production, qualification, distribution, and maintenance remain separate.

### 4.5 Military reality

Armed forces are institutions made of people, qualifications, command relationships, formations, equipment, facilities, doctrine, logistics, and political authority. Capacity is not readiness. A vehicle, ship, factory, base, or formation is not operational merely because its record exists.

The soldier remains the star. Large-scale abstraction must preserve individual identity and consequential history while varying simulation detail according to relevance.

### 4.6 Knowledge and memory

Each actor has a perspective. The authoritative world state, an institution's records, a faction's official narrative, a soldier's beliefs, and the player's current knowledge must not be collapsed into one omniscient description.

Records need provenance, time, integrity, access, custody, corroboration, contradiction, and interpretation. Discovery can change political leverage, trust, legal claims, research, and future events.

## 5. The first universe slice the pipeline must build

The pipeline's first world target is the **Imperial Summons vertical slice**, not a randomly generated galaxy of millions of fully authored locations.

It should be authored as a pinned campaign seed with explicitly unresolved fields. It must include:

1. A campaign identity, version, seed, historical epoch reference, generator version, and simulation version.
2. An Empire entity and Empress office/person reference, with only creator-established attributes asserted as canon.
3. A player-command entity and its faction reference; the faction's name and political identity remain unresolved until authored.
4. A capital-world/location reference, the high-rise observation position, the square, the palace, and the faction headquarters as distinct spatial entities.
5. The two Imperial guards, their duty/authority relationship, and the summons event.
6. Civilian and institutional activity sufficient to make the square visibly alive, without pretending every passer-by requires maximum simulation fidelity.
7. A local timeline that supports the storm, observation, door opening, approach, summons, response window, and possible execution outcome.
8. Separate player knowledge, faction knowledge, Imperial knowledge, public claims, and authoritative facts.
9. Resource, staffing, access, and infrastructure dependencies for the locations that are actually shown.
10. A set of deterministic acceptance tests proving stable identities, valid references, causal ordering, and repeatable reconstruction.

This slice is a campaign entry and world-model test. It is not yet the full war, and it must not fabricate the war's cause or a complete faction roster to make the scene appear finished.

## 6. The pipeline's world-building sequence

Build outward from a coherent authored core. Do not generate millions of disconnected lore records.

1. **Canon register:** capture creator-established facts and unresolved decisions with provenance and status.
2. **Historical spine:** establish the causal eras from Earth-origin reference through interstellar settlement, galactic integration, imperial consolidation, fractures, and the campaign era. Exact dates remain open until authored.
3. **Political atlas:** define the Empire's institutions, major factions, constituent polities, local authorities, claims, alliances, dependencies, and opposition.
4. **Galactic atlas:** pin the Solar System and evidence-backed astronomical reference; define campaign regions and the location of authored systems; generate the wider population deterministically.
5. **World atlas:** describe selected planets, habitats, stations, cities, bases, and strategic infrastructure at useful levels of detail.
6. **Material economy:** connect worlds and facilities to food, water, energy, minerals, industrial processes, labor, transport, and military demand.
7. **Military atlas:** define organizations, personnel, qualifications, formations, fleets, facilities, equipment, doctrine, readiness, and logistics.
8. **Conflict chronology:** define causes, phases, fronts, actors, objectives, operations, turning points, ceasefires, and consequences without making every conflict a battle.
9. **Campaign entry:** instantiate the Imperial Summons and then the first playable operation as consequences of the larger world.
10. **Continuous simulation:** preserve events, ownership, resources, personnel, knowledge, and relationships as the campaign advances.

Each stage must emit structured data, validation results, and human-readable visualizations. An agent must be able to identify which missing fact blocks progress instead of filling it with confident invention.

## 7. Canon, candidate, and evidence rules

Use the GDD's four design states consistently:

- **Canon:** creator-established setting truth.
- **Candidate:** a proposed world detail awaiting confirmation.
- **Experiment:** a deliberate test whose result may inform design but does not automatically become canon.
- **Illumination Needed:** a question whose answer belongs to the creator.

For historical and scientific content, also distinguish observed evidence, derived interpretation, disputed claims, generated assumptions, and fictional rules. A generated object can be deterministic without being canon. Determinism is reproducibility, not truth.

When a required value is unknown:
- preserve a typed unresolved reference or explicit null with a reason;
- report the dependency as blocked or awaiting illumination;
- optionally offer clearly labelled alternatives;
- never silently promote a candidate into canon;
- never invent dates, names, factions, motives, or history merely to satisfy a schema.

## 8. Important scope conflicts to resolve before galaxy-scale population

The current supporting material contains useful candidate contracts, but they are not all settled canon.

- The lineage contract currently says all factions are human. Other raWWar material explores alien civilizations as a speculative domain. The actual setting boundary—human-only, alien polities present, or alien life in a different role—needs an explicit decision.
- The historical framework intentionally leaves most future dates open. The multi-millennia Imperial duration now constrains the timeline, but does not supply exact dates or a continuous succession list.
- The galaxy-generation address grid is a stable addressing mechanism, not the authoritative astronomical map. The Milky Way reference layer must remain separate.
- The warp drive is canon as a capability; its physical model and travel-time rules are not.
- The existence of major factions and a continuing war is canon; the faction identities, war origin, belligerents, objectives, and political settlement are not yet sufficiently illuminated.

These are not reasons to stop. They are explicit authoring gates.

## 9. First acceptance gates

The first autonomous universe-building milestone is complete only when:

- the canonical facts above are represented in structured data and linked to their authority;
- every unresolved canon item remains visibly unresolved;
- the Imperial Summons campaign seed has stable identities and a valid causal event sequence;
- all referenced entities resolve or carry a declared unresolved status;
- historical, political, spatial, and resource relationships pass consistency checks;
- generation is deterministic for the declared seed and versions;
- world-state queries do not mutate history or depend on render order;
- the same authored world can be reconstructed and inspected through a human-readable atlas;
- the first-person opening can be described as a set of physical places, people, activities, and consequences rather than a disconnected screenplay.

Passing these gates proves a coherent authored data slice. It does not prove a fully playable hosted game or a fully simulated galaxy.

## 10. Authoritative companions

- [Game Design Bible](GAME_DESIGN_BIBLE.md) — creator-established game identity and experience rules.
- [Game Design Document](GAME_DESIGN_DOCUMENT.md) — master player experience and current illumination map.
- [Authored Galaxies, Campaign Manifests, and Civilization Lineage](AUTHORED_GALAXIES_CAMPAIGNS_AND_LINEAGE.md) — authored versus generated worlds and lineage.
- [Human Galactic History data](../data/human-galactic-history.json) — candidate historical framework.
- [Campaign Galaxy Manifest contract](../data/campaign-galaxy-manifests.json) — authoring controls and validation gates.
- [Civilization Lineage contract](../data/civilization-lineages.json) — lineage graph and inherited traces.
- [Galaxy Generation contract](../data/galaxy-generation-contract.json) — stable addressing and deterministic generation.
- [Milky Way Reference Model](MAPPING_THE_MILKY_WAY.md) — evidence-backed galactic reference and honest procedural expansion.
- [Spatiotemporal World Model](SPATIOTEMPORAL_WORLD_MODEL_AND_OBSERVATION.md) — authoritative state, explicit time, history, and observer-relative presentation.
- [Open Questions](OPEN_QUESTIONS.md) — decisions that still require creator illumination.

## 11. Immediate next work

Do not start another general sourcebook series. Build the structured Imperial Summons seed, validate it against the existing world contracts, and produce the first visual atlas of the setting: galaxy context, Imperial political relationships, capital-world context, and the opening location. Then use the results to identify the smallest set of creator decisions that truly blocks the next campaign layer.
