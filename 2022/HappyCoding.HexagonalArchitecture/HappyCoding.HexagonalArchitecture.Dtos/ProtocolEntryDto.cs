using System.ComponentModel.DataAnnotations;

namespace HappyCoding.HexagonalArchitecture.Dtos;

public record ProtocolEntryDto
{
    [Required]
    public string Text { get; set; }
    
    [Required]
    public ProtocolEntryTypeDto EntryType { get; set; }
    
    [Required]
    public int Priority { get; set; }
}