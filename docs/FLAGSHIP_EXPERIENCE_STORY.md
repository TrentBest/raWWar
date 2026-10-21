# raWWar — The Flagship Experience Story

**Status:** Visual narrative and design intent; not a claim of a completed playable build.  
**Owner:** raWWar Experience design.  
**Audience:** Newcomers, prospective collaborators, technical reviewers, and investors evaluating the vision.  
**Evidence rule:** Each visual communicates only the scope stated in its caption. Conceptual art, procedural experiments, executable examples, and end-to-end runtime evidence are different things.

## The promise in one sentence

**Make being a person inside a war the game.**

raWWar is intended to be the recognizable flagship Experience shipped with the AnyApp system: a world that makes the Workshop's modular architecture meaningful to a human being. The platform is not the protagonist. The people inside the war are.

This page is a guided visual argument using existing, version-controlled raWWar assets. It deliberately links the human promise to the systems needed to support it, while making the current evidence boundary visible.

## 1. Enter a world that does not wait for you

![A living raWWar installation](images/rawwar-living-world.svg)

*Design illustration — depicts the intended world model, not a captured running simulation.*

A launch crew should be able to work while another squad trains, logistics moves material, researchers run experiments, command reviews a situation, and people spend their downtime together. The player should see signs of a world with its own work, procedures, interruptions, and consequences—not a stage that springs to life only when the player arrives.

![One continuous war across a galaxy](images/rawwar-galactic-war.svg)

*Conceptual world model — expresses the intended continuity and scale of the war; it does not demonstrate a simulated galaxy running today.*

The world is a galaxy spanning many star systems, organizations, units, and individual people. Operations are connected by distance, logistics, information, reinforcement, and history. This is not meant to be a sequence of disposable maps.

The promise is not “simulate everything at maximum fidelity.” The promise is that the world keeps its history, while the amount of detail can be appropriate to what is being observed and what matters.

