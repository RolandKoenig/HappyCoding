using System.ComponentModel.DataAnnotations;

namespace HappyCoding.HexagonalArchitecture.Application.Dtos;

public record WorkshopWithoutIDDto
{
    [Required]
    public string Project { get; set; }

    [Required]
    public string Title { get; set; }
    
    [Required]
    public DateTimeOffset StartTimestamp { get; set; }

    [Required]
    public List<ProtocolEntryDto> Protocol { get; set; }
}
