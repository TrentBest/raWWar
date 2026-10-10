# raWWar — Rendering Performance and GPU Budgets

> **GPU-side does not mean unlimited. Measure the workload, then choose the representation.**

Status: Candidate renderer engineering contract. These are hypotheses and benchmark requirements, not performance claims or guarantees about the current runtime.

## 1. The useful intuition—and its limit

Moving repeated state updates and draw submission toward the GPU can reduce CPU overhead and support far more visible instances than a naive per-object CPU loop. Screen resolution also creates a meaningful bound on visible pixel work. But pixel count is only one term in the budget.

The actual limits include GPU memory capacity and fragmentation, memory bandwidth, cache behavior, vertex and geometry processing, shaded pixels, overdraw, shader complexity, draw/dispatch overhead, synchronization, CPU-to-GPU upload traffic, readback stalls, render-target formats, thermal limits, and the target frame deadline. VR adds stereo workloads and strict refresh deadlines; a good average frame time can still feel bad if the tail misses refresh.

Do not promise “unlimited” quantity. Establish what a defined workload can sustain on named hardware at a stated resolution and refresh rate.

## 2. Keep simulation identity separate from render representation

A mech is not just its visible mesh. It has stable part identity, articulated joints, control surfaces, sensors, cables, equipment, damage state, collision and reach boundaries, repair procedures, and salvage provenance.

The renderer may cache or pack state into GPU buffers or textures, but authoritative identity and persistent outcomes must remain recoverable by the simulation. A render optimization must not make a damaged actuator impossible to identify, a lever impossible to interact with, or a wreck part impossible to recover.

## 3. Mesh joining is a measured tradeoff

| Strategy | Likely benefit | Cost or risk |
|---|---|---|
| Separate component meshes | Independent animation, damage, visibility, repair, and salvage | More submission and object-management overhead if handled naively |
| Material batching | Fewer material changes and grouped submissions | Can complicate visibility and update boundaries |
| Instancing | Reuses geometry for repeated components or many similar machines | Per-instance data and variation still consume bandwidth and processing |
| Selectively joined static groups | Reduces overhead for stable pieces that always move and hide together | Reduces per-part culling, damage, and replacement granularity |
| GPU-driven indirect submission | Can reduce CPU-side submission for large populations | Requires careful visibility, synchronization, buffer management, and platform-specific measurement |

**Join geometry when the parts can safely share lifecycle and visibility. Keep parts independently addressable when articulation, damage, interaction, repair, collision, or salvage requires it.** A hybrid mech is the default hypothesis to test, not a final mandate.

## 4. Benchmark matrix

Benchmark at least:

- one articulated mech with cockpit controls and localized component damage;
- a squad of distinct mechs sharing chassis and component assets;
- a large formation with repeated components and per-instance state;
- damage, debris, and salvage changing visibility during combat;
- VR stereo rendering at the intended refresh rate;
- an integrated GPU and a discrete GPU;
- cold load, warm cache, memory pressure, and device recovery.

Compare separate meshes, material batching, instancing, selectively joined static groups, and GPU-driven indirect submission. Keep visual quality, interaction behavior, and simulation outcomes equivalent between variants.

## 5. Record the actual bottleneck

Report:

- CPU frame time and submission time;
- GPU frame time by pass and frame-time percentiles;
- missed refresh deadlines, especially in VR;
- resident and peak GPU memory;
- memory bandwidth where available;
- draw calls and indirect commands;
- visible and culled instances;
- triangles and shaded pixels;
- upload/readback volume and synchronization stalls;
- interaction latency and divergence between simulation and rendered feedback.

Record hardware, driver, API, build, resolution, refresh target, scene seed, camera path, asset version, and benchmark configuration so results can be reproduced.

## 6. Pages, buses, and residency

Page or tile GPU data to make capacity and residency explicit. Page size, bus transfer cost, cache locality, allocation overhead, and update frequency are different constraints; none can be inferred from pixel count alone. Keep frequently updated hot data separate from cold descriptive data. Avoid moving large semantic records across the bus when compact handles or structured deltas suffice.

A texture can be an excellent compact state plane or presentation cache. It is not automatically a suitable sole source of truth for identity, history, ownership, or persistent damage. If GPU state is ever authoritative for a specific domain, that domain needs explicit readback, recovery, persistence, and deterministic replay rules.

## 7. Definition of done

A performance claim is accepted only with a reproducible workload, named target hardware, correctness checks, and measurements. Optimization must preserve component identity, interaction, damage, repair, salvage, and VR comfort. If an optimization improves average throughput but causes unacceptable frame-time spikes or hides meaningful state, it is not a successful optimization.

The governing rule: **pixel-limited is a useful mental model for some workloads, not a universal hardware law.**
