# raWWar — Authored Galaxies, Campaign Manifests, and Civilization Lineage

> **A campaign is not merely a seed. It is a deliberate history, placed into a world that can continue to live.**

Status: Candidate architecture and authoring contract. The creator retains authority over what becomes canon. Illustrative examples do not establish named faction histories.

## 1. Two creation modes, one world model

raWWar needs two different ways to establish the initial galaxy:

| Concern | Single-player campaign | Multiplayer world |
|---|---|---|
| Primary author | Creator, designer, or campaign author | Versioned generator and scenario rules |
| Galaxy structure | Curated and deliberately composed | Generated from a seed and constraints |
| Important locations | May be pinned to exact identities and addresses | Generated, then validated for mode requirements |
| Historical events | Authored and ordered where story requires | Generated within chronology and causality rules |
| Faction ancestry | May be a core narrative fact | Generated as a coherent lineage graph |
| Variation | Explicitly permitted by the manifest | Expected, deterministic for seed and version |
| Validation | Protect canon, story dependencies, physical feasibility | Protect reproducibility, coherence, reachability, and selected fairness model |
| After campaign start | Simulation history becomes authoritative | Simulation history becomes authoritative |

This is not two separate simulation engines. Both modes use the same stable entity identities, spatial addressing, generation rules, lineage model, physics approximations, event ordering, persistence, and presentation boundaries.

A multiplayer world should not need a lesser universe. A single-player campaign should not be forced to accept whatever a random generator happens to produce.

## 2. The authoring controls

A campaign author needs controls more expressive than a seed field.

- **Pin** — this fact must remain fixed: a particular homeworld, colony, historical event, or starting ownership.
- **Constrain** — generated content may vary, but must meet declared rules: travel distance, resource range, settlement count, or chronology.
- **Seed** — allow reproducible variation inside the permitted space.
- **Override** — replace a generated candidate with an authored value and record the reason.
- **Validate** — check references, chronology, lineage, physical feasibility, logistics, and scenario requirements before accepting the result.
- **Bake** — cache expensive derived content without making the cache the source of truth.
- **Distribute** — package the manifest, required content references, version requirements, and permitted variation so another installation can reconstruct the same campaign.
- **Branch history** — where explicitly supported, create an alternate campaign history without silently rewriting the original campaign's canon.

These controls should be exposed through authoring data first. A future graphical authoring tool can present them as maps, timelines, lineage trees, and constraint panels; it should not invent a second set of rules.

## 3. Civilization is a lineage graph, not a list of unrelated factions

All raWWar factions are human. Their differences emerge from human history and conditions: the worlds they settled, the materials and energy available, the institutions they built, the threats they faced, the trade networks they depended on, and the choices made by generations of people.

A possible history:

```text
Older home civilization
        |
        +-- core worlds retain the original institutions
        |
        +-- frontier colony network
                  |
                  +-- supply chain weakens
                  +-- local industry becomes self-sufficient
                  +-- political succession crisis
                  +-- independence or secession
                            |
                            +-- successor faction
                                  |
                                  +-- inherits selected standards
                                  +-- adapts ship design to local industry
                                  +-- develops a distinct doctrine
                                  +-- contests the parent's account of history
```

This is an illustrative pattern, not predetermined raWWar canon. A colony does not become a new faction simply because it is far away. Its divergence needs causes and consequences.

Lineage should be a **graph**, not a single parent-faction pointer. A faction may inherit industrial standards from one predecessor, political institutions from another, and cultural traditions from a third. A polity may absorb a rival, split into successors, later reunify, or claim a heritage that other factions dispute.

The model must distinguish what actually happened in authoritative history; what records survive; what each faction believes; what each faction publicly claims; and what remains unknown to the player. Propaganda and disputed history become gameplay because facts, records, and claims are represented separately.

## 4. Heritage should leave physical traces

Ancestry matters when it affects the living world, not just a lore page.

| Inherited element | Possible persistent trace |
|---|---|
| Industrial standards | Compatible tools, parts, interfaces, and factory equipment |
| Shipbuilding tradition | Shared frame dimensions, connectors, construction sequence, and repair practices |
| Military doctrine | Training patterns, logistics, squad organization, and preferred mission profiles |
| Governance | Succession rules, administrative boundaries, legal claims, and appointment structures |
| Migration history | Settlement names, demographic connections, old routes, and family ties |
| Shared war | Veterans, memorials, captured equipment, unresolved claims, and distrust |
| Legacy infrastructure | Useful capacity coupled with obsolete systems, maintenance debt, and specialized spares |

Inheritance is selective. A successor can preserve an old connector standard while rejecting the old government's doctrine. A former colony may manufacture parent-era equipment but lack access to the parent civilization's newest components. Cultural similarity does not guarantee an alliance; a shared ancestor can make a rivalry more personal.

