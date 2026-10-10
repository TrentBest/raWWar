using TheSingularityWorkshop.raWWar.History;
using TheSingularityWorkshop.raWWar.Spatiotemporal;

// The address is the stream boundary: unrelated regions must not be mixed during replay.
var address = HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(42))
    .Child(3, 4, 5);
var canonicalAddress = HierarchicalSpatialAddressCodec.EncodeV1(address);

var history = new InMemoryEventHistory();

// Constructing an event does not commit it. The caller explicitly crosses the commit boundary.
var later = SimulationEvent.Create(
    worldSeed: 123,
    simulationModelVersion: "v1",
    addressBytes: canonicalAddress,
    eventDomain: ResourceBalanceReplay.EventDomain,
    eventOrdinal: 1,
    logicalTimeKey: "tick:000002",
    payloadBytes: ResourceBalanceReplay.EncodeDelta(-3));

var earlier = SimulationEvent.Create(
    worldSeed: 123,
    simulationModelVersion: "v1",
    addressBytes: canonicalAddress,
    eventDomain: ResourceBalanceReplay.EventDomain,
    eventOrdinal: 2,
    logicalTimeKey: "tick:000001",
    payloadBytes: ResourceBalanceReplay.EncodeDelta(10));

if (history.Commit(later) != EventCommitResult.Committed
    || history.Commit(earlier) != EventCommitResult.Committed)
{
    Console.Error.WriteLine("A new event was not committed.");
    return 1;
}

// A retry of the exact same event is idempotent within this in-memory ledger.
if (history.Commit(later) != EventCommitResult.AlreadyCommitted)
{
    Console.Error.WriteLine("Expected an identical retry to be recognized.");
    return 1;
}

// Reusing an identity for different content is a conflict, not a valid retry.
var conflictingRetry = SimulationEvent.Create(
    worldSeed: 123,
    simulationModelVersion: "v1",
    addressBytes: canonicalAddress,
    eventDomain: ResourceBalanceReplay.EventDomain,
    eventOrdinal: 1,
    logicalTimeKey: "tick:000002",
    payloadBytes: ResourceBalanceReplay.EncodeDelta(99));

if (history.Commit(conflictingRetry) != EventCommitResult.IdentityConflict)
{
    Console.Error.WriteLine("Expected conflicting content to be rejected.");
    return 1;
}

// The ledger's declared ordering is by logical-time key, then ordinal, then stable event ID.
// Read only this domain and address before passing events to the narrow reducer.
var orderedEvents = history.ReadOrdered(ResourceBalanceReplay.EventDomain, canonicalAddress);
var reconstructedBalance = ResourceBalanceReplay.Replay(initialBalance: 5, orderedEvents: orderedEvents);

var checkpoint = ResourceBalanceCheckpoint.Capture(initialBalance: 5, orderedPrefix: orderedEvents.Take(1));
var checkpointBalance = checkpoint.ReplayTail(orderedEvents.Skip(1));
if (checkpointBalance != reconstructedBalance)
{
    Console.Error.WriteLine("Checkpoint-plus-tail replay did not match full replay.");
    return 1;
}

Console.WriteLine($"Committed events in this stream: {orderedEvents.Count}");
Console.WriteLine($"Initial balance: 5");
Console.WriteLine($"Reconstructed balance: {reconstructedBalance} (5 + 10 - 3)");

if (orderedEvents.Count != 2 || reconstructedBalance != 12)
{
    Console.Error.WriteLine("Event-history/replay example failed.");
    return 1;
}

Console.WriteLine("Event-history/replay example passed.");
return 0;
