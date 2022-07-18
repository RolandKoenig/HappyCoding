using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.WebUI.Client.Services;
using Microsoft.AspNetCore.Components;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client.Pages;

public partial class WorkshopDetail
{
    [Parameter]
    public string WorkshopID { get; set; }

    public WorkshopWithoutIDDto EditingWorkshop { get; private set; } = new WorkshopWithoutIDDto();

    [Inject]
    public IWorkshopClient WorkshopClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public bool IsCreating { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (string.IsNullOrEmpty(this.WorkshopID))
        {
            this.IsCreating = true;
            this.EditingWorkshop = new WorkshopWithoutIDDto()
            {
                StartTimestamp = DateTimeOffset.UtcNow
            };
        }
        else
        {
            this.IsCreating = false;
            this.EditingWorkshop = await this.WorkshopClient.GetWorkshopAsync(
                Guid.Parse(this.WorkshopID),
                CancellationToken.None);
        }
    }

    private async Task SubmitWorkshopAsync()
    {
        await Task.Delay(100);

        this.Navigation.NavigateTo("/ui/workshops");
    }
}
