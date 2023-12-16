using HappyCoding.AspNetTestWithPlaywright.Tests.Util;
using Microsoft.Playwright;

namespace HappyCoding.AspNetTestWithPlaywright.Tests;

public class MyPlaywrightTests(WebHostServerFixture<Startup> server) : IClassFixture<WebHostServerFixture<Startup>>
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
}