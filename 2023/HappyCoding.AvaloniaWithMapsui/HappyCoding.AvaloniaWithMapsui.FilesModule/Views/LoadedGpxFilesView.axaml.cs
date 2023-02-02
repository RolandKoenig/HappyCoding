using Avalonia.Markup.Xaml;
using RolandK.AvaloniaExtensions.Mvvm.Markup;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule.Views;

public partial class LoadedGpxFilesView : MvvmUserControl
{
    public LoadedGpxFilesView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}