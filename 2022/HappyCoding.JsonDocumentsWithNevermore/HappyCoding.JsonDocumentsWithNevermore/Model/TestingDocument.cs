namespace HappyCoding.JsonDocumentsWithNevermore.Model;

public class TestingDocument
{
    public string Value1 { get; set; } = string.Empty;

    public string Value2 { get; set; } = string.Empty;

    public int Value3 { get; set; }

    public int Value4 { get; set; } 

    public string Value5 { get; set; } = string.Empty;

    public List<TestingChildElement> Childs { get; } = new();

    public TestingDocument()
    {

    }

    public void FillWithRandomData(Random random)
    {
        this.Value1 = random.Next(0, int.MaxValue).ToString();
        this.Value2 = random.Next(0, int.MaxValue).ToString();
        this.Value3 = random.Next(0, int.MaxValue);
        this.Value4 = random.Next(0, int.MaxValue);
        this.Value5 = random.Next(0, int.MaxValue).ToString();

        var childCount = random.Next(10, 30);
        for (var loop = 0; loop < childCount; loop++)
        {
            var newChild = new TestingChildElement();
            newChild.Value1 = random.Next(0, int.MaxValue).ToString();
            newChild.Value2 = random.Next(0, int.MaxValue).ToString();
            newChild.Value3 = random.Next(0, int.MaxValue);
            newChild.Value4 = random.Next(0, int.MaxValue);
            newChild.Value5 = random.Next(0, int.MaxValue).ToString();
            this.Childs.Add(newChild);
        }
    }
}