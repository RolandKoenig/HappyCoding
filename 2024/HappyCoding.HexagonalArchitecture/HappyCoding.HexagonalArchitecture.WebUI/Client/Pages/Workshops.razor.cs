using System.Collections.Immutable;
using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.WebUI.Client.Services;
using Microsoft.AspNetCore.Components;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client.Pages;

public partial class Workshops
{
    public WorkshopsSearchFormData SearchFormData { get; set; } = new WorkshopsSearchFormData();

    [Inject] public IWorkshopClient WorkshopClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public IReadOnlyList<WorkshopShortInfoDto> WorkshopInfos { get; set; } = Array.Empty<WorkshopShortInfoDto>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await SubmitQueryAsync();
    }

    private async Task SubmitQueryAsync()
    {
        this.WorkshopInfos = await this.WorkshopClient.SearchAsync(
            SearchFormData.QueryString, 
            CancellationToken.None);
        this.StateHasChanged();
    }

    private async Task ResetQueryAsync()
    {
        this.SearchFormData.QueryString = "";
        await this.SubmitQueryAsync();
    }

    private void CreateWorkshop()
    {
        this.Navigation.NavigateTo("/ui/workshopdetail");
    }

    private void EditWorkshop(WorkshopShortInfoDto workshop)
    {
        this.Navigation.NavigateTo($"/ui/workshopdetail/{workshop.ID}");
    }

    private async Task DeleteWorkshopAsync(WorkshopShortInfoDto workshop)
    {
        await this.WorkshopClient.WorkshopsDELETEAsync(
            workshop.ID,
            CancellationToken.None);
        await this.SubmitQueryAsync();
    }
}
