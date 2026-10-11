# Visualizer — Implementation Roadmap

**Status:** Proposed sequence; no milestone below is complete merely because it is documented.

The roadmap is designed to prove the smallest real GPU path early, then expand the scene without prematurely locking the Workshop into one graphics API.

## Milestone 0 — Repository and architecture audit

**Purpose:** Establish what already exists and avoid rebuilding machinery that is already present.

- Read the current `TheSingularityWorkshop.Renderer` source, tests, benchmarks, package metadata, and README.
- Read AnyApp's current WPF host lifecycle and package references.
- Inspect FSM_COS and MicroBundleDomain integration points without adding upward dependencies.
- Inspect WebPage's existing platform-neutral GUI model and Blazor rendering adapter. Do not assume a GUI tree is a 3D scene renderer.
- Identify existing GPU/device access, if any, from repository evidence rather than memory.
- Record current .NET target frameworks, license, build/CI state, and dependency footprint.
- Write a short decision record for candidate backend(s).

**Exit evidence:** A checked-in audit with links to exact code and tests; no architecture claim based only on a README.

## Milestone 1 — GPU surface proof

**Purpose:** Prove the display path in AnyApp before attempting the living battlefield.

- Create a WPF-hosted GPU surface.
- Present a clear color and a triangle or similarly trivial test primitive.
- Support resize and deterministic shutdown.
- Verify no per-frame WPF scene-control creation.
- Record backend/device/driver details and failure behavior.

**Exit evidence:** Repeatable local run instructions, automated contract tests where possible, screenshot or capture evidence, and documented limitations.

## Milestone 2 — Minimal Visualizer contract

**Purpose:** Define only the abstractions required by the proven surface.

Candidate concepts:

- device and capability description;
- surface and frame lifecycle;
- resource identity and lifetime;
- buffer/texture descriptions;
- shader/pipeline description;
- render and compute work;
- pass/resource dependencies;
- presentation result and diagnostics.

Avoid designing a general-purpose engine API before the first backend demonstrates which concepts are truly necessary.

**Exit evidence:** Backend-neutral contract tests and one concrete implementation. No public API freezes before an API review.

## Milestone 3 — GPU data path

**Purpose:** Prove the GPU can prepare data that graphics consumes without an avoidable CPU round trip.

- Upload compact per-instance data.
- Dispatch a small compute pass to transform or generate presentation data.
- Consume the output in a graphics pass.
- Measure synchronization and transfer cost.
- Compare CPU-generated versus GPU-generated data for the same scene.

**Exit evidence:** Correctness comparison, timings, allocation profile, and documented GPU/CPU responsibility split.

## Milestone 4 — Instanced scene and camera

**Purpose:** Move from a test primitive to a real visualizer.

- Render a large repeated population with stable per-instance identity.
- Add camera/view/projection transforms.
- Add deterministic instance variation.
- Add basic culling or visibility selection when measurements justify it.
- Keep all instance inputs reproducible and make visual correctness inspectable.

**Exit evidence:** Repeatable population benchmark with counts, resolution, hardware, quality settings, CPU time, GPU time, and frame-time distribution.

## Milestone 5 — Renderer integration

**Purpose:** Connect the existing observer-relative Renderer to the GPU path.

- Define a documented render-data boundary.
- Preserve Renderer ownership of Event Horizon policy.
- Represent the same entity at different levels of detail without changing its authoritative identity.
- Ensure the GPU state can be rebuilt from authoritative/presentation inputs.
- Demonstrate a near/middle/far transition with continuity.

**Exit evidence:** Tests for representation selection and stable identity, plus a visible test scene. Do not claim full raWWar integration unless the Experience data reaches the host end to end.

## Milestone 6 — First living battlefield slice

**Purpose:** Deliver a scene that visibly demonstrates why the Workshop architecture exists.

Suggested scope:

- ground plane and horizon;
- repeated soldiers with faction colors and stable identities;
- a squad moving as a unit;
- one visible unit banner/totem;
- dust/smoke particles that affect readability and silhouette;
- camera movement;
- close/far representation changes;
- a simple animated vehicle or structure.

The first banner should be a real rendered object in the world, not a UI overlay. It can be visually simple at first; its motion, occlusion, and place within the squad are what make it convincing.

**Exit evidence:** A running, repeatable scene in AnyApp with a recorded build, performance measurements, and a clear list of what is simulated versus merely visual.

## Milestone 7 — Web path

**Purpose:** Prove that the Visualizer's scene and render-data contracts are not WPF-shaped.

- Implement a minimal browser surface adapter.
- Use WebGPU where supported and explicitly document the fallback strategy.
- Render the same conceptual scene and validate camera, instance, and data semantics.
- Keep DOM/Blazor components out of the core GPU scene representation.
- Record differences caused by browser security, device limits, API features, and hosting lifecycle.

**Exit evidence:** Browser run instructions, capability reporting, a tested fallback decision, and parity report.

## Milestone 8 — Scale and fidelity

Only after the first battlefield slice is measured:

- GPU-driven culling and indirect draws;
- more elaborate Gesture/pose evaluation;
- crowd-level motion and formation representation;
- material and lighting systems;
- shadows and temporal effects;
- dust, smoke, debris, weather, damage, and other battlefield cues;
- streaming and resource residency;
- asynchronous compute where it demonstrably helps;
- deeper Event Horizon policies;
- device-loss recovery and resource reconstruction;
- profiling and regression thresholds.

Do not add complexity because it sounds advanced. Add it when it improves the intended experience at an acceptable measured cost.

## Release discipline

- No NuGet package is considered ready solely because a prototype window opens.
- No publish workflow should publish by default; any release gate follows Workshop policy and remains disabled until explicitly approved.
- Package versioning, license compatibility, dependency audit, public API review, CI, tests, documentation, and performance evidence are required before alpha publication.
- The first public release should state exactly which backend and host are supported and what remains experimental.
