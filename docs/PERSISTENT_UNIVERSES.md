# raWWar — Persistent Universes

## Current vision

raWWar ultimately supports massively online, fully persistent universes.

Each persistent universe begins from a common premise, develops independently, remains active as players enter and leave, accepts new players at any point, and records the consequences of player activity.

A player who loses their position can return as a new member of another faction rather than simply restoring the previous state.

## Economic sustainability

The intended business model includes returning approximately 25% of raWWar revenue directly toward operation of persistent universes once actual infrastructure costs are understood.

This is a design and business target, not yet an accounting rule.

The design must eventually measure persistent storage, simulation cost, bandwidth, database operations, artifact storage, backup and recovery, monitoring, moderation, and compute required while players are absent and present.

The economics should be derived from measurements rather than guesses.

## Persistence model

A key design question is how much of the universe must remain continuously simulated.

We will investigate always-live simulation, event-driven simulation, deterministic reconstruction, tiered persistence, player-proximate high detail, background abstraction, and archival history.

The Renderer and simulation architecture should cooperate here without allowing rendering requirements to define the simulation.

## Cross-universe interaction

Separate persistent universes are not automatically connected.

A candidate concept is the pocket dimension:

- extremely difficult to discover;
- capable of providing a route between otherwise separate universes;
- potentially allowing rare cross-universe interaction.

This remains candidate design, not canon.


## Galactic persistence

A persistent universe is a galaxy, not merely a persistent server containing one battlefield.

The Empire can span many star systems, and worlds can change ownership as the war develops.

The persistent state therefore needs to represent relationships between:

- star systems;
- worlds;
- factions;
- Imperial authority;
- resources;
- fleets;
- facilities;
- organizations;
- individual players and soldiers.

A player entering an established universe should be entering a history already shaped by other players.

## Warp-time persistence

Warp drives require days of initialization and preparation.

Persistent simulation must therefore be capable of representing long-running operations such as:

- ships preparing for warp;
- fleets waiting for readiness;
- orders traveling through organizations;
- worlds changing strategic condition;
- resources being accumulated or depleted;
- military operations unfolding while individual players are elsewhere.

The universe should not need a player to remain physically present for time-dependent strategic consequences to exist.

## Player actions can alter the Empire

The player's actions are not isolated campaign outcomes.

A successful conquest can alter ownership, resource access, faction strength, Imperial revenue, military readiness, and future opportunities.

Refusing Imperial orders can likewise change political relationships and the player's standing.

The persistent universe is therefore both the setting and the historical record of player decisions.

