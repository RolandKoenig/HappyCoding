using System.Text.Json.Serialization;

namespace HappyCoding.AspNetCoreSwaggerGen.API.DTOs;

public record ProductDto
{
    public Guid ID { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
    
    public ProductType Type { get; init; }
}