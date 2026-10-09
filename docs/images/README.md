# raWWar Visual Asset Catalogue

This catalogue is the index for the repository's explanatory artwork. Images should help a reader **see the system being described**: relationships, scale, physical dependencies, procedures, decisions, and consequences.

These SVGs are authored, lightweight diagrams and concept illustrations. Unless an image explicitly says otherwise, it is **not** a capture of a completed runtime, a finalized art asset, or a construction-ready engineering drawing.

## Visual entry points

| Asset | What it helps explain | Best home |
|---|---|---|
| [rawwar-hero.svg](rawwar-hero.svg) | The first impression: a war that remains alive beyond the player's immediate view | Repository landing page |
| [rawwar-galactic-war.svg](rawwar-galactic-war.svg) | The galaxy as one persistent war rather than disconnected maps | Vision, world model |
| [rawwar-living-world.svg](rawwar-living-world.svg) | Multiple kinds of activity continuing in one world | Vision, simulation |
| [rawwar-base-cutaway.svg](rawwar-base-cutaway.svg) | Facilities as interdependent places staffed by real people | Engineering data scape, facilities |
| [rawwar-soldier-progression.svg](rawwar-soldier-progression.svg) | A person's career, qualifications, equipment, and history | Soldier dissertation, careers |
| [rawwar-empress-order.svg](rawwar-empress-order.svg) | The opening political demand and the player's consequential choice | Narrative, campaign |
| [rawwar-experience-architecture.svg](rawwar-experience-architecture.svg) | What raWWar owns versus the reusable machinery provided by the Workshop | Experience architecture |
| [rawwar-event-horizons.svg](rawwar-event-horizons.svg) | How observation detail can vary with distance without stopping world history | Renderer integration, simulation scale |
| [rawwar-gesture-sequence.svg](rawwar-gesture-sequence.svg) | Intent becoming movement, procedure, and individual physical execution | Gestures design |
| [rawwar-faction-spectrum.svg](rawwar-faction-spectrum.svg) | The thirteen-faction political space | Faction design |
| [rawwar-advisor-agency.svg](rawwar-advisor-agency.svg) | Advisors as agents who learn, warn, act, suffer outcomes, and leave history | Advisors and command staff |
| [rawwar-work-kanban.svg](rawwar-work-kanban.svg) | Work-board states and evidence-based transitions grounded in world state | Work orders, logistics, production |
| [rawwar-science-floor-construction.svg](rawwar-science-floor-construction.svg) | Underground science-floor construction from site investigation through support, utilities, lab installation, commissioning, and acceptance | Engineering data scape, advisors, facilities |
| [rawwar-ship-damage-and-salvage.svg](rawwar-ship-damage-and-salvage.svg) | Ship hierarchy, six-face impact resolution, causal secondary effects, and surviving wreck components | Starship sizing, systems design, engineering data scape |
| [rawwar-faction-ship-languages.svg](rawwar-faction-ship-languages.svg) | Six candidate human ship-design archetypes, each with a visible silhouette and engineering tradeoffs | Faction ship design languages, art direction |
| [rawwar-milky-way-reference-layers.svg](rawwar-milky-way-reference-layers.svg) | The Solar System's place in the Milky Way and the separation of observed, inferred, generated, and presentation layers | Galaxy generation, spatial observation, navigation HUD |
| [rawwar-authored-campaign-lineage.svg](rawwar-authored-campaign-lineage.svg) | Authored single-player campaign versus generated multiplayer world, shared validation, persistent history, and branching human civilization ancestry | Campaign authoring, galaxy generation |

## Visual language

- **Deep blue-black backgrounds** establish a consistent technical canvas without implying a particular graphics engine.
- **Cool pale text** carries the main explanation; muted blue-grey carries supporting detail.
- **Warm brass/gold** marks important decisions, boundaries, or conceptual emphasis—not universal success.
- **Green** can indicate functioning, learning, or accepted work when the diagram's legend says so.
- **Amber and muted red** should indicate caution, blockage, hazard, or loss only when the surrounding context supports that meaning.
- **Arrows express a stated relationship**, not necessarily a simple chronological sequence. Label them when ambiguity is possible.
- **Every diagram should remain understandable without color alone.** Use headings, labels, and spatial organization.
- Prefer SVG for diagrams: it remains crisp at different sizes, can be reviewed as text, and avoids adding a heavy asset pipeline.

## Design-authority labels

When an image depicts unsettled design, its caption or nearby text should say so. Use the same authority language as the documents:

- **Canon** — established by the creator.
- **Candidate** — a proposal awaiting confirmation.
- **Experiment** — a behavior or representation to test.
- **Open question / Illumination Needed** — requires further design input.

Do not let polished artwork accidentally promote a candidate into canon. A visually persuasive illustration is still subordinate to the written design authority.

## Where to add the next visuals

1. **Physical construction:** terrain section, excavation and support, foundation, utility corridors, structure, commissioning and acceptance.
2. **Capability and qualification graph:** research eligibility → manufactured equipment → installed configuration → trained crew → tested readiness.
3. **Vehicle cut sheet:** one side for the operator's job and watch points; one side for the commander's cost, dependencies, and capability.
4. **World time and observation:** one authoritative world state seen at different scales and times, with distance reducing detail—not halting history.
5. **Persistent consequences:** warning → decision → physical event → casualty/damage → recovery work → historical record.
6. **Agent and work ownership:** agents performing real tasks, blocked by actual dependencies, with the board only projecting work state.
7. **Ship construction and recovery:** a hierarchy of zones, parts and dependencies; directional impact lookup; causal secondary events; surviving components and provenance-backed recovery.

These are documentation priorities, not claims that the corresponding runtime systems are already complete.

