# raWWar — Galaxy Generation, Cosmic Context, and Spatial Refinement

Status: Living world-generation architecture.

> **The universe is the address space. The seed determines what occupies it. Physics constrains what can happen. The simulation reveals detail only where it is needed.**

## 1. The anchor: universe cell 42

Begin with a universe represented as a (10 \times 10 \times 10) grid: 1,000 first-level cells. The raWWar galaxy occupies **the 42nd cell** in a stable, documented ordering. This is a deliberate address, not a claim that the galaxy is physically at the center of the real universe.

The coordinate convention must be canonical and shared by generation, persistence, rendering, networking, and tools. Use integer cell coordinates and a documented linearization rule; never let an implementation's array order silently define cosmic geography. If we use one-based ordinal 42, its coordinate is derived from that rule and is not an independent magic constant.

The anchor gives every observer and system a stable frame of reference. It also makes the surrounding cells meaningful: the galaxy exists inside a larger cosmic neighborhood rather than floating in an empty skybox.

## 2. Build the cosmic context before the playable galaxy

The generation process has two distinct products:

1. **Baked cosmic context:** the broad universe surrounding the galaxy—sister galaxies, large-scale structures, intergalactic distances, and other far-field context. Generate this deterministically, apply the chosen large-scale physical approximations, and bake its presentation and compact descriptive data. This is stable background context shared by games when the cosmic-context version is unchanged.
2. **Unresolved galaxy region:** reserve the 42nd cell for a galaxy that is deliberately left undefined in the bake. Do not accidentally bake in a canonical star map that every new game inherits.

Conceptually:

```text
10 × 10 × 10 universe cells
        │
        ├── generate and approximate cells outside 42
        │      └── sister galaxies + large-scale cosmic context
        │              └── bake stable far-field representation
        │
        └── cell 42: RESERVED / GALAXY NOT BAKED
                       │
                       └── new game seed generates its galaxy
```

The baked context is not necessarily a fully simulated universe. It is a stable, physically motivated description at the resolution needed for its role. Distant structures may be represented by aggregate data, fields, or rendered projections. They need not carry individual high-detail stars unless a later capability specifically requires that.

## 3. Project the surrounding universe around the anchor

The cosmic context should be assembled into the six directions around the anchor cell, with consistent coordinates, scale, lighting assumptions, and boundaries. It should read as a continuous surrounding universe—not six unrelated sky images.

The generator works in canonical universe coordinates. Rendering is a projection of those coordinates relative to the observer and the 42nd-cell anchor; rendering must not become the source of truth. The same source data should support different viewpoints and display resolutions.

The words “render back to the center” describe the presentation goal: the player experiences the galaxy as the local focus, while the baked universe surrounds it. They do not require moving or distorting the actual cosmic coordinate system to make the camera happy.

## 4. New-game seed: the galaxy is discovered, not selected from a static map

When a new game begins, the player may supply a seed. That seed combines with canonical spatial coordinates and generation-version identifiers to determine the galaxy's initial state.

This retains the appeal of classic 4X exploration—unknown positions become meaningful discoveries—without relying on a fixed star map whose contents are merely randomized after placement. A coordinate is an address into a deterministic generation process, not a lookup into a pre-authored list of stars.

The same seed and generation version must reproduce the same initial galaxy regardless of:
- which region is visited first;
- whether generation runs on CPU or GPU;
- whether the player travels directly to a location or discovers it gradually;
- the order in which neighboring cells are requested;
- whether a client or server independently reconstructs the same initial region.

Generation version is part of the reproducibility contract. If the algorithm changes, old saves must retain their original interpretation or undergo an explicit migration.

## 5. Coordinate-addressed deterministic randomness

Use a faithful, locally implemented Squirrel3-style integer hash/random function as a deterministic building block. The implementation must be tested against known reference vectors and documented as a versioned part of the content-generation contract. Do not silently substitute `System.Random` or depend on call order.

A generated feature should derive its random stream from its **semantic address**, for example:

```text
world seed
+ generator version
+ universe cell (x, y, z)
+ nested subdivision path
+ feature domain
+ feature ordinal
→ deterministic random value(s)
```

