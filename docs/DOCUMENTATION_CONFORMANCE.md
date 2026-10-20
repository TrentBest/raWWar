# raWWar Documentation Conformance

**Status:** Active alignment pass.  
**Reference:** [FSM_COS Documentation Standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/development/DOCUMENTATION_STANDARD.md).  
**Reference maturity:** The cited standard is the current working reference on FSM_COS's `development` branch. Adoption remains repository-by-repository; this audit does not claim universal or exhaustive conformance.

## Purpose

raWWar should share the Workshop's documentation language without becoming a generic package README. FSM_COS is the architectural lynchpin for capability composition, but it does not own raWWar's world model, soldier meaning, game rules, host lifecycle, or renderer. Documentation must preserve that boundary while making the Experience understandable to newcomers and useful to engineers.

The governing editorial rule is **edify, not mystify**: explain the human problem, show the concept, define the technical boundary, provide examples, and give evidence for implementation claims.

## Standard elements applied to the README

The shared front-door sequence is `00` identity, then a meaningful money-shot visual, `01` What, `02` Why, `03` How, `04` See it in a minute, and `05` Available documentation and theory. raWWar's current README still needs a deliberate reorganization to that sequence; this pass records that gap rather than relabeling existing content and implying it is compliant.

The README uses raWWar-specific section names and colored markers, but its current `00`–`13` sequence predates the shared standard's canonical front-door sequence. The mapping below records the existing content; it does **not** mean the README has completed the current `00`–`05` conformance pass. That front-door alignment remains open work:

| Existing ID | Current README topic | Existing treatment |
|---|---|---|
| 00 | Identity and badges | Repository title and license/CI badges; the shared animated Workshop moniker has now been added in the documentation-standardization branch. |
| 01 | The opportunity | Explains the Experience and its broader purpose. |
| 02 | The world we want to make tangible | Uses raWWar concept visuals to establish the intended world. |
| 03 | The living-world thesis | Explains persistent activity and the core design thesis. |
| 04 | Why this is different | Describes the Experience's distinguishing premise. |
| 05 | Progress and present status | Records implementation maturity and limits. |
| 06 | What raWWar owns—and what it reuses | Defines the Experience/Workshop responsibility boundary. |
| 07 | The technology strategy | Explains the architecture and reusable ecosystem. |
| 08 | What can be verified today | Separates available evidence from design targets. |
| 09 | The experience vision | Describes the intended player experience. |
| 10 | Evidence and examples | Links examples and describes their evidentiary limits. |
| 11 | Engineering discipline | States evidence and implementation-truth expectations. |
| 12 | Explore the deep dives | Routes readers to specialist documentation. |
| 13 | The Workshop ecosystem | Links related projects and preserves raWWar's domain ownership. |

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
- [x] Corrected stale fighter-pilot prototype claims in the Technical Design, master GDD, Open Questions, and contract-test README: the 195/195 result is historical evidence for a reverted code/test head, not proof that the prototype or tests exist on the current branch. The remaining station/rig wording now explicitly identifies the prototype as historical and keeps hardware/control-law choices unresolved.

### Remaining audit work

- [ ] Finish the full GDD and companion-document review for contradictions, stale APIs, missing prerequisites, and malformed links; this pass establishes clearer authority and evidence rules, not exhaustive line-by-line validation.
- [x] Ran a repository-wide relative inline-Markdown-link target check across all 102 Markdown files (263 inline links); no missing local file targets were found.
- [x] Scanned all 102 Markdown files for inline Markdown fragment links and raw HTML `href` / `src` attributes; none were found in the checked content. All 21 SVGs also contain no `href` / `xlink:href` references.
- [ ] Validate reference-style Markdown links and external-link availability; the inline-link and embedded-reference checks do not cover those.
- [ ] Ensure every technical capability consumed by raWWar has a repository-local example or a documented reason an example is not yet executable.
- [ ] Continue replacing repeated explanations with one authoritative document and clear cross-links.
- [x] Historical CI evidence: run [37999757533](https://github.com/TrentBest/raWWar/actions/runs/37999757533) passed for head `e76736aea714367f57301e1a6559b59ed5c14493`; this is not verification of later documentation commits.
- [ ] Re-check CI for the current `development` head before describing the branch as green. Immediately before this note-only commit, the checked head was `240e6b81b7113b515054526f792252c894fc5f3e`; the available workflow/status queries returned no checks for that head. This note-only commit also remains unverified until CI reports a result.


This audit is intentionally incremental. The shared FSM_COS standard is still a proposal under review, and this pass does not claim universal or exhaustive conformance.
