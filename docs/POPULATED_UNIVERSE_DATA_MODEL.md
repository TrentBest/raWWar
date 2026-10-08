# Populated Universe Data Model

**Status:** Seed corpus / development  
**Purpose:** Define the relationship between authored science-fiction content and executable raWWar world state.

The data corpus is intentionally organized around capabilities rather than game-unit statistics.

## 1. The capability chain

```text
RESOURCE
   ↓
EXTRACTION
   ↓
REFINING
   ↓
POWER / MATERIAL
   ↓
FACILITY
   ↓
TECHNOLOGY
   ↓
CHASSIS
   ↓
SOCKETS
   ↓
COMPONENTS
   ↓
CONFIGURATION
   ↓
QUALIFIED CREW
   ↓
PROCEDURES / GESTURES
   ↓
CAPABILITY
   ↓
LOGISTICS
   ↓
READINESS
   ↓
HISTORY
```

Every arrow is an opportunity for an FSM relationship.

Nothing in this chain is merely descriptive.

## 2. Record identity

Every authored record should have:

| Field | Meaning |
|---|---|
| `id` | Stable semantic identity |
| `name` | Human-readable name |
| `class` / `family` | Capability classification |
| `environment` | Where the capability can operate |
| `prerequisites` | Required capabilities or research |
| `inputs` | Physical or organizational requirements |
| `outputs` | What the capability produces |
| `procedures` | Work that must actually occur |
| `crew` | People required to operate it |
| `readinessDependencies` | Conditions required before use |
| `failureModes` | Actual ways the capability can degrade |
| `history` | Events that permanently contribute to world knowledge |

## 3. Vehicles

Vehicle identity is:

`Chassis → Sockets → Components → Configuration → Capability → Qualified Crew → Readiness`

A vehicle record therefore does **not** simply say:

> Speed = 80

It says what produces that mobility:

- chassis;
- propulsion;
- power;
- mobility system;
- mass;
- environmental envelope;
- crew;
- qualification;
- maintenance;
- fuel;
- terrain;
- weather;
- current damage.

The resulting observed speed is a consequence.

## 4. Power

Power is a physical capability chain:

`Resource → Generation → Distribution → Facility → Equipment → Procedure → Capability`

A 450 MW reactor does not mean every building automatically has 450 MW.

Power must:

1. be generated;
2. be distributed;
3. reach the facility;
4. pass through functioning equipment;
5. be available at the required time;
6. be consumed by qualified procedures.

A damaged substation can therefore disable a perfectly healthy factory.

## 5. Technology

Technology records represent **capability becoming possible**, not a button becoming unlocked.

The canonical progression is:

`Research → Demonstration → Maturity → Adoption → Tooling → Production → Qualification → Deployment → Field Experience`

A technology may therefore exist at several simultaneous maturity levels.

An alpha missile guidance system can be:

- researched;
- physically built;
- testable;
- dangerous;
- unreliable;
- politically controversial;
- unavailable for mass production.

That is more interesting than a binary unlock.

## 6. Cut sheets

Cut sheets are the commander-facing projection of deeper data.

The commander should see:

- what the capability is for;
- what it enables;
- what it consumes;
- what it requires;
- what can stop it;
- what the organization is currently doing about those requirements.

Deep technical information remains available through the same underlying record.

## 7. Crew and procedures

People are not modifiers attached to machines.

A crew position is a physical/procedural assignment.

`Qualification → Procedure → Ideal Gesture → Execution Variance → Physical Result`

Experience narrows execution variance.

Timing matters as well as spatial accuracy:

`Ideal Gesture + Ideal Time → Quality of Execution`

This is why a reactor watchstander taking a reading three minutes late can matter.

## 8. Failure is data

Failures should identify their causes.

Examples:

- fuel starvation;
- power distribution failure;
- sensor calibration drift;
- missing qualification;
- maintenance backlog;
- stale targeting data;
- weather;
- damaged communications;
- incorrect configuration;
- insufficient tooling;
- overloaded logistics route;
- human procedural error.

The simulation should expose the causal chain rather than report only **Capability Unavailable**.

## 9. Demonstration becomes history

Research records and demonstrations are first-class world events.

A failed prototype can become:

- a research observation;
- a maintenance requirement;
- a training lesson;
- a political argument;
- a procurement decision;
- a redesign;
- a historical event.

A successful test can accelerate adoption.

A spectacular failure can delay it.

The world remembers.

## 10. Why the FSM matters

The authored records describe **what exists**.

FSMs provide the executable relationships that determine:

- what happens next;
- what is waiting;
- who is responsible;
- what procedure is being performed;
- what state changed;
- what dependency failed;
- what recovery is possible;
- when the next action is due;
- what history should be recorded.

That is the distinction between a database of science-fiction objects and a living universe.

> **The data describes the universe. The FSMs make it behave.**