The feature domain separates unrelated decisions. Changing the algorithm that chooses asteroid composition should not reshuffle the names of stars or the orbit of a planet merely because an extra random number was consumed elsewhere.

Prefer stateless coordinate hashing or independently keyed substreams over one global mutable random stream. This makes generation order-independent, reproducible, parallelizable, and suitable for on-demand expansion.

Squirrel3 is a deterministic hash-style pseudo-random generator, not a source of physical laws or cryptographic security. It chooses reproducible variation; the physical and authored rules determine whether the resulting arrangement is coherent.

## 6. Recursive 10 × 10 × 10 spatial refinement

Every cell can be subdivided into another (10 \times 10 \times 10) set of child cells, each one-tenth the parent's linear dimensions. Each child has an integer local coordinate and a path back to its ancestors.

```text
Universe
└── 1,000 first-level cells
    └── selected cell
        └── 1,000 child cells
            └── selected child
                └── 1,000 further cells
                    └── ... continue as needed
```

The hierarchy can continue as far as the domain and numerical representation allow: galactic regions, star systems, stellar neighborhoods, stars, planets, moons, asteroids, surface regions, structures, and smaller physical components. This is a spatial addressing and refinement model, not a promise that every level uses the same physics or that infinite numerical precision exists.

Each level must define:
- parent-relative and canonical coordinates;
- scale and units;
- boundaries and child addressing;
- generation domain and version;
- physical model appropriate to that scale;
- what is stored, what is derived, and what may be regenerated;
- how child state aggregates into parent summaries.

Avoid using floating-point world position as the only identity. Stable integer paths or equivalent exact address keys prevent precision loss and make distant objects reproducible. Convert to observer-relative floating-point coordinates for simulation and rendering when appropriate.

## 7. Deterministic generation is not a substitute for physics

The seed supplies reproducible initial variation. It does not make every random arrangement physically plausible.

Generation should proceed through constraints and dependencies. For example, star properties constrain plausible system environments; stellar radiation and gravity affect orbital conditions; planetary composition and formation history constrain later properties; local geology and environment constrain resources. The exact models may be approximations, but their assumptions should be explicit and internally consistent.

A useful conceptual pipeline is:

```text
seed + spatial address
        ↓
deterministic candidate properties
        ↓
domain constraints and physical approximation
        ↓
coherent generated structure
        ↓
compact authoritative description
        ↓
on-demand detail and presentation
```

Use the appropriate model for the question. A galaxy-scale generator does not need to integrate every orbit at fine time steps. A focused orbital simulation may need much greater fidelity. Approximation is a deliberate resolution choice, not permission for contradictory outcomes.

## 8. Generation, baking, discovery, and persistence are different operations

| Operation | Responsibility |
|---|---|
| Generate | Derive initial content from seed, address, version, and constraints |
| Validate | Reject or repair invalid combinations and record the rule applied |
| Bake | Store stable far-field representations and expensive reusable results |
| Discover | Decide what the player or faction has observed and learned |
| Simulate | Advance authoritative state and produce consequences |
| Persist | Save changes that must survive unloading or reconstruction |
| Render | Project current state for an observer; never define authoritative existence |

A generated star may exist before any faction discovers it. Discovery is player/faction knowledge, not the creation of the star. Likewise, a rendered star is a view of a world record or deterministic generated result, not the world record itself.

## 9. What happens when the player enters a region?

An unresolved region can be reconstructed from its address and generation version, then expanded to the fidelity required by the current event horizon.

```text
unvisited address
    → deterministic generation
    → constraints / validation
    → coarse regional summary
    → focused expansion when needed
    → simulation and persistent consequences
    → compact summary when attention moves away
```

Once a generated region has been affected by play, it is no longer safe to regenerate it as though nothing happened. Persist authoritative changes: ownership, discoveries, battles, destroyed or built assets, depleted resources, faction decisions, and other consequences. Immutable seed-derived properties can be recomputed; mutable history must be retained or represented by a lossless-enough authoritative record.

