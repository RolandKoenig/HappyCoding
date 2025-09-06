using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaReferencingValueConverters.ByMarkupExtension;

public partial class ByMarkupExtensionViewModel : ObservableObject
{
    [ObservableProperty]
    private string _versionString = "1.0.0.0";
}