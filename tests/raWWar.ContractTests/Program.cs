using System.Buffers.Binary;
using System.Security.Cryptography;
using TheSingularityWorkshop.FSM_COS;
using TheSingularityWorkshop.MicroBundleDomain;
using TheSingularityWorkshop.raWWar;
using TheSingularityWorkshop.raWWar.ContractTests;
using TheSingularityWorkshop.raWWar.History;
using TheSingularityWorkshop.raWWar.Spatiotemporal;

var failures = new List<string>();
var checks = 0;

void Check(bool condition, string message)
{
    checks++;
    if (!condition) failures.Add(message);
}

void Near(double actual, double expected, double tolerance, string message) =>
    Check(Math.Abs(actual - expected) <= tolerance, $"{message}: expected {expected}, got {actual}");

var identityTransform = CartesianTransform3d.Identity;
var samplePoint = new Vector3d(2, -3, 4);
var identityPoint = identityTransform.TransformPosition(samplePoint);
Near((identityPoint - samplePoint).Length, 0, 1e-12, "Identity frame transform preserves a point");

var quarterTurnAndOffset = new CartesianTransform3d(
    0, -1, 0,
    1,  0, 0,
    0,  0, 1,
    5,  6, 7);
var transformedPoint = quarterTurnAndOffset.TransformPosition(new Vector3d(1, 0, 0));
Near(transformedPoint.X, 5, 1e-12, "Frame transform rotates point X before adding destination translation");
Near(transformedPoint.Y, 7, 1e-12, "Frame transform rotates point Y before adding destination translation");
Near(transformedPoint.Z, 7, 1e-12, "Frame transform translates point Z");
var transformedDirection = quarterTurnAndOffset.TransformDirection(new Vector3d(1, 0, 0));
Near(transformedDirection.X, 0, 1e-12, "Direction transform rotates X");
Near(transformedDirection.Y, 1, 1e-12, "Direction transform rotates into Y");
Near(transformedDirection.Z, 0, 1e-12, "Direction transform does not gain translation");
var recoveredPoint = quarterTurnAndOffset.Inverse().TransformPosition(transformedPoint);
Near((recoveredPoint - new Vector3d(1, 0, 0)).Length, 0, 1e-12,
    "Orthonormal frame transform followed by its inverse recovers the point");

var offsetOnly = new CartesianTransform3d(1,0,0, 0,1,0, 0,0,1, 10,0,0);
var composed = offsetOnly.Compose(new CartesianTransform3d(0,-1,0, 1,0,0, 0,0,1, 0,0,0));
var composedPoint = composed.TransformPosition(new Vector3d(1, 0, 0));
Near(composedPoint.X, 10, 1e-12, "Transform composition applies the argument before this transform");
Near(composedPoint.Y, 1, 1e-12, "Transform composition preserves documented order");

var rejectedScaledInverse = false;
try { _ = new CartesianTransform3d(2,0,0, 0,1,0, 0,0,1, 0,0,0).Inverse(); }
catch (InvalidOperationException) { rejectedScaledInverse = true; }
Check(rejectedScaledInverse, "Inverse rejects non-orthonormal matrices instead of assuming transpose is inverse");

var rejectedNonFiniteTransform = false;
try { _ = new CartesianTransform3d(1,0,0, 0,1,0, 0,0,1, double.NaN,0,0)
    .TransformPosition(new Vector3d(0,0,0)); }
catch (ArgumentOutOfRangeException) { rejectedNonFiniteTransform = true; }
Check(rejectedNonFiniteTransform, "Non-finite transform components are rejected");

var rejectedNonFiniteVector = false;
try { _ = identityTransform.TransformDirection(new Vector3d(double.PositiveInfinity,0,0)); }
catch (ArgumentOutOfRangeException) { rejectedNonFiniteVector = true; }
Check(rejectedNonFiniteVector, "Non-finite vectors are rejected before coordinate conversion");

var encodedSpatialAddress = HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(42))
    .Child(3, 4, 5)
    .Child(9, 0, 1);
var encodedSpatialBytes = HierarchicalSpatialAddressCodec.Encode(encodedSpatialAddress);
Check(Convert.ToHexString(encodedSpatialBytes) == "5257534101002A00000002030405090001",
    "Hierarchical spatial address V1 has the fixed big-endian reference encoding");
Check(HierarchicalSpatialAddressCodec.Decode(encodedSpatialBytes).Equals(encodedSpatialAddress),
    "Hierarchical spatial address V1 round-trips root and ordered child coordinates");

var addressHashVectors = new[]
{
    (Address: encodedSpatialAddress, Expected: "06C16FED1EC8BC08A1EB95AE7722F7ECD9BC8A3B502BB579229CB090B956033E",
        Label: "root 42 with two ordered child levels"),
    (Address: HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(1)),
        Expected: "46715799BB957132931E309ADA71A4E1F3C9807BAF4071056DB67A819BC315F0",
        Label: "root 1 with no child levels")
};
foreach (var vector in addressHashVectors)
{
    Check(Convert.ToHexString(HierarchicalSpatialAddressHasher.ComputeV1(vector.Address)) == vector.Expected,
        $"Spatial address SHA-256 V1 matches its fixed reference vector ({vector.Label})");
}
Check(HierarchicalSpatialAddressHasher.ComputeV1(encodedSpatialAddress)
        .SequenceEqual(HierarchicalSpatialAddressHasher.ComputeV1(
            HierarchicalSpatialAddressCodec.Decode(encodedSpatialBytes))),
    "Equivalent decoded spatial addresses have identical stable V1 hashes");
Check(Convert.ToHexString(HierarchicalSpatialAddressCodec.Encode(
        HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(1)))) == "5257534101000100000000",
    "Top-level address V1 encoding has an explicit zero-depth representation");

// Stable event identity uses canonical binary fields rather than delimiter-based strings.
var resourceEventId = SimulationEventId.Create(
    123, "v1", encodedSpatialBytes, "resource.depleted", 7, "tick:000042");
Check(resourceEventId.Value == "F223794D5176C58304EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1",
    "Event identity V1 matches a fixed SHA-256 reference vector");

static bool CandidateFrameRejected(byte[] candidate)
{
    try { _ = CandidateEventFrameCodec.Decode(candidate); return false; }
    catch (FormatException) { return true; }
}

static void RecomputeCandidateFrameChecksum(byte[] frame) =>
    SHA256.HashData(frame.AsSpan(0, frame.Length - 32)).CopyTo(frame.AsSpan(frame.Length - 32));

var frameVectorEvent = SimulationEvent.Create(
    123, "v1", encodedSpatialBytes, "resource.depleted", 7, "tick:000042",
    new byte[] { 0x10, 0x20, 0x30 });
var candidateFrame = CandidateEventFrameCodec.Encode(frameVectorEvent);
const string expectedCandidateFrame =
    "5257454A0100000000A0000000000000007B000000027631000000115257534101002A00000002030405090001000000117265736F757263652E6465706C6574656400000000000000070000000B7469636B3A30303030343200000003102030F223794D5176C58304EF88BF2E2C0721B4BFFC5F9C4CC1433C80D701D244A1D1BCE9CCE3807A763076F756157C1D83F117AFFBFBC6831B3427F876FC901D6567";
Check(candidateFrame.Length == 160, "Candidate event frame V1 fixed vector is exactly 160 bytes");
Check(Convert.ToHexString(candidateFrame) == expectedCandidateFrame,
    "Candidate event frame V1 matches the documented complete byte vector");
var decodedCandidateFrame = CandidateEventFrameCodec.Decode(candidateFrame);
Check(decodedCandidateFrame.Id == frameVectorEvent.Id
    && decodedCandidateFrame.WorldSeed == frameVectorEvent.WorldSeed
    && decodedCandidateFrame.SimulationModelVersion == frameVectorEvent.SimulationModelVersion
    && decodedCandidateFrame.StreamKey == frameVectorEvent.StreamKey
    && decodedCandidateFrame.EventDomain == frameVectorEvent.EventDomain
    && decodedCandidateFrame.EventOrdinal == frameVectorEvent.EventOrdinal
    && decodedCandidateFrame.LogicalTimeKey == frameVectorEvent.LogicalTimeKey
    && decodedCandidateFrame.Payload.Span.SequenceEqual(frameVectorEvent.Payload.Span),
    "Candidate event frame V1 round-trips all semantic fields and payload bytes");
Check(CandidateFrameRejected(candidateFrame[..9]),
    "Candidate event frame rejects a truncated fixed header");
Check(CandidateFrameRejected(candidateFrame[..^1]),
    "Candidate event frame rejects a truncated frame");
var badChecksumFrame = candidateFrame.ToArray();
badChecksumFrame[^1] ^= 0x01;
Check(CandidateFrameRejected(badChecksumFrame),
    "Candidate event frame rejects a checksum mismatch");
var unsupportedVersionFrame = candidateFrame.ToArray();
unsupportedVersionFrame[4] = 2;
Check(CandidateFrameRejected(unsupportedVersionFrame),
    "Candidate event frame rejects an unsupported format version");
var nonzeroFlagsFrame = candidateFrame.ToArray();
nonzeroFlagsFrame[5] = 1;
Check(CandidateFrameRejected(nonzeroFlagsFrame),
    "Candidate event frame rejects nonzero reserved flags");
var wrongLengthFrame = candidateFrame.ToArray();
wrongLengthFrame[9] ^= 0x01;
Check(CandidateFrameRejected(wrongLengthFrame),
    "Candidate event frame rejects a declared length that differs from input length");
var oversizeLengthFrame = candidateFrame.ToArray();
Array.Fill(oversizeLengthFrame, (byte)0xFF, 6, 4);
Check(CandidateFrameRejected(oversizeLengthFrame),
    "Candidate event frame rejects an unbounded declared length before parsing fields");
var oversizedModelFieldFrame = candidateFrame.ToArray();
BinaryPrimitives.WriteUInt32BigEndian(oversizedModelFieldFrame.AsSpan(18, 4), 4097);
RecomputeCandidateFrameChecksum(oversizedModelFieldFrame);
Check(CandidateFrameRejected(oversizedModelFieldFrame),
    "Candidate event frame enforces per-field limits even when the checksum is valid");
var malformedUtf8Frame = candidateFrame.ToArray();
malformedUtf8Frame[22] = 0xFF;
RecomputeCandidateFrameChecksum(malformedUtf8Frame);
Check(CandidateFrameRejected(malformedUtf8Frame),
    "Candidate event frame rejects malformed UTF-8 after checksum validation");
var mismatchedStoredIdFrame = candidateFrame.ToArray();
mismatchedStoredIdFrame[96] ^= 0x01;
RecomputeCandidateFrameChecksum(mismatchedStoredIdFrame);
Check(CandidateFrameRejected(mismatchedStoredIdFrame),
    "Candidate event frame rejects a tampered stored identity even with a valid checksum");
Check(SimulationEventId.Create(123, "v1", encodedSpatialBytes, "resource.depleted", 7, "tick:000042")
        == resourceEventId,
    "Equivalent event identity inputs produce the same identifier");
Check(SimulationEventId.Create(124, "v1", encodedSpatialBytes, "resource.depleted", 7, "tick:000042")
        != resourceEventId,
    "Changing world seed changes event identity");
Check(SimulationEventId.Create(123, "v1", encodedSpatialBytes, "resource.depleted", 8, "tick:000042")
        != resourceEventId,
    "Changing event ordinal changes event identity");
Check(SimulationEventId.Create(123, "v1", encodedSpatialBytes, "resource.depleted", 7, "tick:000043")
        != resourceEventId,
    "Changing domain logical-time key changes event identity");

var eventPayload = new byte[] { 0x10, 0x20, 0x30 };
var firstEvent = SimulationEvent.Create(123, "v1", encodedSpatialBytes,
    "resource.depleted", 7, "tick:000042", eventPayload);
eventPayload[0] = 0xFF;
Check(firstEvent.Payload.Span[0] == 0x10,
    "Event payload copies caller bytes to prevent post-construction mutation");
Check(firstEvent.StreamKey == Convert.ToHexString(encodedSpatialBytes),
    "Event retains its canonical entity/region address for stream-scoped replay");