That is also the engineering thesis behind the spectacle: the game should make scale feel like a populated, consequential world, while the system decides which state needs immediate detail, which work can be batched, and which state can be retained compactly or reconstructed when needed. The target is not a leaderboard-sized entity count. It is **more meaningful world per unit of computation**—with reproducible workloads, correctness checks, and measured resource costs before we claim a scale milestone. The broader principles are recorded in [Vision and Pillars, Pillar 11](VISION_AND_PILLARS.md#pillar-11--data-scale-is-part-of-the-spectacle).

## 2. The soldier is the star

![A soldier's career and progression](images/rawwar-soldier-progression.svg)

*Conceptual progression diagram — communicates intended identity, qualification, responsibility, and history; it is not a screenshot of a completed career system.*

A soldier is more than a model with a health bar. Their identity, training, qualifications, relationships, equipment, responsibilities, decisions, and fate should accumulate meaning over time.

Combat matters, but it is not the only way to participate. Repair, construction, research, logistics, piloting, intelligence, command, and recreation should all be meaningful ways to inhabit the same war. The player should be able to understand their place in the organization and feel the consequences of their choices.

![The Empress's first uncompromising order](images/rawwar-empress-order.svg)

*Conceptual narrative illustration — expresses the intended consequential choice. Exact opening-scene sequencing remains an open design question.*

The war should put pressure on the person, not merely ask for a high score. An order can have military, political, personal, and historical consequences. The player's agency at consequential moments is part of the design, not an optional cinematic flourish.

## 3. Make the world legible as a system

![A military base as an interdependent system](images/rawwar-base-cutaway.svg)

*Explanatory illustration — communicates intended relationships between facilities, people, and work; it is not a construction-ready drawing or a runtime capture.*

A living world needs understandable structure. Facilities depend on people, utilities, materials, procedures, and readiness. A visible room should have a reason to exist; work should be connected to prerequisites and outcomes. A diagram should help the reader understand those relationships rather than obscure them behind technical vocabulary.

![raWWar's boundary with the Workshop](images/rawwar-experience-architecture.svg)

*Architecture illustration — describes the intended responsibility boundary, not proof of complete end-to-end loading.*

The boundary is deliberate:

- **raWWar** owns the world, people, rules, activities, and consequences that make this Experience distinct.
- **MicroBundle contracts** describe independently composable capabilities.
- **FSM_COS** composes requested capabilities and hands off a RuntimeAssembly; it is not the game, host, renderer, or application loop.
- **AnyApp and other hosts** execute and manifest the resulting Experience.
- **Renderer and presentation layers** show the world without becoming the source of its domain truth.

The manifest describes the requested composition; the transitive dependency closure must also be resolvable. The checked-in authoring/runtime manifests and AnyApp publication manifest are not yet proven to form a complete launch path. The local composition check is scoped to its test adapter, not an end-to-end hosted Experience.

## 4. Let the visuals point to real experiments

![Visualizer geometry manifested through GUI.SVG](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/samples/PrimitiveGallery/generated/07-gui-svg-connected-points.svg)

*Generated integration artifact from the Workshop Visualize development branch — the C# proof constructs geometry, materials, draw commands, and a VisualizerFrame; the GUI.SVG backend manifests the frame as SVG with explicit SVG animation nodes. This is a real 2D manifestation path, not a production perspective/depth-tested renderer, game simulation, or raWWar runtime.*

**Inspect the source, not just the picture:** [generated SVG](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/samples/PrimitiveGallery/generated/07-gui-svg-connected-points.svg) · [C# integration proof](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/integrations/GuiSvg/Proof/Program.cs) · [Visualize Primitive Gallery recipe](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/samples/PrimitiveGallery/README.md).

This matters for raWWar because it demonstrates a genuine Workshop handoff: semantic geometry and draw commands enter a Visualizer frame, then a reusable GUI.SVG adapter produces an inspectable manifestation. It is a concrete step toward living visual documentation—not proof that a raWWar soldier, world simulation, or AnyApp-hosted scene is already running.

![Procedural soldier exoskeleton — three-quarter view](images/soldier-field/02-three-quarter.svg)

*Procedural blockout illustration — a view from the repository's 38-part soldier model experiment. It is evidence of that model experiment only, not of a complete soldier simulation, animation system, or playable game.*

[Open the Soldier Field experiment documentation](../samples/SoldierField/WebGpuRunner/README.md) for the six-view model and optional WebGPU lab. That lab is a separate experiment; it must not be presented as proof that the Workshop Visualize contracts are complete.

For a runnable, bounded code example, see [KeplerOrbit](../examples/KeplerOrbit/Program.cs). It demonstrates explicit-time orbit queries, repeatability, periodicity, and invalid-boundary behavior. It does **not** demonstrate a playable war or AnyApp host composition.

These different forms of evidence complement one another:

| Evidence | What it can establish | What it cannot establish by itself |
|---|---|---|
| Concept SVG | Intended world, story, or relationship | Running behavior |
| Explanatory architecture SVG | Intended boundaries and composition | Successful dependency resolution or launch |
| Visualize → GUI.SVG generated artifact | A source-backed 2D geometry/resource/frame-to-SVG handoff with explicit SVG animation | Production 3D rendering, raWWar simulation, or AnyApp launch |
| Procedural soldier experiment | The specific model/view experiment that exists | A complete soldier, animation, or game loop |
| Executable example and tests | The behavior they actually execute and assert | Uncovered features or end-to-end host integration |
| End-to-end hosted slice | The specific integrated path exercised | Every planned feature or final production readiness |

## 5. The next proof should be a slice, not another promise

The strongest next milestone is a small, observable first-person slice in which a person occupies a station or rig, a meaningful intent changes the world, and the result can be inspected through the actual AnyApp composition and presentation path.

That slice should make the boundaries visible in evidence: what the Experience defines, what capabilities are composed, what the host loads, what the renderer presents, and which tests verify the behavior. A diagram can explain the path before it exists; a successful integration run must prove it.

Until that evidence exists, the repository states the limitation plainly: the complete playable slice and end-to-end AnyApp host launch have not yet been delivered.

## Continue exploring

- [Game Design Document](GAME_DESIGN_DOCUMENT.md) — comprehensive description of the intended player experience.
- [Game Design Bible](GAME_DESIGN_BIBLE.md) — foundational design authority.
- [Vision and Pillars](VISION_AND_PILLARS.md) — the experience's north star.
- [Experience Architecture](EXPERIENCE_ARCHITECTURE.md) — domain and platform boundaries.
- [Visual asset catalogue](images/README.md) — the broader visual language and available assets.
- [Documentation conformance audit](DOCUMENTATION_CONFORMANCE.md) — current standardization work and remaining audit gaps.

---

**The ambition:** make the war feel inhabited.  
**The engineering obligation:** show exactly which parts are designed, experimented with, executed, and integrated.
