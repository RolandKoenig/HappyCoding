using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaStyleTransitions;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Greeting))]
    [property:Required]
    private string _name = string.Empty;

    public string Greeting => string.IsNullOrEmpty(this.Name)
        ? string.Empty
        : $"Hello, {this.Name}!";
}