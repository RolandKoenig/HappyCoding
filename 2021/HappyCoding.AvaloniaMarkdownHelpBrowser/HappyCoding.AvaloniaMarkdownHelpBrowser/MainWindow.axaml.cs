using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

            var ctrlMarkdownViewer = this.Find<MarkdownScrollViewer>("CtrlMarkdownViewer");
            if(ctrlMarkdownViewer != null)
            {
                ctrlMarkdownViewer.Engine.BitmapLoader = new ResourceBitmapLoader(Assembly.GetExecutingAssembly());
            }
        }
    }
}
