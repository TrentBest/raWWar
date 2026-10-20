# Request from raWWar — Source-connected Visualize → GUI.SVG manifestation

**Requesting project:** raWWar Experience  
**Receiving repository:** [TheSingularityWorkshop.Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) (coordinate with the Renderer and GUI.SVG owners as needed)  
**Status:** Proposed — not yet accepted or implemented  
**Owner:** raWWar engineering  
**Evidence baseline:** Soldier Field has a source-connected procedural mesh and SVG preview generator; Visualize has a separate C# geometry/resource/frame → GUI.SVG proof. The two paths are not yet integrated.

## Problem and evidence

raWWar's Soldier Field asset defines 38 stable semantic parts in [`soldier-geometry.js`](../../samples/SoldierField/WebGpuRunner/soldier-geometry.js). `buildSoldierGeometry()` generates the triangle stream used by the WebGPU lab and by [`render-soldier-previews.mjs`](../../samples/SoldierField/WebGpuRunner/render-soldier-previews.mjs), which creates six SVG views. The repository's CI regenerates those previews and checks that each contains mesh polygons. This is genuine source-connected model-to-view evidence, but the SVG projection is a separate lightweight renderer.

Visualize independently demonstrates a C# [GUI.SVG proof program](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/integrations/GuiSvg/Proof/Program.cs) and [generated SVG](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/samples/PrimitiveGallery/generated/07-gui-svg-connected-points.svg). That proof establishes a bounded geometry/resource/frame-to-SVG handoff; it does not establish that raWWar's semantic model is consumed by that path.

The gap is therefore not “raWWar has no generated visuals.” The gap is a maintainable bridge between the Experience's semantic source model and the Workshop's shared Renderer/Visualize → GUI.SVG manifestation pipeline.

## Desired outcome

Determine and document the supported way for a raWWar-owned semantic asset to produce inspectable SVG manifestations through the Workshop visual pipeline, without making SVG the source of truth or creating a parallel renderer contract in raWWar.

The receiving repository owns the implementation design. This request does not prescribe whether the bridge uses an existing frame/resource contract, a neutral asset schema, a small adapter, or another suitable boundary.

## Acceptance evidence

1. **Source traceability:** the generated view can be traced to a versioned source asset, generator/adapter, and declared model revision.
2. **Semantic identity:** stable part/entity IDs survive the bridge sufficiently to select, label, inspect, or correlate visible components with source records where the chosen backend supports those operations.
3. **Reproducible output:** the checked-in demonstration can be regenerated from source; a meaningful source-model change changes the relevant output without manually redrawing the SVG.
4. **Executable verification:** automated checks validate the source asset and generated output, and detect stale checked-in output or a broken adapter contract.
5. **Multiple useful views:** at minimum, demonstrate two distinct views of the same model (for example, front and three-quarter or a part-detail view) from shared source data rather than separately authored geometry.
6. **Honest rendering scope:** explicitly state which transformations, depth/occlusion behavior, materials, labels, animation, and interaction are supported. Do not imply production 3D or gameplay when the path proves only a 2D projection.
7. **Boundaries preserved:** raWWar retains ownership of game meaning; the Renderer/Visualize path owns presentation transformations; GUI.SVG is a manifestation, not authoritative world state.

## Constraints and non-goals

- No cross-repository implementation is authorized by this request. The responsible repository agent decides whether and how to act on it.
- Do not add Unity dependencies or engine-specific requirements.
- Do not duplicate the full semantic model in a second hand-maintained SVG or graphics-specific data source.
- Do not require all design material to be represented as SVG; use the format where an inspectable, source-connected view is useful.
- A generated SVG is not proof of a running raWWar simulation, an integrated first-person station/rig, or an AnyApp host launch.
- This request does not authorize package publication, merge, or release.

## Related raWWar work

- [Public Design Hub — WYSIWYG manifestation contract](../DESIGN_HUB.md#future-presentation)
- [Renderer Integration](../RENDERER_INTEGRATION.md)
- [Documentation conformance audit](../DOCUMENTATION_CONFORMANCE.md)
- [Soldier Field preview generator](../../samples/SoldierField/WebGpuRunner/render-soldier-previews.mjs)
- [Visualize GUI.SVG proof](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/integrations/GuiSvg/Proof/Program.cs)

## Resolution record

Leave this request **proposed** until the receiving repository evaluates it and the acceptance evidence is demonstrated. A linked implementation or successful adjacent-package CI alone does not resolve it.
