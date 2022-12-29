using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace HappyCoding.GRpcCommunication.ServerApp;

public class ServerOptions
{
    private const string CATEGORY_COMMON = "Common";
    private const string CATEGORY_HTTPS = "Https";
    private const string CATEGORY_HTTP2 = "Http2";

    private const string FILE_NAME = ".grpcCommunicationServerConfig.json";

    [Category(CATEGORY_COMMON)]
    public ushort PortHttp1 { get; set; } = 5000;

    [Category(CATEGORY_COMMON)]
    public ushort PortHttp2 { get; set; } = 5001;

    [Category(CATEGORY_COMMON)]
    public uint SimulatedProcessingTimeMS { get; set; } = 0;

    [Category(CATEGORY_HTTPS)]
    public bool UseHttps { get; set; } = false;

    [Category(CATEGORY_HTTPS)]
    public string CertificateFilePath { get; set; } = string.Empty;

    [Category(CATEGORY_HTTPS)]
    [PasswordPropertyText]
    public string CertificatePassword { get; set; } = string.Empty;

    [Category(CATEGORY_HTTP2)]
    public uint MaxStreamsPerConnection { get; set; } = 100;

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