var eventHistory = new InMemoryEventHistory();
Check(eventHistory.Commit(firstEvent) == EventCommitResult.Committed,
    "First event commit appends the candidate");
Check(eventHistory.Commit(SimulationEvent.Create(123, "v1", encodedSpatialBytes,
        "resource.depleted", 7, "tick:000042", new byte[] { 0x10, 0x20, 0x30 }))
        == EventCommitResult.AlreadyCommitted,
    "Retrying an identical event identity and payload is idempotent");
Check(eventHistory.Count == 1, "Idempotent retry does not duplicate history");
Check(eventHistory.Commit(SimulationEvent.Create(123, "v1", encodedSpatialBytes,
        "resource.depleted", 7, "tick:000042", new byte[] { 0x99 }))
        == EventCommitResult.IdentityConflict,
    "Reusing an event identity with a different payload is reported as a conflict");
Check(eventHistory.Count == 1, "Identity conflict does not replace or append history");

var nextTimeEvent = SimulationEvent.Create(123, "v1", encodedSpatialBytes,
    "resource.depleted", 8, "tick:000043", new byte[] { 4 });
var earlierTimeEvent = SimulationEvent.Create(123, "v1", encodedSpatialBytes,
    "resource.depleted", 9, "tick:000041", new byte[] { 5 });
eventHistory.Commit(nextTimeEvent);
eventHistory.Commit(earlierTimeEvent);
var orderedResourceEvents = eventHistory.ReadOrdered("resource.depleted", encodedSpatialBytes);
Check(orderedResourceEvents.Select(e => e.LogicalTimeKey)
        .SequenceEqual(new[] { "tick:000041", "tick:000042", "tick:000043" }),
    "Address-scoped domain event reads use the declared ordinal logical-time key ordering");
Check(eventHistory.ReadOrdered("unrelated.domain", encodedSpatialBytes).Count == 0,
    "Domain event reads do not leak events from unrelated domains");
var otherAddressBytes = HierarchicalSpatialAddressCodec.Encode(
    HierarchicalSpatialAddress.At(GalaxyCellAddress.FromOrdinal(1)));
eventHistory.Commit(SimulationEvent.Create(123, "v1", otherAddressBytes,
    "resource.depleted", 1, "tick:000001", new byte[] { 6 }));
Check(eventHistory.ReadOrdered("resource.depleted", encodedSpatialBytes).Count == 3,
    "Address-scoped reads exclude events belonging to another entity or region");
Check(eventHistory.ReadOrdered("resource.depleted").Count == 4,
    "Domain-wide reads include each committed stream for diagnostics");

// A deliberately narrow replay example: signed Int64 resource deltas, big-endian payloads.
var balanceHistory = new InMemoryEventHistory();
var balanceAddress = encodedSpatialBytes;
balanceHistory.Commit(SimulationEvent.Create(123, "v1", balanceAddress,
    ResourceBalanceReplay.EventDomain, 1, "tick:000002", ResourceBalanceReplay.EncodeDelta(-3)));
balanceHistory.Commit(SimulationEvent.Create(123, "v1", balanceAddress,
    ResourceBalanceReplay.EventDomain, 2, "tick:000001", ResourceBalanceReplay.EncodeDelta(10)));
balanceHistory.Commit(SimulationEvent.Create(123, "v1", otherAddressBytes,
    ResourceBalanceReplay.EventDomain, 3, "tick:000001", ResourceBalanceReplay.EncodeDelta(900)));
var balanceStream = balanceHistory.ReadOrdered(ResourceBalanceReplay.EventDomain, balanceAddress);
Check(ResourceBalanceReplay.Replay(5, balanceStream) == 12,
    "Balance replay applies address-scoped deltas in logical-time order (5 + 10 - 3)");
var duplicateReplayEventRejected = false;
try { _ = ResourceBalanceReplay.Replay(5, new[] { balanceStream[0], balanceStream[0] }); }
catch (ArgumentException) { duplicateReplayEventRejected = true; }
Check(duplicateReplayEventRejected, "Resource replay rejects duplicate event identities instead of applying consequences twice");
Check(ResourceBalanceReplay.DecodeDelta(ResourceBalanceReplay.EncodeDelta(long.MinValue)) == long.MinValue,
    "Resource delta V1 round-trips the signed Int64 lower boundary");
var malformedBalanceRejected = false;
try { _ = ResourceBalanceReplay.DecodeDelta(new byte[7]); }
catch (FormatException) { malformedBalanceRejected = true; }
Check(malformedBalanceRejected, "Resource delta decoder rejects payloads that are not exactly eight bytes");
var balanceOverflowRejected = false;
try
{
    _ = ResourceBalanceReplay.Replay(long.MaxValue, new[]
    {
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            4, "tick:000004", ResourceBalanceReplay.EncodeDelta(1))
    });
}
catch (OverflowException) { balanceOverflowRejected = true; }
Check(balanceOverflowRejected, "Resource replay rejects signed Int64 overflow rather than wrapping");
var wrongBalanceDomainRejected = false;
try { _ = ResourceBalanceReplay.Replay(0, orderedResourceEvents); }
catch (ArgumentException) { wrongBalanceDomainRejected = true; }

Check(wrongBalanceDomainRejected, "Resource replay refuses events from another domain");

var mixedAddressRejected = false;
try
{
    _ = ResourceBalanceReplay.Replay(0, new[]
    {
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            10, "tick:000001", ResourceBalanceReplay.EncodeDelta(1)),
        SimulationEvent.Create(123, "v1", otherAddressBytes, ResourceBalanceReplay.EventDomain,
            11, "tick:000002", ResourceBalanceReplay.EncodeDelta(2))
    });
}
catch (ArgumentException) { mixedAddressRejected = true; }
Check(mixedAddressRejected, "Resource replay rejects streams that mix canonical addresses");

var mixedModelVersionRejected = false;
try
{
    _ = ResourceBalanceReplay.Replay(0, new[]
    {
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            12, "tick:000001", ResourceBalanceReplay.EncodeDelta(1)),
        SimulationEvent.Create(123, "v2", balanceAddress, ResourceBalanceReplay.EventDomain,
            13, "tick:000002", ResourceBalanceReplay.EncodeDelta(2))
    });
}
catch (ArgumentException) { mixedModelVersionRejected = true; }
Check(mixedModelVersionRejected, "Resource replay rejects mixed simulation-model versions");

var unorderedBalanceStreamRejected = false;
try
{
    _ = ResourceBalanceReplay.Replay(0, new[]
    {
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            14, "tick:000002", ResourceBalanceReplay.EncodeDelta(1)),
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            15, "tick:000001", ResourceBalanceReplay.EncodeDelta(2))
    });
}
catch (ArgumentException) { unorderedBalanceStreamRejected = true; }
Check(unorderedBalanceStreamRejected, "Resource replay rejects streams that violate the declared ordering");

var resourceCheckpoint = ResourceBalanceCheckpoint.Capture(5, balanceStream.Take(1));
Check(resourceCheckpoint.Balance == 15,
    "Resource checkpoint captures the balance at its exact event boundary");
Check(resourceCheckpoint.ReplayTail(balanceStream.Skip(1)) == ResourceBalanceReplay.Replay(5, balanceStream),
    "Checkpoint-plus-tail replay matches full replay for the same ordered resource stream");
var checkpointBoundaryRejected = false;
try { _ = resourceCheckpoint.ReplayTail(balanceStream.Take(1)); }
catch (ArgumentException) { checkpointBoundaryRejected = true; }
Check(checkpointBoundaryRejected,
    "Checkpoint tail rejects replaying an event already included in the checkpoint");
var checkpointScopeRejected = false;
try
{
    var foreignTail = new[]
    {
        SimulationEvent.Create(123, "v1", otherAddressBytes, ResourceBalanceReplay.EventDomain,
            99, "tick:000003", ResourceBalanceReplay.EncodeDelta(1))
    };
    _ = resourceCheckpoint.ReplayTail(foreignTail);
}
catch (ArgumentException) { checkpointScopeRejected = true; }
Check(checkpointScopeRejected,
    "Checkpoint tail rejects events from another canonical address stream");

var emptyCheckpointPrefixRejected = false;
try { _ = ResourceBalanceCheckpoint.Capture(5, Array.Empty<SimulationEvent>()); }
catch (ArgumentException) { emptyCheckpointPrefixRejected = true; }
Check(emptyCheckpointPrefixRejected,
    "Checkpoint capture rejects an empty prefix without a trustworthy history boundary");

var incompatibleCheckpointSchemaRejected = false;
try { _ = (resourceCheckpoint with { SchemaVersion = ResourceBalanceCheckpoint.CurrentSchemaVersion + 1 })
        .ReplayTail(Array.Empty<SimulationEvent>()); }
catch (InvalidOperationException) { incompatibleCheckpointSchemaRejected = true; }
Check(incompatibleCheckpointSchemaRejected,
    "Checkpoint replay rejects an unknown schema version even when the tail is empty");

var foreignSeedTailRejected = false;
try
{
    _ = resourceCheckpoint.ReplayTail(new[]
    {
        SimulationEvent.Create(999, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            99, "tick:999999", ResourceBalanceReplay.EncodeDelta(1))
    });
}
catch (ArgumentException) { foreignSeedTailRejected = true; }
Check(foreignSeedTailRejected,
    "Checkpoint tail rejects events from another world seed");

var foreignModelTailRejected = false;
try
{
    _ = resourceCheckpoint.ReplayTail(new[]
    {
        SimulationEvent.Create(123, "model-v2", balanceAddress, ResourceBalanceReplay.EventDomain,
            99, "tick:999999", ResourceBalanceReplay.EncodeDelta(1))
    });
}
catch (ArgumentException) { foreignModelTailRejected = true; }
Check(foreignModelTailRejected,
    "Checkpoint tail rejects events from another simulation-model version");

var unorderedCheckpointTailRejected = false;
try
{
    _ = resourceCheckpoint.ReplayTail(new[]
    {
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            99, "tick:000099", ResourceBalanceReplay.EncodeDelta(1)),
        SimulationEvent.Create(123, "v1", balanceAddress, ResourceBalanceReplay.EventDomain,
            98, "tick:000098", ResourceBalanceReplay.EncodeDelta(1))
    });
}
catch (ArgumentException) { unorderedCheckpointTailRejected = true; }
Check(unorderedCheckpointTailRejected,
    "Checkpoint replay rejects a tail whose events are not strictly ordered");

var concurrentHistory = new InMemoryEventHistory();
Parallel.For(0, 64, _ => concurrentHistory.Commit(firstEvent));
Check(concurrentHistory.Count == 1,
    "Concurrent identical commits serialize to one history entry");
Check(concurrentHistory.ReadOrdered("resource.depleted", encodedSpatialBytes).Count == 1,
    "Concurrent identical commits remain singly observable");

var badSpatialVersion = (byte[])encodedSpatialBytes.Clone();
badSpatialVersion[4] = 2;
var rejectedSpatialVersion = false;
try { _ = HierarchicalSpatialAddressCodec.Decode(badSpatialVersion); }
catch (FormatException) { rejectedSpatialVersion = true; }
Check(rejectedSpatialVersion, "Spatial address decoder rejects unknown encoding versions");

var badSpatialChild = (byte[])encodedSpatialBytes.Clone();
badSpatialChild[^3] = 10;
var rejectedSpatialChild = false;
try { _ = HierarchicalSpatialAddressCodec.Decode(badSpatialChild); }
catch (FormatException) { rejectedSpatialChild = true; }
Check(rejectedSpatialChild, "Spatial address decoder rejects child coordinates outside the 10x10x10 grid");

var truncatedSpatialAddress = encodedSpatialBytes[..^1];
var rejectedSpatialLength = false;
try { _ = HierarchicalSpatialAddressCodec.Decode(truncatedSpatialAddress); }
catch (FormatException) { rejectedSpatialLength = true; }
Check(rejectedSpatialLength, "Spatial address decoder rejects payload lengths inconsistent with declared depth");

var circular = new KeplerOrbit(
    SemiMajorAxis: 10,
    Eccentricity: 0,
    Inclination: 0,
    LongitudeOfAscendingNode: 0,
    ArgumentOfPeriapsis: 0,
    MeanAnomalyAtEpoch: 0,
    EpochTime: 0,
    GravitationalParameter: 1);

