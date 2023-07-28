using System.Net;
using HappyCoding.TestingWithContainers.SystemTests.TestSetup;

namespace HappyCoding.TestingWithContainers.SystemTests;

[Collection(nameof(TestEnvironmentCollection))]
public class PersonApiTests : IClassFixture<TestEnvironmentFixture>
{
    private readonly TestEnvironmentFixture _fixture;

    public PersonApiTests(TestEnvironmentFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_persons_after_startup_returns_204_NO_CONTENT()
    {
        await _fixture.EnsureContainersLoadedAsync();

        // Act
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{_fixture.ApplicationBaseUrl}/Persons");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}