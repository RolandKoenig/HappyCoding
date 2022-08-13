using System.Collections.Immutable;
using System.Net.Http.Json;
using HappyCoding.HexagonalArchitecture.Dtos;
using Microsoft.AspNetCore.WebUtilities;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client.Services;

public class WorkshopHttpClient : IWorkshopClient
{
    private readonly HttpClient _httpClient;

    public WorkshopHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WorkshopDto> CreateWorkshopAsync(WorkshopWithoutIDDto workshop, CancellationToken cancellationToken)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync("Workshops", workshop, cancellationToken);
        var result = await httpResponse.Content.ReadFromJsonAsync<WorkshopDto>(cancellationToken: cancellationToken);

        if (result == null)
        {
            throw new ApplicationException("Unable to deserialize response from server!");
        }

        return result;
    }

    public async Task<WorkshopDto> UpdateWorkshopAsync(WorkshopDto workshop, CancellationToken cancellationToken)
    {
        var httpResponse = await _httpClient.PutAsJsonAsync("Workshops", workshop, cancellationToken);
        var result = await httpResponse.Content.ReadFromJsonAsync<WorkshopDto>(cancellationToken: cancellationToken);
        
        if (result == null)
        {
            throw new ApplicationException("Unable to deserialize response from server!");
        }
        
        return result;
    }

    public async Task DeleteWorkshopAsync(Guid workshopID, CancellationToken cancellationToken)
    {
        var httpResponse = await _httpClient.DeleteAsync($"Workshops/{workshopID}", cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
    }

    public async Task<ImmutableArray<WorkshopShortInfoDto>> SearchWorkshopsAsync(string queryString, CancellationToken cancellationToken)
    {
        var requestUrl = string.IsNullOrEmpty(queryString)
            ? "Workshops"
            : QueryHelpers.AddQueryString("Workshops", "query", queryString);

        var result = await _httpClient.GetFromJsonAsync<WorkshopShortInfoDto[]>(
            requestUrl,
            cancellationToken);

        if (result == null)
        {
            throw new ApplicationException("Unable to deserialize response from server!");
        }

        return ImmutableArray.Create(result);
    }

    public async Task<WorkshopDto> GetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken)
    {
        var result = await _httpClient.GetFromJsonAsync<WorkshopDto>($"Workshops/{workshopID}", cancellationToken);

        if (result == null)
        {
            throw new ApplicationException("Unable to deserialize response from server!");
        }

        return result;
    }
}
