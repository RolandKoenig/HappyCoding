using HappyCoding.GRpcCommunication.ServerApp.Messages;
using RolandK.Avalonia.CommonControls;
using RolandK.Patterns;

namespace HappyCoding.GRpcCommunication.ServerApp;

internal class MainWindowViewModel : PropertyChangedBase
{
    private bool _isServerRunning;

    public MainWindowFrameStatus Status => _isServerRunning switch
    {
        true => MainWindowFrameStatus.Green,
        false => MainWindowFrameStatus.NeutralGray
    };

    public void OnMessageReceived(ServerStateChangedMessage message)
    {
        _isServerRunning = message.IsRunning;
        this.RaisePropertyChanged(nameof(this.Status));
    }
}