Technology does not teleport between factions. Designs, trained people, tooling, physical assets, supply chains, and operational knowledge must move through plausible channels.

## 5. Campaign manifests

A campaign manifest identifies campaign identity and version; world seed, galaxy generator version, and simulation model version; historical epoch and chronology; authored entities and lineage records; pinned events and starting conditions; generation constraints and permitted variation; player/faction knowledge at campaign entry; scenario objectives and required dependencies; validation profile; and save compatibility policy.

A manifest composes world content. It is not the world itself, and it is not a replacement for the simulation.

For distribution, a campaign references stable content identities and declares required versions. Content that must be identical should be pinned; optional variation should be explicit. A consumer should be able to tell whether a campaign is reproducible, compatible with a newer generator, or requires a declared migration.

## 6. Generation and validation pipeline

```text
Campaign intent + lineage/history
              |
              v
       Authored manifest
              |
       +------+------+
       |             |
   Pin facts     Set constraints
       |             |
       +------+------+
              v
    Seeded candidate generation
              |
              v
  Chronology + lineage + physics
  + travel + resources + scenario
              |
        +-----+-----+
        |           |
      valid       invalid
        |           |
        v           v
  accepted world   explain failures;
        |          repair or re-author
        v
  save initial state and versions
        |
        v
  simulation -> consequences -> persistent history
```

Validation should explain failures rather than silently “fix” a campaign into a different story. An authored colony that cannot be reached by available technology is either a deliberate plot fact that needs explanation, or a contradiction to resolve. The validator should report which constraint failed and what it affects.

Core checks include:
1. Unique stable IDs and resolvable references.
2. Chronological validity of founding, secession, succession, and reunification.
3. Plausible colony origin, travel route, time, population support, and industrial supply.
4. A faction does not exist before its founding or predecessor unless the timeline explicitly defines that predecessor.
5. Pinned story facts survive all allowed variation.
6. Required objectives remain reachable under the scenario's stated rules.
7. Multiplayer starts satisfy the selected mode's fairness contract without pretending every strategic position is identical.
8. The seed and declared versions reproduce initial content.
9. Saves retain authoritative consequences and the versions needed to interpret initial conditions.

## 7. What multiplayer generation may share—and what it must not assume

Automatic generation may create star systems, settlement distributions, lineage graphs, historical events, resource placement, faction starting positions, and initial political relationships. It uses the same rules and data contracts as authored campaigns.

The generator must not assume every faction has a completely separate origin, all factions need equal resources at every location, every historical dispute is known to all players, a colony always becomes independent, every branch of a lineage remains alive, or every campaign contains the same factions and events.

Fairness is a selected game-mode contract. A competitive start may constrain travel time or early resource access; a cooperative or asymmetric scenario may deliberately permit major differences. Validate the selected contract rather than imposing a universal balance score.

## 8. Persistence and versioning

Before play, immutable initial conditions can be reconstructed from the manifest, seed, stable addresses, and versioned generator. Once play changes ownership, destroys a settlement, alters a political relationship, or creates a consequential event, that history must be persisted.

A save needs the campaign manifest identity and version; generator and simulation versions; authored overrides and accepted variations; authoritative event history and checkpoints; discoveries and faction-specific knowledge; lineage facts that changed through play; and compatibility or migration decisions.

Updating the generator must not silently rewrite an existing campaign's ancestry or geography. Migration should be explicit and testable.

## 9. Relationship to existing galaxy generation

This architecture extends the existing spatial generation contract: stable semantic addresses define identity; versioned, event-keyed deterministic variation makes generated content reproducible; recursive spatial refinement expands regions on demand; physics and domain constraints determine coherence; rendering remains a projection of authoritative world state; immutable initial properties may be regenerated, while the history the galaxy has lived through must persist.

The reserved galaxy anchor and larger cosmic context remain part of the existing world architecture. Campaign authoring controls what occupies the playable galaxy and how its history is curated; it does not change the spatial identity contract.

## 10. Status and open decisions

**Established direction:** single-player campaigns need deliberate authoring and distribution; multiplayer can use automatic generation; both use one shared world model; faction ancestry can connect present-day powers through colonization, succession, divergence, and inherited institutions.

**Candidate implementation contracts:** the campaign-manifest and civilization-lineage data files.

**Still open:** the actual named faction lineage, the historical epoch, which events are canon versus campaign-specific, and how much controlled variation each campaign permits. These should be decided deliberately rather than inferred from placeholder data.

> **The galaxy gives history a place to happen. The lineage explains how its people arrived. The campaign decides which part of that history we are here to experience.**
