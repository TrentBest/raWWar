# Cross-repository capability requests

**Owner:** raWWar  
**Scope:** Requests raWWar makes to other Workshop repositories  
**Status:** Convention proposal in use on `development`

These documents record a need discovered while engineering raWWar. They do **not** assign implementation work or prescribe the receiving repository's design. The agent responsible for the receiving repository owns evaluating the request, choosing an appropriate solution, and making any changes there.

## Filename convention

`REQUEST-FROM-raWWar-<Dependency>-<Capability>.md`

Examples:

- `REQUEST-FROM-raWWar-MicroBundleDomain-ExperienceConfiguration.md`
- `REQUEST-FROM-raWWar-AnyApp-ArtifactClosure.md`

The filename identifies the requester, the repository whose capability is involved, and the subject. Keep the capability phrase short, stable, and specific.

## Required document contents

Each request should state:

1. **Requesting project and receiving repository**
2. **Status** — proposed, ready for review, accepted, in progress, blocked, or resolved
3. **Problem and evidence** — what raWWar is trying to do and what current behavior/API prevents it
4. **Desired outcome** — observable behavior, not a preselected implementation
5. **Acceptance evidence** — tests or concrete checks that would demonstrate the need is met
6. **Constraints and non-goals** — especially ownership boundaries and compatibility requirements
7. **Related raWWar work** — links to the local integration contract or TODO entry

Requests describe raWWar's requirements. They must not imply that another repository has accepted the request or that a proposed solution is already part of its API.

## Ownership and workflow

- This assistant changes **only** `TrentBest/raWWar`.
- Keep the originating request in raWWar so its rationale and acceptance criteria survive conversation resets.
- Do not open issues, edit files, create branches/PRs, or implement changes in a receiving repository. The creator coordinates with that repository's responsible agent.
- The receiving agent may refer to the request and determine whether to fulfill it as written, propose an alternative, or explain why the need belongs elsewhere.
- When a contract changes, update the request's status and record the verified version/API and evidence. Do not mark a request resolved solely because a change was proposed.
- No request authorizes package publication or merging.

## Current requests

- [MicroBundleDomain — Experience configuration contract](REQUEST-FROM-raWWar-MicroBundleDomain-ExperienceConfiguration.md)
- [AnyApp — immutable artifact dependency closure](REQUEST-FROM-raWWar-AnyApp-ArtifactClosure.md)
