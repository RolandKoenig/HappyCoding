using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Minio;

namespace Testing.MinioClientApp.MinioHandling;

public class MinioInterface
{
    private readonly MinioInterfaceConfiguration _config;

    public MinioInterface(MinioInterfaceConfiguration config)
    {
        _config = config;
    }

    public async Task<string> WriteFile(string name, Stream data, CancellationToken cancellationToken)
    {
        using var client = CreateClient();
        
        var response = await client.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(_config.Bucket)
                .WithObject(name)
                .WithObjectSize(data.Length)
                .WithStreamData(data),
            cancellationToken);
        
        return response.ObjectName;
    }

    private MinioClient CreateClient()
    {
        return new MinioClient()
            .WithEndpoint(_config.Endpoint)
            .WithCredentials(_config.AccessKey, _config.SecretKey)
            .WithSSL(_config.UseSsl)
            .Build();
    }
}