using System;
using Xunit;

namespace HappyCoding.AvaloniaLogViewer.FileLoaders.Tests;

public class CompactJsonFileLoaderTests
{
    [Fact]
    public void ParseLine_CheckTimestamp_()
    {
        // Arrange
        var line =
            "{'@t':'2022-02-19T16:47:30.055Z','@mt':'Hello, {@User}','User':'Roland'}";
        
        // Act
        var logEntry = CompactJsonFileLoader.TryParseLogFileEntryFromLine(line);

        // assert.
        Assert.NotNull(logEntry);
        Assert.Equal(
            new DateTimeOffset(2022, 2, 19, 16, 47, 30, 55, TimeSpan.Zero),
            logEntry.Timestamp);
    }
    
    [Fact]
    public void ParseLine_CheckTimestamp_WithDateTimeOffset_German()
    {
        // Arrange
        var line =
            "{'@t':'2022-02-19T16:47:30.050+02:00','@mt':'Hello, {@User}','User':'Roland'}";
        
        // Act
        var logEntry = CompactJsonFileLoader.TryParseLogFileEntryFromLine(line);

        // assert.
        Assert.NotNull(logEntry);
        Assert.Equal(
            new DateTimeOffset(2022, 2, 19, 14, 47, 30, 50, TimeSpan.Zero),
            logEntry.Timestamp);
    }
    
    [Fact]
    public void ParseLine_CheckTimestamp_WithDateTimeOffset_Malaysia()
    {
        // Arrange
        var line =
            "{'@t':'2022-02-19T16:47:30.050+08:00','@mt':'Hello, {@User}','User':'Roland'}";
        
        // Act
        var logEntry = CompactJsonFileLoader.TryParseLogFileEntryFromLine(line);

        // assert.
        Assert.NotNull(logEntry);
        Assert.Equal(
            new DateTimeOffset(2022, 2, 19, 8, 47, 30, 50, TimeSpan.Zero),
            logEntry.Timestamp);
    }
}