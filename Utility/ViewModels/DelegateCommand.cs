using System;
using System.Windows.Input;

namespace Utility.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeAction;
        private readonly Func<T, bool> canExecuteFunction;

        #region ICommand
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
            => canExecuteFunction.Invoke((T)parameter);

        public void Execute(object parameter)
        {
            T typedParameter = (T)parameter;
            if (canExecuteFunction.Invoke(typedParameter))
                executeAction?.Invoke(typedParameter);
        }
        #endregion

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunction = null)
        {
            this.executeAction = executeAction;
            this.canExecuteFunction = canExecuteFunction ?? (parameter => true);
        }

        public void OnCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, new EventArgs());
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunction = null)
            : base(executeAction, canExecuteFunction)
        { }
    }
}
