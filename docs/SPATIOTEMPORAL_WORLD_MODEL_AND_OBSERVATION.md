# Spatiotemporal World Model and Observer-Relative Presentation

> **The universe is not a picture. A picture is one observation of the universe at a particular time, position, scale, and point of view.**

This document sharpens the world model behind galaxy generation. The objective is a reproducible, three-dimensional universe whose objects move according to time-dependent state and motion rules, while rendering and GUI presentation remain projections of that authoritative world.

## 1. The world query

Write the conceptual world query as:

\[
F(\mathbf{x}, t) = F(x,y,z,t)
\]

where \(\mathbf{x}\) is a position in the chosen spatial reference frame and \(t\) is a valid simulation time. The result is not necessarily a single object. It may be a spatial sample, a set of intersecting objects, fields, visibility data, or a query result over the authoritative world model.

For object-oriented queries, use a more explicit form:

\[
S_i(t) = \operatorname{Evolve}(S_i(t_0),\; t-t_0,\; M_i,\; E_i)
\]

- \(S_i(t_0)\) is the object's canonical initial or last authoritative state.
- \(M_i\) is its versioned motion/interaction model.
- \(E_i\) is the ordered set of applicable events and interactions.
- \(S_i(t)\) is the state reconstructed or simulated at the requested logical time.

The position is then a projection of state, \(\mathbf{x}_i(t)=\operatorname{Position}(S_i(t))\). This notation is a design contract, not a promise that every object can be evaluated in closed form.

## 2. Motion is a delta over time

A dynamic universe is not produced by selecting a random position independently at every time. It begins with stable identity and initial conditions, then applies a time-dependent motion model.

For a classical mechanical model, the familiar relation is:

\[
\mathbf{F}_{\mathrm{net}}(\mathbf{x},\mathbf{v},t)=m\mathbf{a}
\]

and therefore, where the selected model and assumptions permit,

\[
\frac{d\mathbf{x}}{dt}=\mathbf{v}, \qquad
\frac{d\mathbf{v}}{dt}=\frac{\mathbf{F}_{\mathrm{net}}}{m}
\]

The exact model is domain-specific. Keplerian/orbital approximations may be appropriate for distant bodies; perturbation models or numerical integration may be needed for close interactions; stations, ships, and artificial objects may use controlled trajectories; game-specific phenomena may use other explicitly versioned laws. Do not imply that one equation accurately models every scale.

A simulation step applies a delta:

\[
S(t+\Delta t)=\Phi_{\Delta t}(S(t),\,\text{inputs},\,\text{events})
\]

The transition operator \(\Phi\) is the chosen simulation rule. A request to inspect time \(t\) must not itself advance the world, reroll its seed, or commit a second copy of an event. It either evaluates a valid reconstruction from an authoritative checkpoint and ordered events, or asks the live simulation to advance through an explicitly ordered interval.

## 3. Stable identity, changing state

Keep these concepts separate:

1. **Identity** — the stable semantic address of a star, planet, station, ship, or other feature.
2. **Initial conditions** — reproducible seed-derived properties such as mass class, initial orbital elements, composition, or formation attributes.
3. **Motion model** — versioned rules and parameters that determine how state changes over time.
4. **Live state** — current state resulting from evolution and interactions.
5. **History** — authoritative player/faction/world events that cannot be erased by regenerating the original seed.
6. **Observation** — what a particular observer can currently see or query.
7. **Presentation** — the textures, meshes, labels, icons, overlays, and GUI that communicate the observation.

Random-seeded generation is useful for constructing stable initial conditions. It is not a substitute for motion. The same planet keeps the same identity while its position, orientation, illumination, visible hemisphere, and relationship to other bodies can change with time.

Regenerate immutable properties where the contract permits. Persist player-caused and otherwise authoritative consequences. If an event changes an orbit, destroys a station, moves ownership, or depletes a resource, reconstructing the seed alone must not erase that change.

## 4. Time is logical, bounded, and versioned

