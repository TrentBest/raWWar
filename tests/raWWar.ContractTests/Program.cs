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

Check(chassisIds.Count >= 10, "Detailed chassis catalogue includes all current vehicle chassis");
Check(electronicIds.Count >= 20, "Electronic system catalogue includes more than 20 components");
Check(assemblyIds.Count == facilityIds.Count, "Every facility has a construction assembly definition");
Check(installPackageIds.Count == Ids(upgradesData, "upgrades").Length - 1 || installPackageIds.Count >= 10,
    "Physical installation packages are explicitly catalogued");

Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");
foreach (var failure in failures) Console.Error.WriteLine($"FAIL: {failure}");
return failures.Count == 0 ? 0 : 1;
