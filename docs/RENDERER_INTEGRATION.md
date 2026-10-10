# raWWar — Renderer Integration

Status: Design foundation.

## Purpose

raWWar is intended to demonstrate the Workshop Renderer at the scale for which the architecture is being designed: large populations of complicated, individually distinguishable soldiers operating inside a living world.

The Renderer is not the game simulation. It is the observation and presentation system.

## Authoritative path

The conceptual path is:

**raWWar data**
→ **FSM_API state**
→ **presentation projection**
→ **GPU state**
→ **Gesture/pose computation**
→ **Renderer**
→ **observer**

The reverse direction is not assumed. A rendered pixel does not become authoritative merely because it exists on the GPU.

## Population rendering

A population of 10,000 soldiers should not require 10,000 conventional CPU animation controllers.

Instead, the Renderer should be able to operate over compact state and shared procedures.

An ideal Gesture can be shared across an entire population.

Individual identity then supplies bounded variation.

The GPU performs the repetitive arithmetic in parallel.

## State texture concept

One candidate representation uses four 8-bit channels in each 32-bit pixel.

Each channel can represent one value in the range 0–255.

This creates four compact state values per pixel.

A compute pass can operate on only the channel(s) required for its purpose.

This is currently an architectural hypothesis, not a frozen API.

## Gesture evaluation

A Gesture can be treated as a procedural flipbook:

- identify current pose;
- determine target pose;
- evaluate transition mathematics;
- advance according to progress/rate;
- apply bounded individual variation;
- resolve equipment/context constraints;
- produce presentation state.

The exact partition between CPU, GPU, and hybrid evaluation remains open.

## Event horizons

The same soldier need not be represented with the same fidelity at every distance.

Candidate behavior:

### Near

- richer pose evaluation;
- detailed equipment;
- individualized head/attention movement;
- physical interaction detail;
- higher-frequency state updates.

### Middle

- reduced pose complexity;
- fewer evaluated secondary details;
- coarser state updates;
- preserved identity and major motion.

### Far

- population-level motion;
- cheaper pose representation;
- reduced update frequency;
- silhouette and formation behavior dominate.

The transition must preserve continuity rather than visibly switching between unrelated animation systems.

## Complicated soldiers

The goal is not to make distant soldiers anatomically simple everywhere.

The goal is to make **the right complexity available at the right observation distance**.

A soldier close to the observer may expose:

- exoskeleton structure;
- armor modules;
- weapons;
- tools;
- ribbons and insignia;
- damage;
- personal modifications;
- nuanced Gesture motion.

That same soldier at distance may only require a recognizable silhouette, equipment profile, faction treatment, and appropriate movement.

## Living-world requirement

Event horizons must not turn the world into frozen scenery.

When the observer looks toward a distant launch pad, the Renderer should preserve enough motion and state change to communicate active operations.

Optimization therefore asks:

> What information must remain visible for this part of the world to feel alive?

not merely:

> How many objects can be deleted from the simulation?

## Renderer and AnyApp

AnyApp is the primary host for raWWar.

The host provides the runtime environment and heavy processing.

The Renderer remains a presentation subsystem.

This separation allows the same Experience to have other manifestations without making the game depend on a particular graphics API or device.

## Design principle

**Render what the observer needs to believe the world, not everything the world happens to contain.**
