using TheSingularityWorkshop.raWWar.Formations;

namespace TheSingularityWorkshop.raWWar.ContractTests;

internal static class FormationSlotPlannerChecks
{
    public static void Run(Action<bool, string> check)
    {
        var queue = new[]
        {
            new FormationQueueMember("soldier-03", 2),
            new FormationQueueMember("soldier-01", 0),
            new FormationQueueMember("soldier-04", 3),
            new FormationQueueMember("soldier-02", 1)
        };

        var plan = FormationSlotPlanner.CreatePlan(2, 2, queue);
        check(plan.Select(item => (item.MemberId, item.Slot.Rank, item.Slot.File))
                .SequenceEqual(new[]
                {
                    ("soldier-01", 0, 0),
                    ("soldier-02", 0, 1),
                    ("soldier-03", 1, 0),
                    ("soldier-04", 1, 1)
                }),
            "Formation slot planner fills rear-most rank left-to-right before advancing forward");

        var reordered = FormationSlotPlanner.CreatePlan(2, 2, queue.Reverse());
        check(plan.SequenceEqual(reordered),
            "Formation slot plan is independent of member collection enumeration order");

        var partial = FormationSlotPlanner.CreatePlan(2, 2, queue.Take(3));
        check(partial.Count == 3 && partial[^1].Slot == new FormationSlot(1, 0),
            "Partial formation plan assigns only supplied members without inventing occupancy");

        var duplicateIdentityRejected = false;
        try
        {
            _ = FormationSlotPlanner.CreatePlan(1, 2, new[]
            {
                new FormationQueueMember("same", 0),
                new FormationQueueMember("same", 1)
            });
        }
        catch (ArgumentException) { duplicateIdentityRejected = true; }
        check(duplicateIdentityRejected, "Formation planner rejects duplicate stable member identities");

        var duplicateOrderRejected = false;
        try
        {
            _ = FormationSlotPlanner.CreatePlan(1, 2, new[]
            {
                new FormationQueueMember("first", 0),
                new FormationQueueMember("second", 0)
            });
        }
        catch (ArgumentException) { duplicateOrderRejected = true; }
        check(duplicateOrderRejected, "Formation planner rejects ambiguous queue-order values");

        var overflowRejected = false;
        try
        {
            _ = FormationSlotPlanner.CreatePlan(1, 1, new[]
            {
                new FormationQueueMember("first", 0),
                new FormationQueueMember("second", 1)
            });
        }
        catch (ArgumentException) { overflowRejected = true; }
        check(overflowRejected, "Formation planner rejects queues larger than the declared footprint");

        var invalidSlotRejected = false;
        try { _ = new FormationSlot(-1, 0); }
        catch (ArgumentOutOfRangeException) { invalidSlotRejected = true; }
        check(invalidSlotRejected, "Formation slot rejects negative rank or file coordinates");
    }
}
