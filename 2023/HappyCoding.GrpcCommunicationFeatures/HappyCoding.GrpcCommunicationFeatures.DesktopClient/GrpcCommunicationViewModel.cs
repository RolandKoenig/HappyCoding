using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.DesktopClient;

public partial class GrpcCommunicationViewModel : ObservableObject
{
    private readonly Greeter.GreeterClient _greeterClient;

    [ObservableProperty]
    private string _greeterNameToSend = string.Empty;

    [ObservableProperty]
    private string _greeterLastResponse = string.Empty;

    public static GrpcCommunicationViewModel DesignTimeViewModel => new (null!);

    public GrpcCommunicationViewModel(Greeter.GreeterClient greeterClient)
    {
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
            this.GreeterLastResponse = $"Exception of type ({ex.GetType().FullName}): {ex.Message}";
        }
    }
}
