using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace HappyCoding.AvaloniaImageViewer.Util;

public class CompositeAsyncCommandState : PropertyChangedBase
{
    private readonly IAsyncRelayCommand[] _commands;
    
    public bool IsRunning => _commands.Any(c => c.IsRunning);
    
    public CompositeAsyncCommandState(params IAsyncRelayCommand[] commands)
    {
        _commands = commands;
        foreach (var actCommand in commands)
        {
            actCommand.PropertyChanged += OnChildCommand_PropertyChanged;
        }
    }

    private void OnChildCommand_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAsyncRelayCommand.IsRunning))
        {
            base.OnPropertyChanged(nameof(IsRunning));
        }
    }
}