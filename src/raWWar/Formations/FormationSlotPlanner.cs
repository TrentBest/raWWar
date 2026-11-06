namespace TheSingularityWorkshop.raWWar.Formations;

/// <summary>
/// A stable, formation-relative rectangular slot. Rank zero is the rear-most rank;
/// file zero is the first lateral position in the declared left-to-right convention.
/// These are logical indices, not world coordinates or physical spacing.
/// </summary>
public readonly record struct FormationSlot(int Rank, int File)
{
    public FormationSlot
    {
        if (Rank < 0) throw new ArgumentOutOfRangeException(nameof(Rank));
        if (File < 0) throw new ArgumentOutOfRangeException(nameof(File));
    }
}

/// <summary>
/// A member's explicit position in the single-file Fall In queue.
/// QueueOrder is supplied by the caller; MemberId is a stable tie-breaker.
/// </summary>
public readonly record struct FormationQueueMember(string MemberId, int QueueOrder);

/// <summary>A deterministic assignment from one queued member to one logical slot.</summary>
public readonly record struct FormationSlotAssignment(string MemberId, FormationSlot Slot);

/// <summary>
/// Builds a deterministic rear-to-front, left-to-right assignment plan.
/// It does not move members, reserve physical space, or declare slots occupied.
/// </summary>
public static class FormationSlotPlanner
{
    /// <summary>
    /// Assigns the ordered queue to a rectangular footprint.
    /// Rank zero is filled first, and each rank is filled from file zero upward.
    /// Members are ordered by QueueOrder, then ordinal MemberId.
    /// </summary>
    public static IReadOnlyList<FormationSlotAssignment> CreatePlan(
        int rankCount,
        int filesPerRank,
        IEnumerable<FormationQueueMember> members)
    {
        ArgumentNullException.ThrowIfNull(members);
        if (rankCount <= 0) throw new ArgumentOutOfRangeException(nameof(rankCount));
        if (filesPerRank <= 0) throw new ArgumentOutOfRangeException(nameof(filesPerRank));

        var queue = members.ToArray();
        if (queue.Any(member => string.IsNullOrWhiteSpace(member.MemberId)))
            throw new ArgumentException("Member identities must be non-empty.", nameof(members));
        if (queue.Any(member => member.QueueOrder < 0))
            throw new ArgumentException("Queue order must be non-negative.", nameof(members));
        if (queue.Select(member => member.MemberId).Distinct(StringComparer.Ordinal).Count() != queue.Length)
            throw new ArgumentException("Member identities must be unique.", nameof(members));
        if (queue.Select(member => member.QueueOrder).Distinct().Count() != queue.Length)
            throw new ArgumentException("Queue order values must be unique.", nameof(members));

        var capacity = checked(rankCount * filesPerRank);
        if (queue.Length > capacity)
            throw new ArgumentException("The formation does not contain enough slots for every queued member.", nameof(members));

        var ordered = queue.OrderBy(member => member.QueueOrder)
            .ThenBy(member => member.MemberId, StringComparer.Ordinal);
        var result = new List<FormationSlotAssignment>(queue.Length);
        var index = 0;
        foreach (var member in ordered)
        {
            result.Add(new FormationSlotAssignment(
                member.MemberId,
                new FormationSlot(index / filesPerRank, index % filesPerRank)));
            index++;
        }

        return result.AsReadOnly();
    }
}
