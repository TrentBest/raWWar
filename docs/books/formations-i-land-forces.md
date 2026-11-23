# Formations I: Land Forces

![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg)

![SURPS Rulebook LOD 2](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg)

> The organization, movement, deployment, and tactical employment of land formations.

A reusable manual for land-force formations from small teams and files through squads, platoons, companies, battalions, legions, and field armies.

This raWWar-owned LOD 2 manual is for historical study, roleplaying, and simulation design. It distinguishes evidence from interpretation and fictional adaptation. It is not a real-world operational training manual. Shared physical behavior belongs to Physics; embodied movement belongs to Gestures; this book defines warfare-domain meaning, context, and evaluation.

## Related sourcebooks

- [Formations VII: Amphibious and Littoral Forces](formations-vii-amphibious-and-littoral-forces.md) — cross-domain organization connecting naval forces, embarked units, transfer services, shore infrastructure, and civilian coastal systems.

- [Warfare and Organized Force](warfare-and-organized-force.md)
- [Fortifications and Strongholds](fortifications-and-strongholds.md)
- [Medicine and Healing](medicine-and-healing.md)
- [The Art of Resupply](the-art-of-resupply.md)
- [Bases and Encampments](bases-and-encampments.md)
- [Formations I: Land Forces](formations-i-land-forces.md)
- [Formations II: Naval Forces](formations-ii-naval-forces.md)
- [Formations III: Air Forces](formations-iii-air-forces.md)

## 1. Scope and vocabulary

A formation is an organized arrangement of people, vehicles, animals, or mixed elements for a purpose under constraints of terrain, visibility, command, communication, cohesion, and sustainment. Formation is not synonymous with unit: one describes arrangement and relationships, the other an organization with membership and responsibilities. The same unit may adopt several formations; the same formation pattern may be used by different institutions.

## 2. Formation as a living system

Model each formation through members, slots, roles, command relationships, spacing, orientation, frontage, depth, reserve, support, communications, and permitted transitions. A formation is not a rigid diagram. It is a negotiated arrangement that changes as members tire, terrain narrows, casualties occur, visibility changes, or orders arrive late.

## 3. Small-unit arrangements

Files, ranks, pairs, wedges, lines, columns, staggered files, dispersed groups, and all-round postures solve different problems. Their tradeoffs concern control, frontage, depth, mutual support, exposure, speed, and ability to pass obstacles. Avoid declaring one shape universally best; specify mission, environment, training, and threat.

## 4. Recursive organization

Teams combine into squads; squads into platoons; platoons into companies; companies into battalions and larger formations. Higher echelons coordinate subordinate formations without erasing their local agency. A slot may represent a person, vehicle, team, or subordinate formation. Aggregation must preserve identity, role, readiness, and command provenance.

## 5. Historical case studies

Compare the Roman legion's changing tactical and administrative arrangements with Macedonian pike formations, Greek hoplite systems, early-modern pike-and-shot, Napoleonic corps, twentieth-century combined-arms organizations, and contemporary dispersed formations. These are not timeless national styles. Each emerged from specific recruitment, equipment, terrain, doctrine, communications, and political institutions.

## 6. Encampment and transition

A formation on the march, in assembly, deployed, withdrawing, resting, or encamped has different spatial and command requirements. Formation transitions need explicit states, triggers, authority, timing, and interruption behavior. A unit must not teleport into a slot or silently abandon a casualty, vehicle, or attached element.

## 7. Command, signals, and uncertainty

Orders have issuer, recipient, intent, scope, priority, timestamp, and acknowledgement state. Signals may be visual, audible, tactile, radio, runner-delivered, or networked. Lost communication should produce doctrine-defined local behavior, not omniscient compliance. A simulation should distinguish order issued, received, understood, and executed.

## 8. Terrain and environment

Slope, soil, vegetation, streets, walls, bridges, weather, visibility, smoke, water, temperature, and obstacles change the feasibility and cost of a formation. Formation geometry should be checked against traversability and turning space. A narrow passage creates compression and queueing rather than magically preserving spacing.

## 9. Cohesion, morale, and fatigue

Cohesion is a changing capacity to coordinate under stress, not a personality trait shared by a population. Track fatigue, casualties, separation, noise, fear, trust, leadership availability, and recent success or failure as contextual factors. Units may remain physically present while becoming unable to perform coordinated tasks.

## 10. Training and ghost formations

For raWWar training, render a translucent target arrangement with slot volumes, facing cues, spacing tolerances, role labels, and transition previews. Gestures supplies the embodied movement vocabulary; raWWar supplies formation intent and evaluation criteria. Compare observed position and orientation with the target, show uncertainty, and score the attempt against declared criteria. Haptics should guide attention and timing, never conceal tracking failure or punish a player for inaccessible controls.

## 11. Evaluation and scoring

Score distinct dimensions: correct membership, role assignment, slot occupancy, facing, spacing, cohesion, transition timing, command acknowledgement, obstacle clearance, and safety. Publish the rubric before the drill. Use tolerances and confidence intervals; avoid a single opaque score. A successful formation is one that meets the scenario's mission constraints, not necessarily the most geometrically perfect one.

## 12. Simulation contract

Represent formation definitions as data: identifier, purpose, member/slot schema, allowed roles, geometry, orientation convention, constraints, transitions, command semantics, and scoring rubric. Keep authored doctrine separate from observed state. Physics owns physical motion and collision; Gestures owns embodied movement; raWWar owns formation meaning and tactical context.

## 13. Failure and recovery

Handle missing members, contradictory orders, blocked routes, incapacitated leaders, sensor drift, latency, broken vehicles, and panic without corrupting identity. Recompute only affected slots where possible. Preserve the difference between commanded position, planned position, and actual position. A formation can dissolve, reform, split, or continue with degraded capability.

## 14. Scenarios and research

Scenario seeds include a drill-ground assembly, a narrow village street, a river crossing, a night withdrawal, a mixed mounted convoy, and a damaged command network. Historical reconstruction should record date, unit, source quality, disputed interpretations, and whether a diagram is contemporary, retrospective, or a game abstraction.

## Cross-domain ownership and implementation boundary

These books are descriptive domain knowledge. They do not introduce executable APIs, duplicate a physics engine, or claim that a HUD, haptic rig, or simulation feature already exists. A host may opt into SURPS composition; without it, the raWWar Experience remains valid. Any game-specific doctrine, scoring rubric, or fictional equipment must be versioned and identified as such.
