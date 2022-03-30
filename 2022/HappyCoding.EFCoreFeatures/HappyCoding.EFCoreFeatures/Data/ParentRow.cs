using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyCoding.EFCoreFeatures.Data;

public class ParentRow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; private set; }

    public string Name { get; private set; }

    public List<ChildRow> Childs { get; } = new();

    public ParentRow(string name)
    {
        this.Name = name;
    }
}
