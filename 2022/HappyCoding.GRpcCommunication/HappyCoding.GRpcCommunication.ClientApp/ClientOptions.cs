using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace HappyCoding.GRpcCommunication.ClientApp;

public class ClientOptions
{
    private const string CATEGORY_CONNECTION = "Connection";
    private const string CATEGORY_CALLS = "Calls";
    private const string FILE_NAME = ".grpcCommunicationClientConfig.json";

    [Category(CATEGORY_CONNECTION)]
    public string TargetHost { get; set; } = "localhost";

    [Category(CATEGORY_CONNECTION)]
    public ushort PortHttp1 { get; set; } = 5000;

    [Category(CATEGORY_CONNECTION)]
    public ushort PortHttp2 { get; set; } = 5001;

    [Category(CATEGORY_CONNECTION)]
    public bool UseHttps { get; set; } = false;

    [Category(CATEGORY_CONNECTION)]
    public bool ConnectGrpcAtStart { get; set; } = true;

    [Category(CATEGORY_CONNECTION)]
    public uint PooledConnectionIdleTimeoutMS { get; set; } = (uint)TimeSpan.FromMinutes(1.0).TotalMilliseconds;

    [Category(CATEGORY_CALLS)]
    public ushort DelayBetweenCallsMS { get; set; } = 100;

    [Category(CATEGORY_CALLS)]
    public uint CallTimeoutMS { get; set; } = 10000;

    [Category(CATEGORY_CALLS)]
    public uint SpikeThresholdMS { get; set; } = 250;

    [Category(CATEGORY_CALLS)]
    [Range(2, ClientAppConstants.MAX_PARALLEL_CALLS)]
    public uint CountParallelLoopsOnParallelChannels { get; set; } = 5;

    public static async Task<ClientOptions> LoadAsync(CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            FILE_NAME);
        if(!File.Exists(fullPath))
        {
            return new ClientOptions();
        }

        try
        {
            await using var inStream = File.OpenRead(fullPath);
            return await JsonSerializer.DeserializeAsync<ClientOptions>(
                inStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web), 
                cancellationToken) ?? new ClientOptions();
        }
        catch (Exception)
        {
            return new ClientOptions();
        }
    }

    public static async Task SaveAsync(ClientOptions serverOptions, CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            FILE_NAME);
        try
        {
            await using var outStream = File.Create(fullPath);
            await JsonSerializer.SerializeAsync(
                outStream, serverOptions,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true
                },
                cancellationToken);
        }
        catch (Exception)
        {
            // Ignore
        }
    }
}
