# raWWar — Workshop Composition Boundary

> **raWWar defines the war. The Workshop supplies reusable machinery. MicroBundles are the seams that let the two meet without becoming one codebase.**

Status: Architecture contract. Capability entries marked proposed are requirements for future Workshop-owned MicroBundles, not claims that those bundles have already been implemented or published.

## 1. The rule

raWWar must demonstrate recomposition by consuming independently defined Workshop capabilities. It must not quietly become the owner of generic interaction, control, rendering, vehicle-control, damage-resolution, maintenance-workflow, or archive machinery merely because raWWar is the first demanding experience to need them.

FSM_COS is the composition kernel. It resolves the root MicroBundles requested by a runtime manifest, closes their dependency graph, provides available configuration through its separate configuration-source boundary, loads and arbitrates the composition, and hands a `RuntimeAssembly` to the host. **It does not execute the game, render the world, run the experience loop, or become a GUI framework.**

The current FSM_COS development pause gate is authoritative for this work: broad kernel expansion is paused after the correctness gate passed. This project must adapt to that contract, not ask FSM_COS to absorb game-specific requirements.

## 2. Ownership boundaries

| Responsibility | Owner | raWWar contributes |
|---|---|---|
| FSM state and behavior primitives | FSM_API | Domain state, process requirements, and game rules |
| MicroBundle identity, dependency, load, and arbitration contract | MicroBundleDomain | Implementations only where raWWar truly owns a game-domain capability |
| Runtime composition and `RuntimeAssembly` handoff | FSM_COS | Runtime root selection and host integration |
| Semantic intent boundary | FSM_UserIO | Game-specific meanings and permitted actions |
| Physical interaction resolution | Workshop-owned reusable MicroBundle (proposed extraction) | World-object definitions, controls, damage conditions, access policy |
| Embodied actor performance / Gesture realization | Workshop-owned reusable capability or capabilities (proposed) | Soldier/pilot/engineer qualifications, fatigue, injury, task context |
| Diegetic GUI and physical presentation | GUI/Renderer/host capabilities | Cockpit layouts, instruments, art direction, in-world content |
| Generic component damage, isolation, repair-work creation | Workshop-owned reusable capability (proposed extraction) | Component graphs, material properties, faction-specific equipment |
| Generic LOTO workflow mechanics | Workshop-owned reusable capability (proposed extraction) | RaWWar's deliberately frustrating procedures, local rules, hazards, consequences |
| Vehicle/mech configuration and control | Reusable Workshop capabilities plus vehicle-domain data | Chassis, installed parts, control mappings, power/thermal/mobility constraints |
| War, factions, soldiers, campaigns, missions, authored history, research and economics | raWWar | These are the Experience's domain |
| Battle accounts, betrayals, propaganda, contested memories | raWWar history domain, built on reusable provenance/evidence facilities where available | Events, actors, claims, evidence, political consequences |

“Proposed extraction” means the responsibility is correctly identified but must not be described as available runtime functionality until a separately owned MicroBundle and its tests exist.

## 3. Composition is not a list of aspirations

The actual FSM_COS runtime manifest is intentionally small: runtime ID plus root MicroBundle IDs and requested versions. Configuration is supplied separately. FSM_COS discovers dependencies from the MicroBundles themselves; the Experience should not hand-author a duplicate dependency graph in a game-only manifest.

The root Experience bundle may identify raWWar as a domain contribution, but it must not become a wrapper that secretly owns all generic machinery. A reusable capability must be independently identifiable, versioned, testable, configurable, and consumable by a second Experience without referencing raWWar assemblies or data.

For each proposed reusable capability, the owning Workshop package must eventually provide:
- a stable MicroBundle identity and version;
- a focused responsibility and dependency declaration;
- a configuration schema with defaults when configuration is absent;
- executable contract tests;
- no upward dependency on raWWar or another consuming Experience;
- an explicit statement of what remains the host's responsibility.

Numeric bundle IDs and released versions must be assigned by the owning package—not guessed or reserved in this game repository.

## 4. One physical control, many manifestations

The reusable interaction capability should accept a semantic action against a stable world-object identity. Input adapters express the action through VR tracking, first-person reach/use, mouse, keyboard, controller, accessibility input, or an NPC. The shared resolver checks the same world preconditions regardless of input source.

A keyboard binding may cause the avatar's hand to reach a joystick, then move it. A VR pilot may physically move a grip. Both produce an input to the same control law. Neither directly teleports a vehicle or bypasses interlocks, actuator limits, fatigue, damage, authorization, power, cooling, traction, balance, or collision.

The Experience defines that a particular grip is a mech throttle and that the installed actuators determine what happens. The reusable machinery defines how controls, actors, commands, feedback, and events are resolved.

## 5. A mech is a recomposed machine and workplace

A pilotable battle mech is a priority Experience capability, but not a one-off raWWar interaction system. It composes reusable capabilities for:
- componentized vehicle structure and compatibility;
- occupied stations, restraints, access, hatch, and egress;
- physical controls and semantic input;
- motion capture or manual control interfaces;
- actuators, locomotion, stability, contact, power, and thermal constraints;
- sensors, displays, communication, and degraded data;
- damage propagation, isolation, repair work, and maintenance acceptance;
- crew qualifications, fatigue, injury, and emergency response;
- in-world presentation and GPU/rendering.

raWWar supplies the actual chassis definitions, mission packages, weapons, environmental constraints, faction engineering traditions, and the war in which the machine is used. The same Workshop machinery should support a research walker, mining rig, construction machine, rescue platform, vacuum salvage rig, or deep-ocean crawler.

## 6. Engineering procedures must be composable too

The engineering watch is not a bespoke mini-game. It is a work system composed from observation, instrument reading, logging, threshold evaluation, work-order creation, qualification checks, isolation, repair, calibration, and acceptance.

A breathing Gesture ghost indicates the next ideal action but does not execute it. When a logger port is the target, the engineer retrieves the logger, connects it, captures the equipment state with source and time, and compares the recorded readings to the correct thresholds. Approaching or exceeding limits creates consequences according to the installed system and procedure.

Lockout/tagout should be deliberately annoying because it is a real coordination and safety burden: identify every energy source, notify affected people, shut down, isolate, apply personal locks and tags, release or restrain stored energy, verify zero-energy state, perform work, inspect, clear personnel and tools, remove locks under the correct authority, restore energy, and prove safe operation. A shortcut can leave a hazard, invalidate a repair, injure someone, or create a persistent investigation. The workflow mechanics should be reusable; the specific plant and procedures belong to the Experience.

## 7. Status discipline

- **Current FSM_COS contract:** versioned root MicroBundle entries; configuration source separate; dependency closure and arbitration; host receives `RuntimeAssembly`.
- **Current raWWar design:** embodied interaction, mech platform families, engineering watch, damage/salvage, and rendering budgets are described in data and docs.
- **Current executable slice:** raWWar has a small physical-control resolver used to prove shared input semantics. Treat it as a temporary reference slice, not proof that generic interaction belongs in the raWWar product boundary.
- **Not yet verified:** a separately packaged physical-interaction MicroBundle consumed by raWWar; a fully composed mech runtime; production GPU-driven rendering; a finished FPS/VR presentation.
- **No kernel expansion required for this design:** implement reusable capabilities in their owning packages and consume them through the existing MicroBundle/FSM_COS contract.

The architectural test is not whether raWWar has many MicroBundle-shaped files. It is whether a second Experience can reuse the machinery without taking a dependency on raWWar.
