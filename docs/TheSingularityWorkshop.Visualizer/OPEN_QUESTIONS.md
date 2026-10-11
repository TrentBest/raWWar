# Visualizer — Open Questions and Decision Log

**Status:** Active design questions. The options below are not all equally desirable, and none is silently resolved by implementation convenience.

## 01. What does “closest to the GPU” mean for this project?

**Candidate interpretation:** explicit control of GPU resource allocation, data movement, pipeline setup, synchronization, compute/graphics pass dependencies, and presentation—while keeping authoritative world state and portable contracts outside the backend.

**Still needed:** Define which control points are essential and which abstraction costs are acceptable. Lower-level API access is not inherently faster; measure end to end.

## 02. Which native backend should the first WPF prototype use?

Candidates include Direct3D 12 directly, Vulkan directly, or a maintained cross-backend library.

Evaluate:

- real WPF surface integration;
- C#/.NET interop quality and maintenance;
- graphics and compute feature access;
- driver/device support;
- shader tooling and diagnostics;
- dependency footprint and licensing;
- ability to implement the browser path without forcing the core to become browser-specific;
- measured CPU submission and GPU execution costs.

No decision until a test scene exists.

## 03. Is one backend abstraction enough?

A single common API may simplify portability but can hide advanced features or force the common model down to the weakest backend. A completely backend-specific public API would make shared Visualizer consumers difficult.

**Candidate:** a small common contract plus explicit backend capability queries and controlled escape hatches for specialized operations.

## 04. What is the shader strategy?

Questions include:

- which source language(s) are authored;
- which targets are compiled;
- whether shader compilation occurs at build time, install time, or runtime;
- how reflection and binding layouts are validated;
- how shader variants are selected by capabilities;
- how hot reload works during development;
- how packaged MicroBundles provide shader assets;
- how license and toolchain constraints affect redistribution.

Do not promise a single shader binary will run everywhere.

## 05. How is WPF integrated?

Candidate paths include a native child window/swap chain or an interop surface such as D3DImage where compatible with the selected backend.

Decide after proving:

- resize and DPI behavior;
- presentation latency and tearing policy;
- input routing and focus;
- multi-window lifetime;
- device loss/recreation;
- window close and resource disposal;
- compatibility with AnyApp's host lifecycle.

WPF is the first host, not the core graphics abstraction.

## 06. What is the browser support policy?

Questions:

- WebGPU-only, WebGPU with WebGL2 fallback, or a more limited browser support statement?
- How are device limits and unsupported shader features exposed?
- Does browser rendering use a JavaScript/TypeScript module, .NET WebAssembly, or a hybrid adapter?
- How do Blazor lifecycle and canvas ownership interact with GPU resource lifetime?
- What parity is required before calling the browser path supported?

The browser implementation must be designed from actual browser APIs and constraints, not by assuming native graphics calls can simply be reused.

## 07. What belongs in Renderer versus Visualizer?

**Current intended boundary:** Renderer decides observer-relative computational representation; Visualizer executes GPU presentation work.

Audit the current Renderer source before freezing the split. In particular, define ownership of culling, animation/pose evaluation, camera data, visibility, and render queues. Some responsibilities may cross the boundary but must have a single owner.

## 08. What is the compact data model?

The existing raWWar Renderer Integration document proposes a four-channel, 8-bit-per-channel state-texture concept. It is an architectural hypothesis, not a universal truth.

Investigate alternatives including structured buffers, storage buffers, textures, packed integer fields, and hybrid layouts. Choose by access patterns, precision, bandwidth, update frequency, alignment, portability, and the need for stable identity. Do not force semantically unrelated values into a texture merely because it looks compact.

## 09. How do GPU work and FSM_API schedules interact?

FSM_API remains the behavioral and scheduling substrate; GPU passes may process large arrays in parallel.

Questions include:

- how scheduled state changes become GPU input;
- how to avoid uploading unchanged data;
- when a GPU result is read back, if ever;
- how frame pacing interacts with non-frame simulation;
- how data races and resource hazards are diagnosed;
- which computations are deterministic and what level of determinism is required.

GPU results must not silently become authoritative state.

## 10. What performance target proves the design?

Targets need a named workload, hardware, resolution, settings, population count, and quality threshold. Record frame-time percentiles and CPU/GPU timings, not just average frames per second.

The first target should prove a scalable path and establish a baseline, not promise an arbitrary headline population before measurement.

## 11. What is the first reusable MicroBundle?

A Visualizer capability may be composed as a MicroBundle, but the domain contract, backend implementation, and NuGet package are different concepts.

Determine whether the first composable capability is a device/surface provider, a specific render pass, a scene representation, a shader asset provider, or a complete Visualizer capability bundle. Do not make the core depend on FSM_COS; composition may depend on it from above.

## 12. What should the first public name mean?

**Current recommendation:** `TheSingularityWorkshop.Visualizer` for the repository and primary package. Use `TheSingularityWorkshop.Renderer` for the existing observer-aware rendering model. Consider host-specific packages only after the first working vertical slice proves the separation.

## 13. How do we prove portability rather than merely claim it?

The WPF path should be built first, but a minimal browser path should be part of the architecture validation plan before the public core API is treated as stable. A second host can reveal accidental assumptions about window handles, memory layout, shader language, timing, and input.

## 14. What is the first visually compelling raWWar scene?

The proposed first slice is a squad moving through visible dust/smoke with a physical unit banner, using stable soldier identities and near/far representations. The scene should prove the rendering pipeline, not require the entire game simulation.

The exact geometry, materials, lighting, and animation can begin simple. Presence, motion, occlusion, and coherence matter more than a polished still image.

## 15. What does “performant” mean beyond raw throughput?

Measure memory pressure, shader startup, resource churn, frame latency, input-to-photon delay where measurable, battery/power behavior on relevant hardware, and recovery from device loss. A high peak frame rate is insufficient if the application stutters or becomes fragile.

## 16. How are the package and release kept safe?

The repository must follow Workshop rules: CI and tests, clear .NET support policy, documented dependency direction, a NuGet publish gate disabled by default, no release without explicit approval, and no claim of supported backend features without verification.
