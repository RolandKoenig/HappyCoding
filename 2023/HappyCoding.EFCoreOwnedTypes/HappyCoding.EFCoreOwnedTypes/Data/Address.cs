using System.ComponentModel.DataAnnotations;

namespace HappyCoding.EFCoreOwnedTypes.Data;

public class Address
{
    [MaxLength(100)]
    public string Street { get; set; } = string.Empty;

    [MaxLength(10)]
    public string HouseNumber { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
}
