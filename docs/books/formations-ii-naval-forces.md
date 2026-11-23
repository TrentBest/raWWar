# Formations II: Naval Forces

![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg)

![SURPS Rulebook LOD 2](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg)

> Fleet geometry, station keeping, maneuver, command, and maritime operations.

A naval formation manual spanning ancient fleets, sail, steam, and modern task groups while accounting for sea state, navigation, communications, and mission.

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

## 1. Scope

Naval formation describes the arrangement and coordinated movement of vessels, boats, submarines, aircraft, and support units at sea or in littoral waters. It is not simply a land formation translated onto water: vessels have inertia, turning limits, wake, draft, windage, propulsion constraints, collision risk, and navigation uncertainty.

## 2. Fleet and task organization

Distinguish administrative fleet, task force, task group, escort group, amphibious group, convoy, patrol line, and temporary maneuver element. These terms vary historically and institutionally. A group may be assembled for one mission and dissolve afterward; command authority and support responsibilities must be explicit.

## 3. Geometry and station keeping

Formation parameters include station, bearing, range, speed, course, reference vessel, maneuvering room, and permitted station changes. Relative position is not enough: safe separation depends on speed, sea state, visibility, vessel handling, and navigational confidence. Model station error and correction rather than snapping vessels to perfect coordinates.

## 4. Historical patterns

Compare oared-ship lines and columns, ancient ramming-era maneuver, sailing battle lines, convoy systems, commerce protection, fleet screens, scouting lines, carrier-centered task groups, submarine patrol areas, and distributed maritime operations. Avoid treating a named historical pattern as stable across centuries; propulsion, weapons, signaling, and doctrine change its meaning.

## 5. Wind, current, sea state, and coast

Wind, waves, swell, current, tide, depth, reefs, shoals, ice, visibility, and coastal geography shape maneuver. Sailing fleets depend on wind and sail handling; powered vessels still face sea-state limits, fuel costs, and sensor degradation. A map line must not imply a vessel can instantly reverse course or stop.

## 6. Convoys and protection

Convoy formation is a logistics and risk-management arrangement. It coordinates merchant vessels, escorts, routing, speed, communications, rescue, and port capacity. The convoy's success is measured by cargo and people delivered, not solely by enemy vessels destroyed.

## 7. Amphibious and littoral formations

Littoral operations connect ships, landing craft, aircraft, shore parties, engineers, medical support, and civilian infrastructure. The landing sequence depends on hydrography, weather, access, congestion, and sustainment. Model the shore as an active environment with residents and services, not an empty objective tile.

## 8. Subsurface and mixed-domain coordination

Submarines, surface ships, maritime patrol aircraft, helicopters, unmanned systems, and shore stations have different detection, communication, endurance, and authority constraints. Subsurface communication limitations and classification uncertainty matter. Do not grant a shared omniscient sensor picture.

## 9. Command at sea

Signals include flags, lamps, sound, radio, data links, and prearranged doctrine. A vessel may lose contact, misread a signal, or be unable to maneuver as ordered. Distinguish command intent, acknowledgement, local interpretation, and actual movement.

## 10. Formation training and ghost stations

A training HUD can render ghost stations relative to a selected reference vessel, with bearing/range envelopes, course and speed cues, safe separation, and planned maneuver previews. Gestures can teach helm, lookout, signaling, and equipment interactions. Haptic cues should encode a small, consistent vocabulary and must not overload the player during emergencies.

## 11. Scoring

Evaluate station keeping, collision margin, maneuver timing, signal acknowledgement, navigation confidence, convoy integrity, rescue readiness, and mission delivery. Include penalties for unsafe proximity and for abandoning a disabled vessel. Separate procedural precision from tactical outcome.

## 12. Fiction and future fleets

Autonomous surface and subsurface systems, orbital-to-ocean sensing, and networked fleets require explicit responsibility and communication limits. Mark extrapolations and fictional systems. A distributed fleet still depends on maintenance, repair, fuel or energy, trained crews, ports, data integrity, and political authorization.

## 13. Simulation model

Store each vessel's identity, dimensions, draft, propulsion, turning response, speed envelope, endurance, crew state, sensors, communications, station assignment, and damage state. Formation definitions carry reference frame, stations, tolerances, transitions, and mission constraints. Physics owns fluid and vehicle motion; raWWar owns naval doctrine and organization.

## 14. Scenarios

Use a merchant convoy in poor visibility, an ancient oared fleet entering a constrained strait, a sailing line-of-battle under changing wind, an escort group protecting a damaged tanker, and a mixed crew rescue under communications failure. Label historical reconstruction versus fictional exercise.

## Cross-domain ownership and implementation boundary

These books are descriptive domain knowledge. They do not introduce executable APIs, duplicate a physics engine, or claim that a HUD, haptic rig, or simulation feature already exists. A host may opt into SURPS composition; without it, the raWWar Experience remains valid. Any game-specific doctrine, scoring rubric, or fictional equipment must be versioned and identified as such.

## Related fleet manual

- [Formations X: Armadas and Flotillas](formations-x-armadas-and-flotillas.md) — fleet organization from local flotillas to armadas, including escorts, auxiliaries, coalition command, and maritime sustainment.
