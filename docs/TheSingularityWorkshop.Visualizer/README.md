# TheSingularityWorkshop.Visualizer

> **Make the living world visible without making the world dependent on its display.**

**Status:** Architecture proposal; candidate decisions are identified explicitly.  
**Purpose:** Define the GPU-facing presentation technology needed to turn Workshop Renderer output into real pixels, beginning with AnyApp and a WPF desktop host.  
**Repository / primary NuGet package proposal:** `TheSingularityWorkshop.Visualizer`  
**Owner:** The Singularity Workshop.  
**Evidence rule:** This document describes intended architecture, not an implemented or benchmark-proven graphics engine.

## 00. The missing layer

The Workshop already has a distinct rendering concept. The Renderer is not merely a list of meshes to draw: it determines which representation of an authoritative world is justified for an observer, and how much computational detail that representation needs.

The missing technology is the device-facing path that actually draws.

We propose **Visualizer** for that responsibility.

- **Experience** defines the world, its meaning, rules, and intended observable behavior.
- **FSM_API** provides behavior and scheduling primitives.
- **Workshop Renderer** decides observer-relative representation and computational detail through Event Horizons.
- **Visualizer** converts render work into GPU resources, compute/graphics passes, and presented pixels.
- **Host adapters** connect the visualizer to WPF, browser/WebGPU, or future environments.
- **AnyApp** is the first host and integration proving ground.
- **FSM_COS** may compose these capabilities but is not the renderer, visualizer, host, or game loop.

The names describe separate responsibilities, not a demand that every responsibility become a separate assembly on day one.

## 01. The central proposition

The goal is not to wrap a popular game engine, nor to build a WPF control for every soldier.

The goal is to move large, regular, parallel presentation work toward the GPU while keeping authoritative world meaning and durable state outside the GPU.

> **The CPU organizes meaning and irregular decisions. The GPU performs large amounts of parallel work. The display presents the result.**

That is a design principle, not a claim that every task belongs on the GPU. CPU/GPU placement must be measured. Small, branch-heavy, latency-sensitive, or authoritative operations may remain on the CPU.

## 02. Intended dependency and data flow

```text
raWWar / another Experience
          |
          | authoritative state + semantic meaning
          v
        FSM_API
          |
          | behavior, state, scheduled evaluation
          v
  Workshop Renderer
  - observer context
  - Event Horizon policy
  - representation selection
  - visibility / detail decisions
  - presentation data
          |
          | compact, versioned render data
          v
 TheSingularityWorkshop.Visualizer
  - resource lifetime
  - render/compute graph
  - GPU buffers and textures
  - shader and pipeline management
  - synchronization / uploads
  - frame and presentation scheduling
          |
          v
  Graphics Backend Adapter
  - native desktop backend
  - browser WebGPU backend
  - future backends
          |
          v
  Host Surface Adapter
  - WPF first
  - Blazor/browser path
  - future hosts
          |
          v
        Pixels
```

The diagram is an intended boundary, not proof of an implemented pipeline. The GPU contains derived, disposable presentation state. A pixel, texture, buffer, or GPU-computed pose never becomes authoritative world truth merely because it exists.

## 03. The Visualizer is not a WPF control library

WPF is the first host because AnyApp already has a Windows desktop path and because it offers a practical place to prove a real rendered surface. It is not the architectural center.

WPF must not receive thousands of independently animated controls as the main scene representation. WPF should host the surface and participate in window/input/lifecycle integration; the scene should be drawn through a GPU-facing surface and backend.

Candidate desktop presentation paths include a native child surface or swap chain integrated with WPF, and other interop mechanisms supported by the chosen backend. The correct choice must be tested for resize, presentation timing, device loss, DPI, input routing, and lifetime behavior before it is frozen.

The browser path has different constraints. It should use a browser-compatible graphics path—most plausibly WebGPU where available, with a deliberately chosen fallback policy—and connect through a thin browser surface adapter. Blazor should not force the common rendering core to depend on Blazor types.

## 04. Relationship to the existing Renderer

The existing `TheSingularityWorkshop.Renderer` project owns the **observer-relative computational model**:

- Event Horizons;
- multidimensional detail policy;
- representation selection;
- update-rate and fidelity decisions;
- population-scale presentation intent;
- the distinction between world truth and presentation state.

Visualizer owns the **GPU execution and presentation mechanism**:

- device and queue access;
- resources, memory, and upload strategy;
- graphics and compute pipelines;
- shader compilation/loading policy;
- render-pass/resource dependency management;
- synchronization and frame pacing;
- surface configuration and presentation;
- GPU diagnostics and backend capability reporting.

The boundary should be validated against the current Renderer code before public APIs are frozen. Do not duplicate its Event Horizon policy inside Visualizer.

## 05. Why the GPU matters to raWWar

