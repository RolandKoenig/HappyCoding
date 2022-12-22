using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace HappyCoding.GRpcCommunication.ClientApp;

public class ClientOptions
{
    private const string FILE_NAME = ".grpcCommunicationClientConfig.json";

    public string TargetHost { get; set; } = "localhost";

    public ushort PortHttp1 { get; set; } = 5000;

    public ushort PortHttp2 { get; set; } = 5001;

    public bool UseHttps { get; set; } = false;

    public ushort DelayBetweenCallsMS { get; set; } = 100;

    public uint CallTimeoutMS { get; set; } = 1000;

    [Range(2, uint.MaxValue)]
    public uint CountParallelLoopsOnParallelChannels { get; set; } = 5;

    public bool ConnectGrpcAtStart { get; set; } = true;

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