The game has a supported simulation epoch and time range. It does not promise arbitrary reconstruction back to the beginning of the universe.

Each simulation domain must define:
- its time unit and epoch;
- valid query range and behavior outside that range;
- integration or analytical-evaluation method;
- step size, adaptive-step rules, and numerical tolerances where applicable;
- model/version identity;
- event ordering and stable event IDs;
- checkpoint and persistence policy;
- deterministic replay expectations and accepted numerical tolerances.

Rendering frames are not the authoritative clock. A 144 Hz display and a 30 Hz display must not cause different planetary orbits. GUI refresh, camera motion, loading, culling, or requesting a map tile must not mutate the world.

For long-running or multi-scale simulation, do not blindly integrate every object at every smallest time step. Use appropriate models, checkpoints, bounded prediction horizons, and fidelity tiers. When exact reproducibility is required, define the arithmetic and integration rules; floating-point computations across platforms may need tolerance-based equivalence rather than bit-identical promises.

## 5. Space, scale, and observer-relative queries

The world is three-dimensional even when a view is two-dimensional. A star map is one projection, not the data model.

A view/query should declare enough context to interpret the result:
- observer or camera pose and reference frame;
- target and region of interest;
- simulation time;
- scale and detail budget;
- visibility/occlusion rules;
- requested information layers;
- interaction mode and input mapping.

This allows one authoritative planetary body to support multiple simultaneous or sequential views: a galaxy map, a system map, a station's orbital view, a descent approach, a surface view, and a local GUI overlay. These views must agree about identity and time even when they use different coordinate frames and levels of detail.

### Example: looking down from an orbiting station

1. The station and planet each have stable identities and time-dependent states.
2. At the selected simulation time, evaluate the station pose and planet pose in a shared frame.
3. Derive the observer camera pose from the station's position, orientation, viewport, and user controls.
4. Transform the planet's geometry or surface representation into that view; compute the visible hemisphere, lighting, and occlusion from the selected time and scene state.
5. Render suitable terrain/surface textures and detail for the viewing distance, while the GUI presents target identity, orbital data, orientation, navigation, and available actions.
6. If time advances, reevaluate motion and view-dependent results. If the user only pans, zooms, or opens a panel, do not advance the simulation.

The planet does not need a separate world identity for every camera angle. Nor should the renderer move the planet to make the picture look right. The authoritative state drives the projection.

## 6. Textures are representations, not the universe

Textures and other render assets are derived representations of world data. They may be generated, cached, streamed, mipmapped, tiled, or refined on demand, but their cache lifecycle must not control the identity or physics of the object they depict.

Different texture/data layers may represent:
- albedo, material, and surface composition;
- elevation, terrain class, or bathymetry;
- temperature, illumination, atmosphere, or weather;
- ownership, sensors, communications, and tactical overlays;
- uncertainty, observed versus unobserved regions, or stale intelligence.

These layers are not interchangeable. A tactical ownership overlay should not overwrite physical terrain data; a low-resolution distant texture should not become the canonical source for a planet's radius or orbit.

A rendered view can be expressed conceptually as:

\[
I = R(\text{WorldState}(t),\;\text{Observer},\;\text{Scale},\;\text{Layers},\;\text{RenderBudget})
\]

Here \(R\) is the presentation pipeline. It answers, “What should this observer see, and how should we display it?” It does not answer, “What exists, and how does it move?”

## 7. GUI belongs to the observation contract

The GUI is not a static HUD glued over a screenshot. It is an interactive, time-aware view into the same world.

GUI elements may be anchored to:
- screen space (menus, tool panels, accessible controls);
- observer/camera space (reticles, orientation aids);
- world space (labels, markers, selected objects);
- target-relative space (orbital vectors, range, approach corridor);
- data space (tables, time-series, telemetry, history).

Each element must state which space it uses and how it behaves when the observer, target, scale, or time changes. Selection and focus should retain stable entity identity as the camera moves. If the target is no longer visible, the GUI may show an off-screen cue or stale-data state; it must not silently substitute a different object.