var period = circular.Period;
var start = circular.PositionAt(0);
var quarter = circular.PositionAt(period / 4);
var halfway = circular.PositionAt(period / 2);
var afterPeriod = circular.PositionAt(period);

Near(start.X, 10, 1e-10, "Circular orbit starts at positive X");
Near(start.Y, 0, 1e-10, "Circular orbit starts at zero Y");
Near(quarter.X, 0, 1e-9, "Quarter-period X");
Near(quarter.Y, 10, 1e-9, "Quarter-period Y");
Near(halfway.X, -10, 1e-9, "Half-period X");
Near(halfway.Y, 0, 1e-9, "Half-period Y");
Near((afterPeriod - start).Length, 0, 1e-9, "Orbit repeats after one period");

// Queries are pure: asking for times in a different order must not change any result.
var laterFirst = circular.PositionAt(period * 0.37);
_ = circular.PositionAt(period * 0.91);
var laterAgain = circular.PositionAt(period * 0.37);
Near((laterAgain - laterFirst).Length, 0, 1e-12, "Repeated time query is stable");

// Elliptical orbit: periapsis and apoapsis radii are a(1-e) and a(1+e).
var elliptical = circular with { SemiMajorAxis = 20, Eccentricity = 0.5 };
Near(elliptical.PositionAt(0).Length, 10, 1e-9, "Elliptical periapsis radius");
Near(elliptical.PositionAt(elliptical.Period / 2).Length, 30, 1e-8, "Elliptical apoapsis radius");

// Inclination rotates the orbit out of the reference plane.
var inclined = circular with { Inclination = Math.PI / 2 };
var inclinedQuarter = inclined.PositionAt(inclined.Period / 4);
Near(inclinedQuarter.X, 0, 1e-9, "Inclined quarter-period X");
Near(inclinedQuarter.Y, 0, 1e-9, "Inclined quarter-period Y");
Near(inclinedQuarter.Z, 10, 1e-9, "Inclined quarter-period Z");

var rejectedInvalid = false;
try { _ = (circular with { Eccentricity = 1 }).PositionAt(0); }
catch (ArgumentOutOfRangeException) { rejectedInvalid = true; }
Check(rejectedInvalid, "Parabolic/hyperbolic eccentricity must be rejected explicitly");

var rejectedNonFiniteTime = false;
try { _ = circular.PositionAt(double.NaN); }
catch (ArgumentOutOfRangeException) { rejectedNonFiniteTime = true; }
Check(rejectedNonFiniteTime, "Non-finite logical time must be rejected explicitly");

var rejectedNonFiniteParameter = false;
try { _ = (circular with { GravitationalParameter = double.PositiveInfinity }).PositionAt(0); }
catch (ArgumentOutOfRangeException) { rejectedNonFiniteParameter = true; }
Check(rejectedNonFiniteParameter, "Non-finite gravitational parameter must be rejected explicitly");

// Near-parabolic elliptic motion is a numerically demanding boundary. The solver
// must remain finite and periodic without claiming to support e >= 1.
var highEccentricity = circular with
{
    SemiMajorAxis = 1,
    Eccentricity = 0.999,
    MeanAnomalyAtEpoch = 1e-6
};
var nearPeriapsis = highEccentricity.PositionAt(0);
var nearPeriapsisAgain = highEccentricity.PositionAt(highEccentricity.Period);
Check(double.IsFinite(nearPeriapsis.X) && double.IsFinite(nearPeriapsis.Y)
    && double.IsFinite(nearPeriapsis.Z), "High-eccentricity periapsis query remains finite");
Near((nearPeriapsisAgain - nearPeriapsis).Length, 0, 1e-8,
    "High-eccentricity orbit repeats after one period");
Check(nearPeriapsis.Length >= 1 - highEccentricity.Eccentricity - 1e-10,
    "High-eccentricity orbit does not cross inside its periapsis radius");


 
// The data scape is a graph of composable capability records. Validate its joins
// so expanding the catalogue cannot quietly create orphaned or duplicate identities.
var dataRoot = Path.Combine(AppContext.BaseDirectory, "data");
System.Text.Json.JsonDocument ReadData(string file) => System.Text.Json.JsonDocument.Parse(File.ReadAllText(Path.Combine(dataRoot, file)));
string[] Ids(System.Text.Json.JsonDocument document, string collection) =>
    document.RootElement.GetProperty(collection).EnumerateArray()
        .Select(item => item.GetProperty("id").GetString()!).ToArray();
void Unique(string label, string[] ids) =>
    Check(ids.Length == ids.Distinct(StringComparer.Ordinal).Count(), $"{label} identifiers are unique");
void AllExist(string label, IEnumerable<string> references, HashSet<string> available) =>
    Check(references.All(available.Contains), $"{label} references resolve (unresolved: {string.Join(", ", references.Where(x => !available.Contains(x)).Distinct())})");

using var resources = ReadData("resources.json");
using var technologies = ReadData("technologies.json");
using var vehicles = ReadData("vehicles.json");
using var facilities = ReadData("facilities.json");
using var chassisData = ReadData("vehicle-chassis.json");
using var electronicsData = ReadData("electronic-systems.json");
using var assembliesData = ReadData("building-assemblies.json");
using var securityData = ReadData("security-systems.json");
using var upgradesData = ReadData("upgrades.json");

var resourceIds = Ids(resources, "resources").ToHashSet(StringComparer.Ordinal);
var technologyIds = Ids(technologies, "technologies").ToHashSet(StringComparer.Ordinal);
var vehicleIds = Ids(vehicles, "vehicles").ToHashSet(StringComparer.Ordinal);
var facilityIds = Ids(facilities, "facilities").ToHashSet(StringComparer.Ordinal);
var chassisIds = Ids(chassisData, "chassis").ToHashSet(StringComparer.Ordinal);
var electronicIds = Ids(electronicsData, "systems").ToHashSet(StringComparer.Ordinal);
var assemblyIds = Ids(assembliesData, "assemblies").ToHashSet(StringComparer.Ordinal);
var securityIds = Ids(securityData, "systems").ToHashSet(StringComparer.Ordinal);
var installPackageIds = Ids(upgradesData, "installPackages").ToHashSet(StringComparer.Ordinal);

Unique("Resource", Ids(resources, "resources"));
Unique("Technology", Ids(technologies, "technologies"));
Unique("Vehicle", Ids(vehicles, "vehicles"));
Unique("Facility", Ids(facilities, "facilities"));
Unique("Chassis", Ids(chassisData, "chassis"));
Unique("Electronic system", Ids(electronicsData, "systems"));
Unique("Building assembly", Ids(assembliesData, "assemblies"));
Unique("Security system", Ids(securityData, "systems"));
Unique("Upgrade", Ids(upgradesData, "upgrades"));
Unique("Install package", Ids(upgradesData, "installPackages"));

AllExist("Vehicle chassis", vehicles.RootElement.GetProperty("vehicles").EnumerateArray()
    .Select(v => v.GetProperty("chassisDefinition").GetString()!), chassisIds);
AllExist("Vehicle electronic subsystem", vehicles.RootElement.GetProperty("vehicles").EnumerateArray()
    .SelectMany(v => v.GetProperty("electronicSystems").EnumerateArray().Select(x => x.GetString()!)), electronicIds);
AllExist("Facility assembly", facilities.RootElement.GetProperty("facilities").EnumerateArray()
    .Select(f => f.TryGetProperty("assemblyDefinition", out var a) ? a.GetString()! : string.Empty)
    .Where(x => x.Length > 0), assemblyIds);
AllExist("Assembly facility", assembliesData.RootElement.GetProperty("assemblies").EnumerateArray()
    .Select(a => a.GetProperty("facility").GetString()!), facilityIds);
AllExist("Electronics technology", electronicsData.RootElement.GetProperty("systems").EnumerateArray()
    .Where(s => s.TryGetProperty("technology", out _))
    .Select(s => s.GetProperty("technology").GetString()!), technologyIds);
AllExist("Security controller", securityData.RootElement.GetProperty("systems").EnumerateArray()
    .Select(s => s.GetProperty("controller").GetString()!), electronicIds);
AllExist("Security dependency", securityData.RootElement.GetProperty("systems").EnumerateArray()
    .SelectMany(s => s.GetProperty("dependencies").EnumerateArray().Select(x => x.GetString()!)), electronicIds);
AllExist("Technology prerequisite", technologies.RootElement.GetProperty("technologies").EnumerateArray()
    .SelectMany(t => t.GetProperty("prerequisites").EnumerateArray().Select(x => x.GetString()!)),
    technologyIds.Union(resourceIds).ToHashSet(StringComparer.Ordinal));
AllExist("Upgrade technology", upgradesData.RootElement.GetProperty("upgrades").EnumerateArray()
    .Select(u => u.GetProperty("technology").GetString()!), technologyIds);
AllExist("Upgrade material", upgradesData.RootElement.GetProperty("upgrades").EnumerateArray()
    .SelectMany(u => u.GetProperty("manufacturing").EnumerateArray().Select(x => x.GetString()!)), resourceIds);
AllExist("Upgrade target", upgradesData.RootElement.GetProperty("upgrades").EnumerateArray()
    .SelectMany(u => u.GetProperty("target").EnumerateArray().Select(x => x.GetString()!))
    .Where(x => x != "soldier.equipped"),
    vehicleIds.Union(facilityIds).Union(electronicIds).Union(securityIds).ToHashSet(StringComparer.Ordinal));
AllExist("Upgrade installation package", upgradesData.RootElement.GetProperty("upgrades").EnumerateArray()
    .Select(u => u.GetProperty("installPackage").GetString()!), installPackageIds);
AllExist("Assembly material", assembliesData.RootElement.GetProperty("assemblies").EnumerateArray()
    .SelectMany(a => a.GetProperty("materials").EnumerateArray().Select(m => m.GetProperty("id").GetString()!)), resourceIds);


using var powerDistribution = ReadData("power-distribution.json");
var powerEquipmentIds = Ids(powerDistribution, "equipment");
var powerArchitectureIds = Ids(powerDistribution, "architectures");
Unique("Power distribution equipment", powerEquipmentIds);
Unique("Power architecture", powerArchitectureIds);
Check(powerEquipmentIds.Length >= 10, "Power catalogue covers distribution, protection, storage and control equipment");
Check(powerArchitectureIds.Length >= 3, "Power catalogue includes multiple topology patterns");
Check(assembliesData.RootElement.GetProperty("assemblies").EnumerateArray()
    .All(a => a.TryGetProperty("staffingByShift", out var staffing) && staffing.EnumerateObject().Any()),
    "Every building assembly declares shift staffing requirements");
Check(assembliesData.RootElement.GetProperty("assemblies").EnumerateArray()
    .All(a => a.GetProperty("connectedLoadKW").GetDouble() >= 0 &&
              a.GetProperty("peakDemandKW").GetDouble() >= a.GetProperty("connectedLoadKW").GetDouble()),
    "Every assembly declares non-negative connected load and peak demand at least as large");

Check(chassisIds.Count >= 10, "Detailed chassis catalogue includes all current vehicle chassis");
Check(electronicIds.Count >= 20, "Electronic system catalogue includes more than 20 components");
Check(assemblyIds.Count == facilityIds.Count, "Every facility has a construction assembly definition");
Check(installPackageIds.Count == Ids(upgradesData, "upgrades").Length - 1 || installPackageIds.Count >= 10,
    "Physical installation packages are explicitly catalogued");


using var advisorAppointmentsData = ReadData("advisor-appointments.json");
using var advisorCandidatesData = ReadData("advisor-candidates.json");
using var kanbanData = ReadData("work-kanban-contract.json");
var appointmentIdsArray = Ids(advisorAppointmentsData, "appointments");
var appointmentIds = appointmentIdsArray.ToHashSet(StringComparer.Ordinal);
var candidateIds = Ids(advisorCandidatesData, "candidates");
var kanbanColumnIds = Ids(kanbanData, "columns").ToHashSet(StringComparer.Ordinal);
Unique("Advisor appointment", appointmentIdsArray);
Unique("Advisor candidate", candidateIds);
var bonusIds = advisorCandidatesData.RootElement.GetProperty("candidates").EnumerateArray()
    .SelectMany(c => c.GetProperty("agentBonuses").EnumerateArray().Select(b => b.GetProperty("id").GetString()!)).ToArray();
