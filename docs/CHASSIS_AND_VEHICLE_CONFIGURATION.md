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


## 2.1 The Chassis Pattern Is Reusable

The chassis model should not be considered vehicle-only.

The deeper Workshop abstraction is:

> **A chassis exposes physical interfaces. Components occupy those interfaces. Research can change the chassis. Configuration creates capability.**

An exoskeleton therefore follows the same architecture as a vehicle chassis, at a different physical scale.

- vehicle chassis → crewed machine;
- exoskeleton chassis → worn machine;
- future platforms may use the same pattern.

This gives raWWar one coherent configuration model instead of separate bespoke systems for every kind of machine.

## 2.2 Sockets and Expandable Configuration

A chassis exposes typed **sockets** rather than an arbitrary list of inventory slots.

A socket is a physical capability interface. Depending on the chassis, it may provide:

- mechanical mounting;
- structural attachment;
- power;
- cooling;
- data/control;
- fluid or environmental interfaces;
- ammunition/feed interfaces;
- specialized mission interfaces.

A component can occupy a socket only when its requirements are satisfied.

A chassis generation can also be upgraded to expose more or better sockets.

For example:

`Original Chassis → Research → Structural Upgrade → Additional Sockets → New Configuration Envelope`

This is deliberately different from giving the player an invisible "+2 equipment slots" bonus. The physical chassis has changed.

That change may require:

- research;
- redesigned components;
- tooling;
- factory modification;
- materials;
- installation;
- qualified personnel;
- testing;
- maintenance support.

Thus a larger configuration envelope is itself a manufactured capability.

## 2.3 The Configuration Envelope

The **configuration envelope** is the set of configurations a particular chassis can legally and physically support at a given point in technological and organizational development.

It is constrained by:

`Chassis → Sockets → Mass → Volume → Power → Cooling → Structure → Interfaces → Research → Production → Qualification → Readiness`

This gives the player meaningful engineering choices without requiring every vehicle to be hand-authored as a completely unique object.

The same chassis can become multiple operational vehicles because its valid configuration changes.

The same principle applies to an exoskeleton.

## 2.4 Deterministic Socket Compatibility

Compatibility should be data.

A socket definition describes what it can accept.

A component definition describes what it requires.

The configuration system resolves whether they are compatible.

Given the same chassis generation, socket definitions, component definitions, research state, production state, and configuration choices, the same configuration should be legal.

> **Research determines what can exist. Production determines what can be built. Sockets determine what can be attached. Qualifications determine who can use it. Readiness determines whether it can operate now.**

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