> **Regenerate what is immutable. Persist what the galaxy has lived through.**

## 10. Physics and deterministic randomness across scales

A consistent seed/address contract lets us create content on demand without precomputing every object. It also enables tests such as:

- request the same cell in different orders and compare results;
- generate the same region on different machines and compare canonical data;
- refine a parent and verify child bounds and coordinates;
- verify that sibling feature domains do not perturb one another;
- check that physical constraints hold across generated systems;
- compare baked context against source-generation metadata;
- load, unload, and reconstruct a modified region without losing its history.

For cross-platform reproducibility, specify integer widths, overflow behavior, byte order where serialized, coordinate encoding, and numeric tolerances for physical calculations. Integer hash results can be bit-identical; floating-point physics may require a separately defined determinism contract.

## 11. Relationship to simulation scale and event horizons

The spatial hierarchy and the simulation hierarchy complement each other, but they are not the same thing.

- **Spatial hierarchy:** where something is and how its region subdivides.
- **Simulation horizon:** how much detail and how many updates are justified now.
- **Knowledge state:** what each actor has discovered.
- **Persistence state:** what has changed and must not be forgotten.

A remote star system may have a precise deterministic address but only a coarse strategic summary while distant. A nearby asteroid may be expanded into detailed geometry and physical interactions. A remote battle can remain causally active without every soldier running at the same frequency as a soldier on screen.

See [Simulation Scale and Memory Model](SIMULATION_SCALE_AND_MEMORY.md).

## 12. Decisions to preserve as explicit configuration

The architecture establishes the direction, not arbitrary final constants. Keep these as versioned configuration or implementation decisions:

- the canonical ordering and coordinate mapping for the 1,000 universe cells;
- physical dimensions and unit conventions for each hierarchy level;
- the exact galaxy-generation domain and its relationship to cell 42;
- the Squirrel3-compatible implementation and test vectors;
- generator versions and migration rules;
- physical approximations used at each scale;
- baked-context formats and visual projection strategy;
- which generated properties are immutable versus persistent;
- deterministic guarantees for CPU, GPU, and distributed execution.

The anchor cell is fixed by design. The contents of the galaxy are not fixed until the seed and generation rules produce them.

## Central principle

> **One stable cosmic address space. One reproducible generation contract. Many levels of physical detail. A galaxy that is generated for each new game—and a universe that remembers what happens after play begins.**


## 13. A cell is a stable address, not a rerolled scene

The player-facing invariant is simple: **look away, travel away, return later—the same address still describes the same place.** Camera movement, scrolling, zoom, loading, unloading, or visiting neighboring cells must never reroll a star system or move it merely because it was requested in a different order.

The generator is a deterministic function of an address and a generation contract:

```text
InitialCell = Generate(worldSeed, generatorVersion, canonicalCellAddress)
```

Within that cell, each generated feature receives its own semantic address. A star, orbit, planet, moon, asteroid belt, or sister galaxy is not identified by the order in which code happened to discover it. It is identified by its stable address and feature domain.

Static does not mean frozen for all time. It means **initial identity and placement are stable**. A planet's orbit, a ship's trajectory, a battle, a factory, and a faction can change when simulation time advances and their authoritative rules say they should. Those changes are state transitions—not side effects of rendering or revisiting a cell.

Separate four things:

- **Existence:** deterministic generated identity and initial properties.
- **Observation:** what the player can currently see.
- **Simulation:** what changes as time advances.
- **History:** what must remain true after unloading and reconstruction.

The renderer may cull, simplify, or reproject an object. It must not create a new object identity or change the authoritative state.

## 14. Time-ordered probability: choose outcomes without losing reproducibility

“Time-ordered probability equation” is a useful working hypothesis for the simulation. It should not mean that every frame rerolls the universe. The model needs two layers:

1. **Deterministic initial conditions:** the seed and stable address determine the same initial candidate state.
2. **Ordered state transitions:** at simulation time (t), an FSM evaluates the current state, available actions, physical constraints, and event conditions. If several outcomes are possible, a deterministic random value keyed to that event may select among them.