Unique("Agent-earned bonus", bonusIds);
AllExist("Candidate appointment eligibility", advisorCandidatesData.RootElement.GetProperty("candidates").EnumerateArray()
    .SelectMany(c => c.GetProperty("eligibleAppointments").EnumerateArray().Select(x => x.GetString()!)), appointmentIds);
Check(advisorAppointmentsData.RootElement.GetProperty("appointments").EnumerateArray()
    .All(a => a.GetProperty("workPrograms").EnumerateArray().Any() &&
              a.GetProperty("qualificationGroups").EnumerateArray().Any() &&
              a.GetProperty("bonusDomains").EnumerateArray().Any()),
    "Every advisor appointment connects qualifications, work programs and scoped bonus domains");
Check(advisorCandidatesData.RootElement.GetProperty("candidates").EnumerateArray()
    .All(c => c.GetProperty("agentBonuses").EnumerateArray().All(b =>
        b.GetProperty("earnedBy").GetString() == "agent-experience" &&
        b.GetProperty("evidence").EnumerateArray().Any() &&
        b.GetProperty("scope").GetString()!.Length > 0)),
    "Every illustrative advisor bonus is earned through agent experience, evidenced and scoped");
Check(advisorCandidatesData.RootElement.GetProperty("candidates").EnumerateArray()
    .All(c => c.GetProperty("warningBehavior").GetProperty("canIssueWarnings").GetBoolean() &&
              c.GetProperty("warningBehavior").GetProperty("playerCanOverride").GetBoolean()),
    "Advisor candidates can warn the player while preserving explicit player authority");
AllExist("Kanban transition source", kanbanData.RootElement.GetProperty("transitionRules").EnumerateArray()
    .Select(t => t.GetProperty("from").GetString()!), kanbanColumnIds);
AllExist("Kanban transition destination", kanbanData.RootElement.GetProperty("transitionRules").EnumerateArray()
    .Select(t => t.GetProperty("to").GetString()!), kanbanColumnIds);
Check(kanbanData.RootElement.GetProperty("riskAndOrderContract").GetProperty("overrideDoesNotEraseWarning").GetBoolean(),
    "Risky player orders preserve the advisor's warning as historical evidence");
Check(kanbanData.RootElement.GetProperty("agentAndPersonnelLifecycle").GetProperty("deathIsPersistentAndIrreversibleByDefault").GetBoolean(),
    "Personnel death is modeled as persistent world history");
Check(kanbanData.RootElement.GetProperty("capacityRules").GetProperty("priorityChangesDoNotMagicallyCreateLaborOrMaterials").GetBoolean(),
    "Kanban reprioritization cannot bypass physical labor and material constraints");


// Cross-check the new design catalogues rather than merely checking that each file parses.
var appointmentRecords = advisorAppointmentsData.RootElement.GetProperty("appointments").EnumerateArray().ToArray();
var appointmentById = appointmentRecords.ToDictionary(a => a.GetProperty("id").GetString()!, StringComparer.Ordinal);
var candidateRecords = advisorCandidatesData.RootElement.GetProperty("candidates").EnumerateArray().ToArray();
Unique("Candidate personnel identity", candidateRecords.Select(c => c.GetProperty("personnelId").GetString()!).ToArray());
Check(candidateRecords.All(c =>
{
    var eligible = c.GetProperty("eligibleAppointments").EnumerateArray()
        .Select(x => x.GetString()!).ToArray();
    var validPrograms = eligible
        .Where(appointmentById.ContainsKey)
        .SelectMany(id => appointmentById[id].GetProperty("workPrograms").EnumerateArray()
            .Select(x => x.GetString()!))
        .ToHashSet(StringComparer.Ordinal);
    return c.GetProperty("candidateFirstWork").EnumerateArray()
        .All(x => validPrograms.Contains(x.GetString()!));
}), "Every candidate's initial work is offered by at least one eligible appointment");
Check(candidateRecords.All(c =>
{
    var eligible = c.GetProperty("eligibleAppointments").EnumerateArray()
        .Select(x => x.GetString()!).Where(appointmentById.ContainsKey).ToArray();
    var domains = eligible.SelectMany(id => appointmentById[id].GetProperty("bonusDomains").EnumerateArray()
        .Select(x => x.GetString()!)).ToHashSet(StringComparer.Ordinal);
    return c.GetProperty("agentBonuses").EnumerateArray()
        .All(b => domains.Contains(b.GetProperty("domain").GetString()!));
}), "Every candidate bonus domain is supported by an eligible appointment");
var requiredWorkFields = kanbanData.RootElement.GetProperty("requiredWorkItemFields")
    .EnumerateArray().Select(x => x.GetString()!).ToArray();
Unique("Required work-item field", requiredWorkFields);
Check(requiredWorkFields.Contains("eventHistory", StringComparer.Ordinal) &&
      requiredWorkFields.Contains("completionCriteria", StringComparer.Ordinal) &&
      requiredWorkFields.Contains("ownerAgentId", StringComparer.Ordinal),
    "Executable work items preserve ownership, acceptance criteria, and event history");
Check(kanbanData.RootElement.GetProperty("transitionRules").EnumerateArray()
    .All(t => t.GetProperty("requires").EnumerateArray().Any()),
    "Every legal kanban transition declares explicit prerequisites");


// The warning and override schemas are durable causal records, not transient UI text.
var riskContract = kanbanData.RootElement.GetProperty("riskAndOrderContract");
var requiredWarningFields = new[] { "authorAgentId", "logicalTime", "observations", "inference", "uncertainty", "recommendedMitigation", "affectedWorkItemId" };
var requiredOverrideFields = new[] { "decisionMakerId", "logicalTime", "orderText", "warningIdsAcknowledged", "acceptedRisk", "mitigationsAccepted" };
Check(requiredWarningFields.All(field => riskContract.GetProperty("warningMustCapture").EnumerateArray()
    .Any(x => x.GetString() == field)),
    "Advisor warnings preserve observations, inference, uncertainty, mitigation, authorship and affected work");
Check(requiredOverrideFields.All(field => riskContract.GetProperty("overrideMustCapture").EnumerateArray()
    .Any(x => x.GetString() == field)),
    "Player overrides preserve acknowledged warnings, accepted risk, decision and mitigations");
Check(riskContract.GetProperty("causalHistoryMustLink").EnumerateArray().Select(x => x.GetString())
    .SequenceEqual(new[] { "warning", "order", "physical-actions", "outcome", "casualties", "recovery-work" }),
    "Risk outcomes retain an ordered causal path from warning to recovery");
Check(appointmentRecords.All(a => a.TryGetProperty("successionPolicy", out var policy) &&
    !string.IsNullOrWhiteSpace(policy.GetString())),
    "Every advisor appointment defines vacancy and succession behavior");
Check(candidateRecords.All(c =>
{
    var evidenceReferences = c.GetProperty("recordEvidence").EnumerateArray()
        .Select(e => e.GetProperty("reference").GetString()!).ToHashSet(StringComparer.Ordinal);
    return c.GetProperty("agentBonuses").EnumerateArray().All(b =>
        b.GetProperty("evidence").EnumerateArray().Any() &&
        b.GetProperty("evidence").EnumerateArray().All(e => evidenceReferences.Contains(e.GetString()!)));
}), "Every illustrative earned bonus cites evidence present in that person's record");
Check(candidateRecords.All(c => c.GetProperty("agentBonuses").EnumerateArray().All(b =>
    b.GetProperty("magnitude").GetDouble() >= 0 &&
    double.IsFinite(b.GetProperty("magnitude").GetDouble()))),
    "Illustrative bonus magnitudes are finite and non-negative");
Check(kanbanData.RootElement.GetProperty("agentAndPersonnelLifecycle").GetProperty("deathCausesWorkReassignmentReview").GetBoolean() &&
      kanbanData.RootElement.GetProperty("agentAndPersonnelLifecycle").GetProperty("uniqueKnowledgeMayBeLost").GetBoolean() &&
      kanbanData.RootElement.GetProperty("agentAndPersonnelLifecycle").GetProperty("knowledgeTransferRequiresAnEventOrPractice").GetBoolean(),
    "Personnel death triggers work review and preserves the distinction between records and transferred knowledge");


// Ship assembly and directional-damage contracts: persistent part identity, valid joins,
// six target-local directions, and explicit salvage/cascade semantics.
using var shipAssembliesData = ReadData("ship-assemblies.json");
using var damageModelData = ReadData("directional-damage-and-salvage.json");
var shipRecords = shipAssembliesData.RootElement.GetProperty("examples").EnumerateArray().ToArray();
var shipPartRecords = shipRecords.SelectMany(s => s.GetProperty("parts").EnumerateArray()).ToArray();
var shipPartIds = shipPartRecords.Select(p => p.GetProperty("id").GetString()!).ToArray();
var shipZoneIds = shipRecords.SelectMany(s => s.GetProperty("zones").EnumerateArray())
    .Select(z => z.GetProperty("id").GetString()!).ToHashSet(StringComparer.Ordinal);
Unique("Ship part", shipPartIds);
AllExist("Ship part zone", shipPartRecords.Select(p => p.GetProperty("zone").GetString()!), shipZoneIds);
var shipPartIdSet = shipPartIds.ToHashSet(StringComparer.Ordinal);
AllExist("Ship part mount", shipPartRecords.Where(p => !p.GetProperty("mountsTo").ValueKind.Equals(System.Text.Json.JsonValueKind.Null))
    .Select(p => p.GetProperty("mountsTo").GetString()!), shipPartIdSet);
AllExist("Ship part dependency", shipPartRecords.SelectMany(p => p.GetProperty("dependencies").EnumerateArray()
    .Select(d => d.GetString()!)), shipPartIdSet);
Check(shipPartRecords.All(p => p.GetProperty("massT").GetDouble() > 0 &&
    p.GetProperty("damageModes").EnumerateArray().Any() &&
    !string.IsNullOrWhiteSpace(p.GetProperty("salvageClass").GetString()) &&
    !string.IsNullOrWhiteSpace(p.GetProperty("repair").GetString())),
    "Every ship part declares positive mass, damage modes, salvage class and repair approach");
var damageRoot = damageModelData.RootElement;
var expectedFaces = new[] { "forward", "aft", "port", "starboard", "dorsal", "ventral" };
Check(damageRoot.GetProperty("sixFaces").EnumerateArray().Select(x => x.GetString())
    .SequenceEqual(expectedFaces), "Directional damage defines all six target-local faces in stable order");
Check(expectedFaces.All(face => damageRoot.GetProperty("directionFrame").TryGetProperty(face, out _)),
    "Every directional face has an explicit local-axis mapping");
Check(damageRoot.GetProperty("survival").GetProperty("salvageLotFields").EnumerateArray()
    .Select(x => x.GetString()).Contains("sourceEventId") &&
      damageRoot.GetProperty("survival").GetProperty("salvageLotFields").EnumerateArray()
    .Select(x => x.GetString()).Contains("observedCondition"),
    "Salvage records retain source event and observed condition");
Check(damageRoot.GetProperty("cascade").GetProperty("eventFields").EnumerateArray()
    .Select(x => x.GetString()).Contains("parentEventId") &&
      damageRoot.GetProperty("cascade").GetProperty("eventFields").EnumerateArray()
    .Select(x => x.GetString()).Contains("transferPath"),
    "Secondary effects retain causal parent and physical transfer path");
Check(damageRoot.GetProperty("performance").GetProperty("persistence").GetString()!
    .Contains("source of truth", StringComparison.OrdinalIgnoreCase),
    "Precomputed damage tables remain caches rather than authoritative persistent state");
Check(shipRecords.All(s => s.GetProperty("zones").EnumerateArray().Any() &&
    s.GetProperty("parts").EnumerateArray().Any()),
    "Ship reference models connect structural zones and independently identifiable parts");


