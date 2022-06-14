using System.Collections.Immutable;
using HappyCoding.EFCoreJsonModelValueConverter;
using HappyCoding.EFCoreJsonModelValueConverter.Model;
using HappyCoding.EFCoreJsonModelValueConverter.Util;
using Microsoft.EntityFrameworkCore;

const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_EFCoreJsonModelValueConverter;Integrated Security=SSPI";

// Create DB
Console.WriteLine("Create database...");
await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);
Console.WriteLine("Database created");

// Migrate DB
Console.WriteLine("Migrate database...");
var optionsBuilder = new DbContextOptionsBuilder<TestingDBContext>();
optionsBuilder.UseSqlServer(CONNECTION_STRING);
{
    await using var migrationContext = new TestingDBContext(optionsBuilder.Options);
    await migrationContext.Database.MigrateAsync();
}
Console.WriteLine("Database migrated");

// Insert some rows
Console.WriteLine("Insert some rows...");
var row1ID = Guid.NewGuid();
var row2ID = Guid.NewGuid();
var row3ID = Guid.NewGuid();
{
    await using var insertContext = new TestingDBContext(optionsBuilder.Options);

    await insertContext.Testing.AddAsync(new TestingDataRow(
        row1ID,
        new TestingJsonData() {DetailField1 = "1", DetailField2 = "123"}));
    await insertContext.Testing.AddAsync(new TestingDataRow(
        row2ID,
        new TestingJsonData() {DetailField1 = "2", DetailField2 = "123"}));
    await insertContext.Testing.AddAsync(new TestingDataRow(
        row3ID,
        new TestingJsonData() {DetailField1 = "3", DetailField2 = "123"}));

    await insertContext.SaveChangesAsync();
}
Console.WriteLine("Rows inserted");

// Update one row
Console.WriteLine("Update row 2...");
{
    await using var updateContext = new TestingDBContext(optionsBuilder.Options);

    var dataRow = await updateContext.Testing
        .Where(row => row.Id == row2ID)
        .FirstAsync();
    dataRow.JsonData = dataRow.JsonData with { DetailField2 = "555" };

    await updateContext.SaveChangesAsync();
}
Console.WriteLine("Row updated");

// Print all data
Console.WriteLine("Print all rows...");
{
    await using var printContext = new TestingDBContext(optionsBuilder.Options);

    foreach (var actRow in printContext.Testing)
    {
        Console.WriteLine();
        Console.WriteLine(actRow.Id.ToString() + ":");
        Console.WriteLine(actRow.JsonData.ToString());
    }
}
Console.WriteLine();
Console.WriteLine("All rows printed");

ImmutableArray<int> sdf;