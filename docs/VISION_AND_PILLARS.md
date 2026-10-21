# raWWar — Vision and Pillars

**Status:** Living north-star statement; describes intended experience, not shipped features.  
**Owner:** raWWar Experience design.  
**Audience:** Everyone making design, content, or implementation decisions.  
**Evidence rule:** Use this document to evaluate direction; use source, tests, and host runs to establish implementation status.

## North Star

**Make being a person inside a war the game.**

The player should not feel like an external cursor controlling a unit. They should feel like they have a place in a living military organization and that what they choose to do matters.

## Pillar 1 — The soldier is the star

Individual people matter.

A soldier is not disposable animation attached to a health bar.

Their training, qualifications, relationships, role, actions, successes, failures, and fate should contribute to the story of the war.

## Pillar 2 — First-person, always

raWWar is always first-person and immersive.

The player interacts with the world as a person standing inside it. The game is not VR-only: AnyApp is the primary host, with MyVR as a future immersive manifestation.

## Pillar 3 — Do what you want

The game should support many forms of meaningful participation.

Combat is important, but it is not the only valid way to play.

Repair, construction, research, logistics, piloting, command, recreation, and support roles should all be legitimate gameplay.

## Pillar 4 — Diegetic systems

Whenever practical, the player performs an action in the world instead of operating an abstract menu.

A laboratory is a laboratory.

A repair bay is a repair bay.

A command center is a command center.

A cockpit is a cockpit.

## Pillar 5 — The world remembers

Persistent modes should preserve consequences.

The war should accumulate history.

Players can enter ongoing universes and encounter consequences created by people who played before them.

## Pillar 6 — Agency at consequential moments

Major moments should belong to players whenever practical.

The headquarters confrontation is an example: the encounter, conversation, and final decision should be a live player event rather than a predetermined cinematic.

## Pillar 7 — Simulation serves experience

Simulation complexity is justified when it creates meaningful player experience.

We should not simulate everything merely because we can.

We should identify what must be persistent, what can be reconstructed, what can be abstracted, and what must be physically present.

## Pillar 8 — The technology should prove itself

raWWar is intended to be a flagship demonstration of the Workshop architecture.

It should eventually demonstrate FSM-driven autonomous behavior, MicroBundle composition, manifest-driven Experiences, AnyApp hosting, WebApp distribution and manifestation, the Workshop Renderer, semantic intent and structural grammar where appropriate, scalable persistence, deterministic reconstruction, observation-relative detail, and the ability to build a complex world from independently composable capabilities.

The game design comes first. The technology exists to make the design possible.


## Pillar 9 — One galaxy, many consequences

The game universe is a galaxy spanning many star systems.

The player should never experience the campaign as a sequence of disposable maps. Each operation occurs inside a larger political, military, economic, and historical system.

A player can matter at multiple scales:

**soldier → unit → organization → faction → world → star system → Empire → galaxy**

The game does not require the player to control all of those scales directly. It requires their actions to be capable of propagating upward.

## Pillar 10 — Distance matters

Warp drive exists, but a warp jump takes days to initialize and prepare.

Distance is therefore a gameplay constraint.

The time required to move forces and resources creates opportunities for planning, interception, logistics, intelligence, preparation, and consequence.

## Pillar 11 — Data scale is part of the spectacle

raWWar is not merely a war game with a large map. The long-term ambition is to make enormous amounts of structured, changing world data visible and meaningful through an entertaining first-person experience: many people, units, vehicles, facilities, resources, procedures, relationships, and consequences, all contributing to one legible world.

The game is the human-facing reason to care about the engineering. The deeper technical challenge is to process, organize, update, retain, reconstruct, and present far more world state than can be shown at full detail at once. Optimization therefore belongs throughout the design—not as a final pass after the world has been built.

That means choosing deliberately what must be updated now, what can be processed in batches, what can be represented compactly, what can be reconstructed from authoritative state, what can be simulated at a coarser level, and what deserves expensive detail because the player is observing or interacting with it. Detail should be promoted when it matters and reduced when it does not, without losing the continuity or consequences that make the world feel real.

The target is not a vanity count of entities or a claim that everything runs at maximum fidelity. The target is **more meaningful world per unit of computation**: richer environments, denser populations, more consequential systems, and a player experience that remains understandable and responsive. Every scale claim must eventually be supported by reproducible workloads, correctness checks, and measured resource costs.

The software architecture should remain reusable beyond raWWar. The game makes the capability tangible; it must not become an excuse to hide the capability behind proprietary gatekeeping, unverified claims, or a demo that cannot be inspected. Contracts, evidence, and measured behavior should make the underlying work understandable to other developers as well as players.