// Faction ship style is data-driven, physically consequential, and deliberately not
// assigned to named factions until creator-approved faction canon exists.
using var factionShipDoctrinesData = ReadData("faction-ship-doctrines.json");
var factionStyleRoot = factionShipDoctrinesData.RootElement;
var factionStyleRecords = factionStyleRoot.GetProperty("profiles").EnumerateArray().ToArray();
var factionStyleIds = factionStyleRecords.Select(p => p.GetProperty("id").GetString()!).ToArray();
Unique("Faction ship style profile", factionStyleIds);
var styleDimensionIds = factionStyleRoot.GetProperty("styleDimensions").EnumerateArray()
    .Select(d => d.GetProperty("id").GetString()!).ToArray();
Unique("Faction ship style dimension", styleDimensionIds);
Check(factionStyleRecords.Length >= 6, "Faction ship catalogue includes multiple distinct candidate design languages");
Check(factionStyleRoot.GetProperty("rules").EnumerateArray()
    .Any(r => r.GetString()!.Contains("not real-world nation-to-faction mappings", StringComparison.OrdinalIgnoreCase)),
    "Faction style archetypes are not silently mapped to real-world nations");
Check(factionStyleRecords.All(p =>
    p.TryGetProperty("silhouetteCues", out var silhouette) && silhouette.EnumerateArray().Any() &&
    p.TryGetProperty("operationalStrengths", out var strengths) && strengths.EnumerateArray().Any() &&
    p.TryGetProperty("tradeoffs", out var tradeoffs) && tradeoffs.EnumerateArray().Any() &&
    p.TryGetProperty("industrialDependencies", out var dependencies) && dependencies.EnumerateArray().Any() &&
    p.TryGetProperty("failureAndRepairExpression", out var repairs) && repairs.EnumerateArray().Any()),
    "Every faction ship style connects visible cues to strengths, tradeoffs, industry, and repair");
Check(factionStyleRoot.GetProperty("missionComparisonContract").GetProperty("requiredOutputs").EnumerateArray()
    .Select(x => x.GetString()).Contains("maintenance hours") &&
      factionStyleRoot.GetProperty("missionComparisonContract").GetProperty("requiredOutputs").EnumerateArray()
    .Select(x => x.GetString()).Contains("failure isolation"),
    "Matched mission comparisons include maintenance and survivability consequences");

 
// Authored single-player campaigns and generated multiplayer galaxies share one
// world contract, while civilization ancestry records causal human history.
using var campaignManifestsData = ReadData("campaign-galaxy-manifests.json");
using var civilizationLineagesData = ReadData("civilization-lineages.json");
using var galaxyGenerationContractData = ReadData("galaxy-generation-contract.json");
var campaignRoot = campaignManifestsData.RootElement;
var lineageRoot = civilizationLineagesData.RootElement;
var generationRoot = galaxyGenerationContractData.RootElement;
Check(campaignRoot.GetProperty("modes").GetProperty("singlePlayer").GetProperty("generationPolicy").GetString() == "authored-and-curated",
    "Single-player galaxy creation is authored and curated");
Check(campaignRoot.GetProperty("modes").GetProperty("multiplayer").GetProperty("generationPolicy").GetString() == "automatic-and-seed-reproducible",
    "Multiplayer galaxy creation is automatic and reproducible");
Check(campaignRoot.GetProperty("modes").GetProperty("multiplayer").GetProperty("mustUseSameWorldContract").GetBoolean(),
    "Single-player and multiplayer use the same underlying world-generation contract");
var manifestFields = campaignRoot.GetProperty("manifestFields").EnumerateArray().Select(x => x.GetString()!).ToHashSet(StringComparer.Ordinal);
Check(manifestFields.Contains("authoredLineageRefs") && manifestFields.Contains("permittedVariation") && manifestFields.Contains("saveCompatibilityPolicy"),
    "Campaign manifests can pin ancestry, control variation, and declare save compatibility");
Check(campaignRoot.GetProperty("validationGates").EnumerateArray().Any(x => x.GetString()!.Contains("chronological support", StringComparison.OrdinalIgnoreCase)),
    "Generated campaign histories validate chronology and lineage");
var lineageRelationshipKinds = lineageRoot.GetProperty("relationshipKinds").EnumerateArray().Select(x => x.GetString()!).ToHashSet(StringComparer.Ordinal);
Check(lineageRelationshipKinds.Contains("colonized-by") && lineageRelationshipKinds.Contains("seceded-from") &&
      lineageRelationshipKinds.Contains("succeeded") && lineageRelationshipKinds.Contains("reunified-with"),
    "Civilization lineage supports colonization, secession, succession, and reunification");
Check(lineageRoot.GetProperty("canonPrinciples").EnumerateArray().Any(x => x.GetString()!.Contains("All raWWar factions are human", StringComparison.Ordinal)),
    "Faction diversity is grounded in shared human species and divergent histories");
Check(lineageRoot.GetProperty("inheritancePolicy").GetProperty("inheritanceMustBeExplicitlySelected").GetBoolean() &&
      lineageRoot.GetProperty("inheritancePolicy").GetProperty("culturalSimilarityDoesNotGuaranteePoliticalAlliance").GetBoolean(),
    "Heritage is selective and does not predetermine alliances");
Check(generationRoot.GetProperty("campaignCreation").GetProperty("singlePlayer").GetProperty("policy").GetString() == "authored-and-curated" &&
      generationRoot.GetProperty("campaignCreation").GetProperty("multiplayer").GetProperty("policy").GetString() == "automatic-and-seed-reproducible",
    "Machine-readable galaxy generation contract exposes both campaign creation modes");
Check(generationRoot.GetProperty("campaignCreation").GetProperty("civilizationLineage").GetProperty("model").GetString() == "graph",
    "Galaxy generation contract treats civilization ancestry as a lineage graph");

// The galaxy-generation contract's spatial addresses must be mathematically
// self-consistent, and deterministic generation must not depend on observation order.
var universeContract = generationRoot.GetProperty("universe");
var gridDimensions = universeContract.GetProperty("gridDimensions").EnumerateArray()
    .Select(x => x.GetInt32()).ToArray();
var declaredCellCount = universeContract.GetProperty("cellCount").GetInt32();
Check(gridDimensions.SequenceEqual(new[] { 10, 10, 10 }) && declaredCellCount == 1000,
    "Galaxy spatial grid declares the expected 10×10×10 address space");
var anchor = universeContract.GetProperty("galaxyAnchor");
var anchorCoordinates = anchor.GetProperty("coordinates").EnumerateArray()
    .Select(x => x.GetInt32()).ToArray();
var anchorOrdinal = anchor.GetProperty("ordinal").GetInt32();
Check(anchorOrdinal == 42 && anchorCoordinates.SequenceEqual(new[] { 1, 4, 0 }),
    "Reserved galaxy anchor agrees with the declared x-fastest ordinal convention");
var addressRoundTrips = true;
for (var ordinal = 1; ordinal <= declaredCellCount; ordinal++)
{
    var address = GalaxyCellAddress.FromOrdinal(ordinal);
    if (address.Ordinal != ordinal ||
        address.X < 0 || address.X >= gridDimensions[0] ||
        address.Y < 0 || address.Y >= gridDimensions[1] ||
        address.Z < 0 || address.Z >= gridDimensions[2])
    {
        addressRoundTrips = false;
        break;
    }
}
Check(addressRoundTrips,
    "Every declared galaxy cell round-trips through the production address mapping");
Check(new GalaxyCellAddress(1, 4, 0).Ordinal == anchorOrdinal,
    "Production address mapping preserves the reserved galaxy anchor");
Check(GalaxyCellAddress.FromOrdinal(declaredCellCount) == new GalaxyCellAddress(9, 9, 9),
    "Last top-level cell maps to the maximum coordinate tuple");
var rejectedCellOrdinal = false;
try { _ = GalaxyCellAddress.FromOrdinal(0); }
catch (ArgumentOutOfRangeException) { rejectedCellOrdinal = true; }
Check(rejectedCellOrdinal, "Top-level cell ordinal rejects values below the declared range");
var rejectedCellCoordinate = false;
try { _ = new GalaxyCellAddress(10, 0, 0); }
catch (ArgumentOutOfRangeException) { rejectedCellCoordinate = true; }
Check(rejectedCellCoordinate, "Top-level cell coordinates reject values outside the declared grid");

// A recursive address is a path, not a single top-level ordinal. Each subdivision
// contributes one bounded child coordinate and leaves its parent value unchanged.
var hierarchyRoot = HierarchicalSpatialAddress.At(new GalaxyCellAddress(1, 4, 0));
var firstChild = hierarchyRoot.Child(2, 3, 4);
var grandchild = firstChild.Child(5, 6, 7);
Check(hierarchyRoot.Depth == 0 && firstChild.Depth == 1 && grandchild.Depth == 2,
    "Hierarchical spatial address records each subdivision depth");
Check(firstChild.Root == hierarchyRoot.Root && grandchild.Root == hierarchyRoot.Root,
    "Descendant addresses retain their top-level root cell");
Check(firstChild.Children.Count == 1 &&
      firstChild.Children[0] == new ChildCellCoordinate(2, 3, 4) &&
      grandchild.Children.Count == 2 &&
      grandchild.Children[1] == new ChildCellCoordinate(5, 6, 7),
    "Each hierarchical address retains child coordinates in root-to-leaf order");
Check(grandchild.Equals(hierarchyRoot.Child(2, 3, 4).Child(5, 6, 7)),
    "Equivalent address paths have value equality independent of object identity");
Check(!grandchild.Equals(hierarchyRoot.Child(2, 3, 4).Child(5, 6, 8)) &&
      !grandchild.Equals(firstChild),
    "Different child coordinates or path depths produce distinct addresses");
var rejectedChildCoordinate = false;
try { _ = firstChild.Child(10, 0, 0); }
catch (ArgumentOutOfRangeException) { rejectedChildCoordinate = true; }
Check(rejectedChildCoordinate, "Child coordinates reject values outside the 10×10×10 subdivision");

// SquirrelNoise3 is stateless and must match fixed reference outputs exactly.
// These vectors exercise the operation sequence, seed influence, and uint32 wraparound.
var randomPrimitiveContract = generationRoot.GetProperty("generationIdentity").GetProperty("randomPrimitive");
var referenceVectors = randomPrimitiveContract.GetProperty("referenceVectors").EnumerateArray().ToArray();
Check(referenceVectors.Length >= 6, "SquirrelNoise3 contract includes a useful fixed reference-vector set");
var allRandomVectorsMatch = referenceVectors.All(vector =>
{
    var position = vector.GetProperty("position").GetInt32();
    var seed = vector.GetProperty("seed").GetUInt32();
    var expected = Convert.ToUInt32(vector.GetProperty("expectedUInt32Hex").GetString()![2..], 16);
    return SquirrelNoise3.Hash(position, seed) == expected;
});
Check(allRandomVectorsMatch, "SquirrelNoise3 matches every fixed uint32 reference vector");
Check(SquirrelNoise3.Hash(42, 0) != SquirrelNoise3.Hash(42, 1),
    "SquirrelNoise3 seed changes the deterministic result");
Check(SquirrelNoise3.Hash(42, 0) == SquirrelNoise3.Hash(42, 0),
    "SquirrelNoise3 repeated random-access queries are order-independent");
var generationIdentity = generationRoot.GetProperty("generationIdentity");
var requiredGenerationInputs = generationIdentity.GetProperty("requiredInputs")
    .EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(requiredGenerationInputs.Contains("worldSeed") &&
      requiredGenerationInputs.Contains("generatorVersion") &&
      requiredGenerationInputs.Contains("canonicalSpatialAddress") &&
      requiredGenerationInputs.Contains("featureDomain") &&
      requiredGenerationInputs.Contains("featureOrdinal") &&
      generationIdentity.GetProperty("featureDomainsMustBeIndependent").GetBoolean(),
    "Generated feature identity includes stable semantic address and independent feature domain");
var eventRandomKey = generationRoot.GetProperty("temporalSimulation").GetProperty("eventRandomKey")
    .EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
var forbiddenGenerationInputs = generationRoot.GetProperty("temporalSimulation").GetProperty("mustNotDependOn")
    .EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(eventRandomKey.Contains("logicalTimeKey") &&
      eventRandomKey.Contains("entityOrRegionAddress") &&
      forbiddenGenerationInputs.Contains("camera movement") &&
      forbiddenGenerationInputs.Contains("render frame rate") &&
      forbiddenGenerationInputs.Contains("cell request order"),
    "Event randomness is time/address keyed and independent of camera, frame rate, and request order");
