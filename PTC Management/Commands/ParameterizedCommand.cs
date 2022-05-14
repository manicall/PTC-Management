using System;
using System.Windows.Input;

namespace PTC_Management.Commands
{

    public class ParameterizedCommand<T> : ICommand
    {

        Action<T> _TargetExecuteMethod;
        Func<T,bool> _TargetCanExecuteMethod;

        //Action _TargetExecuteMethod;
        //Func<bool> _TargetCanExecuteMethod;

        public ParameterizedCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public ParameterizedCommand(Action<T> executeMethod,
            Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }

    }
}