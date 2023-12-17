using HappyCoding.AspNetTestWithPlaywright.Tests.Util;
using Microsoft.Playwright;

namespace HappyCoding.AspNetTestWithPlaywright.Tests;

// Test ASP.NET Core Web Application using Playwright
// Method described here: https://www.meziantou.net/automated-ui-tests-an-asp-net-core-application-with-playwright-and-xunit.htm

[Collection(nameof(TestEnvironmentCollection))]
public class PageNavigationTests(WebHostServerFixture<Startup> server)
{
    [Fact]
    public async Task Navigate_to_index_page()
    {
        // Arrange
        using var playwright = await Playwright.CreateAsync();
        
        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();

        // Act
        await page.GotoAsync(server.RootUri.AbsoluteUri);

        // Assert
        var header = await page.WaitForSelectorAsync("h1");
        Assert.NotNull(header);
        Assert.Equal("Hello World!", await header.TextContentAsync());
    }

    [Fact]
    public async Task Navigate_to_index_then_to_privacy_page()
    {
        // Arrange
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();
        
        // Act
        await page.GotoAsync(server.RootUri.AbsoluteUri);
        await page
            .GetByRole(AriaRole.List)
            .GetByRole(AriaRole.Link, new() { Name = "Privacy" })
            .ClickAsync();
        
        // Assert
        var header = await page.WaitForSelectorAsync("h1");
        Assert.NotNull(header);
        Assert.Equal("Privacy Policy", await header.TextContentAsync());
    }
}