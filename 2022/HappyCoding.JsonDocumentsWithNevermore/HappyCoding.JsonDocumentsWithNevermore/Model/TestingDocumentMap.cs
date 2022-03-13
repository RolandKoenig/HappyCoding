using Nevermore.Mapping;

namespace HappyCoding.JsonDocumentsWithNevermore.Model;

internal class TestingDocumentMap : DocumentMap<TestingDocument>
{
    public TestingDocumentMap()
    {
        this.JsonStorageFormat = JsonStorageFormat.CompressedOnly;
        this.ExpectLargeDocuments = true;
        this.TableName = "TestingDocuments";

        this.Id("Id", x => x.ID).KeyHandler(new GuidPrimaryKeyHandler());
        this.Column("Value1", x => x.Value1);
        this.Column("Value2", x => x.Value2);
        this.Column("Value3", x => x.Value3);
    }
}