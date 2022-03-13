using System.Reflection;
using DbUp;
using HappyCoding.JsonDocumentsWithNevermore.Util;
using Nevermore;

namespace HappyCoding.JsonDocumentsWithNevermore;

internal static class Program
{
    // Parameters
    internal const int RANDOM_SEED = 100;
    internal const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_JsonDocumentsWithNevermore;Integrated Security=SSPI";
    internal const int DOCUMENT_COUNT = 1000;
    

    internal static async Task Main()
    {
        var random = new Random(RANDOM_SEED);

        // Setup database
        Console.WriteLine("Setup database");
        await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);
        var upgrader =
            DeployChanges.To
                .SqlDatabase(CONNECTION_STRING)
                .WithScriptsEmbeddedInAssembly(typeof(RelationalStore).Assembly)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .LogToNowhere()
                .Build();
        var upgradeResult = upgrader.PerformUpgrade();
        if (upgradeResult.Successful)
        {
            Console.WriteLine("Setup database successful");
        }
        else
        {
            Console.WriteLine($"ERROR: Unable to setup database: {upgradeResult.Error}");
            return;
        }

        // 
    }
}