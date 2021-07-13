using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HappyCoding.AvaloniaMarkdownHelpBrowser.Util;
using Markdown.Avalonia;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            var viewModel = new MainWindowViewModel();

            var ctrlMarkdownViewer = this.Find<MarkdownScrollViewer>("CtrlMarkdownViewer");
            if(ctrlMarkdownViewer != null)
            {
                ctrlMarkdownViewer.Engine.BitmapLoader = new ResourceBitmapLoader(Assembly.GetExecutingAssembly());
                ctrlMarkdownViewer.Engine.HyperlinkCommand = viewModel.CommandNavigateTo;
            }

            this.DataContext = viewModel;
        }
    }
}
