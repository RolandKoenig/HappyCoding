using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Domain.Ports;

namespace HappyCoding.HexagonalArchitecture.WebUI.Integration.Tests.Controller;

[Collection(nameof(WebApplicationTestCollection))]
public class WorkshopControllerTests : IClassFixture<WebApplicationFixture>
{
    private readonly WebApplicationFixture _application;

    public WorkshopControllerTests(WebApplicationFixture application)
    {
        _application = application;
    }

    [Fact]
    public async Task CreateWorkshopTest()
    {
        // Arrange
        using var client = _application.CreateClient();

        // Act
        var httpResponse = await client.PostAsJsonAsync("Workshops", new WorkshopWithoutIDDto()
        {
            Project = "Dummy Project",
            Title = "Dummy Workshop #1",
            Protocol = new List<ProtocolEntryDto>()
            {
                new()
                {
                    EntryType = ProtocolEntryTypeDto.Information,
                    Priority = 2,
                    Text = "Workshop started"
                }
            },
            StartTimestamp = DateTimeOffset.UtcNow
        });
        var responseObject = await httpResponse.Content.ReadFromJsonAsync<WorkshopDto>();

        // Assert
        Assert.NotNull(responseObject);
        Assert.True(responseObject!.ID != Guid.Empty);

        using var assertScope = _application.Services.CreateScope();
        var assertRepo = assertScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var objInDB = await assertRepo.Workshops.GetWorkshopAsync(responseObject.ID, CancellationToken.None);

        Assert.Equal("Dummy Project", objInDB.Project);
        Assert.Equal("Dummy Workshop #1", objInDB.Title);
        Assert.Single(objInDB.Protocol);
    }
}
