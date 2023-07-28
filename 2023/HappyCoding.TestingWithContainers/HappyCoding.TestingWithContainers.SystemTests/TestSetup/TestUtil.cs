namespace HappyCoding.TestingWithContainers.SystemTests.TestSetup;

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

        var solutionFound = false;
        do
        {
            if (Directory.GetFiles(currentDirectory, "*.sln", SearchOption.TopDirectoryOnly).Length > 0)
            {
                solutionFound = true;
            }
            else
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }
        } 
        while ((!solutionFound) && (!string.IsNullOrEmpty(currentDirectory)));

        if (!solutionFound)
        {
            throw new InvalidOperationException("Unable to find solution directory!");
        }

        return currentDirectory;
    }
}