## 8. Practical simulation tiers

Use one semantic universe with multiple computation and presentation fidelities:

| Tier | Typical use | Expected behavior |
|---|---|---|
| Analytical prediction | Stable orbits, distant bodies, route previews | Evaluate a model at requested time without simulating every intermediate frame |
| Coarse evolution | Distant systems, background factions, long intervals | Advance summarized state and consequential events using declared coarse rules |
| Local numerical simulation | Close orbital interactions, ships, stations, surface operations | Integrate relevant state with the selected solver and bounded step |
| Detailed interaction | Player-operated machinery, landing, combat, repairs | Resolve the finer causal state required by the active experience |
| Presentation-only refinement | Textures, terrain tiles, labels, overlays | Improve what is shown without changing authoritative world state |

These are fidelity tiers, not different universes. Moving the camera closer may request more data and finer rendering; it must not reroll a planet or change its trajectory merely because it became visible.

## 9. Invariants and test scenarios

- The same seed, generator/model versions, initial state, and event history produce the same defined state within the model's determinism contract.
- A planet queried at two different valid times has positions consistent with its motion model.
- Querying time or changing the camera does not commit world transitions.
- Display refresh rate and render-call order do not alter orbital state.
- A station-to-planet view uses a consistent simulation time and coordinate transforms.
- Camera panning and zooming do not change entity identity or motion.
- Texture cache eviction and regeneration do not change physical state.
- A saved event that changes the world survives reconstruction from seed-derived initial conditions.
- Unsupported historical times are rejected or handled according to the domain's declared policy, not silently extrapolated to the origin of the universe.
- Analytical prediction and live simulation agree within declared tolerances over their shared validity horizon.

## 10. Relationship to FSMs

**The data describes the universe. Motion models describe how physical state evolves. FSMs govern discrete state, eligibility, procedures, and consequential transitions. The renderer presents an observer-relative view.**

An FSM can schedule or gate a maneuver, record a stationkeeping procedure, transition a satellite from nominal to degraded control, or commit the consequences of a collision. The continuous or numerical motion model computes the trajectory under its defined inputs. Neither should be forced to impersonate the other.

This division allows the simulation to be mathematical without making every physical trajectory a hand-authored FSM, and allows FSMs to be authoritative without making the entire galaxy tick in lockstep at display-frame rate.


## Executable slice: time-addressable orbital motion

The first runtime proof lives in `src/raWWar/Spatiotemporal/KeplerOrbit.cs`. It is intentionally narrow: a pure, immutable Keplerian two-body evaluator for elliptic orbits. Given orbital elements and an explicit logical time, it returns an inertial-frame 3D position. It does not own a clock, advance the world, consult the camera, generate random values, or mutate state.

This is a useful first contract because it proves the central distinction:

- the orbit's identity and elements are stable inputs;
- position is a time-dependent result;
- requesting a position is an observation, not a simulation side effect;
- changing the observer or render frame rate cannot alter the orbit;
- a single orbit can be queried for a station view, map view, or other presentation at the same time.

The model uses consistent caller-supplied units: the gravitational parameter has units of distance cubed per time squared, the semi-major axis uses the chosen distance unit, and time uses the matching time unit. Angles are radians. The evaluator currently supports only (0 \le e < 1); parabolic and hyperbolic trajectories are deliberately rejected instead of silently approximated.

A dependency-free executable contract suite is in `tests/raWWar.ContractTests`, included in `raWWar.sln`. Run it with:

```sh
dotnet run --project tests/raWWar.ContractTests/raWWar.ContractTests.csproj
```

This is an initial mathematical slice, not a claim that the full spatiotemporal world model is complete. Next contracts should establish canonical entity identity, shared-frame observer transforms, time-aware station/planet observation, explicit maneuver/event application, persistence checkpoints, and render/GUI non-mutation. Close interactions and perturbations will require a separately versioned numerical model rather than stretching this two-body approximation beyond its assumptions.
