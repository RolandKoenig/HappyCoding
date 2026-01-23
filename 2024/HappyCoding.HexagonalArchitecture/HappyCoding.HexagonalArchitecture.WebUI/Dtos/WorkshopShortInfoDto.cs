using System.ComponentModel.DataAnnotations;

namespace HappyCoding.HexagonalArchitecture.WebUI.Dtos;

public class WorkshopShortInfoDto
{
    public Guid ID { get; init; }
    
    public string Project { get; init; }

    public string Title { get; init; }
    
    public DateTimeOffset StartTimestamp { get; init; }
}