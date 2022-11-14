using System.Text.Json;
using System.Text.Json.Serialization;

namespace HappyCoding.AspNetCoreSwaggerGen.ConsoleClient;

partial class ProductsClient
{
    partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    {
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        settings.Converters.Add(new JsonStringEnumConverter());
    }
}