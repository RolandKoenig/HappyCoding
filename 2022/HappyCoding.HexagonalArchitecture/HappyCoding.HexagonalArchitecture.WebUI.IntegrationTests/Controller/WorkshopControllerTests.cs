using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Domain.Model;
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
        var assertUnitOfWork = assertScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var objInDB = await assertUnitOfWork.Workshops.GetWorkshopAsync(responseObject.ID, CancellationToken.None);

        Assert.Equal("Dummy Project", objInDB.Project);
        Assert.Equal("Dummy Workshop #1", objInDB.Title);
        Assert.Single(objInDB.Protocol);
    }

    [Fact]
    public async Task UpdateWorkshopTest()
    {
        // Arrange
        using var client = _application.CreateClient();

        var arrangeScope = _application.Services.CreateScope();
        var arrangeUnitOfWork = arrangeScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var arrangeWorkshop = Workshop.CreateNew(
            "Dummy Project", "Dummy Workshop #1", DateTimeOffset.UtcNow,
            Array.Empty<ProtocolEntry>());
        await arrangeUnitOfWork.Workshops.AddWorkshopAsync(arrangeWorkshop, CancellationToken.None);
        await arrangeUnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var httpResponse = await client.PutAsJsonAsync("Workshops", new WorkshopDto()
        {
            ID = arrangeWorkshop.ID,
            Project = "Dummy Project (Modified name)",
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
        var assertUnitOfWork = assertScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var objInDB = await assertUnitOfWork.Workshops.GetWorkshopAsync(responseObject.ID, CancellationToken.None);

        Assert.Equal("Dummy Project (Modified name)", objInDB.Project);
        Assert.Equal("Dummy Workshop #1", objInDB.Title);
        Assert.Single(objInDB.Protocol);
    }

    [Fact]
    public async Task DeleteWorkshopTest()
    {
        // Arrange
        using var client = _application.CreateClient();

        var arrangeScope = _application.Services.CreateScope();
        var arrangeUnitOfWork = arrangeScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var arrangeWorkshop = Workshop.CreateNew(
            "Dummy Project", "Dummy Workshop #1", DateTimeOffset.UtcNow,
            Array.Empty<ProtocolEntry>());
        await arrangeUnitOfWork.Workshops.AddWorkshopAsync(arrangeWorkshop, CancellationToken.None);
        await arrangeUnitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var httpResponse = await client.DeleteAsync($"Workshops/{arrangeWorkshop.ID}");
        httpResponse.EnsureSuccessStatusCode();

        // Assert
        using var assertScope = _application.Services.CreateScope();
        var assertUnitOfWork = assertScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var objInDB = await assertUnitOfWork.Workshops.TryGetWorkshopAsync(arrangeWorkshop.ID, CancellationToken.None);

        Assert.Null(objInDB);
    }
}
