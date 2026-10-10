using TheSingularityWorkshop.FSM_COS;
using TheSingularityWorkshop.MicroBundleDomain;

namespace TheSingularityWorkshop.raWWar.ContractTests;

/// <summary>
/// Minimal local catalog for proving the checked-in raWWar runtime manifest against
/// the FSM_COS package currently used by AnyApp. It deliberately does not simulate
/// repository retrieval or artifact materialization.
/// </summary>
internal sealed class SingleBundleCatalog : IMicroBundleCatalog
{
    private readonly IMicroBundle _bundle;

    public SingleBundleCatalog(IMicroBundle bundle) =>
        _bundle = bundle ?? throw new ArgumentNullException(nameof(bundle));

    public bool TryResolve(ulong bundleId, string version, out IMicroBundle? bundle)
    {
        if (_bundle.Id == bundleId &&
            string.Equals(_bundle.Descriptor.Version, version, StringComparison.Ordinal))
        {
            bundle = _bundle;
            return true;
        }

        bundle = null;
        return false;
    }

    public bool TryResolve(ulong bundleId, out IMicroBundle? bundle)
    {
        if (_bundle.Id == bundleId)
        {
            bundle = _bundle;
            return true;
        }

        bundle = null;
        return false;
    }
}
