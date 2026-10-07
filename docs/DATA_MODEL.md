# raWWar — Data Model

Status: Design foundation.

## Purpose

raWWar should be describable as relationships and state rather than a collection of bespoke behaviors.

The data model must let the Experience describe soldiers, equipment, vehicles, buildings, facilities, qualifications, procedures, factions, resources, and interactions while allowing FSM_API and the Renderer to operate on that information efficiently.

## Core distinction

There are three useful layers:

1. **Meaning** — what exists and what it means in the game.
2. **Behavior** — what stateful procedure is currently occurring.
3. **Presentation** — how that state is represented to the observer.

The same soldier may therefore be represented simultaneously as:

- a semantic entity with identity, role, rank, qualifications and equipment;
- an FSM participant executing a procedure;
- a compact GPU/rendering record used to display that soldier.

The presentation representation must never become the semantic source of truth.

## Entity identity

Entities need stable identities suitable for:

- persistence;
- deterministic variation;
- relationships;
- equipment ownership;
- qualification history;
- observation;
- networking;
- reconstruction.

A soldier identity should survive changes in presentation and equipment.

## Capability model

Capabilities should be composable.

Candidate capability categories include:

- movement;
- equipment;
- qualification;
- occupation;
- communication;
- vehicle operation;
- research;
- construction;
- intelligence;
- command;
- recreation.

Capabilities may expose providers.

A vehicle can expose interaction points and Gesture Providers without the soldier knowing the vehicle's implementation.

## State

State should describe what matters now, not duplicate permanent identity.

Candidate state includes:

- current location;
- current procedure;
- current FSM state;
- current task;
- current equipment configuration;
- current qualification status;
- readiness;
- physical condition;
- attention/focus;
- relationship context;
- current orders;
- current interaction.

Equipment state should preserve the distinction between what technology **is capable of** and what this particular item currently provides.

Relevant derived equipment state may include:

- research generation;
- production quality;
- condition;
- power state;
- sensor/camera quality;
- communications quality;
- comfort/ergonomics;
- reliability;
- maintenance state;
- installed modifications;
- ownership;
- operating constraints.

The same semantic equipment family can therefore produce materially different experiences depending on research, manufacture, configuration, and use.

## Relationships

Relationships are first-class.

Examples:

- soldier belongs to squad;
- squad belongs to unit;
- soldier reports to commander;
- soldier is qualified for vehicle;
- vehicle requires crew;
- facility provides capability;
- building contains interaction point;
- Gesture Provider belongs to capability;
- procedure requires qualification;
- procedure consumes resource.

This relationship graph is a major part of the simulation.

## Procedures

A procedure should describe requirements and transitions without embedding every participant into the implementation.

A procedure can identify:

- prerequisites;
- required roles;
- required resources;
- interaction points;
- provider queries;
- FSM states;
- completion conditions;
- failure conditions;
- interruption rules;
- resulting state.

## Gesture data

A Gesture should reference:

- pose sequence;
- mathematical transition rules;
- spatial anchors;
- timing/progression;
- valid equipment/configuration;
- variation bounds;
- interruption/completion behavior.

The exact schema remains open.

## Individual variation

Stable identity/seed data can drive bounded variation.

The variation system should be capable of producing differences in:

- timing;
- speed;
- stride;
- posture;
- attention direction;
- lateral position;
- reaction;
- recovery.

Variation must respect procedure constraints. A soldier cannot be randomized into an invalid physical state simply to create visual noise.

## GPU projection

The Renderer may project relevant state into compact GPU-oriented structures.

Candidate population representation:

- four 8-bit state channels per 32-bit pixel;
- compute shaders operate only on channels required by a pass;
- event horizons select appropriate fidelity.

This is a derived representation.

The semantic model remains authoritative.

## Persistence

Persistent universes require the ability to reconstruct meaningful state.

At minimum, persistence eventually needs to distinguish:

- stable identity;
- durable progression;
- current world state;
- owned/configured equipment;
- relationships;
- relevant history;
- content/version identity.

