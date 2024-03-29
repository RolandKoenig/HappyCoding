﻿using System.Collections.ObjectModel;
using HappyCoding.ConsoleLogWindow.Application.Events;
using HappyCoding.ConsoleLogWindow.Application.Model;
using HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;
using HappyCoding.ConsoleLogWindow.Gui.Events;
using HappyCoding.ConsoleLogWindow.Gui.Util;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

internal class ProcessGroupsViewModel : ViewModelBase
{
    private readonly IDocumentModelProvider _documentModelProvider;
    private readonly IFirLibMessagePublisher _messagePublisher;

    private object? _selectedObject;
    private DocumentModel? _documentModel;

    public ObservableCollection<ProcessGroup> ProcessGroups => _documentModel?.ProcessGroups ?? new ObservableCollection<ProcessGroup>();

    public object? SelectedObject
    {
        get => _selectedObject;
        set
        {
            if (_selectedObject != value)
            {
                var prevSelectedObject = _selectedObject;

                _selectedObject = value;
                this.RaisePropertyChanged(nameof(this.SelectedObject));

                _messagePublisher.BeginPublish(
                    new ProcessInfoSelectionChangedEvent()
                    {
                        SelectedProcessOld = prevSelectedObject as ProcessInfo,
                        SelectedProcessNew = _selectedObject as ProcessInfo
                    });
            }
        }
    }

    public ProcessGroupsViewModel(
        IDocumentModelProvider documentModelProvider,
        IFirLibMessagePublisher messagePublisher)
    {
        _documentModelProvider = documentModelProvider;
        _messagePublisher = messagePublisher;
    }

    /// <inheritdoc />
    public override void ViewLoaded(IView view)
    {
        base.ViewLoaded(view);

        _documentModel = _documentModelProvider.GetCurrentDocumentModel();
        this.RaisePropertyChanged(nameof(this.ProcessGroups)); 
    }

    private void OnMessageReceived(CurrentDocumentChangedEvent eventData)
    {
        _documentModel = _documentModelProvider.GetCurrentDocumentModel();
        this.RaisePropertyChanged(nameof(this.ProcessGroups)); 
    }
}
