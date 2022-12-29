namespace RolandK.Patterns.Mvvm;

public class CloseWindowRequestEventArgs
{
    public object? DialogResult { get; }

    public CloseWindowRequestEventArgs(object? dialogResult)
    {
        this.DialogResult = dialogResult;
    }
}