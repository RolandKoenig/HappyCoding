namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner.Tests;

public class StdOutProcessRunnerUtilTests
{
    [Theory]
    [InlineData("C:\\TestDirectory\\TestFile.exe", "C:\\TestDirectory")]
    [InlineData("TestFile.exe", "{CurrentDirectory}")]
    public void GetWorkingDirectoryFromExePathTheory(string fileName, string expectedWorkingDirectory)
    {
        // Arrange
        expectedWorkingDirectory = expectedWorkingDirectory.Replace("{CurrentDirectory}", Environment.CurrentDirectory);

        // Act
        var workingDirectory = StdOutProcessRunnerUtil.GetWorkingDirectory(fileName);

        // Assert
        Assert.Equal(expectedWorkingDirectory, workingDirectory);
    }
}