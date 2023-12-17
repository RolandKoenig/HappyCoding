namespace HappyCoding.AspNetTestWithPlaywright.Tests.Util;

internal static class TestUtil
{
    internal static void EnsureBrowsersInstalled()
    {
        var exitCode = Microsoft.Playwright.Program.Main(new[] {"install"});
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }
    }
}