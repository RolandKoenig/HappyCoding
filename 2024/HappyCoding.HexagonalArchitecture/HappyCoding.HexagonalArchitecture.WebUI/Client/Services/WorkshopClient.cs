using System.Text.Json;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client.Services;

internal partial class WorkshopClient
{
    partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    { 
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
}