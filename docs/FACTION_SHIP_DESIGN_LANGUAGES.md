# Faction Ship Design Languages

Status: Candidate design framework. It establishes how faction identity should affect ships; it does **not** finalize the thirteen major factions or assign a permanent style to any one of them.

## The design goal

In raWWar, the factions are all human. Their ships should not look different because one faction is biologically alien. They should look different because human societies make different decisions about war, industry, risk, command, maintenance, status, and what they believe a ship is for.

A player should be able to recognize a likely faction from a ship's silhouette and arrangement before seeing its markings. A close inspection should then reveal the evidence: access panels, structural choices, system separation, engine mounts, sensor placement, repair standards, and the kinds of modifications that have accumulated during service.

This is the difference between a **design language** and a paint scheme.

## Same mission, different answer

Two factions may both need a destroyer, carrier, patrol ship, troop transport, reconnaissance vessel, or logistics ship. The mission category does not dictate one universal hull.

For a given mission, compare equivalent requirements first:

- same operational environment and technology era;
- similar payload, endurance, crew capacity, and intended duty cycle;
- equivalent mission success criteria;
- comparable production and support assumptions.

Then let each faction solve the problem through its own doctrine and industrial base. One may favor separated redundant systems and roomy repair access; another may integrate equipment tightly to reduce hull volume; another may use replaceable mission pods; another may optimize for mass production and rapid fleet turnaround.

Those are not free bonuses. Each decision produces a bill in mass, volume, power, thermal load, production time, qualification, maintenance, signature, or vulnerability.

## What makes the style legible?

| Design dimension | Questions the design must answer |
|---|---|
| Silhouette | What shape is recognizable at a distance, even in shadow? |
| Architecture | Where are command, crew, mission payload, power, and propulsion placed? |
| Protection | Which approach directions and components receive the most protection, and why? |
| Redundancy | Are critical systems duplicated, separated, cross-connected, or deliberately concentrated? |
| Propulsion | How are engines mounted, protected, serviced, and arranged for the ship's expected maneuvers? |
| Industrial signature | Are parts standardized, precision-integrated, modular, rugged, or optimized for volume? |
| Crew culture | Are watch stations, maintenance access, and working routes generous, compact, automated, or labor-intensive? |
| Ornament and markings | What is ceremonial, political, identifying, or propagandistic—and what is actually structural? |
| Damage and repair | What do field patches, replacement panels, reinforcement, and mixed-generation equipment reveal about service history? |

The art direction should treat these as a coherent system. Do not simply change hull color, add spikes, or apply a new emblem and call the ship faction-specific.

## Candidate style families

The machine-readable catalogue in [`data/faction-ship-doctrines.json`](../data/faction-ship-doctrines.json) currently contains six deliberately unassigned archetypes:

1. **Mass, separation, and redundancy** — generous structure and service access, with costs in mass, fabrication, and labor.
2. **Dense integration and compactness** — compact high-capability hulls, with harder access and greater common-mode risk.
3. **Distributed modules and adaptation** — visible pods and interfaces, with flexibility balanced against connection and logistics burdens.
4. **Forward concentration and decisive engagement** — strong bow identity and directional optimization, with orientation and blind-sector consequences.
5. **Dispersed signature and deception** — separated emitters and ambiguous geometry, requiring specialist calibration and strict signature discipline.
6. **Standardized production and fast service** — repeated hull families and accessible common parts, trading bespoke optimization for fleet availability.

These are a vocabulary for building and testing designs, not six final factions. A real faction can combine tendencies, evolve them across eras, or diverge between shipyards, services, and mission classes. No one-to-one mapping to present-day countries is intended.

## Faction identity is a living consequence

Faction style should be generated and maintained through the same persistent-world rules as the rest of raWWar:

- **Industry:** available materials, fabrication tolerances, shipyard tooling, component supply, and quality-control capacity constrain the design.
- **Doctrine:** expected engagements, acceptable losses, logistics reach, command assumptions, and strategic priorities shape the arrangement.
- **Institutions:** procurement, bureaucracy, elite service traditions, and rival branches create design compromises and competing standards.
- **Maintenance culture:** inspection routines, repair access, spare availability, and tolerance for field modification affect readiness and appearance.
- **History:** combat damage, hurried refits, captured technology, sanctions, shortages, and lessons learned change later ships.
- **Politics and prestige:** markings, ceremonial structures, and conspicuous technology may signal legitimacy or intimidate—but carry cost and can conflict with serviceability.

A faction under pressure may begin with one design philosophy and gradually compromise it. Older hulls may retain an earlier silhouette while receiving newer sensors, weapons, drives, or power systems. Two ships of the same class may visibly differ because one has passed through several refits and the other has not.

Captured or exported ships are useful exceptions. Their original design language should remain visible even after new markings, adapters, weapons, or local repairs are added. Mixed identity tells a story.

## Keep appearance attached to physical truth

A signature exposed engine mount must be an actual mount in the assembly and damage model. A deep armored spine must add mass and manufacturing work. Distributed systems need real connections, alignment, and service requirements. A tight internal arrangement must affect access and repair. A field-repair signature must be the visible result of a recorded repair event, not a randomly applied texture.

This ties faction art to the existing ship model:

- [Ship assemblies](../data/ship-assemblies.json) provide persistent parts, mounts, zones, and repair approaches.
- [Directional damage and salvage](../data/directional-damage-and-salvage.json) records the actual impacted face, affected parts, causal cascades, and recoverable remains.
- [Starship sizing, damage, and salvage](STARSHIP_SIZING_DAMAGE_AND_SALVAGE.md) explains why destruction and repair persist as physical work.

The renderer can simplify distant ships while preserving the faction's strongest silhouette cues. At close range, it can reveal construction seams, access paths, attachment standards, refits, and damage history. Visual level of detail must not change the authoritative ship or its history.

## Comparing faction designs fairly

Do not rank styles with a single arbitrary faction-quality statistic. Run matched mission trials and record the consequences:

- mission completion and survival;
- build cost, fabrication time, and shipyard occupancy;
- dry mass, usable volume, power demand, and heat rejection;
- crew size, qualifications, and watch burden;
- maintenance hours, common spares, and repair duration;
- system isolation and graceful degradation;
- observability and signature management;
- upgrade flexibility and compatibility across generations.

A design that wins a short engagement may be a poor choice for a long campaign if it consumes scarce parts or spends too much time in dock. A ship that is easy to mass-produce may be strategically valuable even if it loses a direct comparison against a specialized peer.

## What remains open

- Which of the thirteen major factions expresses each design tendency, and which combines or rejects it?
- How do faction styles differ between patrol craft, capital ships, transports, carriers, and support vessels?
- Which differences are canon, and which are regional shipyard or historical variants?
- What common interfaces permit allied logistics, export, repair, or captured-ship conversion?
- How do the Empress's power and the persistent war pressure factions to copy, steal, standardize, or abandon design traditions?

These are deliberate elicitation points. The framework gives us a consistent way to make the answers visible and testable without inventing faction canon prematurely.

## Governing principle

**The silhouette tells you who probably built the ship. The engineering tells you why. The damage and repairs tell you what happened to it.**