var persistenceContract = generationRoot.GetProperty("persistence");
Check(persistenceContract.GetProperty("regenerate").EnumerateArray()
        .Any(x => x.GetString()!.Contains("immutable seed-derived", StringComparison.OrdinalIgnoreCase)) &&
      persistenceContract.GetProperty("persist").EnumerateArray()
        .Any(x => x.GetString()!.Contains("battles and casualties", StringComparison.OrdinalIgnoreCase)) &&
      persistenceContract.GetProperty("persist").EnumerateArray()
        .Any(x => x.GetString()!.Contains("ownership changes", StringComparison.OrdinalIgnoreCase)),
    "World reconstruction regenerates immutable origins while preserving consequential history");
var reproducibilityRequirements = generationRoot.GetProperty("reproducibilityTests")
    .EnumerateArray().Select(x => x.GetString()!).ToArray();
Check(reproducibilityRequirements.Any(x => x.Contains("request order", StringComparison.OrdinalIgnoreCase)) &&
      reproducibilityRequirements.Any(x => x.Contains("persisted history", StringComparison.OrdinalIgnoreCase)) &&
      reproducibilityRequirements.Any(x => x.Contains("probabilistic event outcomes", StringComparison.OrdinalIgnoreCase)),
    "Machine-readable world contract retains request-order, event-replay, and persistence test obligations");



// Human history remains speculative where canon is open; archive records retain
// provenance and can expose more history than a single mission presents.
using var humanHistoryData = ReadData("human-galactic-history.json");
using var historyDiscoveriesData = ReadData("campaign-history-discoveries.json");
var humanHistoryRoot = humanHistoryData.RootElement;
var historyDiscoveryRoot = historyDiscoveriesData.RootElement;
var historicalEras = humanHistoryRoot.GetProperty("eras").EnumerateArray().ToArray();
Check(historicalEras.Length >= 8, "Human history spans Earth-origin through a campaign-era galactic order");
Check(humanHistoryRoot.GetProperty("authority").GetProperty("eraDatesAreDeliberatelyUnfixed").GetBoolean(),
    "Speculative future chronology does not claim unsupported absolute dates");
Check(historicalEras.Any(e => e.GetProperty("id").GetString() == "history.imperial-fractures"),
    "Long history explicitly includes imperial fractures, rebellion, and civil war");
Check(humanHistoryRoot.GetProperty("campaignDiscoveryPrinciples").GetProperty("playerKnowledgeIsTrackedSeparately").GetBoolean(),
    "Player knowledge remains distinct from historical truth");
Check(historyDiscoveryRoot.GetProperty("recordKinds").EnumerateArray().Select(x => x.GetString())
    .Contains("maintenance-history") &&
      historyDiscoveryRoot.GetProperty("recordKinds").EnumerateArray().Select(x => x.GetString())
    .Contains("political-correspondence"),
    "Archives connect technical evidence with political history");
Check(historyDiscoveryRoot.GetProperty("archiveInteractionContract").GetProperty("recordProvenanceMustNotBeDiscardedDuringSummarization").GetBoolean(),
    "Archive summaries preserve record provenance");
Check(historyDiscoveryRoot.GetProperty("archiveInteractionContract").GetProperty("fullArchiveDoesNotNeedToBeRenderedOrLoadedAtOnce").GetBoolean(),
    "Deep archives can feel vast without loading every record at once");
Check(historyDiscoveryRoot.GetProperty("illustrativeDiscovery").GetProperty("canonStatus").GetString() == "illustrative-not-canon",
    "Example derelict-battle discovery does not silently establish canon");



// Milky Way mapping preserves scientific provenance, time scales, and presentation boundaries.
using var milkyWayData = ReadData("milky-way-reference-model.json");
var milkyWayRoot = milkyWayData.RootElement;
Check(milkyWayRoot.GetProperty("schemaVersion").GetString() == "raWWar.milky-way-reference-model.v1",
    "Milky Way reference model has a versioned schema");
Check(milkyWayRoot.GetProperty("solarSystem").GetProperty("canonicalEntityId").GetString() == "system.sol" &&
      milkyWayRoot.GetProperty("solarSystem").GetProperty("homeworldEntityId").GetString() == "planet.earth",
    "Earth and the Solar System have stable galactic anchor identities");
Check(milkyWayRoot.GetProperty("solarSystem").GetProperty("galacticReference").GetProperty("coordinatesStatus").GetString() == "coarse-cartographic-anchor-not-precision-astrometry",
    "Coarse Solar System placement is not misrepresented as precision astrometry");
Check(milkyWayRoot.GetProperty("catalogueStrategy").GetProperty("recordProvenanceRequired").EnumerateArray()
    .Select(x => x.GetString()).Contains("sourceVersionOrRelease") &&
      milkyWayRoot.GetProperty("catalogueStrategy").GetProperty("recordProvenanceRequired").EnumerateArray()
    .Select(x => x.GetString()).Contains("coordinateFrame"),
    "Astronomical records preserve catalogue version and coordinate-frame provenance");
Check(milkyWayRoot.GetProperty("authority").GetProperty("absenceFromCatalogueDoesNotProveAbsenceOfObject").GetBoolean(),
    "Catalogue incompleteness is not treated as proof that an object does not exist");
Check(milkyWayRoot.GetProperty("proceduralExpansion").GetProperty("mustNotDo").EnumerateArray()
    .Any(x => x.GetString()!.Contains("label generated stars or planets as observed real objects", StringComparison.Ordinal)),
    "Procedural populations cannot be mislabeled as observed astronomy");
Check(milkyWayRoot.GetProperty("motionAndTime").GetProperty("millenniaScaleRule").GetString()!.Contains("0.016 degrees", StringComparison.Ordinal),
    "Galactic motion across millennia is scale-checked rather than artificially accelerated");
Check(milkyWayRoot.GetProperty("representationContract").GetProperty("authoritativeGeometry").GetString()!.Contains("remain unchanged", StringComparison.Ordinal),
    "Navigation presentation cannot rewrite authoritative geometry");
Check(milkyWayRoot.GetProperty("representationContract").GetProperty("navigationHorizon").GetProperty("visibilityModes").EnumerateArray()
    .Select(x => x.GetString()).Contains("optically-visible") &&
      milkyWayRoot.GetProperty("representationContract").GetProperty("navigationHorizon").GetProperty("visibilityModes").EnumerateArray()
    .Select(x => x.GetString()).Contains("catalogue-known"),
    "Navigation distinguishes visible objects from catalogue-known objects");

// Navigation charts evolve with civilization, but never rewrite authoritative space.
using var navigationFramesData = ReadData("navigation-reference-frames.json");
var navigationFramesRoot = navigationFramesData.RootElement;
Check(navigationFramesRoot.GetProperty("schemaVersion").GetString() == "raWWar.navigation-reference-frames.v1",
    "Navigation frame contract is explicitly versioned");
Check(navigationFramesRoot.GetProperty("authority").GetProperty("chartFrameIsNotWorldIdentity").GetBoolean() &&
      navigationFramesRoot.GetProperty("coreDistinction").GetProperty("shipNavigationSolution").GetString()!.Contains("instruments", StringComparison.Ordinal),
    "Chart coordinates remain distinct from authoritative world identity and ship navigation estimates");
var candidateFrames = navigationFramesRoot.GetProperty("frameHierarchy").GetProperty("candidateFrames")
    .EnumerateArray().Select(x => x.GetProperty("id").GetString()!).ToHashSet(StringComparer.Ordinal);
Check(candidateFrames.Contains("frame.earth-local") && candidateFrames.Contains("frame.solar-system") &&
      candidateFrames.Contains("frame.local-stellar") && candidateFrames.Contains("frame.galactocentric"),
    "Navigation contract covers Earth-origin through galactocentric reference frames");
var transformFields = navigationFramesRoot.GetProperty("frameHierarchy").GetProperty("requiredTransformFields")
    .EnumerateArray().Select(x => x.GetString()!).ToHashSet(StringComparer.Ordinal);
Check(transformFields.Contains("sourceFrameId") && transformFields.Contains("targetFrameId") &&
      transformFields.Contains("validAtOrEpoch") && transformFields.Contains("uncertaintyModel"),
    "Coordinate transformations declare endpoints, epoch, and uncertainty");
var navigationPhases = navigationFramesRoot.GetProperty("historicalNavigationEras").GetProperty("phases").EnumerateArray().ToArray();
Check(navigationPhases.Any(x => x.GetProperty("id").GetString() == "nav-era.earth-origin") &&
      navigationPhases.Any(x => x.GetProperty("id").GetString() == "nav-era.galactocentric"),
    "Historical navigation model explicitly spans Earth-origin and galactocentric charting");
var chartFields = navigationFramesRoot.GetProperty("chartRecordContract").GetProperty("requiredFields")
    .EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(chartFields.Contains("coordinateEpoch") && chartFields.Contains("accuracyAndUncertainty"),
    "Chart editions preserve epoch, provenance, and uncertainty");
Check(navigationFramesRoot.GetProperty("historyAndGameplay").GetProperty("notAutomatic").EnumerateArray()
    .Any(x => x.GetString()!.Contains("does not create new observations", StringComparison.Ordinal)),
    "A new coordinate standard cannot grant sensors or observations");
var persistentNavArtifacts = navigationFramesRoot.GetProperty("historyAndGameplay").GetProperty("persistentArtifacts")
    .EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(persistentNavArtifacts.Contains("superseded charts") && persistentNavArtifacts.Contains("navigation failures"),
    "Superseded charts and navigation failures remain part of persistent history");
Check(navigationFramesRoot.GetProperty("validationGates").EnumerateArray()
    .Any(x => x.GetString()!.Contains("different polities can adopt or retain different standards", StringComparison.Ordinal)),
    "Civilizations can retain competing navigation standards without breaking shared world identity");



// Contested history keeps events, accounts, evidence, interpretations, and public narratives distinct.
using var contestedHistoryData = ReadData("contested-history-and-betrayal.json");
var contestedHistoryRoot = contestedHistoryData.RootElement;
Check(contestedHistoryRoot.GetProperty("schemaVersion").GetString() == "raWWar.contested-history-and-betrayal.v1",
    "Contested history and betrayal use a versioned machine-readable contract");
Check(contestedHistoryRoot.GetProperty("authority").GetProperty("victorNarrativeIsNotAutomaticallyTrue").GetBoolean() &&
      contestedHistoryRoot.GetProperty("authority").GetProperty("defeatedNarrativeIsNotAutomaticallyTrue").GetBoolean(),
    "Neither victory nor defeat grants an account automatic truth");
var historyLayers = contestedHistoryRoot.GetProperty("coreModel");
Check(historyLayers.TryGetProperty("event", out _) && historyLayers.TryGetProperty("account", out _) &&
      historyLayers.TryGetProperty("evidence", out _) && historyLayers.TryGetProperty("interpretation", out _) &&
      historyLayers.TryGetProperty("publicNarrative", out _) && historyLayers.TryGetProperty("playerUnderstanding", out _),
    "Historical event, account, evidence, interpretation, public narrative, and player understanding are separate");
var accountKinds = contestedHistoryRoot.GetProperty("accountKinds").EnumerateArray().Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(accountKinds.Contains("official-victory-chronicle") && accountKinds.Contains("defeated-side-testimony") &&
      accountKinds.Contains("forged-document") && accountKinds.Contains("later-historical-synthesis"),
    "History supports victor accounts, defeated testimony, forgery, and later reinterpretation");
var evidenceRules = contestedHistoryRoot.GetProperty("evidenceRules").EnumerateArray().Select(x => x.GetString()!).ToArray();
Check(evidenceRules.Any(x => x.Contains("not independent corroboration", StringComparison.Ordinal)) &&
      evidenceRules.Any(x => x.Contains("does not automatically prove every claim", StringComparison.Ordinal)),
    "Evidence contract guards against copied sources and overclaiming from authentic records");
var betrayal = contestedHistoryRoot.GetProperty("betrayalModel");
Check(betrayal.GetProperty("eventFields").EnumerateArray().Select(x => x.GetString()).Contains("relationshipOrObligationRef") &&
      betrayal.GetProperty("eventFields").EnumerateArray().Select(x => x.GetString()).Contains("actorKnowledgeAtDecision") &&
      betrayal.GetProperty("rules").EnumerateArray().Any(x => x.GetString()!.Contains("motive", StringComparison.Ordinal)),
    "Betrayal records a relationship, actor knowledge, and disputed motive rather than a surprise label");
