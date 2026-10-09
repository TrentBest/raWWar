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
Check(humanHistoryRoot.GetProperty("discoveryPrinciples").GetProperty("playerKnowledgeIsTrackedSeparately").GetBoolean(),
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

Console.WriteLine($"raWWar spatiotemporal contract checks: {checks - failures.Count}/{checks} passed");
foreach (var failure in failures) Console.Error.WriteLine($"FAIL: {failure}");
return failures.Count == 0 ? 0 : 1;
