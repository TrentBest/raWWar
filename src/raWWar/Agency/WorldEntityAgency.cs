namespace TheSingularityWorkshop.raWWar.Agency;

/// <summary>Stable domain identity for a person or organization participating in an Experience.</summary>
public readonly record struct WorldEntityPrincipalId(string Value);

/// <summary>One narrowly scoped action that may be authorized against a world entity.</summary>
[Flags]
public enum WorldEntityCapability : byte
{
    None = 0,
    Operate = 1,
    DelegateOperation = 2,
    ManageOperations = 4
}

/// <summary>An explicit assignment of direct operating responsibility for one entity.</summary>
public readonly record struct WorldEntityDirectOperator(
    History.WorldEntityId EntityId,
    WorldEntityPrincipalId OperatorId);

/// <summary>
/// A trusted domain-issued mandate describing the capabilities an organization holds over one entity.
/// This value is an authorization input, not proof of legal ownership or an authentication mechanism.
/// </summary>
public readonly record struct WorldEntityOrganizationMandate(
    History.WorldEntityId EntityId,
    WorldEntityPrincipalId OrganizationId,
    WorldEntityCapability Capabilities);

/// <summary>
/// A revocable, entity-scoped delegation from an organization to one principal.
/// Delegation grants only the stated capability; it never transfers the organization's mandate.
/// </summary>
public readonly record struct WorldEntityDelegation(
    History.WorldEntityId EntityId,
    WorldEntityPrincipalId OrganizationId,
    WorldEntityPrincipalId DelegateId,
    WorldEntityCapability Capability,
    bool IsRevoked = false);

/// <summary>
/// Evaluates the smallest shared-entity agency contract. Unknown, malformed, composite, and ungranted
/// capabilities fail closed. Mandates must come from the authoritative domain; this policy does not
/// authenticate their provenance, persist them, or establish ownership.
/// </summary>
public static class WorldEntityAgencyPolicy
{
    private const WorldEntityCapability KnownCapabilities =
        WorldEntityCapability.Operate |
        WorldEntityCapability.DelegateOperation |
        WorldEntityCapability.ManageOperations;

    public static bool CanPerform(
        History.WorldEntityId entityId,
        WorldEntityPrincipalId principalId,
        WorldEntityCapability capability,
        IEnumerable<WorldEntityDirectOperator> directOperators,
        IEnumerable<WorldEntityOrganizationMandate> mandates,
        IEnumerable<WorldEntityDelegation> delegations)
    {
        ArgumentNullException.ThrowIfNull(directOperators);
        ArgumentNullException.ThrowIfNull(mandates);
        ArgumentNullException.ThrowIfNull(delegations);

        if (string.IsNullOrWhiteSpace(entityId.Value) ||
            string.IsNullOrWhiteSpace(principalId.Value) ||
            capability == WorldEntityCapability.None ||
            (capability & ~KnownCapabilities) != 0 ||
            !IsSingleCapability(capability))
            return false;

        if (capability == WorldEntityCapability.Operate &&
            directOperators.Any(x => x.EntityId == entityId && x.OperatorId == principalId))
            return true;

        if (capability != WorldEntityCapability.Operate &&
            mandates.Any(x => x.EntityId == entityId &&
                              x.OrganizationId == principalId &&
                              IsValidCapabilitySet(x.Capabilities) &&
                              (x.Capabilities & capability) == capability))
            return true;

        if (capability != WorldEntityCapability.Operate)
            return false;

        return delegations.Any(delegation =>
            !delegation.IsRevoked &&
            !string.IsNullOrWhiteSpace(delegation.OrganizationId.Value) &&
            delegation.EntityId == entityId &&
            delegation.DelegateId == principalId &&
            delegation.Capability == WorldEntityCapability.Operate &&
            mandates.Any(mandate =>
                mandate.EntityId == entityId &&
                mandate.OrganizationId == delegation.OrganizationId &&
                IsValidCapabilitySet(mandate.Capabilities) &&
                (mandate.Capabilities & WorldEntityCapability.DelegateOperation) != 0));
    }

    private static bool IsValidCapabilitySet(WorldEntityCapability capabilities) =>
        capabilities != WorldEntityCapability.None &&
        (capabilities & ~KnownCapabilities) == 0;

    private static bool IsSingleCapability(WorldEntityCapability capability)
    {
        var value = (byte)capability;
        return (value & (value - 1)) == 0;
    }
}
