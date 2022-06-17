using System;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

internal class ViewModelBase
{
    protected IView? View { get; private set; }

    public bool IsAttachedToView => this.View != null;

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
}