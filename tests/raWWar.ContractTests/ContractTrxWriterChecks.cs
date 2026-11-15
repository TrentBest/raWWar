using System.Xml.Linq;

namespace TheSingularityWorkshop.raWWar.ContractTests;

internal static class ContractTrxWriterChecks
{
    public static void Run(Action<bool, string> check)
    {
        var path = Path.Combine(Path.GetTempPath(), $"raWWar-trx-writer-{Guid.NewGuid():N}.trx");
        try
        {
            // This deliberately synthetic failure exists only in a temporary writer self-test.
            // It is never added to the real contract results or uploaded as provider evidence.
            var fixture = new[]
            {
                new ContractCheckResult("WriterFixturePass", "Synthetic writer pass fixture", true, null),
                new ContractCheckResult("WriterFixtureFail", "Synthetic writer failure fixture", false,
                    "Controlled failure used only to verify TRX failure serialization.")
            };

            ContractTrxWriter.Write(path, fixture);
            var document = XDocument.Load(path);
            XNamespace trx = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            var results = document.Root?.Element(trx + "Results")?.Elements(trx + "UnitTestResult").ToArray()
                ?? Array.Empty<XElement>();
            var counters = document.Root?.Element(trx + "ResultSummary")?.Element(trx + "Counters");
            var outcomes = results.Select(result => (string?)result.Attribute("outcome")).ToArray();
            var uniqueIds = results.Select(result => (string?)result.Attribute("testId"))
                .Where(value => value is not null).Distinct(StringComparer.Ordinal).Count();
            var failureMessage = results.SingleOrDefault(result =>
                    (string?)result.Attribute("outcome") == "Failed")
                ?.Element(trx + "Output")?.Element(trx + "ErrorInfo")?.Element(trx + "Message")?.Value;

            check(results.Length == 2, "TRX writer emits one result for each supplied fixture");
            check(uniqueIds == 2, "TRX writer assigns unique test identities");
            check(outcomes.Count(outcome => outcome == "Passed") == 1
                && outcomes.Count(outcome => outcome == "Failed") == 1,
                "TRX writer preserves both passing and controlled failing outcomes");
            check(counters is not null
                && (string?)counters.Attribute("total") == "2"
                && (string?)counters.Attribute("passed") == "1"
                && (string?)counters.Attribute("failed") == "1",
                "TRX writer summary counters match the serialized result outcomes");
            check(failureMessage == "Controlled failure used only to verify TRX failure serialization.",
                "TRX writer preserves the controlled failure diagnostic");
        }
        catch (Exception exception)
        {
            check(false, $"TRX writer self-test could not complete: {exception.Message}");
        }
        finally
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
