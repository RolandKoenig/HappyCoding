using System.Data;
using System.Diagnostics;
using DbUp;
using HappyCoding.JsonDocumentsWithNevermore.Model;
using HappyCoding.JsonDocumentsWithNevermore.Util;
using Nevermore;

namespace HappyCoding.JsonDocumentsWithNevermore;

internal static class Program
{
    // Parameters
    internal const int RANDOM_SEED = 100;
    internal const string CONNECTION_STRING = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HappyCoding_2022_JsonDocumentsWithNevermore;Integrated Security=SSPI";
    internal const int DOCUMENT_COUNT = 10000;
    internal const int DOCUMENT_INSERT_BATCH_SIZE = 100;
    internal const int RANDOM_QUERY_COUNT = 20;
    
    internal static async Task Main()
    {
        // Setup database
        Console.WriteLine("Setup database");
        await DBUtil.EnsureNewDBAsync(CONNECTION_STRING);
        var upgrader =
            DeployChanges.To
                .SqlDatabase(CONNECTION_STRING)
                .WithScriptsEmbeddedInAssembly(typeof(RelationalStore).Assembly) // <-- Upgrade scripts of Nevermore
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)         // <-- Own upgrade scripts
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

        // Create store
        Console.WriteLine("Configure store");
        var config = new RelationalStoreConfiguration(CONNECTION_STRING);
        config.DocumentMaps.Register(new TestingDocumentMap());
        var store = new RelationalStore(config);

        // Generate documents
        Console.WriteLine($"Create {DOCUMENT_COUNT} documents");
        var random = new Random(RANDOM_SEED);
        var newDocuments = new List<TestingDocument>(DOCUMENT_COUNT);
        var newDocumentsBatch = new List<TestingDocument>(DOCUMENT_INSERT_BATCH_SIZE);
        using var transaction = await store.BeginWriteTransactionAsync();
        while (newDocuments.Count < DOCUMENT_COUNT)
        {
            
            for (var loop = 0; loop < DOCUMENT_INSERT_BATCH_SIZE; loop++)
            {
                newDocumentsBatch.Clear();

                var actNewDocument = new TestingDocument();
                actNewDocument.FillWithRandomData(random);
                newDocumentsBatch.Add(actNewDocument);

                await transaction.InsertManyAsync(newDocumentsBatch);

                newDocuments.AddRange(newDocumentsBatch);
            }
        }
        await transaction.CommitAsync();

        // Execute random queries
        var stopWatch = new Stopwatch();
        using var readTransaction = await store.BeginReadTransactionAsync(IsolationLevel.ReadCommitted);
        for (var loop = 0; loop < RANDOM_QUERY_COUNT; loop++)
        {
            var readIndex = random.Next(0, newDocuments.Count);
            var expectedDocument = newDocuments[readIndex];

            stopWatch.Reset();
            stopWatch.Start();

            var readDocument =
                readTransaction.LoadRequiredAsync<TestingDocument>(expectedDocument.ID);
            GC.KeepAlive(readDocument);

            stopWatch.Stop();
            Console.WriteLine($"Read single document at index {readIndex} in {stopWatch.Elapsed.TotalMilliseconds:N2} ms");
        }
    }
}