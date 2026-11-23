# The Art of Resupply

![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg)

![SURPS Rulebook LOD 2](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.SingularityUniversalRoleplayingSystem/development/docs/assets/rulebook-lod-2.svg)

> The logistics of keeping people, formations, fleets, aircraft, and bases capable.

A domain manual for supply networks, sustainment, replenishment, repair, medical logistics, contested routes, and the operational art of resupply.

This raWWar-owned LOD 2 manual is for historical study, roleplaying, and simulation design. It distinguishes evidence from interpretation and fictional adaptation. It is not a real-world operational training manual. Shared physical behavior belongs to Physics; embodied movement belongs to Gestures; this book defines warfare-domain meaning, context, and evaluation.

## Related sourcebooks

- [Warfare and Organized Force](warfare-and-organized-force.md)
- [Fortifications and Strongholds](fortifications-and-strongholds.md)
- [Medicine and Healing](medicine-and-healing.md)
- [The Art of Resupply](the-art-of-resupply.md)
- [Bases and Encampments](bases-and-encampments.md)
- [Formations I: Land Forces](formations-i-land-forces.md)
- [Formations II: Naval Forces](formations-ii-naval-forces.md)
- [Formations III: Air Forces](formations-iii-air-forces.md)

## 1. Resupply is an operation

Resupply is not an inventory button. It is a coordinated process that moves the right resource, in usable condition, to the right place and time, under authority and constraints. It includes demand estimation, procurement, storage, allocation, transport, receipt, maintenance, waste, and accountability.

## 2. The supply picture

Track stocks, consumption rates, serviceability, shelf life, compatibility, location, ownership, reservations, transport capacity, and uncertainty. Distinguish on-hand inventory from usable inventory, committed stock, in-transit stock, and stock that is present but inaccessible.

## 3. Classes and categories

Food, water, fuel, energy, ammunition, medical supplies, clothing, shelter, spare parts, tools, communications equipment, construction materials, and specialist consumables have different handling and risk. A generic resource counter hides compatibility, storage, expiry, and substitution constraints.

## 4. Demand and forecasting

Demand changes with force size, mission tempo, environment, damage, weather, distance, and duration. Forecasts are estimates, not facts. Record assumptions, confidence, and observed variance. Players should be able to understand why a plan failed rather than being surprised by an arbitrary shortage.

## 5. The supply chain

A chain may include production, purchase, collection, inspection, storage, staging, transport, transfer, distribution, use, recovery, and disposal. Each handoff has a responsible actor, quantity, condition, timestamp, and record. Bottlenecks can arise at ports, roads, loading points, workshops, fuel points, clinics, or the final distribution step.

## 6. Movement and transport

Pack animals, carts, wagons, trucks, trains, ships, aircraft, pipelines, drones, and local carriers have different capacity, speed, cost, terrain access, weather limits, maintenance, and vulnerability. The fastest transport is not automatically the most resilient or affordable.

## 7. The last mile

The final movement from a depot to the person or machine in need is often the hardest. Distribution may require small loads, route knowledge, local trust, safe passage, and accurate identification. A delivery marked complete at a warehouse is not complete for the recipient.

## 8. Repair and recovery

Resupply includes spare parts, tools, technicians, diagnostic capacity, salvage, recovery vehicles, workshops, and return-to-service decisions. Repair can restore capability more efficiently than replacement, but only when parts, time, skill, and safety permit. Track cannibalization and its downstream costs.

## 9. Medical logistics

Medical capability depends on trained staff, clean supplies, medications, blood products where applicable, power, temperature control, transport, records, and receiving facilities. A medical HUD should surface shortages and transfer status without pretending that inventory guarantees clinical suitability.

## 10. Bases and depots

Bases are nodes in a network, not bottomless containers. Model capacity, throughput, protection, labor, storage conditions, access, maintenance, and local relationships. A depot may be full while a forward unit is empty because the route, allocation, or handling capacity has failed.

## 11. Naval, air, and expeditionary sustainment

Ships need fuel, food, water, parts, munitions, crew rest, and repair; aircraft need fuel, maintenance, qualified ground crews, parts, and safe operating conditions. Replenishment at sea, airlift, forward arming/refueling, and expeditionary staging have distinct hazards and capacity limits. Treat these as systems with explicit constraints, not instant transfer animations.

## 12. Contested and disrupted supply

Weather, terrain, infrastructure failure, labor disputes, political restrictions, piracy, blockade, interdiction, misinformation, cyber disruption, and combat can interrupt supply. Do not assume every disruption is an enemy attack. Civilian infrastructure and humanitarian supply may be affected; consequences must be represented.

## 13. Allocation and ethics

Scarcity forces choices among units, civilians, allies, prisoners, and essential services. Allocation rules should be visible and contestable. Distinguish lawful military prioritization, humanitarian obligations, corruption, favoritism, and emergency triage. A simulation should not reward cruelty as if it were merely efficient accounting.

## 14. Resupply as player gameplay

Players can plan loads, inspect stock, request replenishment, escort transport, repair routes, receive cargo, redistribute supplies, recover equipment, and negotiate access. Roleplay can focus on quartermasters, logisticians, drivers, sailors, maintainers, medics, local suppliers, and community representatives.

## 15. HUD and ghost logistics

The HUD should show requested versus approved quantities, stock confidence, estimated arrival window, route state, cargo condition, receiving capacity, and unresolved dependencies. A ghost route or pallet layout can guide loading and staging. Gestures may teach safe, game-defined handling sequences; haptics confirm selection or alignment, not replace safety instruction.

## 16. Measuring performance

Score fulfillment, timeliness, accuracy, serviceability, waste, route resilience, equitable allocation, safety, and continuity. Do not optimize only tonnage delivered: the wrong item, expired stock, unusable spare, or unstaffed clinic is not successful sustainment.

## 17. Simulation model

Represent resources, lots, containers, facilities, routes, transport assets, actors, requests, reservations, transfers, condition, expiry, and audit events. Physics owns movement and physical handling; raWWar owns military logistics meaning and doctrine; host systems own persistence and execution.

## 18. Scenarios

Include a remote patrol with a delayed water delivery, a legionary column planning a march, a fleet replenishment window, an airbase parts shortage, a field hospital running low on critical consumables, a damaged bridge, a winter convoy, and a humanitarian corridor negotiation.

## 19. The art

The art of resupply is matching demand to capacity while preserving trust, readiness, human dignity, and future options. Logistics is not the background to strategy. It is one of the ways strategy becomes possible—or impossible.

## Cross-domain ownership and implementation boundary

These books are descriptive domain knowledge. They do not introduce executable APIs, duplicate a physics engine, or claim that a HUD, haptic rig, or simulation feature already exists. A host may opt into SURPS composition; without it, the raWWar Experience remains valid. Any game-specific doctrine, scoring rubric, or fictional equipment must be versioned and identified as such.


## Resource and industrial foundations

This volume is complemented by [Military Resources and Sustainment](military-resources-and-sustainment.md), which expands the full resource lifecycle and the human consequences of scarcity; [Extraction, Materials, and Industrial Foundations](extraction-materials-and-industrial-foundations.md), which follows raw materials into usable industrial capacity; and [Orbital Industry and Strategic Resource Networks](orbital-industry-and-strategic-resource-networks.md), which extends logistics into off-world industrial networks.
