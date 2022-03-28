using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HappyCoding.EFCoreFeatures.Util;

// ## Disable some warnings because of EntityFrameworkCore
// ReSharper disable UnusedAutoPropertyAccessor.Local
#pragma warning disable CS8618 

namespace HappyCoding.EFCoreFeatures.Data;

public class TestingRow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; private set; }

    [MaxLength(30)]
    public string Name { get; private set; }

    public int CalculationA { get; private set; }

    public int CalculationB { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int CalculationResult { get; private set; }

    public JsonModel<TestingTagCollection> TagCollection { get; private set; }

    [NotMapped]
    public string Value1
    {
        get => this.TagCollection.ModelInstance.Tag1;
        set => this.TagCollection.UpdateModel(model => model.Tag1 = value);
    }

    [NotMapped]
    public string Value2
    {
        get => this.TagCollection.ModelInstance.Tag2;
        set => this.TagCollection.UpdateModel(model => model.Tag2 = value);
    }

    [NotMapped]
    public string Value3
    {
        get => this.TagCollection.ModelInstance.Tag3;
        set => this.TagCollection.UpdateModel(model => model.Tag3 = value);
    }

    private TestingRow()
    {

    }

    public TestingRow(string name, TestingTagCollection model)
    {
        this.Name = name;
        this.TagCollection = new JsonModel<TestingTagCollection>(model);
    }

    public void SetCalculationValues(int a, int b)
    {
        this.CalculationA = a;
        this.CalculationB = b;
    }
}
