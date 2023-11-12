using System.ComponentModel.DataAnnotations;

namespace HappyCoding.WebApiWithSqliteDb.Data;

public class PersonDataRow
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)] 
    public string City { get; set; } = string.Empty;
}
