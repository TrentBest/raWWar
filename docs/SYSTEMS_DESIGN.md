# raWWar — Systems Design

**Status:** Living systems design; intentionally separate from implementation code.  
**Owner:** raWWar Experience design and engineering.  
**Audience:** Designers and engineers translating the GDD into testable domain systems.  
**Evidence rule:** Proposed models and ideal behavior are design intent unless a linked source/test demonstrates them.

## Purpose
This document describes the systemic building blocks required to make the GDD possible. It is deliberately separate from implementation code.

## Core systemic model
raWWar is expected to emerge from large amounts of interconnected data:
- entities;
- roles;
- qualifications;
- requirements;
- procedures;
- resources;
- rules;
- relationships;
- providers;
- observations;
- events;
- state;
- permissions;
- consequences.

FSM_API supplies behavior and state transitions. It should not be replaced by bespoke occupation-specific state machines scattered through the Experience.

## Gestures

Gestures provide the physical execution layer between an entity's intent and its visible motion.

An ideal Gesture consists of poses and the mathematical transition between those poses. The same Gesture can be applied to a population, then individualized from each entity's stable seed, statistics, current state, and context.

This creates coordinated but non-identical motion. Individualization should remain bounded and purposeful rather than becoming uncontrolled random noise.

## Gesture Providers

A world capability should own knowledge of its own physical interfaces. A vehicle MicroBundle can expose a Gesture Provider for entering a cockpit, climbing into a hatch, operating a console, or other vehicle-specific procedures.

The soldier can therefore reason in terms of a need — such as "enter cockpit" — rather than knowing the implementation details of every vehicle.

The general relationship is:

1. Soldier or procedure identifies a need.
2. Environment identifies a compatible interaction point.
3. The target capability provides the appropriate Gesture/FSM.
4. The soldier moves into the required starting position.
5. FSM_API governs the physical procedure.
6. Completion produces the next state.

Walking to the interaction point remains generic movement; the domain-specific physical interaction belongs to the target capability.

## Individual agency
An individual soldier should not need omniscient knowledge of the entire war.

Providers can expose the information relevant to a soldier's current situation:
- where they should be;
- what they should be doing;
- what they need;
- what has changed;
- what procedure applies.

A human player or autonomous participant can then follow that "shadow" of expected behavior.

The same principle can support subtle interaction guidance: a control may visibly indicate that it wants to be operated without a conventional tutorial overlay.

## Interdependent procedures
Complex operations should emerge from relationships between independently meaningful states.

Examples include:
- aircraft pre-flight;
- launch-pad staffing;
- ground-crew safety;
- maintenance;
- formation movement;
- road guards;
- construction;
- laboratory procedures;
- command handoff.

## Qualification system
Qualification is expected to combine:
- training;
- certification;
- permissions;
- role access;
- equipment access;
- organizational need;
- rank/status requirements.

The final qualification graph is not yet defined.

## Readiness
Readiness should be derived from real requirements rather than a single arbitrary percentage.

Candidate inputs include staffing, qualifications, training, fatigue, maintenance, supplies, equipment state, and current assignments.

## Morale and command presence
Morale should be systemic rather than a simple commander's aura.

The commander's physical presence can affect the organization through leadership visibility, reputation, recent events, speeches, treatment of personnel, operational success, and direct interaction.

Military responses should depend on current duty and criticality. For example, personnel performing safety-critical work should not be forced to interrupt that work merely to perform ceremonial behavior.

## GPU population state

The long-term Renderer can represent large populations using compact state textures. A candidate layout stores four independent 8-bit state values in each 32-bit pixel, allowing compute shaders to operate on only the channels required by a particular operation.

This is a candidate implementation representation, not yet a frozen renderer contract. Its purpose is to make enormous populations tractable while keeping authoritative meaning outside the rendered pixels.

## Living-world simulation
The simulation should distinguish:
- high-detail observed behavior;
- nearby active simulation;
- background organizational simulation;
- deterministic reconstruction;
- persistent historical state.

The final fidelity model remains an engineering/design question.

## Personal equipment
Equipment should have state and relationships, not just appearance.

Equipment quality is part of gameplay, not merely cosmetic progression.

Potential properties include:

