using System.Net;
using System.Net.Http.Json;
using HappyCoding.TestingWithContainers.Data;
using HappyCoding.TestingWithContainers.IntegrationTests.TestSetup;

namespace HappyCoding.TestingWithContainers.IntegrationTests;

[Collection(nameof(WebApplicationTestCollection))]
public class PersonsControllerTests
{
    private readonly TestApplicationFixture _fixture;
    private readonly HttpClient _httpClient;

    public PersonsControllerTests(TestApplicationFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.CreateClient();
    }
    
    [Fact]
    public async Task Get_persons_after_startup_returns_empty_result()
    {
        // Arrange
        await _fixture.CleanupDatabaseAsync();
        
        // Act
        var response = await _httpClient.GetAsync("Persons");
        
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
        var responsePost = await _httpClient.PostAsJsonAsync("Persons", newPerson);
        var responseGetAll = await _httpClient.GetAsync("Persons");

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