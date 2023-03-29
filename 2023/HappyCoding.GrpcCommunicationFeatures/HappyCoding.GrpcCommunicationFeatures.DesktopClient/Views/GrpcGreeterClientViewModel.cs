using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient.Views;

public partial class GrpcGreeterClientViewModel : ObservableObject
{
    private readonly ILogger _logger;
    private readonly Greeter.GreeterClient _greeterClient;

    [ObservableProperty] 
    private string _greeterNameToSend = "Dummy name";

    [ObservableProperty]
    private string _greeterLastResponse = string.Empty;

    public static GrpcGreeterClientViewModel DesignTimeViewModel => new (null!, null!);

    public GrpcGreeterClientViewModel(
        ILogger<GrpcGreeterClientViewModel> logger,
        Greeter.GreeterClient greeterClient)
    {
        _logger = logger;
        _greeterClient = greeterClient;
    }

    [RelayCommand]
    public async void SendGreeterRequest()
    {
        try
        {
            var request = new HelloRequest()
            {
                Name = _greeterNameToSend
            };

            var response = await _greeterClient.SayHelloAsync(request);
            this.GreeterLastResponse = response.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling Greeter.SayHello");
        }
    }
}
