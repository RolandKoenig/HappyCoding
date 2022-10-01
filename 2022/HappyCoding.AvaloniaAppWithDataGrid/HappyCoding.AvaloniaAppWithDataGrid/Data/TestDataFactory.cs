using System;
using System.Collections.Generic;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;

public static class TestDataFactory
{
    public static IEnumerable<TestDataRow> CreateTestData(int randomSeed, int countRows)
    {
        var random = new Random(randomSeed);

        for (var loop = 0; loop < countRows; loop++)
        {
            yield return new TestDataRow()
            {
                ValueInt = random.Next(0, 50000),
                ValueString = $"STR_{random.Next(0, 50000)}",
                ValueBool = random.Next(0, 50000) >= 25000
            };
        }
    }
}