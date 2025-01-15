using System.ComponentModel.DataAnnotations;

namespace HappyCoding.AspNetOptionsPattern;

public class ApplicationOptions
{
    public const string SECTION_NAME = "ApplicationOptions";
    
    [Required]
    public string OtherServiceName { get; set; } = string.Empty;
    
    public ushort OtherServicePort { get; set; }
}