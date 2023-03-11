namespace HappyCoding.EFCoreMultipleContextsInSameDb;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var dbConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2023_MultipleContextsInSameDb;Integrated Security=SSPI";

        Console.WriteLine("Migrating Context1");
        await Context1.DbMigrator.MigrateDbAsync(dbConnectionString);

        Console.WriteLine("Migrating Context2");
        await Context2.DbMigrator.MigrateDbAsync(dbConnectionString);
    }
}
