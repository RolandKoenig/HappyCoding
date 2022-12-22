using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Google.Protobuf;

namespace HappyCoding.GRpcCommunication.ServerApp;

public class ServerOptions
{
    private const string FILE_NAME = ".grpcCommunicationServerConfig.json";

    public ushort PortHttp1 { get; set; } = 5000;

    public ushort PortHttp2 { get; set; } = 5001;

    public bool UseHttps { get; set; } = false;

    public uint SimulatedProcessingTimeMS { get; set; } = 0;

    public static async Task<ServerOptions> LoadAsync(CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            FILE_NAME);
        if(!File.Exists(fullPath))
        {
            return new ServerOptions();
        }

        try
        {
            await using var inStream = File.OpenRead(fullPath);
            return await JsonSerializer.DeserializeAsync<ServerOptions>(
                inStream,
                new JsonSerializerOptions(JsonSerializerDefaults.Web), 
                cancellationToken) ?? new ServerOptions();
        }
        catch (Exception)
        {
            return new ServerOptions();
        }
    }

    public static async Task SaveAsync(ServerOptions serverOptions, CancellationToken cancellationToken)
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
