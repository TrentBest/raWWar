# Request from raWWar — LLM-composed human decision surface

**Requesting project:** raWWar Experience / Autonomous Development proving ground  
**Receiving repository:** [TheSingularityWorkshop.GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI)  
**Status:** Proposed — not yet accepted or implemented  
**Evidence baseline:** The Workshop GUI architecture defines a platform-neutral semantic tree and Blazor manifestation, but its own architecture document lists semantic input as not yet a stable runtime contract. raWWar's CODE RED integration now needs a human-facing decision surface rather than a pass/fail-only report.

## Problem and evidence

The Autonomous Development Pipeline must bring a human into the loop when an agent reaches a decision it cannot safely make: for example, choosing between visual treatments, resolving an ambiguous requirement, accepting a proposed behavior, or defining a reusable rule.

The human should not receive an opaque blob of model text or a generic form disconnected from the evidence. The LLM should compose an explanation and the smallest useful interaction from a constrained semantic vocabulary. Depending on the ambiguity, that may be a choice list, a multi-select, a structured approval, or a rich-text response.

The existing GUI architecture identifies GUI.Core as the semantic model, GUI.Blazor as the browser manifestation, and the host as the owner of policy and authorization. It also explicitly says semantic input is not yet a stable runtime contract. This request asks the receiving repository to evaluate the missing contract; it does not assume it already exists.

## Desired outcome

Define a safe, renderer-independent decision-surface contract that allows an LLM to propose a human interaction without granting the model authority to execute it.

A decision presentation should be able to express:

- the question and why a decision is needed;
- the ambiguity or competing interpretations;
- available choices, including a recommendation and the reasons for it;
- consequences, scope, and any affected repositories/tasks;
- evidence references and exact task/repository/commit identity;
- a structured answer when possible;
- a rich-text answer when structured options are insufficient;
- explicit submit, defer, reject, or request-more-context actions.

The GUI renderer must own the actual controls and safe manifestation. The model may select from supported semantic controls and provide content, but must not emit executable HTML, JavaScript, arbitrary component types, or event handlers.

## Reusable decision policies

The surface should support a second class of decision: **approve this choice as a reusable rule for similar future cases**.

The interaction should distinguish a one-time answer from a reusable policy. A reusable policy must have a visible scope and match description, a human-confirmed rule, a stable policy identity/version, a rationale, and a revocation path. The system should show which decisions were grouped as similar and which policy version applies to each one. LLM similarity suggestions are candidates for human review, not proof that two decisions are equivalent.

A human should be able to review a grouped set, inspect each member and its evidence, then approve or reject the common rule. Applying a rule must not silently authorize actions outside its declared match scope.

## Acceptance evidence

1. A platform-neutral semantic contract represents a decision, options, evidence references, and answer shape.
2. A browser manifestation renders the same decision contract without requiring the LLM to author platform markup or executable code.
3. Structured choice and rich-text response paths both have validation and tests.
4. A one-time answer is distinguishable from a reusable policy approval.
5. Similarity grouping is inspectable, versioned, and reviewable per affected decision.
6. Missing, malformed, stale, or mismatched task/evidence identity fails closed.
7. Model output cannot authorize an action; the host validates the submitted answer and performs any consequential action through its own authorization boundary.
8. Keyboard accessibility, screen-reader semantics, and responsive browser behavior are tested.

## Constraints and non-goals

- GUI owns semantic presentation contracts and manifestations; the host owns identity, policy, authorization, persistence, and execution.
- Do not put GitHub credentials, repository write permissions, or authorization decisions in GUI.Core or GUI.Blazor.
- Do not treat an LLM-generated interface description as executable code.
- Do not add a parallel approval ledger or task scheduler to GUI.
- Do not assume the current 14-kind visual vocabulary already provides a stable semantic input contract.
- This request does not authorize package publication, merge, release, or deployment.

## Related raWWar work

- [Autonomous Development Integration](../AUTONOMOUS_DEVELOPMENT_INTEGRATION.md#10-human-decision-and-reporting-surface)
- [raWWar issue #9 — flagship Autonomous Development consumer](https://github.com/TrentBest/raWWar/issues/9)
- [GUI architecture](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/master/docs/ARCHITECTURE.md)

## Resolution record

Leave this request **proposed** until the receiving repository evaluates it and the acceptance evidence is demonstrated.
