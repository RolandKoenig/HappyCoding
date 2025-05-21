using HappyCoding.AvaloniaImageViewer.ViewServices.Default;
using RolandK.AvaloniaExtensions.Mvvm.Controls;

namespace HappyCoding.AvaloniaImageViewer;

public partial class MainWindow : MvvmWindow
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.ViewServices.Add(new DefaultFileDialogViewService(this));
    }
}