namespace HappyCoding.EFCoreJsonModelValueConverter.Model;

public class TestingDataRow
{
    public Guid Id { get; set; }

    public TestingJsonData JsonData { get; set; }

    public TestingDataRow(Guid id, TestingJsonData jsonData)
    {
        this.Id = id;
        this.JsonData = jsonData;
    }
}