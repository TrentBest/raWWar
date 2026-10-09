# Mapping the Milky Way: Scientific Reference, Procedural Expansion, and Player Legibility

**Status:** Candidate technical design. The creator's canon and later verified astronomical data take precedence. This document does not claim that the galaxy is fully mapped.

## The governing rule

**Keep the universe physically honest; make its representation understandable.** A map may simplify, declutter, magnify markers, or use a logarithmic scale. It must not silently move stars closer together, convert an artistic spiral into measured star positions, or confuse an unobserved object with an absent one.

The machine-readable contract is [`milky-way-reference-model.json`](../data/milky-way-reference-model.json). It separates four things that must never be conflated:

1. **Observed records** — measurements and catalogue entries with source, epoch, coordinate frame, uncertainty, and selection limits.
2. **Derived structure** — scientific inferences such as large-scale arm structure, which remain models with versioned assumptions.
3. **Procedural population** — stable, seed-generated stars and systems added because the known catalogues are incomplete for a galaxy-scale simulation.
4. **Presentation** — the map, HUD, labels, routes, and visual abstractions through which a player learns about the first three.

## What the scientific baseline says

- The Milky Way is a barred spiral galaxy with a stellar disk, central bulge/bar, gas and dust, and stellar halo. Its detailed spiral-arm structure is reconstructed from observations from inside the disk, not from a single external photograph.
- ESA's Gaia mission made more than three trillion observations of roughly two billion stars and other objects, measuring properties including positions, motions, brightness, temperature, and composition. That is an extraordinary survey, not a complete census of every star or planetary system. Gaia's next data release is expected in December 2026 according to the current mission page, so catalogue ingestion must be versioned and repeatable.
- The Solar System lies in the Local (Orion) Arm/Orion Spur, roughly 26,000 light-years from the Galactic Centre. Treat that as a broad cartographic anchor until we choose a coordinate frame and source-specific astrometry for exact positions.
- NASA gives the Sun's galactic orbit as roughly 230 million years; published values and models vary. This is a vital scale check: a 10,000-year campaign advances a simple circular orbital angle by only about 0.016 degrees. A visible fast-spinning galaxy would be a presentation fantasy, not the motion implied by that timescale.

