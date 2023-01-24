using System;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace HappyCoding.AvaloniaWithMapsui.Shared;

public class OwnViewModelBase : ObservableObject, IAttachableViewModel
{
    private object? _associatedView;

    /// <inheritdoc />
    public object? AssociatedView
    {
        get => _associatedView;
        set
        {
            if (_associatedView == value) { return; }

            if (_associatedView != null)
            {
                _associatedView = null;
                this.OnViewDetached();
            }

            if (value != null)
            {
                _associatedView = value;
                this.OnViewAttached();
            }
        }
    }
    
    /// <inheritdoc />
    public event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;
    
    /// <inheritdoc />
    public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

    protected TViewService? TryGetViewService<TViewService>()
        where TViewService : class
    {
        var request = new ViewServiceRequestEventArgs(typeof(TViewService));
        this.ViewServiceRequest?.Invoke(this, request);
        return request.ViewService as TViewService;
    }

    protected TViewService GetViewService<TViewService>()
        where TViewService : class
    {
        var viewService = this.TryGetViewService<TViewService>();
        if (viewService == null)
        {
            throw new InvalidOperationException($"ViewService of type {typeof(TViewService).FullName} not found!");
        }
        return viewService;
    }
    
    protected virtual void OnViewAttached()
    {
        
    }
    
    protected virtual void OnViewDetached()
    {
        
    }
}