using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HappyCoding.AvaloniaHeadlessTesting.Views;

public partial class CounterViewModel : ObservableObject
{
    [ObservableProperty]  
    private int _currentCount;
    
    [RelayCommand]
    private void IncrementCount()
    {
        this.CurrentCount++;
    }
}