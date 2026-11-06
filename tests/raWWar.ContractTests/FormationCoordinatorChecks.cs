using TheSingularityWorkshop.raWWar.Formations;

namespace TheSingularityWorkshop.raWWar.ContractTests;

internal static class FormationCoordinatorChecks
{
    public static void Run(Action<bool, string> check)
    {
        var created = FormationCoordinator.Create("company-a", 2, 2);
        var fallIn = created.TryIssueFallIn(new[]
        {
            new FormationQueueMember("s1", 0),
            new FormationQueueMember("s2", 1)
        });
        check(fallIn.Accepted && !fallIn.After.IsFormed &&
              fallIn.After.Members.All(x => x.SlotState == FormationSlotState.ReservedForApproach),
            "Fall In reserves assignments but does not claim physical occupancy");

        var first = fallIn.After.Members.Single(x => x.MemberId == "s1");
        var partial = fallIn.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision, true, false, false, false));
        check(partial.Accepted && partial.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.ReservedForApproach,
            "Position without confirmed facing cannot occupy a slot");

        var arrived = partial.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision, true, true, false, false));
        check(arrived.Accepted && arrived.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.Occupied,
            "Matching position and facing observations confirm occupancy");

        var stale = arrived.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision - 1, true, true, false, false));
        check(!stale.Accepted && stale.After == stale.Before,
            "Stale approach observations are rejected without changing coordinator state");

        var fallOut = arrived.After.TryIssueFallOut("s1", "egress-s1");
        var releasing = fallOut.After.Members.Single(x => x.MemberId == "s1");
        check(fallOut.Accepted && releasing.SlotState == FormationSlotState.ReservedForRelease &&
              releasing.MemberState == FormationMemberState.Releasing,
            "Fall Out reserves the occupied slot until physical clearance");

        var notClear = fallOut.After.TryObserveClearance(new FormationMemberObservation(
            "s1", releasing.OrderRevision, false, false, false, false));
        check(notClear.Accepted && notClear.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.ReservedForRelease,
            "Unconfirmed clearance does not free the slot");

        var clear = notClear.After.TryObserveClearance(new FormationMemberObservation(
            "s1", releasing.OrderRevision, false, false, false, true));
        check(clear.Accepted && clear.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.Available &&
              clear.After.Members.Single(x => x.MemberId == "s1").MemberState == FormationMemberState.Clear,
            "Explicit physical clearance releases the slot");

        var blockedFormation = FormationCoordinator.Create("company-b", 1, 1)
            .TryIssueFallIn(new[] { new FormationQueueMember("blocked", 0) }).After;
        var blockedMember = blockedFormation.Members.Single();
        var blocked = blockedFormation.TryObserveApproach(new FormationMemberObservation(
            "blocked", blockedMember.OrderRevision, false, false, true, false));
        check(blocked.Accepted && blocked.After.Members.Single().SlotState == FormationSlotState.Blocked,
            "Blocked approach remains explicit and is not treated as arrival");

        var retried = blocked.After.TryRetryApproach("blocked");
        check(retried.Accepted && retried.After.Members.Single().MemberState == FormationMemberState.Approaching &&
              retried.After.Members.Single().OrderRevision > blockedMember.OrderRevision,
            "Blocked approach retry receives a new member revision");
        var staleAfterRetry = retried.After.TryObserveApproach(new FormationMemberObservation(
            "blocked", blockedMember.OrderRevision, true, true, false, false));
        check(!staleAfterRetry.Accepted,
            "Observation from before a blocked-approach retry is rejected");

        var unavailableFormation = FormationCoordinator.Create("company-c", 1, 1)
            .TryIssueFallIn(new[] { new FormationQueueMember("unavailable", 0) }).After;
        var unavailable = unavailableFormation.TryMarkUnavailable("unavailable", "actor not materialized");
        check(unavailable.Accepted &&
              unavailable.After.Members.Single().SlotState == FormationSlotState.Unavailable &&
              unavailable.After.Members.Single().StatusReason == "actor not materialized",
            "Unavailable members retain an explicit reason and do not occupy a slot");
        var cannotEraseOccupancy = arrived.After.TryMarkUnavailable("s1", "disconnect");
        check(!cannotEraseOccupancy.Accepted &&
              cannotEraseOccupancy.After.Members.Single(x => x.MemberId == "s1").SlotState == FormationSlotState.Occupied,
            "Unavailable status cannot erase an already occupied physical slot");

        var emptyReform = created.TryIssueFallIn(Array.Empty<FormationQueueMember>());
        check(!emptyReform.Accepted, "Empty Fall In order is rejected");
        var duplicateFormation = fallIn.After.TryIssueFallIn(Array.Empty<FormationQueueMember>());
        check(!duplicateFormation.Accepted, "An assigned formation cannot be silently replaced");
    }
}
