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

        check(created.Revision == 0 && fallIn.After.Revision == created.Revision + 1,
            "Issuing Fall In advances the formation snapshot revision");
        var initialReadiness = fallIn.After.Readiness;
        check(initialReadiness.Capacity == 4 && initialReadiness.AssignedMembers == 2 &&
              initialReadiness.ApproachingMembers == 2 && initialReadiness.OccupiedMembers == 0 &&
              initialReadiness.UnassignedSlots == 2 && initialReadiness.ReleasedAvailableSlots == 0 &&
              initialReadiness.AvailableSlots == 2 && initialReadiness.BlockedSlots == 0 &&
              initialReadiness.UnavailableSlots == 0 && initialReadiness.ReservedApproachSlots == 2,
            "Readiness separates unassigned capacity, released slots, blocked slots, and reservations");

        var first = fallIn.After.Members.Single(x => x.MemberId == "s1");
        var partial = fallIn.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision, true, false, false, false));
        check(partial.Accepted && partial.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.ReservedForApproach && partial.After.Revision == fallIn.After.Revision,
            "An incomplete observation is a no-op and does not advance the snapshot revision");

        var arrived = partial.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision, true, true, false, false));
        check(arrived.Accepted && arrived.After.Members.Single(x => x.MemberId == "s1").SlotState ==
              FormationSlotState.Occupied && arrived.After.Revision == partial.After.Revision + 1,
            "Matching position and facing observations confirm occupancy and advance the snapshot revision");

        check(!arrived.After.Readiness.IsFormed && arrived.After.Readiness.OccupiedMembers == 1 &&
              arrived.After.Readiness.ApproachingMembers == 1,
            "One settled member does not mark a partially assembled formation as formed");
        var second = arrived.After.Members.Single(x => x.MemberId == "s2");
        var fullyArrived = arrived.After.TryObserveApproach(new FormationMemberObservation(
            "s2", second.OrderRevision, true, true, false, false));
        check(fullyArrived.Accepted && fullyArrived.After.Readiness.IsFormed &&
              fullyArrived.After.Readiness.OccupiedMembers == 2,
            "Formation readiness becomes formed only when every assigned member is settled");

        var stale = arrived.After.TryObserveApproach(new FormationMemberObservation(
            "s1", first.OrderRevision - 1, true, true, false, false));
        check(!stale.Accepted && stale.After == stale.Before,
            "Stale approach observations are rejected without changing coordinator state");

        var fallOut = arrived.After.TryIssueFallOut("s1", "egress-s1");
        var releasing = fallOut.After.Members.Single(x => x.MemberId == "s1");
        check(fallOut.Accepted && releasing.SlotState == FormationSlotState.ReservedForRelease &&
              releasing.MemberState == FormationMemberState.Releasing &&
              fallOut.After.Revision == arrived.After.Revision + 1,
            "Fall Out reserves the occupied slot until physical clearance and advances the snapshot revision");

        var interruptedApproach = fallIn.After.TryInterrupt("s2", "formation order paused");
        var pausedApproach = interruptedApproach.After.Members.Single(x => x.MemberId == "s2");
        check(interruptedApproach.Accepted && pausedApproach.MemberState == FormationMemberState.Interrupted &&
              pausedApproach.SlotState == FormationSlotState.ReservedForApproach,
            "Interrupting approach preserves its slot reservation");
        check(interruptedApproach.After.Readiness.InterruptedMembers == 1 &&
              interruptedApproach.After.Readiness.ApproachingMembers == 1 &&
              interruptedApproach.After.Readiness.ReservedApproachSlots == 2 &&
              interruptedApproach.After.Readiness.AvailableSlots == 2,
            "Interrupted approach remains counted against its reserved capacity");
        var resumedApproach = interruptedApproach.After.TryResumeInterrupted("s2");
        var resumedApproachMember = resumedApproach.After.Members.Single(x => x.MemberId == "s2");
        check(resumedApproach.Accepted && resumedApproachMember.MemberState == FormationMemberState.Approaching &&
              resumedApproachMember.OrderRevision > pausedApproach.OrderRevision,
            "Resuming approach advances revision and restores approach state");
        check(!resumedApproach.After.TryObserveApproach(new FormationMemberObservation(
              "s2", pausedApproach.OrderRevision, true, true, false, false)).Accepted,
            "Pre-interruption approach observation is stale after resume");

        var interruptedRelease = fallOut.After.TryInterrupt("s1", "egress paused");
        var pausedRelease = interruptedRelease.After.Members.Single(x => x.MemberId == "s1");
        check(interruptedRelease.Accepted && pausedRelease.MemberState == FormationMemberState.Interrupted &&
              pausedRelease.SlotState == FormationSlotState.ReservedForRelease,
            "Interrupting release retains its slot reservation");
        check(interruptedRelease.After.Readiness.InterruptedMembers == 1 &&
              interruptedRelease.After.Readiness.ApproachingMembers == 1 &&
              interruptedRelease.After.Readiness.ReservedApproachSlots == 1 &&
              interruptedRelease.After.Readiness.ReservedReleaseSlots == 1 &&
              interruptedRelease.After.Readiness.ReleasedAvailableSlots == 0,
            "Interrupted release remains reserved and is not counted as released capacity");
        var resumedRelease = interruptedRelease.After.TryResumeInterrupted("s1");
        var resumedReleaseMember = resumedRelease.After.Members.Single(x => x.MemberId == "s1");
        check(resumedRelease.Accepted && resumedReleaseMember.MemberState == FormationMemberState.Releasing &&
              resumedReleaseMember.SlotState == FormationSlotState.ReservedForRelease &&
              resumedReleaseMember.OrderRevision > pausedRelease.OrderRevision,
            "Resuming release preserves the reservation and advances revision");
        check(!resumedRelease.After.TryObserveClearance(new FormationMemberObservation(
              "s1", pausedRelease.OrderRevision, false, false, false, true)).Accepted,
            "Pre-interruption clearance observation is stale after resume");

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
        check(clear.After.Readiness.UnassignedSlots == 2 &&
              clear.After.Readiness.ReleasedAvailableSlots == 1 &&
              clear.After.Readiness.AvailableSlots == 3 &&
              clear.After.Readiness.ClearMembers == 1,
            "Released capacity is reported separately from never-assigned capacity");

        var blockedFormation = FormationCoordinator.Create("company-b", 1, 1)
            .TryIssueFallIn(new[] { new FormationQueueMember("blocked", 0) }).After;
        var blockedMember = blockedFormation.Members.Single();
        var blocked = blockedFormation.TryObserveApproach(new FormationMemberObservation(
            "blocked", blockedMember.OrderRevision, false, false, true, false));
        check(blocked.Accepted && blocked.After.Members.Single().SlotState == FormationSlotState.Blocked,
            "Blocked approach remains explicit and is not treated as arrival");
        check(blocked.After.Readiness.BlockedSlots == 1 &&
              blocked.After.Readiness.AvailableSlots == 0 &&
              blocked.After.Readiness.BlockedMembers == 1,
            "Blocked slot capacity is not reported as available");

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
        check(unavailable.After.Readiness.UnavailableSlots == 1 &&
              unavailable.After.Readiness.AvailableSlots == 0 &&
              unavailable.After.Readiness.UnavailableMembers == 1,
            "Unavailable slot capacity is distinct from free capacity");
        var cannotEraseOccupancy = arrived.After.TryMarkUnavailable("s1", "disconnect");
        check(!cannotEraseOccupancy.Accepted &&
              cannotEraseOccupancy.After.Members.Single(x => x.MemberId == "s1").SlotState == FormationSlotState.Occupied,
            "Unavailable status cannot erase an already occupied physical slot");

        var emptyReform = created.TryIssueFallIn(Array.Empty<FormationQueueMember>());
        check(!emptyReform.Accepted, "Empty Fall In order is rejected");
        var duplicateFormation = fallIn.After.TryIssueFallIn(Array.Empty<FormationQueueMember>());
        check(!duplicateFormation.Accepted, "An assigned formation cannot be silently replaced");

        CheckCapacityInvariant(created, check, "empty formation");
        CheckCapacityInvariant(fallIn.After, check, "initial assignments");
        CheckCapacityInvariant(partial.After, check, "partial arrival");
        CheckCapacityInvariant(arrived.After, check, "one settled member");
        CheckCapacityInvariant(fullyArrived.After, check, "fully formed");
        CheckCapacityInvariant(fallOut.After, check, "release issued");
        CheckCapacityInvariant(interruptedApproach.After, check, "approach interrupted");
        CheckCapacityInvariant(resumedApproach.After, check, "approach resumed");
        CheckCapacityInvariant(interruptedRelease.After, check, "release interrupted");
        CheckCapacityInvariant(resumedRelease.After, check, "release resumed");
        CheckCapacityInvariant(notClear.After, check, "clearance unconfirmed");
        CheckCapacityInvariant(clear.After, check, "clearance confirmed");
        CheckCapacityInvariant(blockedFormation, check, "blocked approach pending");
        CheckCapacityInvariant(blocked.After, check, "blocked approach observed");
        CheckCapacityInvariant(retried.After, check, "blocked approach retried");
        CheckCapacityInvariant(unavailableFormation, check, "unavailable member");
        CheckCapacityInvariant(unavailable.After, check, "member marked unavailable");
    }

    private static void CheckCapacityInvariant(
        FormationCoordinator coordinator,
        Action<bool, string> check,
        string snapshot)
    {
        var readiness = coordinator.Readiness;
        var accountedCapacity =
            readiness.UnassignedSlots +
            readiness.ReleasedAvailableSlots +
            readiness.OccupiedMembers +
            readiness.ApproachingMembers +
            readiness.BlockedMembers +
            readiness.ReleasingMembers +
            readiness.InterruptedMembers +
            readiness.UnavailableMembers;

        check(accountedCapacity == readiness.Capacity,
            $"Formation capacity is conserved across {snapshot}: expected {readiness.Capacity}, accounted {accountedCapacity}");
    }
}
