using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaInBrowser.Data;
using HappyCoding.AvaloniaInBrowser.Services;
using RolandK.AvaloniaExtensions.ViewServices;

namespace HappyCoding.AvaloniaInBrowser.Views;

public partial class MainViewViewModel : OwnViewModelBase
{
    public static MainViewViewModel DesignViewModel => new(new BogusTestDataGenerator());
    
    private readonly ITestDataGenerator _testDataGenerator;

    [ObservableProperty] 
    private string _title = string.Empty;

    [ObservableProperty]
    private ObservableCollection<UserData> _dataRows = new()
    {
        new UserData()
    };

    public MainViewViewModel(ITestDataGenerator testDataGenerator)
    {
        _testDataGenerator = testDataGenerator;

        this.Title = "RolandK.AvaloniaExtension Test Application";
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }

    [RelayCommand]
    private void RecreateTestData()
    {
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }

    [RelayCommand]
    private async Task ShowDummyMessageBoxAsync()
    {
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();

        await srvMessageBox.ShowAsync(
            "Show dummy MessageBox",
            "Dummy message",
            MessageBoxButtons.Ok);
    }
}