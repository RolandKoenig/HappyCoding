using Xunit;

namespace HappyCoding.AvaloniaLogViewer.FileLoaders.Tests;

public class CompactJsonFileLoaderTests
{
    [Fact]
    public void ParseLine_01()
    {
        // Arrange
        var line =
            "{'@t':'2022-02-19T16:47:30.0554314Z','@mt':'Hello, {@User}','User':'Roland'}";
        
        // Act
        var logEntry = CompactJsonFileLoader.TryParseLogFileEntryFromLine(line);

        // assert.
        Assert.NotNull(logEntry);
    }
}