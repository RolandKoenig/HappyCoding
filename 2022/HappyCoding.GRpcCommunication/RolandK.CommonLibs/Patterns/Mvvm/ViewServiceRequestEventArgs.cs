namespace RolandK.Patterns.Mvvm;

public class ViewServiceRequestEventArgs
{
    public Type ViewServiceType { get; }

    public object? ViewService { get; set; }

    public ViewServiceRequestEventArgs(Type viewServiceType)
    {
        this.ViewServiceType = viewServiceType;
    }
}