A compact conceptual form is:

```text
S(t + Δt) = Transition(
    S(t),
    Δt,
    physical constraints,
    active FSMs,
    EventRandom(seed, address, event type, event ordinal, time key))
```

For a probabilistic event with conditional probability (p), map a stable event-keyed integer to a documented uniform interval and compare it with (p). The event key must identify *which decision at what logical time* is being evaluated. Do not use frame number or call order unless the simulation contract deliberately makes those authoritative: variable frame rates, parallel execution, and different observation orders would otherwise change outcomes.

For an event whose probability varies over time, define the time step and the conditional hazard or transition probability explicitly. In a simple discrete model, the chance of an event during step (k), conditional on its not having occurred earlier, is (p_k). The cumulative chance by step (n) is:

```text
P(event by n) = 1 - product(k = 1..n, 1 - p_k)
```

This is a modeling option, not a claim that every physical process is memoryless or follows this formula. Select the right model for the phenomenon. Deterministic hashing makes the sampled choices repeatable; it does not prove that the probability model describes nature correctly.

**FSMs provide the ordered state transitions.** They define when a decision becomes eligible, what state it reads, what outcome it commits, and what consequences follow. Randomness supplies a reproducible choice only where the model calls for one. A transition should be committed once for its logical event, not rerun because the camera moved or a region reloaded.

## 15. Sister galaxies are real destinations, not skybox decoration

The baked cosmic context includes sister galaxies outside cell 42. In a single-player game, the player may choose to pilot a starship toward one. We should not implement a magical invisible wall at the edge of the playable galaxy.

Instead, travel feasibility emerges from distance, route conditions, propulsion capability, and remaining resources. The outermost consequential object or region can provide a natural point at which the crew warns that the next leg is not currently feasible. The warning is advisory: the player remains in control and may continue, turn back, wait, refit, or accept the consequences.

A sample in-world warning:

> “Sire, the next navigational landmark is beyond our present jump budget. The route requires an estimated 1,240 jumps under this drive model; our current reactor reserve supports 86 at this load. We can proceed, but we cannot promise a return voyage.”

Those numbers are illustrative only. Runtime estimates must be computed from the actual ship configuration, route, drive model, load, fuel/reactant availability, and reactor condition—not copied from narrative text.

A useful feasibility calculation is:

```text
RequiredJumps = RouteCost(distance, obstacles, driveModel, shipConfiguration)
AvailableJumps = JumpBudget(reactorState, storedEnergy, fuel, load, safetyReserve)
Margin = AvailableJumps - RequiredJumps
```

The result informs the warning and the player's decision; it is not automatically a movement lock. If the player presses on, the simulation should continue to produce physical consequences: depletion, reduced power, inability to make another jump, drift, distress, or other outcomes supported by the ship's actual design. Do not guarantee any particular consequence unless the model establishes it.

The travel model must distinguish **a galaxy boundary** from **a cell boundary**. Crossing a recursive spatial cell boundary is an addressing operation, not a wall. Leaving cell 42 means leaving the galaxy's reserved generation region and entering the already-defined cosmic context or an explicitly expandable region. The player can travel into that context. If the destination needs finer detail, expand it deterministically from its own stable address and the relevant generator version.

For single-player, generation can happen locally on demand. The same seed/address/version contract still applies, and any player-caused changes to visited sister-galaxy regions must be persisted just like changes inside the home galaxy.

## 16. Invariants to test

- Requesting a cell in any order produces the same initial canonical data.
- Camera movement, zoom, culling, and reload do not reroll or relocate objects.
- Advancing simulation time changes only state authorized by simulation rules.
- Each probabilistic decision has a stable event identity and explicit logical time.
- Replaying the same saved state and ordered event stream reproduces the same outcomes within the documented determinism contract.
- Repeated evaluation of one already-committed event does not apply its consequence twice.
- Crossing a cell boundary does not itself create a travel barrier.
- Sister-galaxy travel warnings are calculated from current route and vessel state, and do not silently disable player movement.
- Unvisited cosmic context can be regenerated; visited and modified regions preserve authoritative history.
