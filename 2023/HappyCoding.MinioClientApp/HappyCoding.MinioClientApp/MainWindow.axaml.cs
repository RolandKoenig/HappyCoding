using Avalonia;
using Avalonia.Markup.Xaml;
using RolandK.AvaloniaExtensions.Mvvm.Markup;

namespace HappyCoding.MinioClientApp
{
    public partial class MainWindow : MvvmWindow
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}