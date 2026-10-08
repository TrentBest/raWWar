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

## Design rule

Numbers describe a capability. They do not replace the people, procedures, equipment, dependencies or history that make the capability real.

A vehicle with no qualified crew is not ready.

A factory with no power is not producing.

A refinery with no keyed input is not refining.

A researched technology is not deployed until someone chooses to adopt it, tooling exists, production occurs, personnel qualify, and logistics deliver it.

The FSM layer is what turns these relationships from documentation into executable world state.
