using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient;

public partial class GrpcCommunicationViewModel : ObservableObject
{
    private readonly ILogger _logger;
    private readonly Greeter.GreeterClient _greeterClient;

    [ObservableProperty]
    private string _greeterNameToSend = string.Empty;

    [ObservableProperty]
    private string _greeterLastResponse = string.Empty;

    public static GrpcCommunicationViewModel DesignTimeViewModel => new (null!, null!);

    public GrpcCommunicationViewModel(
        ILogger<GrpcCommunicationViewModel> logger,
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