The exact persistence format is intentionally not yet fixed.

## Design rule

**Data describes the world. FSMs describe changing behavior. Gestures describe physical motion. The Renderer describes what the observer sees.**


## Training capacity as data

Training capacity should be represented explicitly rather than inferred from a generic building count.

Relevant relationships include:

- barracks → training modules;
- training module → current training assignment;
- squad → four-person membership;
- squad → qualification requirements;
- facility/vehicle → required crew composition;
- crew role → qualification requirements;
- qualification → training path;
- training path → required capacity;
- construction project → required construction crew;
- advanced construction crew → prerequisite qualifications.

This allows the simulation to answer questions such as:

- Can this facility be staffed?
- How many barracks are required?
- Which squads are blocking readiness?
- Which qualifications are missing?
- Is a building complete but operationally useless?
- What additional training capacity would unlock the next capability?

## Decision and intent data

Diegetic management does not eliminate structured decisions.

The underlying data should represent:

- available options;
- prerequisites;
- resource requirements;
- consequences;
- authority required;
- current decision state;
- accepted/rejected status;
- resulting procedure.

A tablet, advisor conversation, terminal, or other physical interaction is a presentation of that decision data.

Future ProtocolAi integration should map semantic intent to these available choices rather than directly mutating authoritative state.



## Faction, sub-faction, and capability provenance

A faction is an organizational parent, not a complete inventory.

The data model should represent capability provenance explicitly.

Candidate hierarchy:

```
Galaxy
  → MajorFaction
    → SubFaction
      → Organization
        → Facility / Unit
          → Individual
```

A capability instance should be able to answer where it came from and why it exists.

Candidate provenance fields:

- parent faction;
- owning sub-faction;
- originating technology;
- production source;
- acquisition source;
- capture source;
- transfer history;
- local doctrine;
- local specialization;
- qualification requirements;
- current crew;
- current location;
- operational state.

The distinction is important:

**Faction capability** = what the faction's doctrine, technology, and economy make plausible.

**Sub-faction capability** = what a particular world/colony/moon/asteroid/etc. can actually support.

**Capability instance** = the specific physical thing that exists here, with its own history and state.

Two worlds belonging to the same House can therefore have radically different military realities.

## Skills and individual progression

Soldiers should be modeled as persistent individuals rather than anonymous interchangeable unit members.

A soldier can have:

- skills;
- qualifications;
- attributes;
- experience history;
- rank;
- assignments;
- equipment;
- relationships;
- injuries/condition;
- certifications;
- procedures learned;
- combat experience.

A useful skill model is **GURPS-like in spirit, not necessarily GURPS-compatible in implementation**.

Skills should describe what an individual can actually do, while qualifications and organizational permissions determine what they are authorized to do.

For example:

```
Skill
  → demonstrated ability

Qualification
  → recognized/certified ability

Permission
  → organizational authority to exercise it

Readiness
  → ability to perform it now
```

Experience can improve a skill when the relevant activity provides meaningful practice.

Examples:

- firing a weapon can improve weapons-related skill;
- surviving combat can improve combat-relevant judgment;
- repairing vehicles can improve mechanical skill;
- repeatedly navigating difficult routes can improve navigation;
- successfully commanding under pressure can improve command-related skills;
- defending against an enemy can build experience specific to that kind of threat.

Experience should not be free background progression merely because time passed.

The simulation should record **what the individual actually did**.

## Skill progression is evidence-based

A candidate progression chain is:

```
Skill
  ↓
attempt
  ↓
meaningful practice
  ↓
outcome
  ↓
experience evidence
  ↓
skill progression
```

Training can accelerate learning.

Real operation can validate and deepen it.

Combat can expose weaknesses that classroom or VR training cannot.

This creates an important distinction:

> **Training teaches a soldier how to perform. Experience teaches the soldier what happens when performance meets reality.**

The exact mathematical progression curve remains intentionally open.
