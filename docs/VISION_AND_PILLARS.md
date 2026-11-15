# raWWar — Vision and Pillars

**Status:** Living north-star statement; describes intended experience, not shipped features.  
**Owner:** raWWar Experience design.  
**Audience:** Everyone making design, content, or implementation decisions.  
**Evidence rule:** Use this document to evaluate direction; use source, tests, and host runs to establish implementation status.

## North Star

**Make being a person inside a living galaxy shaped by war the game.**

The player should not feel like an external cursor controlling a unit or an abstract economy. They should feel present in a shared world, with a meaningful place in its people, work, organizations, and events—and with choices that matter.

## Pillar 1 — People are the heart of the world

Individual people matter.

A soldier is not disposable animation attached to a health bar, and a civilian is not background scenery attached to an economy. People have identities, relationships, responsibilities, ambitions, and histories.

The soldier remains the star of the flagship military campaign. The wider Experience must also make the lives of pilots, crews, researchers, builders, traders, travelers, and other inhabitants meaningful in the same shared universe.

## Pillar 2 — First-person, always

raWWar is always first-person and immersive.

The player interacts with the world as a person standing inside it. The game is not VR-only: AnyApp is the primary host, with MyVR as a future immersive manifestation.

## Pillar 3 — Many ways to exist in one universe

The game should support many forms of meaningful participation without splitting them into disconnected modes or separate worlds.

Combat is important, but it is not the only valid way to live in the galaxy. A player might serve as a soldier, fly freight, captain a passenger ship, travel the outer rim, hide as a pirate in an asteroid field, or work in research, construction, logistics, and support.

The player may prefer to operate an asset personally, work as part of its crew, own or lead the organization that operates it, compete with other organizations, or participate as a customer. These are different scales and relationships of agency—not mutually exclusive character classes. The world and its consequences remain shared when the player's control becomes delegated or organizational.

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


## Pillar 9 — One galaxy, many ways to exist

The game universe is a galaxy spanning many star systems. War is a major force within it, not the only activity or identity available to its inhabitants.

The player should never experience the campaign as a sequence of disposable maps. Military operations, civilian journeys, commercial activity, piracy, organizational competition, and ordinary work all take place in the same persistent world. They share locations, people, infrastructure, events, history, and consequences.

A player can matter at multiple scales and through different relationships:

**person → crew or team → organization → faction → world → star system → Empire → galaxy**

The player does not need to control every scale directly. They may act, lead, delegate, own, compete, or participate. Changing the scale of agency changes the player's responsibilities and controls—not the underlying universe.

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
