using System.ComponentModel.DataAnnotations;

namespace HappyCoding.EFCoreOwnedTypes.Data;

public class Person
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public Address Address { get; set; } = new Address();
}
