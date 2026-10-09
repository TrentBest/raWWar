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


// Input adapters converge on the same physical control transition, rather than bypassing the world.
var throttleDefinition = new TheSingularityWorkshop.raWWar.Interaction.PhysicalControlDefinition(
    "control.test.throttle", "grip", 0, 1, 0.5, 0.1, true, "move-hand-to-grip");
var initialControl = new TheSingularityWorkshop.raWWar.Interaction.PhysicalControlState(0.5);
var keyboardMove = TheSingularityWorkshop.raWWar.Interaction.PhysicalControlResolver.Apply(
    throttleDefinition, initialControl,
    new("pilot-1", TheSingularityWorkshop.raWWar.Interaction.PhysicalControlAction.Move, 0.2,
        TheSingularityWorkshop.raWWar.Interaction.PhysicalInputSource.Keyboard, 10));
var vrMove = TheSingularityWorkshop.raWWar.Interaction.PhysicalControlResolver.Apply(
    throttleDefinition, initialControl,
    new("pilot-1", TheSingularityWorkshop.raWWar.Interaction.PhysicalControlAction.Move, 0.2,
        TheSingularityWorkshop.raWWar.Interaction.PhysicalInputSource.VrTrackedHand, 10));
Near(keyboardMove.State.Position, vrMove.State.Position, 1e-12,
    "Keyboard and VR resolve to the same physical control state");
Check(keyboardMove.Event.ControlId == vrMove.Event.ControlId &&
      keyboardMove.Event.ResultingPosition == vrMove.Event.ResultingPosition,
    "Different input devices retain one authoritative control identity and outcome");
Check(keyboardMove.ActorMotion == "move-hand-to-grip" && keyboardMove.WorldStateChanged,
    "A successful semantic input requests visible actor motion and changes world state");
var lockedControl = TheSingularityWorkshop.raWWar.Interaction.PhysicalControlResolver.Apply(
    throttleDefinition, initialControl with { InterlockSatisfied = false },
    new("pilot-1", TheSingularityWorkshop.raWWar.Interaction.PhysicalControlAction.Move, 0.2,
        TheSingularityWorkshop.raWWar.Interaction.PhysicalInputSource.Keyboard, 11));
Check(!lockedControl.Event.Accepted && lockedControl.RejectionReason == "interlock-open" &&
      lockedControl.State.Position == initialControl.Position,
    "An open interlock blocks input without moving the actual control");
var damagedControl = TheSingularityWorkshop.raWWar.Interaction.PhysicalControlResolver.Apply(
    throttleDefinition, initialControl with { Integrity = 0 },
    new("pilot-1", TheSingularityWorkshop.raWWar.Interaction.PhysicalControlAction.Move, 0.2,
        TheSingularityWorkshop.raWWar.Interaction.PhysicalInputSource.VrTrackedHand, 12));
Check(!damagedControl.Event.Accepted && damagedControl.RejectionReason == "destroyed",
    "A destroyed physical control rejects input and records the failed attempt");
Check(lockedControl.State.EventCount == 1 && !lockedControl.Event.Accepted,
    "Rejected interactions remain auditable state transitions");
Check(keyboardMove.Event.LogicalTime == 10 &&
      keyboardMove.Event.InputSource == TheSingularityWorkshop.raWWar.Interaction.PhysicalInputSource.Keyboard,
    "Physical interaction events retain logical time and originating input source");



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
    File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "experience-manifest.json")));
Check(experienceManifestData.RootElement.GetProperty("runtimeManifestPath").GetString() == "runtime-manifest.json",
    "Experience authoring metadata points to the separate machine-oriented runtime manifest");

Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");
foreach (var failure in failures) Console.Error.WriteLine($"FAIL: {failure}");
return failures.Count == 0 ? 0 : 1;
