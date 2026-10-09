# Request from raWWar: MicroBundle Experience configuration contract

- **Requesting repository:** [TrentBest/raWWar](https://github.com/TrentBest/raWWar)
- **Receiving repository:** [TrentBest/TheSingularityWorkshop.MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain)
- **Status:** Ready for receiving-agent review; not an implementation assignment
- **Priority:** High for configuration-driven Experience composition
- **Related raWWar contract:** [Configuration and Provider Resolution](../architecture/CONFIGURATION_AND_PROVIDER_RESOLUTION.md)
- **Related integration context:** [AnyApp manifest bridge](../integration/ANYAPP_MANIFEST_BRIDGE.md)

## Problem

raWWar needs an Experience to provide configuration overrides to installed MicroBundles without taking ownership of each bundle's configuration schema or default behavior. The current integration investigation has not yet established a fully verified, published contract that raWWar can use end to end for parameter overrides, defaults, validation, and provider declarations.

The concepts must remain distinct:

- **Parameter ID:** identifies a configurable parameter.
- **Override value:** the literal value supplied by the Experience.
- **Provider ID:** identifies a capability provider; it is not a parameter or its value.
- **Default and validation rules:** belong to the MicroBundle that owns the parameter.
- **Missing override:** should preserve the owning bundle's defined default unless its contract explicitly says otherwise.
- **Provider lookup:** may return no provider; required and optional capability absence need explicit behavior.

Transport encoding (for example, a host's Base64 field) is not itself the domain-level configuration contract.

## Desired outcome

A clear, versioned, consumable MicroBundle configuration contract that allows an Experience or host to supply overrides while the owning bundle retains responsibility for parameter meaning, defaults, validation, and declared capabilities.

The receiving agent should choose the appropriate API and representation for MicroBundleDomain. This request does not mandate a particular interface, class layout, serialization format, or dependency.

## Acceptance evidence

raWWar can add executable checks demonstrating, using the actual supported contract, that:

1. A parameter ID is not confused with its override value or a provider ID.
2. An omitted override preserves the owning bundle's default.
3. Supplied overrides are validated according to the owning bundle's rules.
4. Unknown or invalid parameters fail clearly and deterministically, according to the agreed contract.
5. Optional provider absence is handled explicitly, while missing required capability is reported clearly.
6. The behavior is documented and can be consumed without raWWar duplicating generic MicroBundle configuration machinery.

The exact tests should be reconciled with the receiving agent's chosen API rather than assuming these concepts already exist in the current published package.

## Constraints and non-goals

- raWWar owns game-specific configuration meaning; MicroBundleDomain owns reusable configuration mechanics.
- Do not make ProtocolAi a mandatory dependency solely to represent parameter names or values.
- Do not claim a proposed or development-only API is available in a published package until verified.
- This document records a requirement; it does not authorize changes in the receiving repository or a package release.

## Evidence still needed

Before raWWar integrates the final contract, verify the actual receiving-repository API, package version, and behavior against source and tests. Update this request with the verified contract and acceptance evidence when available.
