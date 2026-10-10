# Navigation Frames, Chart Evolution, and the Equipment That Makes Them Useful

**Status:** Candidate design contract. Dates, specific technologies, and faction adoption remain open until creator canon establishes them.

## The core idea

A civilization does not simply discover a better map. It changes the *coordinate language* in which it understands travel—and must build, maintain, teach, distribute, and trust the equipment that makes that language operational.

Early charts naturally grow from humanity's lived reference: Earth and its immediate surroundings. Interplanetary operations make Earth an inconvenient universal origin because Earth itself moves around the Sun. Interstellar travel pushes navigation toward local stellar networks and eventually a shared galactic reference. A galactocentric atlas can become the strategic language of a galaxy-spanning civilization, while individual ships still navigate locally using system, stellar, inertial, and sensor-relative frames.

**Changing the origin changes the description, not the universe.** The Sun does not move because a chart is recentered. A route does not become shorter because its coordinates look simpler. New equipment and better observations can make a route safer or newly feasible; a new coordinate convention by itself cannot.

Machine-readable companion: [Navigation Reference Frames](../data/navigation-reference-frames.json).

## 1. One world, many coordinate frames

The simulation owns authoritative identity, state, motion, and time. Charts and ship computers are consumers of that world, with their own coverage, epoch, assumptions, uncertainty, and limitations.

Candidate frame families:

| Frame | Natural use | What it does not solve |
|---|---|---|
| Earth-local | Surface, launch, near-Earth operations | Earth is not a universal fixed origin |
| Earth-Moon | Lunar routes and local infrastructure | Does not scale to all planetary or stellar routes |
| Solar System | Planetary ephemerides and interplanetary logistics | Is not a practical galaxy-wide origin |
| Local stellar | Routes between nearby stars and regional networks | Regional datums and catalogues can disagree |
| Local/Orion Arm | Regional galactic logistics | Arm naming and geometry are model-dependent |
| Galactocentric | Compare positions and routes at galactic scale | Does not provide omniscience or remove local uncertainty |
| Campaign inertial/reference frame | Consistent simulation and historical reconstruction | Must have a declared epoch, model, and validity horizon |

These frames are not necessarily a single rigid parent-child tree. Some are rotating, translating, barycentric, body-fixed, or operational frames. Every transformation must identify its source and target, units, axes and handedness, origin/orientation definition, evaluation epoch, motion-model version, uncertainty, validity horizon, and calibration provenance.

Before comparing two moving objects, transform them into a compatible frame **at a common epoch**. A chart that gives a coordinate without saying which origin, orientation, units, and epoch it uses is incomplete. More digits do not imply more accuracy.

## 2. The historical shift in humanity's mental map

These are capability phases, not locked dates or a compulsory universal sequence. Different colonies and polities may progress at different rates, preserve older standards, or operate hybrid systems.

### Earth-origin navigation

Earth is the practical anchor because people, observatories, clocks, launch infrastructure, and records are concentrated there. Charts focus on terrestrial datums, Earth orbit, and nearby destinations. Early procedures may rely on manually curated ephemerides and a narrow network of observations.

The limitation becomes obvious as permanent off-world societies grow: Earth is a moving destination, communication delays increase, and a route involving several worlds is awkward to describe as though every journey begins at home.

### Solar-System navigation

A Sun- or Solar-System-barycenter reference gives planetary routes a coherent basis. Precision clocks, ephemerides, ranging, and automated calculation become logistical infrastructure. This is not merely a better screen: ships, observatories, beacon networks, crew training, and standards all need to agree.

### Local-stellar navigation

Interstellar routes need stable stellar identities, measured motion, route records, and compatible regional datums. A chart becomes an operational asset whose age and provenance matter. A destination may be known in an archive but not known precisely enough for a particular vessel to arrive safely.

### Galactocentric navigation

A declared Galactic Centre datum gives distant systems a common cartographic language. Governments can compare strategic distances, maintain large-scale route networks, and reconcile local charts. It changes how civilization *thinks*: Earth is now one named locality within a vast coordinate system, not the natural origin of every map.

It does **not** make the Galactic Centre a motionless absolute or every distant star known. The galactic frame itself must define its axes, epoch, adopted geometry and motion model. The Solar System and other stars continue to move.

### Hybrid and fragmented navigation

