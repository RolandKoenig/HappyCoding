using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.AspNetCore.Components;

namespace HappyCoding.GrpcCommunicationFeatures.AspNetClient.Pages;

public partial class GrpcClientPage
{
    [Inject]
    private Greeter.GreeterClient GreeterClient
    {
        get;
        set;
    } = null!;

    public string LastAnswer { get; private set; } = "-";

    public async Task CallGreeterClientAsync()
    {
        var result = await this.GreeterClient.SayHelloAsync(new HelloRequest() {Name = "Test-Name"});
        this.LastAnswer = result.Message;

        this.StateHasChanged();
    }
}
