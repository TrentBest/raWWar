# raWWar — Content Authoring Laboratories

**Status:** Living design model.

This document defines how raWWar should grow from system catalogues into individual content records.

## Why folders matter

The master GDD explains the game. The capability catalogues explain relationships. Individual content records let us become obsessive about the things the player can actually touch, drive, fly, build, crew, repair, destroy, survive, and remember.

The working content tree is:

- Vehicles/
- Structures/
- Soldiers/

More families can be added later: Equipment, Weapons, Research, Factions, Worlds, Facilities, and so on.

## The same game at every scale

raWWar has two deliberately different viewpoints over one authoritative world.

### Commander

The Commander expresses intent and observes organizational consequences:

- build a base;
- train more soldiers;
- establish a factory;
- keep a sector supplied;
- stage an invasion;
- reinforce a defensive position;
- research a technology;
- move a fleet.

### Soldier

The Soldier inhabits the consequences:

- attend boot camp;
- receive equipment;
- qualify for a vehicle;
- fly a starfighter;
- load ammunition;
- repair a tank;
- fight in a fleet action;
- survive a vehicle loss;
- become a crew member;
- earn another qualification;
- eventually command.

These are not separate simulations. They are different manifestations of the same state, procedures, relationships, resources, and consequences.

## Intent is the control language

The player should generally state what they want rather than manually execute every logistical transaction.

For example:

> Keep the eastern anti-air network operational.

The organization then has to make that true through actual capabilities:

storage → missiles → transport → qualified loading crew → launcher → qualified crew → maintenance → readiness

If missiles run out, the launcher cannot fire until missiles arrive. If the transport fleet is busy, delivery is delayed. If qualified crew are unavailable, readiness falls. The simulation exposes the consequence instead of inventing a resource.

This is deliberate abstraction, not simplification of the world.

## Resource depletion

Resources and consumables are physical state.

Examples:

- tanks carry finite ammunition;
- missile vehicles carry finite missiles;
- factories consume materials;
- aircraft consume their defined stores and operating resources;
- maintenance consumes parts and labor;
- construction consumes materials and qualified crews.

When a dependency is depleted, the dependent capability waits, pauses, degrades, or changes procedure according to its rules.

The player does not need to wait for an exact magic resource number before expressing intent. The organization can begin working and continue as far as the available resources permit.

## Survivors remain people

Vehicle destruction and personnel destruction are different events.

If a vehicle is destroyed and a crew member survives, that person retains their identity, history, qualifications, relationships, and experience.

They may be rescued, wounded, reassigned, retrained, promoted, or placed into another available role.

This creates a direct relationship between FPS survival and Commander-level organizational strength.

## Keyed structures

Many structures are instantiated with a capability key.

Examples:

- resource processing plant → resource/process;
- storage → inventory category;
- barracks → training program;
- factory → production family;
- research facility → research domain/program.

The key should be authoritative configuration, not merely a label painted on the building.

## Re-keying

Compatible structures may be re-keyed rather than rebuilt.

Current Candidate rule:

- one-quarter of original construction time;
- twice the normal construction cost.

Re-keying must still account for tooling, equipment, staff qualifications, inventory movement, and any capability-specific conversion procedure.

## Content-record rule

Every individual content record should answer enough questions to make the thing real:

1. What is it?
2. Why does it exist?
3. What can it do?
4. What does it require?
5. Who operates it?
6. What qualifications are required?
7. What does it consume?
8. What produces or supplies it?
9. What can prevent it from operating?
10. How is it researched?
11. How is it manufactured or constructed?
12. How does the player experience it in first person?
13. How does the Commander experience it?
14. What happens when it is damaged or destroyed?
15. What survives when the thing itself is lost?
16. What can it become through research/configuration?

The point is not to produce bureaucracy. The point is to make the imagined thing sufficiently concrete that implementation can begin without inventing its fundamental behavior.

## raWWar: unfiltered war

The title should mean something in the world.

War leaves evidence.

Destroyed vehicles remain destroyed. Ammunition is consumed. Wreckage can remain. Survivors carry their histories. Casualties can remain visible where the experience and content rules permit. Logistics matters because things do not magically reappear.

Exact brutality, gore, corpse persistence, content ratings, and presentation boundaries remain Illumination Needed.

> **raWWar is war without pretending that the machinery of war is consequence-free.**
