# Configuration and provider resolution

**Status:** Contract clarification for the raWWar/Workshop integration; implementation is not yet claimed.
**Reviewed:** 2026-10-09
**Working branch:** `development`

## The responsibility chain

The Experience configures the MicroBundles it requests. Each MicroBundle declares its own identity, dependencies, and available providers. During composition/use, code asks for a provider through the supported checked lookup; the requested provider may not exist.

```text
Experience
  | selects MicroBundles and supplies parameter-ID/value overrides
  v
MicroBundle
  | owns parameter meaning, defaults, validation, and provider availability
  v
Checked provider lookup
  | present                         | absent
  v                                 v
Use provider result            Explicit unavailable outcome
                                    |
                                    v
                         Capability-defined fallback,
                         optional behavior, or clear failure
```

This is a contract sketch, not a claim that every branch or provider-resolution API is already implemented in raWWar.

## Configuration is an override map, not a second registry

A configuration entry is conceptually:

- **Parameter ID:** which MicroBundle-owned parameter is being overridden.
- **Value:** the literal value supplied for that parameter.

The parameter ID is not the value. Neither is automatically a provider ID. Keep those identities distinct.

A bundle with no override uses its own defaults. A supplied override should be validated by the owning bundle against the parameter's expected type, range, units, and domain constraints. Unknown parameter IDs should not silently change unrelated behavior; the configuration contract must specify whether they are rejected, reported, or preserved for a forward-compatible consumer.

Example shape (illustrative only; not a finalized wire schema):

```json
{
  "parameters": [
    { "id": "combat.damage-model", "value": "localized" },
    { "id": "vehicle.max-operating-temperature-k", "value": 820 }
  ]
}
```

These IDs and values are examples, not raWWar canon or implemented parameter definitions. A final format must specify type representation, duplicate-ID handling, units, validation, versioning, and how missing values inherit defaults.

## Provider availability is checked

A MicroBundle's descriptor/contract is where provider availability is declared. Consumers request a provider using the supported lookup operation and handle both outcomes.

- **Present:** use the returned provider through its real contract.
- **Absent:** follow the specific capability's documented optional behavior, choose an explicitly permitted alternative, or fail with a clear diagnostic.

Do not infer that a provider exists merely because a configuration entry names it. Do not manufacture a provider when lookup fails. Do not make every provider mandatory just because one Experience uses it.

## Where ProtocolAi may help

[TheSingularityWorkshop.ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) describes a provider-neutral, deterministic vocabulary for application-owned semantic identities. That makes it a promising *optional* tool for defining/resolving stable parameter IDs or provider IDs across authored data, tools, and future LLM-facing workflows.

Potential division of responsibility:

| Concern | Owner |
|---|---|
| Stable vocabulary / identity mapping | ProtocolAi, if adopted by the owning package |
| Parameter definitions, type, units, default and validation | The MicroBundle that owns the parameter |
| Per-Experience override values | Experience configuration |
| Provider declaration and checked availability | MicroBundle contract / composition system |
| Authorization, object creation, execution and consequences | The consuming application or capability |

ProtocolAi should not be introduced as a required dependency solely because IDs exist. First prove the need for shared vocabulary, and verify the exact API/version on the ProtocolAi branch intended for integration. The MicroBundle contract must remain understandable and usable without an LLM.

## FSM_COS boundary

The FSM_COS runtime manifest identifies runtime roots and requested bundle versions; configuration remains a separate concern carried by the supported configuration source. AnyApp's current publication manifest has its own configuration transport field, but that host representation must not be confused with the domain-level meaning of parameter IDs and values.

The host passes configuration; it should not need to interpret each bundle's private parameters. A MicroBundle consumes its own overrides. Provider absence remains a valid runtime condition unless a specific required dependency or capability contract says otherwise.

## Required executable proofs before implementation is called complete

1. No configuration means the owning MicroBundle's defaults remain effective.
2. A known parameter ID overrides exactly the intended parameter with the intended value.
3. Invalid type/range/unit and duplicate parameter IDs follow explicit policy.
4. Unknown parameter IDs follow explicit policy and never silently target another parameter.
5. A provider lookup succeeds when the provider is declared and available.
6. A provider lookup reports absence without null dereference, fabricated provider, or implicit substitution.
7. Optional absence follows a documented fallback; required capability absence produces a clear failure.
8. Provider and parameter identities remain distinct in manifests, configuration, diagnostics, and any ProtocolAi vocabulary.
9. The behavior works without a ProtocolAi dependency unless the owning package explicitly adopts it.


## Usage examples

The examples index at [`examples/README.md`](../../examples/README.md) is the source of truth for checked-in usage examples, including their verification boundaries. When this contract becomes consumable through a verified package API, add a runnable raWWar-side configuration/provider example there; this document alone does not prove that a specific provider-lookup API exists in the currently referenced MicroBundleDomain package.
