# raWWar Documentation Conformance

**Status:** Active alignment pass.  
**Reference:** [FSM_COS Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/development/DOCUMENTATION_STANDARD.md).  
**Reference maturity:** The cited standard is the current working reference on FSM_COS's `development` branch. Adoption remains repository-by-repository; this audit does not claim universal or exhaustive conformance.

## Purpose

raWWar should share the Workshop's documentation language without becoming a generic package README. FSM_COS is the architectural lynchpin for capability composition, but it does not own raWWar's world model, soldier meaning, game rules, host lifecycle, or renderer. Documentation must preserve that boundary while making the Experience understandable to newcomers and useful to engineers.

The governing editorial rule is **edify, not mystify**: explain the human problem, show the concept, define the technical boundary, provide examples, and give evidence for implementation claims.

## Standard elements applied to the README

The README now follows the shared front-door sequence: `00` identity, then a meaningful money-shot visual, `01` What, `02` Why, `03` How, `04` See it in a minute, and `05` Available documentation and theory. The content retains raWWar's game-design voice while making its intended role as the Workshop's flagship Experience explicit.

The README is not a generic package introduction. It is also a visual argument: concept SVGs establish the destination, explanatory diagrams make architecture legible, rendering experiments explore manifestation, and runnable examples prove bounded behavior. These evidence classes must remain distinct; no diagram or experiment should be described as a completed playable game or end-to-end host launch.

| Standard position | README topic | Intended reader outcome |
|---|---|---|
| 00 | Identity, badges, flagship statement, hero SVG | Understand the ambition before reading implementation detail. |
| 01 What | First-person living-war Experience | Understand what raWWar is. |
| 02 Why | World visuals, persistent-world thesis, player vision | See why the Experience matters and what makes it compelling. |
| 03 How | Domain ownership, MicroBundles, FSM_COS, RuntimeAssembly, host boundary | Understand how the system is intended to be assembled. |
| 04 See it in a minute | Build command, KeplerOrbit example, evidence limitations | Try a verified, bounded behavior without mistaking it for the full game. |
| 05 Available documentation and theory | Design authority, technical references, visual catalogue, ecosystem links | Choose the right deeper path for the reader's role. |
| Supplemental local headings | Status, engineering discipline, ecosystem | Preserve necessary implementation detail without extending the global numbered front door. |

The number and marker color are navigation aids, not the meaning itself. GitHub Markdown does not guarantee arbitrary heading colors, so the portable number-plus-emoji convention is used as described by the reference standard.

## Architectural accuracy rules

- **raWWar owns the Experience domain.** Soldiers, factions, qualifications, equipment, procedures, world history, and consequences are game meaning.
- **FSM_COS owns composition.** It resolves the requested capability composition and hands off a RuntimeAssembly. It is not the host, the renderer, the game, or a general-purpose application loop.
- **MicroBundleDomain owns the MicroBundle contract.** raWWar must consume the real versioned contract rather than document an imagined local substitute.
- **The host owns execution and manifestation.** AnyApp is the first host direction; raWWar does not make AnyApp part of its domain logic.
- **A manifest root is not the transitive dependency closure.** Do not imply that a publication manifest, authoring manifest, and runtime manifest are interchangeable.
- **Source and tests govern implementation claims.** Design intent, local composition tests, package creation, and end-to-end host loading are different levels of evidence.

See [Experience Architecture](EXPERIENCE_ARCHITECTURE.md) and [the AnyApp manifest bridge investigation](integration/ANYAPP_MANIFEST_BRIDGE.md).

## Visual and example rules

- Prefer version-controlled SVGs for architecture visuals; keep labels, direction, alt text, and captions meaningful.
- Distinguish conceptual diagrams from runtime captures and construction-ready engineering drawings.
- Maintain [the visual asset catalogue](images/README.md) when adding or materially changing visuals.
- Maintain [the examples index](../examples/README.md) when consuming a package, schema, API, or capability.
- Label examples as verified, source-shaped, or conceptual; record the version/commit, assumptions, boundary behavior, and what the example does not prove.
- Do not copy a dependency's full domain documentation into raWWar. Explain why raWWar consumes it and link to the owner.

## Current conformance state

### Applied

- [x] README uses the canonical `00`–`05` front-door order, with the raWWar hero SVG immediately after identity/badges and before section 01.
- [x] README section 00 now provides explicit Workshop/Experience identity plus verified-license and development-CI badges; no package publication badge is implied.
- [x] The responsibility boundary explicitly describes FSM_COS as composition rather than host execution.
- [x] README describes the current manifest/AnyApp integration blocker without implying end-to-end loading.
- [x] Example and contract-check links are included with limits on what they prove.
- [x] Shared Workshop footer and related-project navigation are included.
- [x] Existing visual-first game-design voice is retained, with raWWar explicitly framed as the intended flagship Experience for the Workshop/AnyApp system.
- [x] The README distinguishes concept visuals, architecture explanation, rendering experiments, executable examples, and end-to-end playable/host evidence.

