using System.Net;
using System.Net.Http.Json;
using HappyCoding.TestingWithContainers.SystemTests.Data;
using HappyCoding.TestingWithContainers.SystemTests.TestSetup;

namespace HappyCoding.TestingWithContainers.SystemTests;

[Collection(nameof(TestEnvironmentCollection))]
public class PersonApiTests
{
    private readonly TestEnvironmentFixture _fixture;

    public PersonApiTests(TestEnvironmentFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_persons_after_startup_returns_empty_result()
    {
        await _fixture.EnsureContainersStartedAsync();

        // Act
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{_fixture.ApplicationBaseUrl}/Persons");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_new_person_and_get_it_over_person_list()
    {
        // Arrange
        await _fixture.CleanupDatabaseAsync();
        
        // Act
        var newPerson = new PersonDataRow()
        {
            Name = "Roland",
            City = "Erlangen"
        };
        var httpClient = new HttpClient();
        var responsePost = await httpClient.PostAsJsonAsync($"{_fixture.ApplicationBaseUrl}/Persons", newPerson);
        var responseGetAll = await httpClient.GetAsync($"{_fixture.ApplicationBaseUrl}/Persons");

        // Assert
        Assert.True(responsePost.IsSuccessStatusCode);
        Assert.True(responseGetAll.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);
        Assert.Equal(HttpStatusCode.OK, responseGetAll.StatusCode);

        var getAllResult = await responseGetAll.Content.ReadFromJsonAsync<PersonDataRow[]>();
        Assert.NotNull(getAllResult);
        Assert.Single(getAllResult);
        Assert.NotEqual(0, getAllResult[0].Id);
        Assert.Equal("Roland", getAllResult[0].Name);
        Assert.Equal("Erlangen", getAllResult[0].City);
    }
}