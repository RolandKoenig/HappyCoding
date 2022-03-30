using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyCoding.EFCoreFeatures.Data;

public class ChildRow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; private set; }

    public DateTimeOffset Timestamp { get; private set; }

    public int ParentRowID { get; private set; }

    public ChildRow(DateTimeOffset timestamp)
    {
        this.Timestamp = timestamp;
    }
}
