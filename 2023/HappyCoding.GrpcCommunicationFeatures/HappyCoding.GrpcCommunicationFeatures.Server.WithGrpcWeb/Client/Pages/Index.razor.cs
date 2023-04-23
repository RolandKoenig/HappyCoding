using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.AspNetCore.Components;

namespace HappyCoding.GrpcCommunicationFeatures.Server.WithGrpcWeb.Client.Pages;

public partial class Index
{
    [Inject]
    public Greeter.GreeterClient? GreeterClient { get; set; }

    public string LastResponse { get; set; } = string.Empty;

    public async void GreetAsync()
    {
        if (this.GreeterClient == null) { return; }

        var response = await this.GreeterClient.SayHelloAsync(new HelloRequest() { Name = "BlazorClient" });
        this.LastResponse = response?.Message ?? string.Empty;
        
        this.StateHasChanged();
    }
}