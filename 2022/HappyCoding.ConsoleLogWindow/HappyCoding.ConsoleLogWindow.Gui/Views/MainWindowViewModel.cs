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

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly StartupArguments _startupArguments;
    private readonly IDocumentModelProvider _documentModelProvider;
    private readonly IUseCaseExecutor _useCaseExecutor;

    private IView? _view;
    private string? _loadedDocumentFileName;
    private bool _containsUnsavedChanges;

    public DelegateCommand Command_Bootstrap { get; }

    public DelegateCommand Command_New { get; }

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
            }
            else
            {
                strBuilder.Append(" - ");
                strBuilder.Append("New");
            }

            if (_containsUnsavedChanges)
            {
                strBuilder.Append('*');
            }

            return strBuilder.ToString();
        }
    }

    public DocumentModel CurrentDocument => _documentModelProvider.GetCurrentDocumentModel();

    public MainWindowViewModel(
        StartupArguments startupArguments,
        IDocumentModelProvider documentModelProvider,
        IUseCaseExecutor useCaseExecutor)
    {
        _startupArguments = startupArguments;
        _documentModelProvider = documentModelProvider;
        _useCaseExecutor = useCaseExecutor;

        this.Command_Bootstrap = new DelegateCommand(this.Bootstrap);
        this.Command_New = new DelegateCommand(this.New);
        this.Command_Open = new DelegateCommand(this.Open);
        this.Command_Close = new DelegateCommand(this.Close);
        this.Command_Save = new DelegateCommand(this.Save);
        this.Command_SaveAs = new DelegateCommand(this.SaveAs);
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    {
        base.ViewLoaded(view);

        _view = view;
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

    private async void Bootstrap()
    {
        if (string.IsNullOrEmpty(_startupArguments.InitialFile))
        {
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<LoadDocumentFromFileUseCase, string>(_startupArguments.InitialFile);

        _loadedDocumentFileName = _startupArguments.InitialFile;
        _containsUnsavedChanges = false;
        
        this.TriggerUIUpdate();
    }

    private void New()
    {
        _documentModelProvider.CloseAndCreateNew();

        _loadedDocumentFileName = null;
        _containsUnsavedChanges = false;

        this.TriggerUIUpdate();
    }

    private void Close()
    {
        _documentModelProvider.CloseAndCreateNew();

        _loadedDocumentFileName = null;
        _containsUnsavedChanges = false;

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

        _containsUnsavedChanges = false;

        this.TriggerUIUpdate();
    }

    private async void SaveAs()
    {
        if (_view == null) { return; }

        var dlgSaveFile = _view.GetViewService<ISaveFileViewService>();
        var targetFile = await dlgSaveFile.ShowSaveFileDialogAsync(
            new[] { new FileDialogFilter("Process list", "processList") },
            "processList");
        if (string.IsNullOrEmpty(targetFile))
        {
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<SaveDocumentToFileUseCase, DocumentModel, string>(
            _documentModelProvider.GetCurrentDocumentModel(),
            targetFile);

        _loadedDocumentFileName = targetFile;
        _containsUnsavedChanges = false;

        this.TriggerUIUpdate();
    }

    private async void Open()
    {
        if (_view == null) { return; }

        var dlgOpenFile = _view.GetViewService<IOpenFileViewService>();
        var sourceFile = await dlgOpenFile.ShowOpenFileDialogAsync(
            new[] { new FileDialogFilter("Process list", "processList") },
            "Load process list");
        if (string.IsNullOrEmpty(sourceFile))
        {
            return;
        }

        await _useCaseExecutor.ExecuteUseCaseAsync<LoadDocumentFromFileUseCase, string>(sourceFile);

        _loadedDocumentFileName = sourceFile;
        _containsUnsavedChanges = false;
        
        this.TriggerUIUpdate();
    }

    private void OnMessageReceived(CurrentDocumentContentChangedEvent eventData)
    {
        _containsUnsavedChanges = true;

        this.TriggerUIUpdate();
    }

    private void OnMessageReceived(CurrentDocumentChangedEvent eventData)
    {
        _containsUnsavedChanges = false;

        this.TriggerUIUpdate();
    }
}