Mature civilization will not necessarily standardize everything. Local systems remain useful for approach and docking; disconnected colonies may preserve older frames; rival powers may publish incompatible chart editions or dispute the authority behind a common datum. Legacy charts are valuable historical evidence, not junk to delete.

## 3. Coordinate systems are capabilities, not cosmetic options

A ship's effective navigation capability depends on the complete working chain:

- sensors and angular/range measurement;
- clocks, synchronization, and inertial state estimation;
- catalogue coverage, freshness, and provenance;
- transformation software and compatible reference data;
- uncertainty propagation and route computation;
- power, calibration, maintenance, and environmental robustness;
- qualified operators, procedures, and access to beacons or communications.

A ship may carry a sophisticated transform algorithm but lack the observations or calibration needed to use it safely. It may know a star's catalogue identity without having a precise enough position for its current drive or approach procedure. Damaged sensors, stale data, clock disagreement, or an unqualified watch can degrade a technically advanced ship.

**The coordinate convention is one input to navigation, not a magic upgrade.** Better measurements and equipment may reduce uncertainty. A more convenient origin alone must not grant sensor range, reveal hidden systems, shorten physical distances, or guarantee a successful jump.

## 4. Charts are versioned artifacts with histories

Every chart edition should preserve:

- its origin/publisher and intended users;
- frame, epoch, units, clock convention, and transformation version;
- source catalogues and coverage;
- accuracy, uncertainty, and known blind spots;
- last validation/update and required equipment;
- integrity/authenticity and access restrictions;
- optional route networks, hazards, political claims, names, and aliases.

A chart can be outdated yet historically correct about what its makers knew. A government may deliberately publish political borders or preferred names. Two charts may disagree because their epochs differ, their observations are poor, their transformations are incompatible, or their publishers are deceptive. The simulation should preserve that distinction instead of collapsing all records into a single omniscient map.

This naturally connects to raWWar's discoverable archives: a wreck may contain an obsolete navigation computer, a route log in a forgotten datum, or calibration records that let the player reconstruct a lost expedition. The chart is both a tool and an artifact of civilization's history.

## 5. Make the shift consequential in play

The evolution from Earth-centered to galactocentric navigation can change:

- which routes can be planned with confidence;
- how far a ship can travel independently from a beacon or network;
- what an expedition needs to carry onboard;
- which systems become logistical hubs;
- how fast maps, standards, and updates spread;
- how isolated colonies interpret distances and historical routes;
- the ability to reconcile old battle reports, wreck coordinates, and migration records;
- political power, when one polity controls a trusted reference network or withholds updated data.

These are consequences of data, instruments, infrastructure, and institutions—not a free technology-tree unlock. A new standard needs implementation, distribution, training, maintenance, and compatible equipment. A remote faction may have excellent local navigation but a poor galaxy-scale atlas; another may own a vast atlas but lack current local data at a frontier.

Route feasibility should combine the authoritative world at the relevant time, ship and drive capabilities, chart coverage/freshness, uncertainty and safety margins, hazards, and equipment/crew condition. When a transform is missing or the uncertainty is too large, the system should report that limitation rather than silently invent precision.

## 6. Time, motion, and historical reconstruction

Charts describe positions relative to an epoch. Over long history, stellar motion changes local neighborhoods and galactic relationships. Do not take a present-day chart, rotate the entire image, and call it a historical reconstruction. Use a declared motion model and preserve its validity horizon and uncertainty.

A historical navigation event should record at least the simulation time, chart edition, frame/transform versions, source knowledge available to the crew, equipment condition, and uncertainty model. That lets the player compare what *actually happened* with what a crew could reasonably have known at the time.

## 7. What is decided—and what remains open

The contract establishes that frames are versioned, transformations are explicit, equipment limits knowledge and action, charts preserve provenance, and different civilizations can coexist with different standards. It deliberately does not lock exact campaign dates, a single canonical transform library, the precision required by each future drive, or whether all factions accept one galactocentric datum.

The next implementation step is to make coordinate-frame metadata part of the astronomical import pipeline and test transform provenance before importing a larger real star sample. Navigation capability can then consume the same source-aware positions instead of inventing its own universe.

> **A civilization's map is not the universe. It is what that civilization can measure, compute, remember, and safely act upon.**
