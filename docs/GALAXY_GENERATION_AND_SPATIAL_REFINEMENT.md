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
