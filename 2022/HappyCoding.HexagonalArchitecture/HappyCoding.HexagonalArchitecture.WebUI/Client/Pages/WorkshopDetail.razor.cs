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
                StartTimestamp = DateTimeOffset.UtcNow,
                Protocol = new List<ProtocolEntryDto>()
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
        if (this.IsCreating)
        {
            await this.WorkshopClient.CreateWorkshopAsync(
                this.EditingWorkshop,
                CancellationToken.None);
        }
        else
        {
            await this.WorkshopClient.UpdateWorkshopAsync(
                new WorkshopDto()
                {
                    ID = Guid.Parse(this.WorkshopID),
                    Project = EditingWorkshop.Project,
                    Protocol = EditingWorkshop.Protocol,
                    StartTimestamp = EditingWorkshop.StartTimestamp,
                    Title = EditingWorkshop.Title
                },
                CancellationToken.None);
        }

        this.Navigation.NavigateTo("/ui/workshops");
    }

    private void CancelEditing()
    {
        this.Navigation.NavigateTo("/ui/workshops");
    }
}
