namespace TheSingularityWorkshop.raWWar.Formations;

/// <summary>Authoritative observations supplied by the host after its movement/world systems evaluate a member.</summary>
public readonly record struct FormationMemberObservation(
    string MemberId,
    int OrderRevision,
    bool SlotPositionConfirmed,
    bool FacingConfirmed,
    bool Blocked,
    bool PhysicallyClear);

public enum FormationSlotState
{
    Available,
    ReservedForApproach,
    Occupied,
    ReservedForRelease,
    Blocked,
    Unavailable
}

public enum FormationMemberState
{
    Approaching,
    Settled,
    Blocked,
    Releasing,
    Clear,
    Unavailable
}

/// <summary>Current organizational state for one assigned member.</summary>
public sealed record FormationMemberStatus(
    string MemberId,
    FormationSlot Slot,
    FormationSlotState SlotState,
    FormationMemberState MemberState,
    int OrderRevision,
    string? EgressTargetId);

/// <summary>
/// Immutable, host-independent first-slice coordinator. It issues slot assignments and
/// consumes explicit observations; it never moves members or infers physical occupancy.
/// </summary>
public sealed class FormationCoordinator
{
    private readonly IReadOnlyDictionary<string, FormationMemberStatus> _members;

    private FormationCoordinator(
        string formationId,
        int rankCount,
        int filesPerRank,
        int revision,
        IReadOnlyDictionary<string, FormationMemberStatus> members)
    {
        FormationId = formationId;
        RankCount = rankCount;
        FilesPerRank = filesPerRank;
        Revision = revision;
        _members = members;
    }

    public string FormationId { get; }
    public int RankCount { get; }
    public int FilesPerRank { get; }
    public int Revision { get; }
    public IReadOnlyList<FormationMemberStatus> Members =>
        _members.Values.OrderBy(member => member.MemberId, StringComparer.Ordinal).ToArray();

    public bool IsFormed =>
        _members.Count > 0 && _members.Values.All(member => member.MemberState == FormationMemberState.Settled);

    public static FormationCoordinator Create(string formationId, int rankCount, int filesPerRank)
    {
        if (string.IsNullOrWhiteSpace(formationId))
            throw new ArgumentException("Formation identity must be non-empty.", nameof(formationId));
        if (rankCount <= 0) throw new ArgumentOutOfRangeException(nameof(rankCount));
        if (filesPerRank <= 0) throw new ArgumentOutOfRangeException(nameof(filesPerRank));
        _ = checked(rankCount * filesPerRank);
        return new FormationCoordinator(
            formationId, rankCount, filesPerRank, 0,
            new Dictionary<string, FormationMemberStatus>(StringComparer.Ordinal));
    }

    /// <summary>Issues one deterministic Fall In order. No member is considered occupied.</summary>
    public FormationCoordinatorTransition TryIssueFallIn(IEnumerable<FormationQueueMember> queue)
    {
        ArgumentNullException.ThrowIfNull(queue);
        if (_members.Count != 0)
            return Block("A formation with assigned members cannot be re-formed by this first-slice operation.");

        IReadOnlyList<FormationSlotAssignment> plan;
        try
        {
            plan = FormationSlotPlanner.CreatePlan(RankCount, FilesPerRank, queue);
        }
        catch (ArgumentException exception)
        {
            return Block(exception.Message);
        }

        if (plan.Count == 0)
            return Block("At least one member is required to issue Fall In.");

        var nextRevision = checked(Revision + 1);
        var next = plan.ToDictionary(
            assignment => assignment.MemberId,
            assignment => new FormationMemberStatus(
                assignment.MemberId, assignment.Slot,
                FormationSlotState.ReservedForApproach,
                FormationMemberState.Approaching,
                nextRevision, null),
            StringComparer.Ordinal);
        return Accept(new FormationCoordinator(
            FormationId, RankCount, FilesPerRank, nextRevision, next));
    }

    /// <summary>
    /// Applies a host observation. Position and facing must both be confirmed before
    /// the slot becomes occupied. A blocked observation never fabricates arrival.
    /// </summary>
    public FormationCoordinatorTransition TryObserveApproach(FormationMemberObservation observation)
    {
        if (!TryGetCurrent(observation, FormationMemberState.Approaching, out var current, out var error))
            return Block(error);

        if (observation.Blocked)
            return Replace(current with
            {
                SlotState = FormationSlotState.Blocked,
                MemberState = FormationMemberState.Blocked
            });

        if (!observation.SlotPositionConfirmed || !observation.FacingConfirmed)
            return Accept(this);

        return Replace(current with
        {
            SlotState = FormationSlotState.Occupied,
            MemberState = FormationMemberState.Settled
        });
    }

    /// <summary>Reserves a named egress target; the slot remains unavailable until clearance is observed.</summary>
    public FormationCoordinatorTransition TryIssueFallOut(string memberId, string egressTargetId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
            return Block("Member identity must be non-empty.");
        if (string.IsNullOrWhiteSpace(egressTargetId))
            return Block("Egress target identity must be non-empty.");
        if (!_members.TryGetValue(memberId, out var current))
            return Block("The member is not assigned to this formation.");
        if (current.MemberState != FormationMemberState.Settled ||
            current.SlotState != FormationSlotState.Occupied)
            return Block("Fall Out requires an occupied, settled member.");

        var nextRevision = checked(current.OrderRevision + 1);
        return Replace(current with
        {
            SlotState = FormationSlotState.ReservedForRelease,
            MemberState = FormationMemberState.Releasing,
            OrderRevision = nextRevision,
            EgressTargetId = egressTargetId
        }, nextRevision);
    }

    /// <summary>Frees a slot only after the host explicitly confirms physical clearance.</summary>
    public FormationCoordinatorTransition TryObserveClearance(FormationMemberObservation observation)
    {
        if (!TryGetCurrent(observation, FormationMemberState.Releasing, out var current, out var error))
            return Block(error);
        if (!observation.PhysicallyClear)
            return Accept(this);

        return Replace(current with
        {
            SlotState = FormationSlotState.Available,
            MemberState = FormationMemberState.Clear,
            EgressTargetId = null
        });
    }

    private bool TryGetCurrent(
        FormationMemberObservation observation,
        FormationMemberState expectedState,
        out FormationMemberStatus current,
        out string error)
    {
        current = null!;
        if (string.IsNullOrWhiteSpace(observation.MemberId))
        {
            error = "Observation member identity must be non-empty.";
            return false;
        }
        if (!_members.TryGetValue(observation.MemberId, out current!))
        {
            error = "Observation refers to a member not assigned to this formation.";
            return false;
        }
        if (observation.OrderRevision != current.OrderRevision)
        {
            error = "Observation is stale for the member's current order revision.";
            return false;
        }
        if (current.MemberState != expectedState)
        {
            error = $"Observation is not valid while the member is {current.MemberState}.";
            return false;
        }
        error = string.Empty;
        return true;
    }

    private FormationCoordinatorTransition Replace(FormationMemberStatus updated, int? revision = null)
    {
        var next = _members.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal);
        next[updated.MemberId] = updated;
        return Accept(new FormationCoordinator(
            FormationId, RankCount, FilesPerRank, revision ?? Revision, next));
    }

    private FormationCoordinatorTransition Accept(FormationCoordinator after) =>
        new(true, null, this, after);

    private FormationCoordinatorTransition Block(string reason) =>
        new(false, reason, this, this);
}

public sealed record FormationCoordinatorTransition(
    bool Accepted,
    string? BlockReason,
    FormationCoordinator Before,
    FormationCoordinator After);
