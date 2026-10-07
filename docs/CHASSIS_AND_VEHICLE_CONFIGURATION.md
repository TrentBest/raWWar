# raWWar — Chassis and Vehicle Configuration

Status: Living design model.

> **A vehicle begins with a chassis. What can be attached to it depends on what the chassis and technology can support.**

## 1. Vehicle identity

Rather than treating every vehicle as a completely unrelated named asset, raWWar should model a vehicle as a configurable capability.

Candidate relationship:

    Chassis
      +
    Power
      +
    Mobility
      +
    Protection
      +
    Sensors
      +
    Communications
      +
    Weapons
      +
    Tools / Mission Modules
      +
    Crew Configuration
      =
    Operational Vehicle

The exact component taxonomy remains subject to refinement.

## 2. Chassis is a researchable capability

A chassis has physical limits.

Research can change those limits.

Examples:

- greater structural strength;
- lower structural mass;
- improved mounting points;
- greater power delivery;
- improved cooling;
- larger internal volume;
- improved suspension;
- improved thruster capacity;
- better stability;
- additional hardpoints;
- improved survivability.

Research can therefore change what can be affixed to a chassis.

A future chassis generation should not merely provide a larger number in a hidden stat table. It can physically permit a configuration that previously could not exist.

## 3. Affixable capability

Each chassis exposes candidate attachment points/capability interfaces.

A module can be attached only when its requirements are satisfied.

Potential requirements include:

- physical mounting;
- mass;
- volume;
- power;
- cooling;
- structural strength;
- control interfaces;
- crew qualification;
- research generation;
- production availability;
- ammunition/energy supply;
- maintenance capability.

Thus:

    Research chassis
    → new structural capability
    → new attachment becomes possible
    → research/production of module
    → factory assembly
    → qualified crew
    → operational configuration

## 4. Configuration is deterministic

The game should not randomly generate vehicle configurations.

A vehicle configuration is the result of available technology and deliberate player/organization choices.

Two identical chassis can therefore be configured differently because the organization selected different valid modules.

## 5. Heavy VTOL example

A heavy carryall VTOL may support:

- troop transport module;
- vehicle cargo interface;
- external vehicle sling/load system;
- additional armor;
- larger thrusters;
- countermeasures;
- improved sensors;
- communications;
- faster deployment hardware.

Research can expand the envelope:

    Chassis research
    → lift capacity
    → structural margin
    → larger cargo configuration
    → larger vehicle transport capability

At the same time, production and maintenance determine whether the configuration can actually be built and operated.

## 6. Configuration is not merely equipment

The assembled vehicle becomes a new operational capability.

It has:

- a configuration identity;
- component history;
- production quality;
- condition;
- maintenance state;
- assigned crew;
- qualification requirements;
- readiness;
- current mission;
- ownership/organizational assignment.

## 7. Research and configuration

Research does not automatically install anything.

Instead:

    Research reaches threshold
    → configuration becomes legal
    → player chooses configuration
    → production/tooling becomes necessary
    → vehicle is assembled
    → crew qualifies
    → vehicle becomes available

This keeps research, production, and use separate.

## 8. Deterministic configuration

Given the same:

- chassis generation;
- component definitions;
- research state;
- production state;
- configuration choices;

the same configuration should be legal.

There is no hidden random technology roll.

Variation may still exist in manufacturing quality or individual performance, but what technologies are available is deterministic.

## 9. Open design questions

Still to illuminate:

- exact chassis families;
- universal vs faction-specific interfaces;
- whether modules can be swapped in field conditions;
- how much structural modification requires factory time;
- exact hardpoint taxonomy;
- damage effects on installed modules;
- crew transfer rules;
- vehicle ownership rules.
