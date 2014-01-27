
using System;
using System.Windows.Input;

namespace ListaSpesa.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Func<object, bool> _canExecuteMethod;


        public DelegateCommand(Action<object> executeMethod, Func<object,bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod(parameter);
        }

        public event System.EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged!=null)
            {
                CanExecuteChanged(this, null);
            }
        }

        public void Execute(object parameter)
        {
            if (_executeMethod == null) return;
            _executeMethod(parameter);
        }
    }
}
