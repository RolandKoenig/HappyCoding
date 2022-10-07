using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;

public static class TestDataLoader
{
    public static IReadOnlyCollection<TestDataRow> LoadTestData(int randomSeed, int countRows)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var rootNamespace = typeof(Program).Namespace ?? "";
        using var inStream = assembly.GetManifestResourceStream($"{rootNamespace}.Resources.TestData.json");

        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        return JsonSerializer.Deserialize<TestDataRow[]>(inStream!, jsonOptions) ?? Array.Empty<TestDataRow>();
    }
}