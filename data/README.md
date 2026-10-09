# raWWar Seed Universe Data

This directory is the first populated content corpus for the raWWar simulation.

The records are intentionally **data-first**. They are not a conventional static unit roster. Each record describes a capability that can participate in the same underlying world:

`Resource → Power → Facility → Technology → Chassis → Configuration → Crew → Qualification → Procedure → Capability → Logistics → History`

The identifiers in these files are stable semantic keys. They are intended to become content records consumed by FSM-driven runtime systems rather than duplicated in application code.

## Files

- `resources.json` — physical resources and refined materials.
- `power-systems.json` — generation, storage and distribution capabilities.
- `technologies.json` — researchable capabilities and their dependencies.
- `vehicles.json` — populated vehicle/chassis/configuration records across environments.
- `facilities.json` — production, research, logistics, training and infrastructure facilities.
- `cut-sheets.json` — commander-facing consequences and crew-facing operational summaries.
- `vehicle-chassis.json` — physical chassis envelopes, sockets, payloads, power/thermal budgets, mobility, service and compatible families.
- `electronic-systems.json` — electronics, power conversion, relays, I/O, sensors, data buses, communications, safety controls and event recorders.
- `building-assemblies.json` — building footprints, foundations, shell, materials, connected/peak loads, contents, utilities, construction stages and commissioning.
- `security-systems.json` — access, perimeter, industrial safety, shipboard damage control, custody and sensor-fusion panels.
- `upgrades.json` — research-to-manufacture-to-dispatch-to-install-to-acceptance upgrade lifecycle.

## World-generation architecture

The seed universe content corpus complements the spatial generation model. See [Galaxy Generation and Spatial Refinement](../docs/GALAXY_GENERATION_AND_SPATIAL_REFINEMENT.md) for the reserved cell-42 galaxy anchor, baked cosmic context, stable cell identity, time-ordered probability, recursive 10 × 10 × 10 refinement, sister-galaxy travel, and the boundary between reproducible generated properties and persistent gameplay history. The machine-readable [Galaxy Generation Contract](galaxy-generation-contract.json) records the current address ordering, seed/event keys, persistence rules, travel-warning policy, bounded spatiotemporal query model, observer-relative projection rules, and unresolved implementation decisions. The [Spatiotemporal World Model and Observation](../docs/SPATIOTEMPORAL_WORLD_MODEL_AND_OBSERVATION.md) document defines the conceptual `F(x,y,z,t)` query, time-dependent motion, and how galaxy/system/orbit/surface views and GUI derive from one authoritative world.

## Design rule

Numbers describe a capability. They do not replace the people, procedures, equipment, dependencies or history that make the capability real.

A vehicle with no qualified crew is not ready.

A factory with no power is not producing.

A refinery with no keyed input is not refining.

A researched technology is not deployed until someone chooses to adopt it, tooling exists, production occurs, personnel qualify, and logistics deliver it.

The FSM layer is what turns these relationships from documentation into executable world state.


## Engineering data is deliberately composable

The expanded catalogues describe capabilities at different levels instead of duplicating every detail into every vehicle or building. A vehicle points to a stable chassis identity; a chassis declares sockets and physical limits; electronic systems describe their interfaces, load, failure modes and maintenance; facilities point to construction assemblies; and upgrades describe how a researched design becomes a physically installed and accepted capability.

These are seed data and simulation design values, not real-world engineering specifications. Unknown or balance-sensitive quantities remain explicit estimates rather than false precision. Facility loads are not self-powered: generation, distribution, backup, route diversity, staffing, qualification, spare parts and maintenance must all be modeled. A research unlock does not retrofit assets globally. The player must manufacture, accept, dispatch and install each physical upgrade, and the asset must pass its defined tests.