Sources:
- [ESA Gaia Milky Way model and artist impressions based on Gaia data](https://www.cosmos.esa.int/web/gaia/milky-way)
- [ESA Gaia mission and data-release status](https://www.esa.int/Science_Exploration/Space_Science/Gaia)
- [NASA: The Sun and its galactic orbit](https://science.nasa.gov/sun/facts/)
- [NASA: Solar System and distance to the Galactic Centre](https://science.nasa.gov/learn/basics-of-space-flight/chapter1-1/)
- [NASA: Oort Cloud facts and scale](https://science.nasa.gov/solar-system/oort-cloud/facts/)
- [NASA Exoplanet Archive](https://exoplanetarchive.ipac.caltech.edu/)
- [SIMBAD](https://simbad.cds.unistra.fr/simbad/)
- [VizieR catalogue service](https://vizier.cds.unistra.fr/)

## Build a reference atlas, not a decorative map

### 1. Establish a coordinate frame and provenance

Every imported object needs a stable identity, catalogue/source identity, catalogue release, observation/reference epoch when available, coordinate frame, units, uncertainty, known selection effects, and transformation version. Different surveys have different coverage, sensitivities, epochs, and definitions. Cross-identification must preserve the original source records.

Use the Sun and Earth as stable, human-readable anchors. The Sun's galactic location is a coarse placement on the galactic atlas; exact local star/system positions must come from source-aware catalogues and a documented coordinate transformation. Do not make a single approximate distance pretend to be a complete three-dimensional coordinate.

### 2. Layer known structure and measured objects

Represent the central bar, bulge, stellar disk, gas/dust, halo, clusters, and spiral structure at their appropriate confidence. Use ESA/Gaia scientific visualizations as interpretive references. The artist image is valuable because we live inside the galaxy and cannot photograph the whole thing from outside; it is not permission to hard-code every bright streak as a physical boundary.

Catalogue-backed stars, known exoplanets, and other objects should be queryable by identity and location. Keep source releases and import manifests so a later catalogue update can replace or refine the reference dataset reproducibly.

### 3. Fill the gaps honestly

The game needs a galaxy-sized population. Known catalogues are incomplete and unevenly sampled, and confirmed exoplanet lists are not a complete list of all planetary systems. Add deterministic synthetic populations where data is missing. Mark every such object as generated and retain the generation model/version/seed.

The generator must not distribute stars uniformly, assign planets to every star by default, make every system habitable, or mistake a catalogue gap for empty space. At appropriate fidelity, it should account for radial and vertical gradients, population age/metallicity, clustered star formation, dust and survey selection effects, multiplicity, and uncertain planet occurrence. Parameters are candidate model inputs to validate—not facts to invent.

When better measured data arrives, it wins over a compatible placeholder, while the old generated assumptions remain in provenance for debugging and save migration.

## Time: stars orbit; the map does not spin as a prop

The runtime needs distinct motion models for:
- the Sun's galactic orbit;
- local stars' individual positions and velocities;
- the spiral-arm pattern, which is not the same thing as the orbit of each star;
- gas, dust, and clusters where the chosen simulation fidelity supports them.

For short campaign histories, use the selected model to calculate the actual small displacement. For millions or hundreds of millions of years, a static picture rotated backward is not a historical reconstruction. Use a declared Galactic potential or limited approximation, record its validity horizon and uncertainty, and distinguish a modelled reconstruction from a directly observed fact.

A historical galaxy view must always declare simulation time, coordinate frame, motion-model version, and whether the shown positions are observed, reconstructed, or illustrative. The world's authoritative positions never depend on the camera or render frame rate.

## Make astronomical distances legible

Real space is sparse. An asteroid field should not be packed like a cinematic obstacle course by default. Objects may be too small, too dim, or too far apart to appear together as visible bodies.

The navigation UI can solve the human-comprehension problem without falsifying geometry:

- Use a **distance-aware horizon** with object name, bearing, distance, uncertainty/range, relative motion when known, and route relevance.
- Distinguish **optically visible**, **sensor detected**, **catalogue known**, **predicted**, and **unknown contact**. A HUD marker is not a claim that the pilot can see the object with their eyes.
- Draw routes and travel corridors as overlays. Show actual distances and time estimates from the ship's capability and the route model.
- Allow logarithmic zoom, density layers, decluttering, labels, local insets, and enlarged screen-space markers. Keep a visible scale and never change the underlying world-space separation.
- Present confidence and provenance in the deep dive: measured position, inferred region, generated system, stale report, or uncertain historical reconstruction.

This makes a sparse universe navigable while preserving the feeling of scale. A player can see a bearing marker for an asteroid without seeing a second asteroid floating beside it.

## The Earth-to-galaxy bridge

The first atlas should support a clear drill-down:

**Milky Way → Local/Orion Arm → Solar neighbourhood → Solar System → Earth.**

The galaxy view marks the Solar System; the local view can show catalogued neighbours with true distances; the system view switches to planetary-scale units; Earth is the historical origin anchor. Each transition changes the coordinate scale and visible layers, not the physical facts.

That gives our long human history a place to begin. Later, campaign history can attach settlements, colonies, battles, abandoned systems, and successor civilizations to real or procedurally generated stellar locations without pretending those historical events are already astronomical data.

## Integration with raWWar's existing world model

The existing [Galaxy Generation Contract](../data/galaxy-generation-contract.json) provides stable semantic addresses, seed/event-keyed determinism, persistence rules, recursive spatial refinement, and observer-relative rendering. This Milky Way reference model is an evidence and population layer within that architecture—not a replacement for it.

- **Reference model:** what is measured, inferred, or unknown about the real galaxy.
- **Procedural universe:** how we expand that baseline into a stable, playable population.
- **Simulation:** how positions, motion, hazards, travel, history, and ownership evolve.
- **Renderer/HUD:** how the observer sees or learns about the world.

The renderer may cache and simplify the representation. It cannot become the authority for object identity, position, or history.

## First implementation sequence

1. Agree on the canonical Galactic coordinate frame and versioned transforms.
2. Import a small, reproducible, well-documented real-data slice around the Solar System before attempting a massive catalogue dump.
3. Add the Sun/Earth anchor chain and validate coordinate and distance conversions.
4. Implement source-aware object records and the measured / derived / generated distinction.
5. Build a deterministic local-neighbourhood expansion and validate its distributions against published surveys.
6. Add a galactic-scale model using Gaia-informed structure, with uncertainty rather than false precision.
7. Add the distance-aware horizon and scale transitions; test that presentation never mutates world-space distances.
8. Only then widen catalogue ingestion and procedural coverage, with data manifests, licensing review, validation, and repeatable builds.

## Open decisions

- Exact coordinate frame, transformation conventions, and astrometry library.
- Which catalogue release/subsets to ingest first and how to package them reproducibly.
- Storage and spatial indexes for large catalogues.
- Long-timespan galactic potential and spiral-pattern model.
- Procedural stellar and planetary population parameters and their validation data.
- The actual campaign chronology: millennia, hundreds of thousands of years, or millions of years. That decision changes how much galactic motion is physically meaningful.

**Design promise:** no artist's map will be mistaken for a survey, no generated star will be passed off as a measured one, and no cinematic convenience will silently rewrite the distances of the universe.
