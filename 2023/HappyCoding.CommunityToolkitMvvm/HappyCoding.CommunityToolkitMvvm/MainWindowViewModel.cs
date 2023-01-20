using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.CommunityToolkitMvvm.Data;

namespace HappyCoding.CommunityToolkitMvvm;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IMessenger _messenger;
    
    [ObservableProperty]
    private string _title = "CommunityToolkitMvvm - Testing application";

    [ObservableProperty]
    private ObservableCollection<UserData> _dataRows;

    [ObservableProperty]
    private UserData? _selectedUserData;

    public static MainWindowViewModel DesignViewModel
    {
        get => new MainWindowViewModel(
            new WeakReferenceMessenger());
    }
    
    public MainWindowViewModel(IMessenger messenger)
    {
        _messenger = messenger;
        
        this.DataRows = new ObservableCollection<UserData>(
            UserData.CreateFakeData(50));
    }

    [RelayCommand]
    public void GenerateNewData()
    {
        this.DataRows = new ObservableCollection<UserData>(
            UserData.CreateFakeData(50));
        this.SelectedUserData = null;
    }

    partial void OnSelectedUserDataChanged(UserData? value)
    {
        _messenger.Send(new UserDataSelectedMessage(value));
    }
}