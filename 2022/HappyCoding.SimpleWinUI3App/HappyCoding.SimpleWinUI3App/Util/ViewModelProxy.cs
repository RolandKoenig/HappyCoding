using Microsoft.UI.Xaml;

namespace HappyCoding.SimpleWinUI3App.Util;

internal class ViewModelProxy
{
    public ViewModelBase ViewModel { get; }

    public FrameworkElement HostElement { get; }

    public bool IsLoaded { get; private set; }

    public ViewModelProxy(
        ViewModelBase viewModel,
        FrameworkElement hostElement)
    {
        this.ViewModel = viewModel;
        this.HostElement = hostElement;

        this.HostElement.Loaded += this.PageLoaded;
        this.HostElement.Unloaded += this.PageUnloaded;
    }

    public virtual void PageLoaded(object sender, RoutedEventArgs e)
    {
        if (!this.IsLoaded)
        {
            this.IsLoaded = true;
            this.ViewModel.OnHostLoaded();
        }
    }

    public virtual void PageUnloaded(object sender, RoutedEventArgs e)
    {
        if (this.IsLoaded)
        {
            this.IsLoaded = false;
            this.ViewModel.OnHostUnloaded();
        }
    }
}
