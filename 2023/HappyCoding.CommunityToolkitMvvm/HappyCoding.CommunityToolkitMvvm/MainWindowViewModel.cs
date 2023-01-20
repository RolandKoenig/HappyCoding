using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.CommunityToolkitMvvm.Data;

namespace HappyCoding.CommunityToolkitMvvm;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "CommunityToolkitMvvm - Testing application";

    [ObservableProperty]
    private ObservableCollection<UserData> _dataRows;

    public static MainWindowViewModel DesignViewModel
    {
        get => new MainWindowViewModel();
    }
    
    public MainWindowViewModel()
    {
        this.DataRows = new ObservableCollection<UserData>(
            UserData.CreateFakeData(50));
    }

    [RelayCommand]
    public void GenerateNewData()
    {
        this.DataRows = new ObservableCollection<UserData>(
            UserData.CreateFakeData(50));
    }
}