# raWWar

**raWWar is an Experience, not an application.**

It is a fully first-person, fully VR war Experience in which the soldiers themselves are the stars.

A player may live the war as an individual soldier, grow into leadership, command an organization, participate cooperatively, fight other players, or enter a massively persistent universe whose history continues whether they are present or not.

## What this repository is

This repository has two jobs:

1. capture what raWWar is intended to become as a coherent Game Design Bible; and
2. build the Experience data and MicroBundle shape that can eventually be manifested by AnyApp, WebApp, and future VR manifestations.

The old legacy project shell has been removed. There is no engine-specific project, scene format, or engine asset pipeline here.

## The first design truth

> **The soldiers themselves are the stars of this game.**

The game is always experienced from the first-person perspective.

Players can begin as a Soldier, Officer, or Cooperative participant and eventually enter FFA, Team-vs-Team, Persistent Multiplayer, and Massively Online Fully Persistent Universes.

The game should not force every player into the same definition of fun.

A player might qualify to operate a weapon system, accept missions, march with their unit, repair vehicles, conduct research, construct buildings, transport materials, fly aircraft, crew spacecraft, operate weapons or tactical stations, command a ship, command an army, or simply spend downtime socializing and playing diegetic games.

These are intended to be different ways of inhabiting the same war.

## Persistence

The long-term vision includes persistent universes that begin from a common premise but develop independently.

Players can join an ongoing universe at any point. If they lose their position, they can return as a new member of another faction.

Once actual storage, simulation, bandwidth, persistence, and usage costs are measured, the business vision is to return approximately 25% of raWWar revenue toward sustaining persistent universes.

A future concept under consideration is the existence of extremely difficult-to-find pocket dimensions that could allow rare crossings between otherwise separate universes. This is currently a candidate idea, not canon.

## Architecture

The intended relationship is:

raWWar Experience
    |
Experience Manifest
    |
MicroBundles
    |
FSM_COS
    |
AnyApp / WebApp / future VR manifestations
    |
Workshop Renderer
    |
first-person VR

The Experience owns the meaning and requirements of raWWar.

Hosts do not define the game. The manifest describes what is required. MicroBundles provide independently composable capabilities. FSM_COS composes them. The manifestation supplies the appropriate observation, interaction, rendering, and local services.

## Documentation

The authoritative design documentation lives under docs/.

- Game Design Bible
- Vision and Pillars
- Player Roles
- Persistent Universes
- Open Questions

These documents distinguish canon, candidate design, and experiments so implementation never silently becomes game design.

## Development principle

We are going to discover raWWar before we fully build raWWar.

The creator's descriptions are primary source material. The repository should help organize those ideas, expose contradictions, identify missing rules, and turn the resulting design into a deterministic Experience shape.

**Make the soldiers matter. Make the war alive.**
