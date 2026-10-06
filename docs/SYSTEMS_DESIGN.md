# raWWar — Systems Design

Status: Living systems design.

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

Potential properties include condition, power, compatibility, qualification requirements, maintenance requirements, ownership, modifications, and operational capability.

## Economy
Personal and organizational economies must eventually be defined separately.

The arcade and personal customization can use raWWar digital currency. Persistent-universe economics must eventually account for the actual cost of operating the universe.

## Rules and adaptation
Military rules can become observable procedures. A failure may cause an organization to establish a new rule, provided the design eventually defines who has authority to create, modify, revoke, and enforce that rule.

## Persistence
Persistent universes need an authoritative state model capable of surviving player absence and reconstructing appropriate detail without making rendering the source of truth.

## Design discipline
No system becomes canon merely because it was convenient to implement.
