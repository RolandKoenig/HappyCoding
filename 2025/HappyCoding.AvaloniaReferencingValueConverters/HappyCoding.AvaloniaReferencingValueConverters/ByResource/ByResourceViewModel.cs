using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaReferencingValueConverters.ByResource;

public partial class ByResourceViewModel : ObservableObject
{
    [ObservableProperty]
    private string _versionString = "1.0.0.0";
}