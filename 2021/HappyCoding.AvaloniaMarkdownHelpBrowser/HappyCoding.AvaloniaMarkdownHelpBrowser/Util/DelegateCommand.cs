using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Util
{
    public class DelegateCommand<T> : ICommand
    {
        private Func<T?, bool>? _canExecuteAction;
        private Action<T?> _executeAction;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<T?> executeAction)
        {
            _executeAction = executeAction;
        }

        public DelegateCommand(Action<T?> executeAction, Func<T?, bool> canExecuteAction)
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

            var castedParameter = default(T);
            if (parameter != null) { castedParameter = (T) parameter;}

            return _canExecuteAction(castedParameter);
        }

        public void Execute(object? parameter)
        {
            var castedParameter = default(T);
            if (parameter != null) { castedParameter = (T) parameter;}

            _executeAction(castedParameter);
        }
    }
}
