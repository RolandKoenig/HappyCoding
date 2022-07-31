namespace HappyCoding.ConsoleLogWindow.StdOutProcessRunner;

public static class StdOutProcessRunnerUtil
{
    public static string GetWorkingDirectory(string targetFile)
    {
        var fullFilePath = Path.GetFullPath(targetFile);
        return Path.GetDirectoryName(fullFilePath) ?? "";
    }
}
