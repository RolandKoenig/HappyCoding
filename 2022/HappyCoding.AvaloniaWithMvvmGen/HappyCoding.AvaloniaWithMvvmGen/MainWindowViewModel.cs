using MvvmGen;

namespace HappyCoding.AvaloniaWithMvvmGen;

[ViewModel]
public partial class MainWindowViewModel
{
    [Property]
    private string _title;

    partial void OnInitialize()
    {
        this.Title = "⚡Avalonia with MvvmGen";
    }
}