### Audit findings and changes

- [x] Clarified the Game Design Bible's role as design authority, the GDD's role as comprehensive experience description, and specialist documents' obligation not to silently overrule creator canon.
- [x] Added owner, audience, status, and implementation-evidence metadata to the GDD; made its conflict-resolution rule explicit.
- [x] Added scope/status/evidence headers to Vision and Pillars, Systems Design, Player Roles, and UX and Interaction so conceptual opportunities are not mistaken for implemented features.
- [x] Added ownership, audience, purpose, and evidence-rule metadata to the Game Design Bible, Technical Design, and Production Plan.
- [x] Reaffirmed the critical boundary: FSM_COS composes capabilities; it is not the game loop, host, renderer, or source of raWWar's domain meaning.
- [x] Technical Design distinguishes intended architecture from implemented orbit-query coverage and from unproven end-to-end AnyApp integration.
- [x] Experience Architecture includes an implementation-evidence table and identifies what current scaffold/package/composition checks do not prove.
- [x] Open Questions is explicitly labeled as an elicitation register; unanswered questions are not implied decisions, and the Gesture question is numbered consistently.
- [x] The visual asset catalogue identifies its owner, audience, status, and evidence rule so conceptual artwork is not mistaken for completed runtime or engineering output.
- [x] Art Direction now records owner, audience, status, and evidence boundaries; a duplicated soldier-description block was removed without changing its intent.
- [x] Audio Direction now records owner, audience, status, and evidence boundaries.
- [x] Corrected the README license link to match the repository's actual `LICENSE` path after a targeted navigation check.
- [x] Corrected stale fighter-pilot prototype claims in the Technical Design, master GDD, Open Questions, and contract-test README: the 195/195 result is historical evidence for a reverted code/test head, not proof that the prototype or tests exist on the current branch. The remaining station/rig wording now explicitly identifies the prototype as historical and keeps hardware/control-law choices unresolved.

### Remaining audit work

- [ ] Finish the full GDD and companion-document review for contradictions, stale APIs, missing prerequisites, and malformed links; this pass establishes clearer authority and evidence rules, not exhaustive line-by-line validation.
- [x] Added [Flagship Experience Story](FLAGSHIP_EXPERIENCE_STORY.md), a connected reader journey from the living world and soldier identity through facility relationships, Workshop architecture, and bounded procedural/executable evidence; each visual's caption names its evidence class and scope.
- [x] Connected the story to the source-backed [Visualize → GUI.SVG generated artifact](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/samples/PrimitiveGallery/generated/07-gui-svg-connected-points.svg), its [C# proof program](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/integrations/GuiSvg/Proof/Program.cs), and its [Primitive Gallery recipe](https://github.com/TrentBest/TheSingularityWorkshop.Visualize/blob/development/samples/PrimitiveGallery/README.md). The caption limits the claim to a 2D geometry/resource/frame-to-SVG handoff with SVG animation; it does not claim production 3D rendering or raWWar runtime integration.
- [ ] Capture a raWWar-specific manifestation through the real Workshop path and connect it to an observable first-person station/rig slice and AnyApp host launch. The Visualize proof is a real adjacent capability, not a substitute for this Experience-level integration.
- [x] Ran a repository-wide relative inline-Markdown-link target check across all 102 Markdown files (263 inline links); no missing local file targets were found.
- [x] Scanned all 102 Markdown files for inline Markdown fragment links and raw HTML `href` / `src` attributes; none were found in the checked content. All 21 SVGs also contain no `href` / `xlink:href` references.
- [ ] Validate reference-style Markdown links and external-link availability; the inline-link and embedded-reference checks do not cover those.
- [ ] Ensure every technical capability consumed by raWWar has a repository-local example or a documented reason an example is not yet executable.
- [ ] Continue replacing repeated explanations with one authoritative document and clear cross-links.
- [x] Historical CI evidence: run [37999757533](https://github.com/TrentBest/raWWar/actions/runs/37999757533) passed for head `e76736aea714367f57301e1a6559b59ed5c14493`; this is not verification of later documentation commits.
- [ ] Re-check CI for the current `development` head before describing the branch as green. Immediately before this note-only commit, the checked head was `240e6b81b7113b515054526f792252c894fc5f3e`; the available workflow/status queries returned no checks for that head. This note-only commit also remains unverified until CI reports a result.


This audit is intentionally incremental. The shared FSM_COS standard is still a proposal under review, and this pass does not claim universal or exhaustive conformance.
