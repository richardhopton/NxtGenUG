using System;
using System.Windows.Input;

namespace WPFClient.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecuteMethod;
        private readonly Action _executeMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null && canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod", "Cannot be null");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion

        public bool CanExecute()
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod();
        }

        public void Execute()
        {
            if (_executeMethod == null) return;
            _executeMethod();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}