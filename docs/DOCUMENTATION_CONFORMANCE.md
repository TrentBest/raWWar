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
| 00 | Identity and badges | Repository title and identity remain the page entry point; do not invent package/release badges for a game Experience. |
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
- [x] The responsibility boundary explicitly describes FSM_COS as composition rather than host execution.
- [x] README describes the current manifest/AnyApp integration blocker without implying end-to-end loading.
- [x] Example and contract-check links are included with limits on what they prove.
- [x] Shared Workshop footer and related-project navigation are included.
- [x] Existing visual-first game-design voice is retained.

### Audit findings and changes

- [x] Clarified the Game Design Bible's role as design authority, the GDD's role as comprehensive experience description, and specialist documents' obligation not to silently overrule creator canon.
- [x] Added ownership, audience, purpose, and evidence-rule metadata to the Game Design Bible, Technical Design, and Production Plan.
- [x] Reaffirmed the critical boundary: FSM_COS composes capabilities; it is not the game loop, host, renderer, or source of raWWar's domain meaning.
- [x] Technical Design distinguishes intended architecture from implemented orbit-query coverage and from unproven end-to-end AnyApp integration.
- [x] Experience Architecture includes an implementation-evidence table and identifies what current scaffold/package/composition checks do not prove.

### Remaining audit work

- [ ] Finish the full GDD and companion-document review for contradictions, stale APIs, missing prerequisites, and malformed links; this pass establishes clearer authority and evidence rules, not exhaustive line-by-line validation.
- [ ] Verify relative links across the documentation set and visual catalogue.
- [ ] Ensure every technical capability consumed by raWWar has a repository-local example or a documented reason an example is not yet executable.
- [ ] Continue replacing repeated explanations with one authoritative document and clear cross-links.
- [ ] Re-run CI after the documentation changes and record the actual result; empty status/run responses are **unverified**, not green.

This audit is intentionally incremental. The shared FSM_COS standard is still a proposal under review, and this pass does not claim universal or exhaustive conformance.
