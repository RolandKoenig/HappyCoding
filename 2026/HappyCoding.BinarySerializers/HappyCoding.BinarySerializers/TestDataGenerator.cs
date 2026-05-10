using HappyCoding.BinarySerializers.Model;

namespace HappyCoding.BinarySerializers;

public class TestDataGenerator
{
    public static ExportData CreateTestData(int rowCount)
    {
        var rows = new List<ExportDataRow>(capacity: rowCount);

        var categories = new[] { "Development", "Consulting", "Support", "Documentation" };
        var topics = new[] { "Backend", "Frontend", "DevOps", "Architecture" };
        var dayTypes = new[] { "Workday", "Weekend", "Holiday" };
        var lineTypes = new[] { "Billable", "Internal", "Meeting" };

        for (var loop = 0; loop < rowCount; loop++)
        {
            rows.Add(new ExportDataRow(
                Kategorie: categories[loop % categories.Length],
                Thema: topics[loop % topics.Length],
                Datum: DateOnly.FromDateTime(DateTime.Today.AddDays(-loop % 365)).ToString("yyyy-MM-dd"),
                TagTyp: dayTypes[loop % dayTypes.Length],
                ZeilenTyp: lineTypes[loop % lineTypes.Length],
                Zeitaufwand: 0.5 + (loop % 8) * 0.5,
                Abgerechnet: 0.5 + (loop % 8) * 0.5,
                BillingMultiplier: 1.0 + (loop % 3) * 0.25,
                Kommentar: $"Generated test row {loop} with some text to simulate a realistic description line"));
        }

        return new ExportData(
            Version: "1.0",
            ExportZeitstempel: DateTime.UtcNow,
            Rows: rows);
    }
}