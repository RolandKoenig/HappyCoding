using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.CommunityToolkitMvvm.Data;

namespace HappyCoding.CommunityToolkitMvvm;

public partial class SelectedDataRowViewModel : ObservableObject, IRecipient<UserDataSelectedMessage>
{
    private readonly IMessenger _messenger;

    [ObservableProperty]
    private UserData? _selectedUserData;
    
    public SelectedDataRowViewModel(IMessenger messenger)
    {
        _messenger = messenger;
        _messenger.RegisterAll(this);
    }
    
    public static SelectedDataRowViewModel DesignViewModel
    {
        get => new SelectedDataRowViewModel(
            new WeakReferenceMessenger());
    }

    /// <inheritdoc />
    public void Receive(UserDataSelectedMessage message)
    {
        this.SelectedUserData = message.SelectedUserData;
    }
}