using System.IO;
using System.Text;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Gui.Model;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;
using HappyCoding.ConsoleLogWindow.Gui.ViewServices.FileDialogs;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly DesktopDocumentModelProvider _documentModelProvider;
    private readonly IFirLibMessagePublisher _messagePublisher;
    private readonly IFirLibMessageSubscriber _messageSubscriber;

    private IView? _view;
    private string? _loadedDocumentFileName;
    private DocumentModel? _loadedDocument;

    public DelegateCommand Command_New { get; private set; }

    public DelegateCommand Command_Open { get; private set; }

    public DelegateCommand Command_Close { get; private set; }

    public DelegateCommand Command_Save { get; private set; }

    public DelegateCommand Command_SaveAs { get; private set; }

    public string Title
    {
        get
        {
            var strBuilder = new StringBuilder(64);
            strBuilder.Append("ConsoleLogWindow");
            if (!string.IsNullOrEmpty(_loadedDocumentFileName))
            {
                strBuilder.Append(" - ");
                strBuilder.Append(Path.GetFileName(_loadedDocumentFileName));
            }
            return strBuilder.ToString();
        }
    }

    public MainWindowViewModel(
        IDocumentModelProvider documentModelProvider,
        IFirLibMessagePublisher messagePublisher,
        IFirLibMessageSubscriber messageSubscriber)
    {
        _documentModelProvider = (DesktopDocumentModelProvider)documentModelProvider;
        _messagePublisher = messagePublisher;
        _messageSubscriber = messageSubscriber;

        this.Command_SaveAs = new DelegateCommand(this.SaveAs);
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    { 
        base.ViewLoaded(view);

        _view = view;
    }

    /// <inheritdoc />
    public override void ViewUnloaded()
    {
        base.ViewUnloaded();
    }

    public void DoSomething()
    {
        
    }

    private async void SaveAs()
    {
        var dlgSaveFile = _view.TryGetViewService<ISaveFileViewService>();
        var file = dlgSaveFile.ShowSaveFileDialogAsync(
            new[] {new FileDialogFilter("Process list", ".processList")},
            ".processList");
    }
}