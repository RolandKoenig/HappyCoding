using System.ComponentModel.DataAnnotations;

namespace HappyCoding.EFCoreOwnedTypes.Data;

public class Company
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public Address Address { get; set; } = new Address();

    public Address SecondaryAddress { get; set; } = new Address();
}