Check(contestedHistoryRoot.GetProperty("battleNarratives").GetProperty("accountVariantsMayDifferOn").EnumerateArray()
    .Select(x => x.GetString()).Contains("who fired first") &&
      contestedHistoryRoot.GetProperty("battleNarratives").GetProperty("accountVariantsMayDifferOn").EnumerateArray()
    .Select(x => x.GetString()).Contains("who abandoned whom"),
    "One battle can have competing accounts about initiation, orders, and betrayal");
Check(contestedHistoryRoot.GetProperty("archiveIntegration").GetProperty("preserveOriginals").GetBoolean() &&
      contestedHistoryRoot.GetProperty("archiveIntegration").GetProperty("contradictoryRecordsAreFirstClass").GetBoolean(),
    "Original evidence and contradictory records remain first-class archive objects");
Check(contestedHistoryRoot.GetProperty("validationGates").EnumerateArray()
    .Any(x => x.GetString()!.Contains("victor's official account receives no automatic truth bonus", StringComparison.Ordinal)),
    "Validation explicitly rejects victor-biased truth scoring");

// A mech is boarded and operated through physical, damageable controls; GPU performance remains measurable.
using var mechData = ReadData("mech-and-exotic-platforms.json");
var mechRoot = mechData.RootElement;
Check(mechRoot.GetProperty("schema").GetString() == "raWWar.mech-and-exotic-platforms.v1",
    "Mech and exotic platform contract is versioned");
Check(mechRoot.GetProperty("authority").GetProperty("battleMechsAreAnExplicitDesiredExperience").GetBoolean(),
    "Pilotable battle mechs are an explicit desired experience");
var pilotExperience = mechRoot.GetProperty("pilotExperience");
Check(pilotExperience.GetProperty("boardingSequence").EnumerateArray().Select(x => x.GetString())
    .Contains("secure-seat-and-restraints") &&
      pilotExperience.GetProperty("boardingSequence").EnumerateArray().Select(x => x.GetString())
    .Contains("run-self-test"),
    "Mech operation includes physical boarding, restraint, and system checks");
Check(pilotExperience.GetProperty("motionInterface").GetProperty("status").GetString() ==
      "candidate-option-not-universal-requirement" &&
      pilotExperience.GetProperty("motionInterface").GetProperty("requiredChecks").EnumerateArray()
      .Select(x => x.GetString()).Contains("latency-and-loss-detection"),
    "Motion capture is an optional control path with calibration and failure handling");
Check(pilotExperience.GetProperty("interfaceSemantics").GetProperty("oneAuthoritativeWorldObject").GetString()!
    .Contains("same semantic action", StringComparison.Ordinal),
    "Desktop and VR controls invoke the same world-level semantic action");
Check(pilotExperience.GetProperty("damageableInterfaces").GetProperty("targets").EnumerateArray()
    .Select(x => x.GetString()).Contains("screen-surface") &&
      pilotExperience.GetProperty("damageableInterfaces").GetProperty("targets").EnumerateArray()
    .Select(x => x.GetString()).Contains("hatch-interlock"),
    "Cockpit displays and mechanisms are separately damageable physical components");
var renderContract = mechRoot.GetProperty("renderingPerformanceContract");
Check(renderContract.GetProperty("measuredLimits").EnumerateArray().Select(x => x.GetString())
    .Contains("GPU memory capacity and fragmentation") &&
      renderContract.GetProperty("measuredLimits").EnumerateArray().Select(x => x.GetString())
    .Contains("pixel/fragment workload and overdraw"),
    "GPU rendering budgets include memory and pixel work rather than assuming unlimited capacity");
Check(renderContract.GetProperty("meshJoiningPolicy").GetProperty("compareStrategies").EnumerateArray()
    .Select(x => x.GetString()).Contains("instanced repeated components") &&
      renderContract.GetProperty("meshJoiningPolicy").GetProperty("compareStrategies").EnumerateArray()
    .Select(x => x.GetString()).Contains("selectively joined static groups"),
    "Mesh joining and instancing are benchmarked as alternatives");
Check(renderContract.GetProperty("validationRules").EnumerateArray().Select(x => x.GetString())
    .Any(x => x!.Contains("VR comfort", StringComparison.Ordinal)),
    "Rendering validation accounts for VR refresh deadlines and comfort");

// Embodied interactions route every input device through physical world objects and semantic actions.
using var embodiedData = ReadData("embodied-interaction-and-control.json");
var embodiedRoot = embodiedData.RootElement;
Check(embodiedRoot.GetProperty("schemaVersion").GetString() == "raWWar.embodied-interaction-and-control.v1",
    "Embodied interaction contract is versioned");
Check(embodiedRoot.GetProperty("authority").GetProperty("worldObjectsRemainAuthoritative").GetBoolean() &&
      embodiedRoot.GetProperty("authority").GetProperty("visualGuidanceDoesNotExecuteAnAction").GetBoolean(),
    "Physical world state is authoritative and breathing guidance never acts for the player");
var actionOutcomes = embodiedRoot.GetProperty("actionContract").GetProperty("outcomes").EnumerateArray()
    .Select(x => x.GetString()).ToHashSet(StringComparer.Ordinal);
Check(actionOutcomes.Contains("blocked-by-interlock") && actionOutcomes.Contains("target-damaged") &&
      actionOutcomes.Contains("interrupted"),
    "Physical interactions report interlocks, damage, and interruption as distinct outcomes");
Check(embodiedRoot.GetProperty("inputMapping").GetProperty("example").GetProperty("actorAnimation").GetString()!
    .Contains("hand", StringComparison.Ordinal) &&
      embodiedRoot.GetProperty("inputMapping").GetProperty("example").GetProperty("semanticAction").GetString()!
    .Contains("throttle", StringComparison.Ordinal),
    "Keyboard input animates a visible pilot hand while operating a semantic physical control");
var loto = embodiedRoot.GetProperty("lockoutTagout");
Check(loto.GetProperty("requiredSteps").EnumerateArray().Select(x => x.GetString()).Contains("verify-zero-energy-state") &&
      loto.GetProperty("requiredSteps").EnumerateArray().Select(x => x.GetString()).Contains("apply-personal-lock") &&
      loto.GetProperty("requiredSteps").EnumerateArray().Select(x => x.GetString()).Contains("restore-energy-in-stages"),
    "Lockout/tagout requires physical isolation, personal locks, verification, and controlled restoration");
Check(loto.GetProperty("noMagicRules").EnumerateArray().Any(x => x.GetString()!.Contains("checkbox does not isolate", StringComparison.Ordinal)),
    "Lockout/tagout cannot be satisfied by a UI checkbox alone");

// The detailed mech contract preserves component identity and treats performance as a measurable hypothesis.
using var detailedMechData = ReadData("manned-mech-platforms.json");
var detailedMechRoot = detailedMechData.RootElement;
Check(detailedMechRoot.GetProperty("schemaVersion").GetString() == "raWWar.manned-mech-and-exotic-environment-platforms.v1",
    "Detailed manned mech contract is versioned");
Check(detailedMechRoot.GetProperty("designAuthority").GetProperty("mechWarriorLikePlayIsAnExplicitExperienceGoal").GetBoolean(),
    "Boarded MechWarrior-like pilot play is explicitly in scope");
var platformIds = detailedMechRoot.GetProperty("platformFamilies").EnumerateArray()
    .Select(x => x.GetProperty("id").GetString()).ToHashSet(StringComparer.Ordinal);
Check(platformIds.Contains("mech.walker.combat") && platformIds.Contains("mech.platform.orbital-work") &&
      platformIds.Contains("mech.platform.environmental"),
    "Platform families cover combat walkers, orbital work, and environmental research");
Check(detailedMechRoot.GetProperty("componentHierarchy").GetProperty("componentsMustHave").EnumerateArray()
    .Select(x => x.GetString()).Contains("failureModes") &&
      detailedMechRoot.GetProperty("componentHierarchy").GetProperty("componentsMustHave").EnumerateArray()
    .Select(x => x.GetString()).Contains("inspectionAndMaintenanceProcedure"),
    "Mech components carry failure and maintenance identity rather than one generic health pool");
Check(detailedMechRoot.GetProperty("performanceAndRendering").GetProperty("falsifiableHypotheses").EnumerateArray()
    .Any(x => x.GetString()!.Contains("reducing render resolution", StringComparison.Ordinal)) &&
      detailedMechRoot.GetProperty("performanceAndRendering").GetProperty("falsifiableHypotheses").EnumerateArray()
    .Any(x => x.GetString()!.Contains("simulation dominates", StringComparison.Ordinal)),
    "Renderer performance hypotheses distinguish pixel cost from simulation cost");
