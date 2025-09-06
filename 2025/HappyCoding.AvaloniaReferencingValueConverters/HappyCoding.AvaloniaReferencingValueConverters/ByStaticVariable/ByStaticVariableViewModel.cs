using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaReferencingValueConverters.ByStaticVariable;

public partial class ByStaticVariableViewModel : ObservableObject
{
    [ObservableProperty]
    private string _versionString = "1.0.0.0";
}