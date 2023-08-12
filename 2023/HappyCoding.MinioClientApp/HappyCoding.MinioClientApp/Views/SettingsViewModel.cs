using CommunityToolkit.Mvvm.ComponentModel;
using HappyCoding.MinioClientApp.MinioHandling;

namespace HappyCoding.MinioClientApp.Views;

public class SettingsViewModel : ObservableObject
{
    public MinioInterfaceConfiguration Configuration { get; }

    public static SettingsViewModel DesignViewModel { get; } = new SettingsViewModel(new MinioInterfaceConfiguration());
    
    public SettingsViewModel(MinioInterfaceConfiguration configuration)
    {
        this.Configuration = configuration;
    }
}