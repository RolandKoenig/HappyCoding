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

    public ImmutableArray<WorkshopShortInfoDto> WorkshopInfos { get; set; } = ImmutableArray<WorkshopShortInfoDto>.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await SubmitQueryAsync();
    }

    private async Task SubmitQueryAsync()
    {
        this.WorkshopInfos = await this.WorkshopClient.SearchWorkshopsAsync(
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
        await this.WorkshopClient.DeleteWorkshopAsync(
            workshop.ID,
            CancellationToken.None);
        await this.SubmitQueryAsync();
    }
}
