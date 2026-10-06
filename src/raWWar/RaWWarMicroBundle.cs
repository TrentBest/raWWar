using TheSingularityWorkshop.MicroBundleDomain;

namespace TheSingularityWorkshop.raWWar;

/// <summary>
/// The initial raWWar Experience payload.
/// The design is deliberately ahead of the implementation: capabilities
/// will be decomposed into MicroBundles as the vertical slice is discovered.
/// </summary>
public sealed class RaWWarMicroBundle : IMicroBundle
{
    public const ulong ExperienceId = 3301;
    public const ulong RuntimeId = 3311;
    public const ulong BundleId = 3301;
    public const string ProviderId = "rawwar";

    public MicroBundleDescriptor Descriptor { get; } =
        new(BundleId, "1.0.0", providers: [new MicroBundleProvider(ProviderId)]);

    public ulong Id => Descriptor.Id;

    public IReadOnlyList<MicroBundleDependencyRequest> Dependencies => [];

    public void Load(IMicroBundleLoadContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        // Capability composition will be added as the raWWar vertical slice is defined.
    }

    public bool Arbitrate(IMicroBundleArbitrationContext context, int roundIndex)
    {
        ArgumentNullException.ThrowIfNull(context);
        return false;
    }
}
