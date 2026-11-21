# Request from raWWar — Authenticated decision inbox and browser notifications

**Requesting project:** raWWar Experience / Autonomous Development proving ground  
**Receiving repository:** [WebPage](https://github.com/TrentBest/WebPage) (coordinate with the ServerApp owner for the intended durable service boundary)  
**Status:** Proposed — not yet accepted or implemented  
**Evidence baseline:** WebPage is an existing Blazor WebAssembly browser host with Azure Static Web Apps authentication and an Azure Functions v4 API project. The current API is a public naming-poll example backed by Azure Table Storage; no browser Push API/service-worker notification implementation or durable human-decision inbox was found in the inspected tree.

## Problem and evidence

CODE RED needs to notify the appropriate human when a decision is required, allow that person to sign in from any supported browser, and present an aggregated inbox across the repositories they are authorized to review.

The existing WebPage application already has:
- a Blazor WebAssembly browser client;
- StaticWebAppAuthenticationStateProvider, which reads the Azure Static Web Apps /.auth/me principal;
- a PollingAPI Azure Functions v4 project;
- an existing Azure Table Storage example in PollingAPI/VoteFunction.cs.

These are useful starting points, not a completed secure inbox. The current vote endpoints use anonymous authorization and must not be reused as authenticated approval endpoints. The client-side authentication provider is not, by itself, server-side authorization. No existing service-worker/Web Push implementation was found in the inspected repository tree.

## Desired outcome

Provide a browser-independent human decision experience with two separate delivery layers:

1. **Durable inbox (authoritative):** authenticated users can sign in and retrieve pending, grouped, deferred, answered, expired, and historical decisions across authorized repositories. The inbox remains available if push or email delivery fails.
2. **Notifications (best effort):** opted-in browsers can receive Web Push notifications through a service worker and the standard browser Push/Notification APIs. Email remains a fallback/invitation channel where configured. Notification payloads should contain only a minimal opaque decision reference; sensitive task/evidence content is fetched after authentication.

The initial host may use WebPage's existing Azure Static Web Apps + Azure Functions shape if the receiving repository confirms that it is suitable. The long-term ServerApp role should be evaluated rather than assumed: its current repository surface is only a minimal shell, not an existing notification backend.

## Required user and repository model

- A repository maps to its configured decision recipients (GitHub users/teams and, where explicitly configured, verified email addresses).
- A person signs in using the supported identity provider and is authorized against the repository/decision assignment on the server.
- Do not trust a client-supplied username, email, repository list, role, or approval scope.
- A person may have access to multiple repositories; the inbox aggregates only records the server confirms they may see.
- The UI should show repository identity, task, exact commit/evidence identity, decision status, age, and the policy/rule (if any) that grouped or pre-authorized similar cases.

## Decision and policy behavior

- A decision is persisted before a notification is attempted.
- A notification is not an approval, and opening a notification does not change task state.
- A one-time answer is distinct from approving a reusable rule.
- Reusable rules have an explicit scope, stable identity/version, human approver, rationale, and revocation mechanism.
- Similar decisions may be grouped for review, but every member remains individually inspectable with its own task and evidence references.
- A rule applies only to cases that match its declared scope; uncertain matches return to a human.
- Free-form responses are preserved as user input, then interpreted/validated by the host. An LLM may propose a structured interpretation but cannot silently convert prose into authorization.

## Security and reliability requirements

- Enforce authentication and repository/decision authorization server-side on every read and write.
- Store notification subscriptions and decision records under the authenticated principal; protect subscription endpoints against cross-user assignment and replay.
- Use a standards-based browser push design (service worker + Push API + Notification API, with server-side VAPID key management) only after explicit user opt-in and permission.
- Do not place secrets, full prompts, private source, or sensitive evidence in push/email payloads or logs.
- Use opaque, expiring notification references and fetch the decision after sign-in.
- Support unsubscribe, endpoint rotation, expired subscriptions, duplicate delivery, retries, and idempotent answer submission.
- Persist decisions and answers durably; notification failure must not lose a decision.
- Maintain an audit trail for who answered, what they answered, whether it was one-time or reusable, which policy version applied, and what task/evidence identities were in scope.
- Fail closed if repository identity, authenticated principal, task identity, evidence digest, or policy scope cannot be verified.

## Acceptance evidence

1. A user can authenticate in a browser and see only their authorized repository/decision inbox.
2. A decision is available in the inbox before and after notification delivery.
3. At least two supported browser families receive an opted-in Web Push notification, and a denied/unavailable permission path still leaves the durable inbox functional.
4. Email invitation/notification routing uses explicitly configured recipients and does not expose private addresses.
5. One-time answers and reusable policies are persisted distinctly and audited.
6. A grouped decision can be expanded to inspect every member, its repository, exact task/SHA/evidence, and applicable policy version.
7. A rich-text answer can be submitted, retrieved, and interpreted without treating model interpretation as approval.
8. Cross-user, cross-repository, expired, replayed, and out-of-scope requests are rejected by server-side tests.

## Constraints and non-goals

- WebPage owns the browser manifestation; the backend/host owns durable state, authentication enforcement, authorization, notifications, and audit.
- Prefer existing Workshop and Azure hosting capabilities where they are verified; do not introduce a second scheduler, approval ledger, or CI verdict engine.
- Do not expose GitHub write credentials to the browser.
- Do not reuse the existing anonymous vote endpoints for approval.
- This request does not authorize changes to WebPage or ServerApp by the raWWar agent, nor package publication, merge, release, or deployment.

## Related raWWar work

- [Autonomous Development Integration](../AUTONOMOUS_DEVELOPMENT_INTEGRATION.md#10-human-decision-and-reporting-surface)
- [raWWar issue #9 — flagship Autonomous Development consumer](https://github.com/TrentBest/raWWar/issues/9)
- [WebPage authentication provider](https://github.com/TrentBest/WebPage/blob/master/TheSingularityWorkshop/Services/StaticWebAppAuthenticationStateProvider.cs)
- [WebPage Azure Functions API](https://github.com/TrentBest/WebPage/tree/master/PollingAPI)

## Resolution record

Leave this request **proposed** until the receiving repository evaluates it and the acceptance evidence is demonstrated.