raWWar needs a battlefield that feels inhabited, not a stage populated by a few expensive characters and frozen scenery.

A close soldier may need armor plates, tools, weapon handling, nuanced Gesture motion, damage, rank markings, service medals, and individual attention. A distant soldier may need a stable silhouette, faction identity, formation membership, major motion, and a visible banner. The same identity can move between representations without the underlying soldier changing.

The existing Renderer concept proposes Event Horizons as changes in computational representation, not merely mesh LOD. Visualizer gives that strategy a place to execute efficiently.

Potential GPU work includes:

- instanced geometry and repeated draw data;
- GPU-driven culling and indirect drawing, where supported and justified;
- batched pose/Gesture evaluation;
- compact state and pose buffers;
- procedural variation derived from stable identity;
- particle, dust, smoke, sparks, weather, and other large-scale effects;
- compute passes that prepare data consumed by graphics passes;
- reduced-frequency updates for distant populations;
- texture/field representations of broad environmental or galactic state, when the data model and measurements justify them.

These are candidate techniques, not requirements to use every feature. The first implementation must prove the data path and measure it before expanding the feature set.

## 06. Performance is an end-to-end property

“GPU accelerated” is not the same as “fast.” A design that performs many tiny CPU/GPU transfers, synchronizes after every pass, recompiles shaders during play, or allocates new objects every frame can be slower than a simpler design.

The project must measure at least:

- CPU submission time;
- GPU execution time by pass;
- frame latency and frame-time distribution, not just average FPS;
- upload/download volume and synchronization stalls;
- allocations and garbage collection;
- memory use and resource churn;
- visible population and instance counts;
- draw/dispatch counts;
- resolution and effect settings;
- device/backend/driver details;
- cold start, shader compilation, and device-loss recovery.

Targets must be declared with the workload and hardware. A benchmark without a repeatable scene, explicit quality settings, and a stated device is not a performance claim.

## 07. Proposed package shape

Start with one repository: **`TheSingularityWorkshop.Visualizer`**.

Likely package boundaries, to be confirmed by prototypes:

- `TheSingularityWorkshop.Visualizer` — backend-neutral public contracts, frame/resource abstractions, capability reporting, and orchestration.
- `TheSingularityWorkshop.Visualizer.Wpf` — Windows desktop surface integration.
- `TheSingularityWorkshop.Visualizer.Web` — browser surface integration, potentially a .NET/JavaScript boundary rather than a pure .NET assembly.
- backend-specific packages or MicroBundles — only where isolation materially improves portability, dependency management, or composition.

The target is for Visualizer to be distributable as a NuGet package and discoverable/composable as a MicroBundle capability in the Workshop runtime. The NuGet artifact is the distribution unit; the bundle manifest/provider is the composition contract and may expose one or more independently selectable capabilities. Their mapping should be explicit and deterministic. Do not create every possible package before a working vertical slice demonstrates the boundaries. Lower-level Visualizer packages must not depend upward on FSM_COS.

## 08. First vertical slice

The first milestone is deliberately narrow:

1. AnyApp launches a WPF window.
2. A GPU-backed surface is created and presented.
3. A clear color and a small, known test scene render reliably.
4. A camera/view transform can be changed and the result is visible.
5. A simple instanced population renders from compact input data.
6. Resize, shutdown, and device/resource cleanup are verified.
7. CPU/GPU timings and a reproducible benchmark are captured.
8. The Renderer supplies representation data through a documented boundary.
9. No scene object needs to be represented as an individual WPF visual element.
10. The implementation is described honestly as a prototype until cross-host and performance evidence exists.

Only after this path is real should we grow into soldier animation, dust/smoke, Event Horizon transitions, large populations, and browser parity.

## 09. Naming and terminology

Use these terms consistently:

- **Renderer** — decides what representation is appropriate for an observer.
- **Visualizer** — executes GPU presentation work and produces pixels.
- **Graphics backend** — implementation using a concrete graphics API.
- **Surface adapter** — connects the backend to a host-owned display surface.
- **Render data** — derived data supplied for presentation.
- **Authoritative state** — durable world data and rules, never owned by a GPU frame.
- **Event Horizon** — a boundary at which the representation or computational detail changes.

Avoid calling Visualizer a “game engine.” It is reusable Workshop presentation technology.

## 10. Decisions not yet made

The API, backend, shader language/toolchain, GPU data layout, scene representation, synchronization strategy, exact WPF surface integration, browser compatibility policy, package split, license implications, and performance targets are still open.

See [GPU Backend Strategy](GPU_BACKEND_STRATEGY.md), [Implementation Roadmap](IMPLEMENTATION_ROADMAP.md), and [Open Questions](OPEN_QUESTIONS.md).

---

<p align="center">
  <em>The Singularity Workshop — Tools for the curious, the bold, and the systemically inclined.</em>
</p>
