using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace HappyCoding.GRpcCommunication.ClientApp;

public class ClientOptions
{
    private const string FILE_NAME = ".grpcCommunicationClientConfig.json";

    public string TargetHost { get; set; } = "localhost";

    public ushort Port { get; set; } = 5000;

    public bool UseHttps { get; set; } = false;

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
            await using var outStream = File.OpenWrite(fullPath);
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
