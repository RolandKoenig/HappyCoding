using Nevermore.Mapping;

namespace HappyCoding.JsonDocumentsWithNevermore.Model;

internal class TestingDocumentMap : DocumentMap<TestingDocument>
{
    public TestingDocumentMap()
    {
        Column("Value1", x => x.Value1);
        Column("Value2", x => x.Value2);
        Column("Value3", x => x.Value3);
    }
}