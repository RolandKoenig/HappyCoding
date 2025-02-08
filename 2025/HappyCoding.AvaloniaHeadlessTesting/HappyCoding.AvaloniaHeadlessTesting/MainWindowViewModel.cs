using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaHeadlessTesting;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Avalonia App";

    [ObservableProperty]
    private int _inputInt;
}