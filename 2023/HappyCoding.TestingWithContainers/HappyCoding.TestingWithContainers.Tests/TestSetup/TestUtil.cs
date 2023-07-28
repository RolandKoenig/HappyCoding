namespace HappyCoding.TestingWithContainers.Tests.TestSetup;

internal static class TestUtil
{
    public static string GetSolutionDirectory()
    {
        var thisAssembly = typeof(TestUtil).Assembly;
        
        var currentDirectory = Path.GetDirectoryName(thisAssembly.Location);
        if (string.IsNullOrEmpty(currentDirectory))
        {
            throw new InvalidOperationException("Unable to find solution directory!");
        }
        
        var currentSolutionPath = Path.Combine(currentDirectory, "HappyCoding.TestingWithContainers.sln");
        while (!File.Exists(currentSolutionPath))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            if (string.IsNullOrEmpty(currentDirectory))
            {
                throw new InvalidOperationException("Unable to find solution directory!");
            }
            
            currentSolutionPath = Path.Combine(currentDirectory, "HappyCoding.TestingWithContainers.sln");
        }

        if (!File.Exists(currentSolutionPath))
        {
            throw new InvalidOperationException("Unable to find solution directory!");
        }

        return currentDirectory;
    }
}
