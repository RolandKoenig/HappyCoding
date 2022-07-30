using System.IO;
using System.Text;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Gui.Util.ViewServices;
using HappyCoding.ConsoleLogWindow.Gui.ViewServices.FileDialogs;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly IDocumentModelProvider _documentModelProvider;
    private readonly IFirLibMessagePublisher _messagePublisher;
    private readonly IFirLibMessageSubscriber _messageSubscriber;
    private readonly IUseCaseExecutor _useCaseExecutor;

    private IView? _view;
    private string? _loadedDocumentFileName;
    private bool _containsUnsafedChanges;
    private DocumentModel? _loadedDocument;

    public DelegateCommand Command_New { get; private set; }

    public DelegateCommand Command_Open { get; }

    public DelegateCommand Command_Close { get; }

    public DelegateCommand Command_Save { get; }

    public DelegateCommand Command_SaveAs { get; }

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

                if (_containsUnsafedChanges)
                {
                    strBuilder.Append('*');
                }
            }
            return strBuilder.ToString();
        }
    }

    public MainWindowViewModel(
        IDocumentModelProvider documentModelProvider,
        IFirLibMessagePublisher messagePublisher,
        IFirLibMessageSubscriber messageSubscriber,
        IUseCaseExecutor useCaseExecutor)
    {
        _documentModelProvider = documentModelProvider;
        _messagePublisher = messagePublisher;
        _messageSubscriber = messageSubscriber;
        _useCaseExecutor = useCaseExecutor;

        Command_New = new DelegateCommand(New);
        Command_Open = new DelegateCommand(Open);
        Command_Close = new DelegateCommand(
            Close,
            () => _loadedDocument != null && _loadedDocument.ProcessGroups.Count > 0);
        Command_Save = new DelegateCommand(
            Save,
            () => _loadedDocument != null && _loadedDocument.ProcessGroups.Count > 0);
        Command_SaveAs = new DelegateCommand(
            SaveAs,
            () => _loadedDocument != null && _loadedDocument.ProcessGroups.Count > 0);
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

    private void TriggerUIUpdate()
    {
        this.Command_New.RaiseCanExecuteChanged();
        this.Command_Open.RaiseCanExecuteChanged();
        this.Command_Close.RaiseCanExecuteChanged();
        this.Command_Save.RaiseCanExecuteChanged();
        this.Command_SaveAs.RaiseCanExecuteChanged();

        this.RaisePropertyChanged(nameof(this.Title));
    }

    private void New()
    {
        _documentModelProvider.CloseAndCreateNew();

        _loadedDocumentFileName = null;
        _containsUnsafedChanges = false;

        this.TriggerUIUpdate();
    }

    private void Close()
    {
        _documentModelProvider.CloseAndCreateNew();

        _loadedDocumentFileName = null;
        _containsUnsafedChanges = false;

        this.TriggerUIUpdate();
    }

    private async void Save()
    {
        if (string.IsNullOrEmpty(_loadedDocumentFileName))
        {
            this.SaveAs();
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<SaveDocumentToFileUseCase, DocumentModel, string>(
            _documentModelProvider.GetCurrentDocumentModel(),
            _loadedDocumentFileName);

        _containsUnsafedChanges = false;

        this.TriggerUIUpdate();
    }

    private async void SaveAs()
    {
        var dlgSaveFile = _view.GetViewService<ISaveFileViewService>();
        var targetFile = await dlgSaveFile.ShowSaveFileDialogAsync(
            new[] { new FileDialogFilter("Process list", ".processList") },
            ".processList");
        if (string.IsNullOrEmpty(targetFile))
        {
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<SaveDocumentToFileUseCase, DocumentModel, string>(
            _documentModelProvider.GetCurrentDocumentModel(),
            targetFile);

        _loadedDocumentFileName = targetFile;
        _containsUnsafedChanges = false;

        this.TriggerUIUpdate();
    }

    private async void Open()
    {
        var dlgOpenFile = _view.GetViewService<IOpenFileViewService>();
        var sourceFile = await dlgOpenFile.ShowOpenFileDialogAsync(
            new[] { new FileDialogFilter("Process list", ".processList") },
            "Load process list");
        if (string.IsNullOrEmpty(sourceFile))
        {
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<LoadDocumentFromFileUseCase, string>(sourceFile);

        _containsUnsafedChanges = false;
        
        this.TriggerUIUpdate();
    }

    private void OnMessageReceived(CurrentDocumentContentChangedEvent @event)
    {
        _containsUnsafedChanges = true;

        this.TriggerUIUpdate();
    }
}