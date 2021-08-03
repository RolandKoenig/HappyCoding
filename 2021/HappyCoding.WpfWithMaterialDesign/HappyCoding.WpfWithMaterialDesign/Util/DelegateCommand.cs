using System;
using System.Windows.Input;

namespace HappyCoding.WpfWithMaterialDesign.Util
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool>? _canExecuteAction;
        private readonly Action _executeAction;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecuteAction == null) { return true; }
            return _canExecuteAction();
        }

        public void Execute(object? parameter)
        {
            _executeAction();
        }
    }
}
