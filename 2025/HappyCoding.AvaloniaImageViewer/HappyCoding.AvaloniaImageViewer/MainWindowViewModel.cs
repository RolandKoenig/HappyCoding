using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaImageViewer;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private Bitmap? _currentBitmap;
    
    public string Title => "🧪 RolandK ImageViewer (Prototype)";
}