- condition;
- research generation;
- production quality;
- power;
- comfort/ergonomics;
- sensor/camera quality;
- communications quality;
- compatibility;
- qualification requirements;
- maintenance requirements;
- ownership;
- modifications;
- acquisition cost;
- operating cost;
- reliability;
- operational capability.

A useful systemic distinction is:

**research defines what can be built → production defines what can be fielded → ownership/use defines what the individual experiences.**

A more expensive exoskeleton can improve comfort and performance, while better cameras and sensors can improve the information presented to the operator.

Observation equipment also affects remote squad feeds. A terminal operator sees through an actual sensor/communication/display chain rather than receiving an omniscient game camera.

## Economy
Personal and organizational economies must eventually be defined separately.

The arcade and personal customization can use raWWar digital currency. Persistent-universe economics must eventually account for the actual cost of operating the universe.

## Rules and adaptation
Military rules can become observable procedures. A failure may cause an organization to establish a new rule, provided the design eventually defines who has authority to create, modify, revoke, and enforce that rule.

## Observation-relative computation

Simulation fidelity and presentation fidelity should not be uniform across the entire world.

Event horizons can determine how much Gesture detail, pose evaluation, and rendering computation is justified by what the player can observe. Nearby entities can receive richer individualized motion; distant populations can use cheaper representations while preserving the visual evidence of organized life.

The optimization target is not "simulate everything equally." It is "preserve believable observed behavior at the appropriate cost."

## Persistence
Persistent universes need an authoritative state model capable of surviving player absence and reconstructing appropriate detail without making rendering the source of truth.

## Design discipline
No system becomes canon merely because it was convenient to implement.


## Training capacity and organizational bottlenecks

Training capacity is a real resource in the organization.

A standard barracks contains four VR training modules and can therefore train one four-person squad at a time. This makes barracks a production bottleneck for **human capability**, not merely a housing requirement.

A larger vehicle or facility that requires multiple specialized squads must have a corresponding number of trained squads available. The requirement propagates recursively:

```text
Capability
  → required crew
    → required squads
      → required qualifications
        → required training
          → required barracks capacity
            → required construction / staffing
```

Construction participates in the same dependency graph. A building may require a qualified construction crew; an advanced building may require more advanced construction qualifications; those qualifications require training capacity.

The organization therefore grows through physical infrastructure and human infrastructure together.

## Diegetic management and decisions

The management model should preserve the useful decision density of an RTS while removing the requirement that the player operate an abstract management screen.

The authoritative state remains structured data. The presentation of that state can be physical.

For example:

- an advisor explains a research opportunity;
- the administrator provides the current program state;
- prerequisites and consequences are communicated;
- a tablet presents the finite choices;
- the player authorizes or rejects the proposal;
- the resulting decision becomes authoritative state;
- FSM procedures propagate the consequences through staffing, training, construction, research, and production.

The tablet is a **diegetic control surface**, not a replacement for the underlying data model.

This gives the Experience both things it needs:

1. deterministic, machine-readable choices;
2. first-person physical presence.

## Research facility lifecycle

A research facility is itself a capability network.

Conceptually:

```text
Headquarters underground office
        ↓
Administrator / research management
        ↓
Research program
        ↓
Required personnel + qualifications
        ↓
Research facility
        ↓
Experiment / prototype
        ↓
Scheduled demonstration
        ↓
Commander observes
        ↓
Authorization / rejection
        ↓
Production opportunity
        ↓
Field deployment
        ↓
Field experience
        ↓
New research questions
```

The player should be able to physically visit the facility and observe the research operating.

A facility that cannot be staffed or supported is not magically productive because it exists.

## ProtocolAi and future conversational control

A future conversational layer is a **Candidate**, not a current implementation requirement.

The desired boundary is:

```text
Player behavior / intent
        ↓
ProtocolAi
        ↓
deterministic semantic representation
        ↓
available capabilities / legal choices
        ↓
FSM_COS / FSM procedures
        ↓
authoritative state
```

A future LLM may sit above this boundary as a language/reasoning interface, including for advisors or enemy leaders. It should not be the authority that invents game actions.

ProtocolAi can provide the semantic bridge that allows a language model to understand what the player is doing or asking while restricting the resulting action to choices the Experience actually exposes.

The long-term possibility of a Workshop-native LLM built on the same technology is explicitly deferred. It is **not required for the current raWWar architecture**.

The deterministic rule is more important than the model:

> **The model may interpret intent. The Experience decides what can actually happen.**

