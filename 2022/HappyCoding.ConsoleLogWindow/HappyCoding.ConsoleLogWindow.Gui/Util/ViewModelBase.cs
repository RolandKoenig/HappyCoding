using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

public class ViewModelBase : INotifyPropertyChanged
{
    protected IView? View { get; private set; }

    public bool IsAttachedToView => this.View != null;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected IView EnsureViewAttached()
    {
        if (View == null)
        {
            throw new InvalidOperationException($"No view attached to viewmodel of type {this.GetType().FullName}!");
        }

        return View;
    }

    public virtual void ViewLoaded(IView view)
    {
        this.View = view;
    }

    public virtual void ViewUnloaded()
    {
        this.View = null;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}