using System.Collections.ObjectModel;
using TheSingularityWorkshop.raWWar.Agency;
using TheSingularityWorkshop.raWWar.History;

namespace TheSingularityWorkshop.raWWar.Places;

/// <summary>
/// Immutable authoritative snapshot for one inhabited place. It records stable references,
/// not resident simulation, service execution, persistence, or observer knowledge.
/// </summary>
public sealed class InhabitedPlaceSnapshot
{
    private readonly ReadOnlyCollection<WorldEntityPrincipalId> _residents;
    private readonly ReadOnlyCollection<string> _serviceIds;
    private readonly ReadOnlyCollection<string> _backgroundEventIds;

    public WorldEntityId PlaceId { get; }
    public string Name { get; }
    public IReadOnlyList<WorldEntityPrincipalId> Residents => _residents;
    public IReadOnlyList<string> ServiceIds => _serviceIds;
    public IReadOnlyList<string> BackgroundEventIds => _backgroundEventIds;

    public InhabitedPlaceSnapshot(
        WorldEntityId placeId,
        string name,
        IEnumerable<WorldEntityPrincipalId> residents,
        IEnumerable<string> serviceIds,
        IEnumerable<string> backgroundEventIds)
    {
        if (string.IsNullOrWhiteSpace(placeId.Value))
            throw new ArgumentException("A stable place entity identity is required.", nameof(placeId));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(residents);
        ArgumentNullException.ThrowIfNull(serviceIds);
        ArgumentNullException.ThrowIfNull(backgroundEventIds);

        var residentCopy = residents.ToArray();
        var serviceCopy = serviceIds.ToArray();
        var eventCopy = backgroundEventIds.ToArray();

        if (residentCopy.Any(x => string.IsNullOrWhiteSpace(x.Value)) ||
            residentCopy.Distinct().Count() != residentCopy.Length)
            throw new ArgumentException("Resident identities must be non-empty and unique.", nameof(residents));
        ValidateIds(serviceCopy, nameof(serviceIds), "Service");
        ValidateIds(eventCopy, nameof(backgroundEventIds), "Background event");

        PlaceId = placeId;
        Name = name;
        _residents = Array.AsReadOnly(residentCopy);
        _serviceIds = Array.AsReadOnly(serviceCopy);
        _backgroundEventIds = Array.AsReadOnly(eventCopy);
    }

    /// <summary>
    /// Creates a read-only perspective from explicitly supplied observation references.
    /// This validates referential integrity only; it is not an authorization or knowledge policy.
    /// </summary>
    public InhabitedPlaceProjection Project(
        WorldEntityPrincipalId observerId,
        IEnumerable<WorldEntityPrincipalId> observedResidents,
        IEnumerable<string> observedServiceIds,
        IEnumerable<string> observedBackgroundEventIds)
    {
        if (string.IsNullOrWhiteSpace(observerId.Value))
            throw new ArgumentException("A stable observer identity is required.", nameof(observerId));
        ArgumentNullException.ThrowIfNull(observedResidents);
        ArgumentNullException.ThrowIfNull(observedServiceIds);
        ArgumentNullException.ThrowIfNull(observedBackgroundEventIds);

        var residentView = observedResidents.ToArray();
        var serviceView = observedServiceIds.ToArray();
        var eventView = observedBackgroundEventIds.ToArray();

        if (residentView.Any(x => !_residents.Contains(x)) ||
            serviceView.Any(x => !_serviceIds.Contains(x, StringComparer.Ordinal)) ||
            eventView.Any(x => !_backgroundEventIds.Contains(x, StringComparer.Ordinal)))
            throw new ArgumentException("A perspective cannot reference facts absent from the authoritative place snapshot.");

        return new InhabitedPlaceProjection(
            PlaceId, observerId, Name,
            Array.AsReadOnly(residentView),
            Array.AsReadOnly(serviceView),
            Array.AsReadOnly(eventView));
    }

    private static void ValidateIds(string[] values, string parameterName, string kind)
    {
        if (values.Any(string.IsNullOrWhiteSpace) ||
            values.Distinct(StringComparer.Ordinal).Count() != values.Length)
            throw new ArgumentException($"{kind} identities must be non-empty and unique.", parameterName);
    }
}

/// <summary>A read-only observer projection that retains the authoritative place identity.</summary>
public sealed record InhabitedPlaceProjection(
    WorldEntityId PlaceId,
    WorldEntityPrincipalId ObserverId,
    string Name,
    IReadOnlyList<WorldEntityPrincipalId> ObservedResidents,
    IReadOnlyList<string> ObservedServiceIds,
    IReadOnlyList<string> ObservedBackgroundEventIds);
