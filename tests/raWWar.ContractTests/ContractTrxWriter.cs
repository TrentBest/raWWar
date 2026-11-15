using System.Security.Cryptography;
using System.Xml.Linq;

namespace TheSingularityWorkshop.raWWar.ContractTests;

internal sealed record ContractCheckResult(
    string Name,
    string DisplayName,
    bool Passed,
    string? Diagnostic);

internal static class ContractTrxWriter
{
    private static readonly XNamespace Trx = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
    private static readonly Guid UnitTestType = new("13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b");
    private static readonly Guid AllTestsList = new("8c7b7a5d-3c2b-4f8a-9a1d-4c6d8e0f2a10");

    public static void Write(string path, IReadOnlyList<ContractCheckResult> results)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(results);
        if (results.Count == 0)
            throw new InvalidOperationException("Refusing to emit an empty contract-test TRX file.");

        var duplicate = results.GroupBy(result => result.Name, StringComparer.Ordinal)
            .FirstOrDefault(group => group.Count() != 1);
        if (duplicate is not null)
            throw new InvalidOperationException($"Duplicate contract check name: {duplicate.Key}");

        var started = DateTime.UtcNow;
        var finished = DateTime.UtcNow;
        var runId = Guid.NewGuid();
        var settingsId = Guid.NewGuid();
        var executionIds = results.ToDictionary(result => result.Name, _ => Guid.NewGuid(), StringComparer.Ordinal);
        var testIds = results.ToDictionary(result => result.Name, result => CreateStableId(result.Name), StringComparer.Ordinal);
        var passed = results.Count(result => result.Passed);
        var failed = results.Count - passed;
        var runName = $"raWWar contract checks {runId:D}";
        var computer = Environment.MachineName;

        var root = new XElement(Trx + "TestRun",
            new XAttribute("id", runId),
            new XAttribute("name", runName),
            new XAttribute("runUser", Environment.UserName),
            new XElement(Trx + "Times",
                new XAttribute("creation", started.ToString("o")),
                new XAttribute("queuing", started.ToString("o")),
                new XAttribute("start", started.ToString("o")),
                new XAttribute("finish", finished.ToString("o"))),
            new XElement(Trx + "TestSettings",
                new XAttribute("name", "raWWar contract checks"),
                new XAttribute("id", settingsId),
                new XElement(Trx + "Deployment")),
            new XElement(Trx + "Results",
                results.Select(result =>
                {
                    var outcome = result.Passed ? "Passed" : "Failed";
                    var unitResult = new XElement(Trx + "UnitTestResult",
                        new XAttribute("executionId", executionIds[result.Name]),
                        new XAttribute("testId", testIds[result.Name]),
                        new XAttribute("testName", GetTestName(result)),
                        new XAttribute("computerName", computer),
                        new XAttribute("duration", "00:00:00"),
                        new XAttribute("startTime", started.ToString("o")),
                        new XAttribute("endTime", finished.ToString("o")),
                        new XAttribute("testType", UnitTestType),
                        new XAttribute("outcome", outcome),
                        new XAttribute("testListId", AllTestsList),
                        new XAttribute("relativeResultsDirectory", executionIds[result.Name].ToString("N")));
                    unitResult.Add(new XElement(Trx + "Output",
                        new XElement(Trx + "StdOut", result.DisplayName)));
                    if (!result.Passed)
                        unitResult.Element(Trx + "Output")!.Add(
                            new XElement(Trx + "ErrorInfo",
                                new XElement(Trx + "Message", result.Diagnostic ?? result.DisplayName)));
                    return unitResult;
                })),
            new XElement(Trx + "TestDefinitions",
                results.Select(result =>
                {
                    var executionId = executionIds[result.Name];
                    return new XElement(Trx + "UnitTest",
                        new XAttribute("name", GetTestName(result)),
                        new XAttribute("storage", "raWWar.ContractTests.dll"),
                        new XAttribute("id", testIds[result.Name]),
                        new XElement(Trx + "Execution", new XAttribute("id", executionId)),
                        new XElement(Trx + "TestMethod",
                            new XAttribute("codeBase", "raWWar.ContractTests.dll"),
                            new XAttribute("adapterTypeName", "raWWar executable contract harness"),
                            new XAttribute("className", "TheSingularityWorkshop.raWWar.ContractTests.Program"),
                            new XAttribute("name", result.Name)));
                })),
            new XElement(Trx + "TestEntries",
                results.Select(result =>
                    new XElement(Trx + "TestEntry",
                        new XAttribute("testId", testIds[result.Name]),
                        new XAttribute("executionId", executionIds[result.Name]),
                        new XAttribute("testListId", AllTestsList)))),
            new XElement(Trx + "TestLists",
                new XElement(Trx + "TestList",
                    new XAttribute("name", "All Loaded Results"),
                    new XAttribute("id", AllTestsList))),
            new XElement(Trx + "ResultSummary",
                new XAttribute("outcome", failed == 0 ? "Completed" : "Failed"),
                new XElement(Trx + "Counters",
                    new XAttribute("total", results.Count),
                    new XAttribute("executed", results.Count),
                    new XAttribute("passed", passed),
                    new XAttribute("failed", failed),
                    new XAttribute("error", 0),
                    new XAttribute("timeout", 0),
                    new XAttribute("aborted", 0),
                    new XAttribute("inconclusive", 0),
                    new XAttribute("passedButRunAborted", 0),
                    new XAttribute("notRunnable", 0),
                    new XAttribute("notExecuted", 0),
                    new XAttribute("disconnected", 0),
                    new XAttribute("warning", 0),
                    new XAttribute("completed", results.Count),
                    new XAttribute("inProgress", 0),
                    new XAttribute("pending", 0)))));

        var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        var fullPath = Path.GetFullPath(path);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        var temporaryPath = fullPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            document.Save(temporaryPath);
            File.Move(temporaryPath, fullPath, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath))
                File.Delete(temporaryPath);
        }
    }

    private static string GetTestName(ContractCheckResult result) => $"{result.Name}: {result.DisplayName}";

    private static Guid CreateStableId(string name)
    {
        var hash = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(name));
        return new Guid(hash.AsSpan(0, 16));
    }
}
