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

    public JsonModel<TestingDocument> Model { get; private set; }

    [NotMapped]
    public string Value1
    {
        get => this.Model.ModelInstance.Value1;
        set => this.Model.UpdateModel(model => model.Value1 = value);
    }

    [NotMapped]
    public string Value2
    {
        get => this.Model.ModelInstance.Value2;
        set => this.Model.UpdateModel(model => model.Value2 = value);
    }

    [NotMapped]
    public string Value3
    {
        get => this.Model.ModelInstance.Value3;
        set => this.Model.UpdateModel(model => model.Value3 = value);
    }

    private TestingRow()
    {

    }

    public TestingRow(TestingDocument model)
    {
        Model = new JsonModel<TestingDocument>(model);
    }
}
