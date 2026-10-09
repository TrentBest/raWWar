# raWWar Documentation Conformance

**Status:** Active alignment pass.  
**Reference:** [FSM_COS Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/docs/ecosystem-documentation-standard/DOCUMENTATION_STANDARD.md).  
**Reference maturity:** The cited standard is on FSM_COS's `docs/ecosystem-documentation-standard` branch and is a proposal under review, not yet a claim that every Workshop repository has adopted it.

## Purpose

raWWar should share the Workshop's documentation language without becoming a generic package README. FSM_COS is the architectural lynchpin for capability composition, but it does not own raWWar's world model, soldier meaning, game rules, host lifecycle, or renderer. Documentation must preserve that boundary while making the Experience understandable to newcomers and useful to engineers.

The governing editorial rule is **edify, not mystify**: explain the human problem, show the concept, define the technical boundary, provide examples, and give evidence for implementation claims.

## Standard elements applied to the README

The README now uses the shared semantic section identifiers and colored markers where they apply:

| ID | Topic | raWWar treatment |
|---|---|---|
| 00 | Identity and badges | Repository identity is explicit; MIT license and development-branch CI badges point to the real license and workflow. No NuGet release badge is implied for an Experience that has not been published. |
| 01 | Definition | Defines raWWar as a first-person war Experience, not an application, in the opening paragraph. |
| 02 | Visual identity | Hero and galaxy visuals establish the Experience's visual identity. |
| 03 | Plain-language explanation | Explains the living war and continuous galaxy before deep implementation details. |
| 04 | Audience / choose your path | Routes readers by intent rather than requiring a linear read. |
| 05 | At a glance | States type, host direction, .NET baseline, architecture, and honest implementation status. |
| 06 | Responsibility boundary | Separates raWWar domain ownership from reusable Workshop machinery. |
| 07 | Architecture and ecosystem | Shows manifest → MicroBundle roots → FSM_COS → RuntimeAssembly → host and explicitly records current integration limits. |
| 08 | Quick start | Provides the shortest useful reader path and concrete developer commands. |
| 09 | Core concepts / how it works | Introduces player roles and the Canon/Candidate/Experiment/Illumination Needed certainty model. |
| 10 | Usage and examples | Links the examples index and contract checks, with explicit evidence expectations. |
| 11 | Verification and development | Distinguishes the current scaffold from the intended playable slice and points to the evidence queue. |
| 12 | Documentation map / further reading | Provides reader-oriented paths into canonical and specialist documents. |
| 13 | Related projects and Workshop footer | Explains relevant neighboring packages and preserves the shared Workshop identity. |

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

- [x] README has a reader-oriented path and the shared section identifiers/marker colors.
- [x] README section 00 now provides explicit Workshop/Experience identity plus verified-license and development-CI badges; no package publication badge is implied.
- [x] The responsibility boundary explicitly describes FSM_COS as composition rather than host execution.
- [x] README describes the current manifest/AnyApp integration blocker without implying end-to-end loading.
- [x] Example and contract-check links are included with limits on what they prove.
- [x] Shared Workshop footer and related-project navigation are included.
- [x] Existing visual-first game-design voice is retained.

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

### Remaining audit work

- [ ] Finish the full GDD and companion-document review for contradictions, stale APIs, missing prerequisites, and malformed links; this pass establishes clearer authority and evidence rules, not exhaustive line-by-line validation.
- [x] Ran a repository-wide relative inline-Markdown-link target check across all 102 Markdown files (263 inline links); no missing local file targets were found.
- [ ] Validate heading anchors, reference-style links, raw HTML `href`/`src` paths, and external-link availability; those checks are not covered by the inline-link pass.
- [ ] Ensure every technical capability consumed by raWWar has a repository-local example or a documented reason an example is not yet executable.
- [ ] Continue replacing repeated explanations with one authoritative document and clear cross-links.
- [x] Re-ran CI after the README/conformance/queue changes; run [37999757533](https://github.com/TrentBest/raWWar/actions/runs/37999757533) passed for head `e76736aea714367f57301e1a6559b59ed5c14493`.


This audit is intentionally incremental. The shared FSM_COS standard is still a proposal under review, and this pass does not claim universal or exhaustive conformance.
