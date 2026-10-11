# Visualizer — GPU Backend Strategy

**Status:** Research and decision framework. No graphics API is selected.  
**Decision rule:** Choose from measured end-to-end requirements, not API fashion or assumptions about a vendor's roadmap.

## 00. What “close to the GPU” actually means

There is no single universal GPU API that exposes every capability on every vendor and operating system. GPU work is reached through driver-supported programming interfaces, and the appropriate interface depends on whether the work is graphics, general compute, presentation, or a mixture.

A low-level API can reduce hidden work and expose explicit resource and synchronization control. It also transfers more responsibility to our implementation: resource lifetime, synchronization, pipeline creation, device loss, shader compatibility, validation, and platform integration. Lower level is an opportunity for performance, not a performance guarantee.

Our goal is **control over the critical path with a portable architecture**, not the largest number of low-level API calls.

## 01. CUDA: close to the desired idea, but not the whole answer

CUDA is a general-purpose GPU compute platform associated with NVIDIA. It is valuable when a workload is primarily parallel computation and the deployment environment can rely on CUDA-capable hardware and its driver/toolchain.

It is not a general cross-vendor graphics-and-presentation API. CUDA kernels do not, by themselves, replace the need to create a display surface, rasterize geometry, manage a graphics pipeline, and present frames. CUDA/graphics interoperability is possible, but it adds a second API and additional constraints.

**Conclusion:** Learn from CUDA's model of data-parallel computation, explicit device work, and profiling. Do not make CUDA the mandatory Visualizer foundation. An NVIDIA-only path could be explored later for specialized compute if a measured workload warrants it.

Reference: [NVIDIA CUDA Programming Guide](https://docs.nvidia.com/cuda/cuda-programming-guide/index.html).

## 02. Candidate API families

| Candidate | Strengths | Costs / risks | Current role in our investigation |
|---|---|---|---|
| **Direct3D 12** | Explicit Windows graphics/compute control; established Windows GPU ecosystem; supports advanced GPU-driven techniques | Windows-focused; explicit synchronization and resource management; does not directly solve browser rendering | Strong candidate for a WPF-first native prototype |
| **Vulkan** | Explicit, cross-platform graphics/compute API; broad native platform reach | Complex setup and synchronization; browser does not expose native Vulkan directly; platform/device variation | Strong candidate if one native API across platforms proves valuable |
| **WebGPU** | Designed as a modern, safer cross-platform GPU interface for web content; compute and graphics; browser surface is natural for the web path | Browser availability/capabilities vary; native support depends on implementation; not identical to direct native APIs | Strong candidate for browser path and possible shared programming model |
| **OpenGL** | Broad legacy support and simpler entry path for some prototypes | Older API model and less direct alignment with a GPU-driven future; platform-specific differences | Possible compatibility/fallback or prototype tool, not the default target |
| **CUDA** | Powerful NVIDIA compute ecosystem and tooling | Vendor-specific; not a complete graphics/presentation stack | Optional specialized compute backend, never assumed universal |
| **Higher-level cross-backend library** | Can reduce backend-specific work and help share a programming model | May constrain access to newest features, add dependencies, or hide behavior we need to measure | Investigate as an accelerator to the project, not accept without a prototype and audit |

Direct3D 12 is not the same thing as “DirectX is deprecated.” DirectX is a family of technologies and Direct3D 12 remains a current API. We should not assume a “DirectX 13” exists or plan around an unverified successor. If a future API appears, the architecture should make backend replacement a contained engineering decision.

## 03. A layered strategy rather than a permanent bet

The proposed architecture has three separable decisions:

1. **Visualizer contracts:** the backend-neutral description of resources, passes, draw/dispatch work, frame lifecycle, and device capabilities.
2. **Backend implementation:** translates those contracts to a specific API or library.
3. **Host surface integration:** connects presentation to WPF, browser canvas, or another host.

This boundary must not become an abstract lowest-common-denominator that prevents advanced techniques. It should expose capabilities explicitly, allow backend-specific extensions behind a controlled interface, and make unsupported features detectable rather than silently emulated.

A common interface does not require every backend to execute the same shader binary. Shader source, compilation targets, reflection, binding layouts, and feature requirements must be handled deliberately.

## 04. How to select the first backend

Do not decide from a feature checklist alone. Build the smallest meaningful test for each serious candidate, then compare on the same hardware and scene.

Minimum comparison workload:

- create a device and host surface;
- clear and present;
- upload static vertex/index data;
- draw one indexed object;
- draw a large instanced population;
- execute one compute pass that writes data consumed by a graphics pass;
- resize the surface;
- handle a missing optional feature;
- shut down cleanly;
- record CPU submission time, GPU time, frame-time distribution, memory use, and synchronization stalls.

For WPF-first delivery, compare the real surface integration too. A fast command buffer with poor window integration is not a successful Visualizer.

## 05. Proposed investigation order

1. **Prototype A — native Windows:** establish a genuine GPU surface hosted in AnyApp/WPF. Compare a direct native API against a credible cross-backend library.
2. **Prototype B — compute-to-draw:** update compact instance or pose data in a compute pass, then consume it in a graphics pass without an unnecessary CPU round trip.
3. **Prototype C — browser:** render the same conceptual test scene using a browser-compatible WebGPU path through a minimal surface adapter.
4. **Compare:** document API coverage, portability, dependency footprint, debugging/profiling, driver issues, implementation complexity, and measured performance.
5. **Choose provisionally:** select the backend that best meets the first supported host and keeps a realistic route to the second.
6. **Preserve escape routes:** keep the backend boundary small and capability-aware; avoid making API objects leak into Renderer contracts or Experience data.

Do not optimize around a benchmark that does not resemble the intended scene. A colored triangle proves a surface works; it does not prove population-scale performance.

## 06. What must remain outside the backend

- raWWar's authoritative soldier, faction, and world records;
- FSM_API state and domain decisions;
- Renderer Event Horizon policy;
- MicroBundle and FSM_COS composition semantics;
- host-independent semantic input contracts;
- long-lived persistence and event history.

The backend may hold transient GPU buffers, textures, indirect arguments, visibility lists, pose data, and render targets. These are disposable projections of the authoritative world.

## 07. References for initial study

- [NVIDIA CUDA Programming Guide](https://docs.nvidia.com/cuda/cuda-programming-guide/index.html) — GPU compute model, synchronization, memory, and profiling concepts.
- [wgpu project](https://github.com/gfx-rs/wgpu) — example of a cross-platform graphics abstraction that maps to multiple native backends and browser WebGPU.
- [WebGPU specification](https://www.w3.org/TR/webgpu/) — browser-facing graphics and compute API model.
- [Vulkan specification and resources](https://www.vulkan.org/) — explicit cross-platform native API.
- [Direct3D 12 documentation](https://learn.microsoft.com/windows/win32/direct3d12/direct3d-12-graphics) — Windows graphics and compute API.

These links are study material, not endorsements or decisions. Availability, library licensing, .NET interoperability, browser support, and maintenance health must be checked before choosing dependencies.
