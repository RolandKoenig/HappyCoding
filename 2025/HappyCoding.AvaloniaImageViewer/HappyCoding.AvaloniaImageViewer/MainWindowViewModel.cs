using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.AvaloniaImageViewer.Util;
using RolandK.AvaloniaExtensions.ViewServices;

namespace HappyCoding.AvaloniaImageViewer;

public partial class MainWindowViewModel : OwnViewModelBase
{
    [ObservableProperty]
    private Bitmap? _currentBitmap;
    
    public string Title => "🧪 RolandK ImageViewer (Prototype)";
    
    [RelayCommand]
    public async Task LoadImageAsync()
    {
        var srvOpenFile = this.GetViewService<IOpenFileViewService>();
        
        var selectedFile = await srvOpenFile.ShowOpenFileDialogAsync([], "Load Image");
        if (string.IsNullOrEmpty(selectedFile))
        {
            return;
        }

        this.CurrentBitmap = await Task.Run(() => new Bitmap(selectedFile));
    }
}