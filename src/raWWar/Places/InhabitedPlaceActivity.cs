using TheSingularityWorkshop.raWWar.Agency;
using TheSingularityWorkshop.raWWar.History;

namespace TheSingularityWorkshop.raWWar.Places;

/// <summary>
/// Binds one resident's activity to a background occurrence at the same inhabited place.
/// The binding is referential only: it does not schedule activity, grant visibility, or mutate history.
/// </summary>
public sealed record InhabitedPlaceActivity(
    WorldEntityId PlaceId,
    WorldEntityPrincipalId ResidentId,
    SimulationEventId BackgroundOccurrenceId,
    SimulationEventId ResidentActivityId)
{
    public static InhabitedPlaceActivity Create(
        InhabitedPlaceSnapshot place,
        WorldEntityPrincipalId residentId,
        SimulationEvent backgroundOccurrence,
        SimulationEvent residentActivity,
        ReadOnlySpan<byte> canonicalPlaceAddress)
    {
        ArgumentNullException.ThrowIfNull(place);
        ArgumentNullException.ThrowIfNull(backgroundOccurrence);
        ArgumentNullException.ThrowIfNull(residentActivity);
        if (string.IsNullOrWhiteSpace(residentId.Value) || !place.Residents.Contains(residentId))
            throw new ArgumentException("The activity resident must be a member of the authoritative place snapshot.", nameof(residentId));
        if (canonicalPlaceAddress.IsEmpty)
            throw new ArgumentException("A canonical place address is required.", nameof(canonicalPlaceAddress));

        var expectedStreamKey = Convert.ToHexString(canonicalPlaceAddress);
        if (!StringComparer.Ordinal.Equals(backgroundOccurrence.StreamKey, expectedStreamKey) ||
            !StringComparer.Ordinal.Equals(residentActivity.StreamKey, expectedStreamKey))
            throw new ArgumentException("Both events must belong to the same canonical place address.");
        if (!StringComparer.Ordinal.Equals(backgroundOccurrence.EventDomain, "place.background") ||
            !StringComparer.Ordinal.Equals(residentActivity.EventDomain, "resident.activity"))
            throw new ArgumentException("The events must represent a place background occurrence and resident activity.");

        return new InhabitedPlaceActivity(
            place.PlaceId, residentId, backgroundOccurrence.Id, residentActivity.Id);
    }
}