Check(detailedMechRoot.GetProperty("performanceAndRendering").GetProperty("batchingRules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("independently damaged", StringComparison.Ordinal)) &&
      detailedMechRoot.GetProperty("performanceAndRendering").GetProperty("noUnlimitedClaim").GetString()!
    .Contains("bandwidth", StringComparison.Ordinal),
    "Mesh batching preserves independently damaged components and acknowledges hardware ceilings");


// Engineering watch rounds, measurement quality, threshold decisions, maintenance, and LOTO form one auditable workflow.
using var engineeringOpsData = ReadData("engineering-watch-and-maintenance.json");
var engineeringOps = engineeringOpsData.RootElement;
Check(engineeringOps.GetProperty("schemaVersion").GetString() == "raWWar.engineering-watch-and-maintenance.v1",
    "Engineering watch and maintenance contract is versioned");
var loggerContract = engineeringOps.GetProperty("loggerInteraction");
Check(loggerContract.GetProperty("recordFields").EnumerateArray().Select(x => x.GetString())
    .Contains("calibrationState") &&
      loggerContract.GetProperty("recordFields").EnumerateArray().Select(x => x.GetString())
    .Contains("rawRecordRef"),
    "Engineering logger records calibration and preserves raw reading provenance");
Check(loggerContract.GetProperty("rules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("does not automatically decide", StringComparison.Ordinal)),
    "Instrument capture remains separate from engineering judgment");
var thresholdBands = engineeringOps.GetProperty("thresholdAssessment").GetProperty("bands")
    .EnumerateArray().Select(x => x.GetProperty("id").GetString()).ToHashSet(StringComparer.Ordinal);
Check(thresholdBands.Contains("condition.watch") && thresholdBands.Contains("condition.limit-approach") &&
      thresholdBands.Contains("condition.limit-exceeded") && thresholdBands.Contains("condition.unknown"),
    "Engineering assessment distinguishes trends, limit approach, exceedance, and unknown condition");
Check(engineeringOps.GetProperty("maintenanceDecision").GetProperty("lifecycle").EnumerateArray()
    .Select(x => x.GetString()).Contains("isolated-and-verified") &&
      engineeringOps.GetProperty("maintenanceDecision").GetProperty("lifecycle").EnumerateArray()
    .Select(x => x.GetString()).Contains("returned-to-service"),
    "Maintenance lifecycle requires verified isolation and a distinct return-to-service state");
var detailedLoto = engineeringOps.GetProperty("lockoutTagout");
Check(detailedLoto.GetProperty("procedureStages").EnumerateArray().Count() >= 7 &&
      detailedLoto.GetProperty("blockingConditions").EnumerateArray().Select(x => x.GetString())
    .Contains("zero-energy-test-failed"),
    "LOTO is a staged procedure with explicit physical blockers");
Check(detailedLoto.GetProperty("deliberateFriction").EnumerateArray()
    .Any(x => x.GetString()!.Contains("shift change", StringComparison.Ordinal)) &&
      detailedLoto.GetProperty("deliberateFriction").EnumerateArray()
    .Any(x => x.GetString()!.Contains("stored pressure", StringComparison.OrdinalIgnoreCase)),
    "LOTO friction comes from custody, shift handover, and stored energy rather than random timers");
Check(detailedLoto.GetProperty("noMagicRules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("checkbox", StringComparison.Ordinal)) &&
      detailedLoto.GetProperty("noMagicRules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("suitable instrument", StringComparison.Ordinal)),
    "LOTO requires instrument-backed verification rather than a UI flag");
Check(engineeringOps.GetProperty("eventAndHistory").GetProperty("everyProcedureStepProducesAuditableEvent").GetBoolean() &&
      engineeringOps.GetProperty("eventAndHistory").GetProperty("preserveFailedAttempts").GetBoolean(),
    "Failed engineering and LOTO attempts remain part of persistent audit history");


// Composition must follow the current FSM_COS manifest boundary and keep generic capabilities
// explicitly owned by reusable Workshop MicroBundles rather than by the raWWar Experience.
using var workshopCompositionData = ReadData("workshop-composition-contract.json");
var workshopComposition = workshopCompositionData.RootElement;
Check(workshopComposition.GetProperty("schemaVersion").GetString() == "raWWar.workshop-composition-contract.v1",
    "Workshop composition contract is versioned");
Check(workshopComposition.GetProperty("currentFsmCosContract").GetProperty("configurationBoundary").GetString() ==
      "separate IMicroBundleConfigurationSource" &&
      workshopComposition.GetProperty("currentFsmCosContract").GetProperty("hostHandoff").GetString() == "RuntimeAssembly",
    "raWWar aligns with FSM_COS configuration and assembly boundaries");
Check(workshopComposition.GetProperty("currentFsmCosContract").GetProperty("kernelExpansionStatus").GetString() ==
      "paused-after-correctness-gate" &&
      !workshopComposition.GetProperty("currentFsmCosContract").GetProperty("nugetPublishAuthorized").GetBoolean(),
    "raWWar does not require FSM_COS scope expansion or authorize publishing");
Check(workshopComposition.GetProperty("reusableCapabilities").EnumerateArray()
    .Where(x => x.GetProperty("logicalId").GetString() == "capability.physical-interaction")
    .All(x => x.GetProperty("status").GetString() == "proposed-extraction" &&
              x.GetProperty("mustNotDependOn").EnumerateArray().Select(y => y.GetString()).Contains("raWWar")),
    "Generic physical interaction is assigned to a reusable capability, not a raWWar dependency");
Check(workshopComposition.GetProperty("manifestRules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("does not embed configuration bytes", StringComparison.Ordinal)) &&
      workshopComposition.GetProperty("manifestRules").EnumerateArray()
    .Any(x => x.GetString()!.Contains("not reported as integrated", StringComparison.Ordinal)),
    "Manifest configuration and truthful integration-status rules are explicit");
using var runtimeManifestData = System.Text.Json.JsonDocument.Parse(
    File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "runtime-manifest.json")));
var runtimeManifest = runtimeManifestData.RootElement;
Check(runtimeManifest.TryGetProperty("runtimeId", out _) &&
      runtimeManifest.GetProperty("bundles").EnumerateArray()
        .All(x => x.TryGetProperty("bundleId", out _) && x.TryGetProperty("version", out _) &&
                  !x.TryGetProperty("configurationBase64", out _)),
    "raWWar runtime manifest matches FSM_COS root ID/version entries without embedded configuration");
using var experienceManifestData = System.Text.Json.JsonDocument.Parse(
    File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "manifest.json")));
Check(experienceManifestData.RootElement.GetProperty("runtimeManifestPath").GetString() == "runtime-manifest.json",
    "Experience authoring metadata points to the separate machine-oriented runtime manifest");

// Compose the actual runtime-manifest request through the same published FSM_COS
// alpha package currently used by AnyApp. This is an in-memory local catalog proof,
// not a repository-backed artifact download or an Experience execution loop.
var runtimeIdForComposition = runtimeManifest.GetProperty("runtimeId").GetUInt64();
var rootBundle = new RaWWarMicroBundle();
var declaredBundleEntries = runtimeManifest.GetProperty("bundles").EnumerateArray().ToArray();
Check(declaredBundleEntries.All(entry =>
        string.Equals(entry.GetProperty("version").GetString(), rootBundle.Descriptor.Version, StringComparison.Ordinal)),
    "The checked-in runtime manifest requests the exact version declared by the raWWar root bundle");
var runtimeRootsForComposition = declaredBundleEntries
    .Select(entry => MicroBundleDependencyRequest.Unconfigured(entry.GetProperty("bundleId").GetUInt64()))
    .ToArray();
try
{
    // AnyApp currently consumes FSM_COS 0.1.0-alpha.5, whose RuntimeManifest roots
    // are MicroBundleDependencyRequests. The checked-in manifest's version is
    // validated above; host artifact-version resolution remains a separate contract.
    var composition = new FsmCos(new SingleBundleCatalog(rootBundle))
        .Execute(new RuntimeManifest(runtimeIdForComposition, runtimeRootsForComposition));
    Check(composition.RuntimeId == runtimeIdForComposition,
        "FSM_COS assembly preserves the runtime identity requested by raWWar's runtime manifest");
    Check(composition.TryGetBundle<RaWWarMicroBundle>(RaWWarMicroBundle.BundleId, out var loadedRoot) &&
          loadedRoot is not null,
        "FSM_COS resolves and loads the actual raWWar Experience MicroBundle");
    Check(composition.Bundles.Count == 1 &&
          composition.Bundles[0].Id == RaWWarMicroBundle.BundleId,
        "The current manifest composes exactly its declared root without inventing undeclared capabilities");
}
catch (Exception exception)
{
    Check(false, $"Runtime manifest composes through the current AnyApp FSM_COS package: {exception.Message}");
}

// Fighter-station slice: the station secures the occupant, connects the interface, then
// requires the pilot to raise the physical rig before any control request can be accepted.
var pilotAxes = new TheSingularityWorkshop.raWWar.Interaction.PilotControlAxes(
    Pitch: 0.25, Roll: -0.5, Yaw: 0.1, Throttle: 0.7);
var emptyPilotStation = TheSingularityWorkshop.raWWar.Interaction.FighterPilotStation.Create(9001);
var emptyControlAttempt = emptyPilotStation.TryApplyControl(
    42, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop, pilotAxes);
Check(!emptyControlAttempt.Accepted &&
      emptyControlAttempt.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.NoOccupant,
    "An empty fighter station cannot accept pilot input");

var seatedPilot = emptyPilotStation.TrySeat(42, fighterPilotQualified: true);
Check(seatedPilot.Accepted && seatedPilot.After.OccupantId == 42 &&
      emptyPilotStation.OccupantId is null,
    "Seating returns a new station state without mutating the prior state");
var duplicateSeat = seatedPilot.After.TrySeat(43, fighterPilotQualified: true);
Check(!duplicateSeat.Accepted &&
      duplicateSeat.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.AlreadyOccupied &&
      duplicateSeat.After == duplicateSeat.Before,
    "An occupied station rejects a second occupant without changing state");

var beforeSecure = seatedPilot.After.TryApplyControl(
    42, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop, pilotAxes);
Check(!beforeSecure.Accepted &&
      beforeSecure.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.RestraintsNotSecured,
    "Pilot controls remain unavailable until the station secures the occupant");

var securedPilot = seatedPilot.After.TrySecureOccupant();
var rigBeforeConnection = securedPilot.After.TryRaiseControlRig();
Check(!rigBeforeConnection.Accepted &&
      rigBeforeConnection.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.InterfaceNotConnected,
    "The physical rig cannot raise before the pilot interface connects");
var connectedPilot = securedPilot.After.TryConnectInterface();
var inputBeforeRigRaised = connectedPilot.After.TryApplyControl(
    42, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop, pilotAxes);
Check(!inputBeforeRigRaised.Accepted &&
      inputBeforeRigRaised.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.RigNotRaised,
    "A connected interface cannot accept flight input before the pilot raises the rig");
var raisedPilot = connectedPilot.After.TryRaiseControlRig();
Check(securedPilot.Accepted && connectedPilot.Accepted && raisedPilot.Accepted &&
      raisedPilot.After.ControlsReady,
    "The station must secure, connect, and raise the control rig before becoming ready");

var desktopPilotInput = raisedPilot.After.TryApplyControl(
    42, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop, pilotAxes);
var vrPilotInput = raisedPilot.After.TryApplyControl(
    42, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.VirtualReality, pilotAxes);
Check(desktopPilotInput.Accepted && vrPilotInput.Accepted &&
      desktopPilotInput.Request is not null && vrPilotInput.Request is not null &&
      desktopPilotInput.Request.Axes == vrPilotInput.Request.Axes &&
      desktopPilotInput.Request.Axes == pilotAxes &&
      desktopPilotInput.Request.ActorId == vrPilotInput.Request.ActorId &&
      desktopPilotInput.Request.StationId == vrPilotInput.Request.StationId,
    "Desktop and VR inputs produce the same bounded authoritative control intent");
var wrongPilotInput = raisedPilot.After.TryApplyControl(
    99, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Controller, pilotAxes);
Check(!wrongPilotInput.Accepted &&
      wrongPilotInput.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.WrongOccupant,
    "Only the station's current occupant may command its controls");

var unqualifiedStation = TheSingularityWorkshop.raWWar.Interaction.FighterPilotStation.Create(9002);
var unqualifiedSeated = unqualifiedStation.TrySeat(77, fighterPilotQualified: false).After;
var unqualifiedSecured = unqualifiedSeated.TrySecureOccupant().After;
var unqualifiedConnected = unqualifiedSecured.TryConnectInterface().After;
var unqualifiedRaised = unqualifiedConnected.TryRaiseControlRig().After;
var unqualifiedInput = unqualifiedRaised.TryApplyControl(
    77, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Controller, pilotAxes);
Check(!unqualifiedInput.Accepted &&
      unqualifiedInput.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.QualificationRequired,
    "Physical occupancy does not grant fighter-pilot qualification");

var unpoweredStation = TheSingularityWorkshop.raWWar.Interaction.FighterPilotStation.Create(9003, powered: false);
var unpoweredReady = unpoweredStation.TrySeat(88, fighterPilotQualified: true).After
    .TrySecureOccupant().After
    .TryConnectInterface().After
    .TryRaiseControlRig().After;
var unpoweredInput = unpoweredReady.TryApplyControl(
    88, TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop, pilotAxes);
Check(!unpoweredInput.Accepted &&
      unpoweredInput.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.StationUnpowered,
    "A raised rig cannot command an unpowered station");

var invalidAxes = raisedPilot.After.TryApplyControl(
    42,
    TheSingularityWorkshop.raWWar.Interaction.PilotInputSource.Desktop,
    new TheSingularityWorkshop.raWWar.Interaction.PilotControlAxes(double.NaN, 0, 0, 0.5));
Check(!invalidAxes.Accepted &&
      invalidAxes.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.InvalidControlInput,
    "Non-finite control axes are rejected rather than entering the flight-control pipeline");
var invalidInputSource = raisedPilot.After.TryApplyControl(
    42,
    (TheSingularityWorkshop.raWWar.Interaction.PilotInputSource)999,
    pilotAxes);
Check(!invalidInputSource.Accepted &&
      invalidInputSource.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.InvalidControlInput,
    "Unsupported input-source values are rejected");

var emergencyRelease = unpoweredReady.TryEmergencyRelease();
Check(emergencyRelease.Accepted &&
      emergencyRelease.After.OccupantId is null &&
      !emergencyRelease.After.RestraintsSecured &&
      !emergencyRelease.After.InterfaceConnected &&
      !emergencyRelease.After.ControlRigRaised,
    "Emergency egress releases the occupant and rig without depending on station power");

var damagedStation = TheSingularityWorkshop.raWWar.Interaction.FighterPilotStation.Create(9004, controlsIntact: false);
var damagedSeated = damagedStation.TrySeat(101, fighterPilotQualified: true).After;
var damagedSecured = damagedSeated.TrySecureOccupant().After;
var damagedConnection = damagedSecured.TryConnectInterface();
Check(!damagedConnection.Accepted &&
      damagedConnection.BlockReason == TheSingularityWorkshop.raWWar.Interaction.PilotStationBlockReason.ControlsDamaged,
    "Damaged station controls block interface connection explicitly");

CandidateFileEventJournalChecks.Run(Check);

Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");
foreach (var failure in failures) Console.Error.WriteLine($"FAIL: {failure}");
return failures.Count == 0 ? 0 : 1;
