namespace HappyCoding.ConsoleLogWindow.Gui.Tests;

public class StartupArgumentsTests
{
    [Fact]
    public void EmptyArguments()
    {
        // Act
        var startupArguments = StartupArguments.ParseStartupArguments(Array.Empty<string>());

        // Assert
        Assert.NotNull(startupArguments);
        Assert.True(string.IsNullOrEmpty(startupArguments.InitialFile));
    }

    [Fact]
    public void WithInitialFile()
    {
        // Act
        var startupArguments = StartupArguments.ParseStartupArguments(new[]
        {
            "test.processList"
        });

        // Assert
        Assert.NotNull(startupArguments);
        Assert.Equal("test.processList", startupArguments.InitialFile);
    }

    [Fact]
    public void WithUnknownArgument()
    {
        // Act
        var startupArguments = StartupArguments.ParseStartupArguments(new[]
        {
            "test.processList",
            "-test"
        });

        // Assert
        Assert.NotNull(startupArguments);
        Assert.Equal("test.processList", startupArguments.InitialFile);
    }
}