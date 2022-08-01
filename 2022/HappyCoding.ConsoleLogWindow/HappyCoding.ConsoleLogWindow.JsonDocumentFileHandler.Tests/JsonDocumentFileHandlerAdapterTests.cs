using HappyCoding.ConsoleLogWindow.Application.Model;

namespace HappyCoding.ConsoleLogWindow.JsonDocumentFileHandler.Tests;

public class JsonDocumentFileHandlerAdapterTests
{
    [Fact]
    public async Task SerializeAndDeserializeDocumentModel()
    {
        // Arrange
        var testFile = "TestFile.processList";
        if (File.Exists(testFile))
        {
            File.Delete(testFile);
        }

        var documentModel = new DocumentModel();
        documentModel.ProcessGroups.Add(new ProcessGroup()
        {
            Title = "Group1",
            Processes = new()
            {
                new ProcessInfo()
                {
                    Arguments = "TestArg1",
                    FileName = "TestFile1.exe",
                    Title = "TestTitle1"
                },
                new ProcessInfo()
                {
                    Arguments = "TestArg2",
                    FileName = "TestFile2.exe",
                    Title = "TestTitle2"
                },
                new ProcessInfo()
                {
                    Arguments = "TestArg3",
                    FileName = "TestFile3.exe",
                    Title = "TestTitle3"
                }
            }
        });

        // Act
        var jsonDocumentFileHandler = new JsonDocumentFileHandlerAdapter();
        await jsonDocumentFileHandler.SaveDocumentToFileAsync(documentModel, testFile);
        var deserialized = await jsonDocumentFileHandler.LoadDocumentFromFileAsync(testFile);

        // Assert
        Assert.Single(deserialized.ProcessGroups);
        Assert.Equal("Group1", deserialized.ProcessGroups[0].Title);
        Assert.Equal(3, deserialized.ProcessGroups[0].Processes.Count);
    }
}
