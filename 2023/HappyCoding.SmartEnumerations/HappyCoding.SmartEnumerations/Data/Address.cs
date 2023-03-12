using System.ComponentModel.DataAnnotations;
using HappyCoding.SmartEnumerations.Enums;

namespace HappyCoding.SmartEnumerations.Data;

public class Address
{
    [Key]
    public long Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Street { get; set; } = string.Empty;

    [MaxLength(50)]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(50)] public string City { get; set; } = string.Empty;

    public Nation Country { get; set; } = Nation.Germany;
}
