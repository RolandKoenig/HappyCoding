using System.ComponentModel.DataAnnotations;

namespace HappyCoding.EFCoreMultipleContextsInSameDb.Context2;

internal class DataRow2
{
    [Key]
    public long Id { get; set; }

    [MaxLength(50)]
    public string Dummy1 { get; set; }

    [MaxLength(50)]
    public string Dummy2 { get; set; }

    [MaxLength(50)]
    public string Dummy3 { get; set; }
}
