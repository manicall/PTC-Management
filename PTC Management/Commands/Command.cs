﻿using System;
using System.Windows.Input;

namespace PTC_Management.Commands
{
    // класс для выполнения команды с параметром
    public class Command<T> : ICommand
    {
        // действие, которое следует выполнить
        Action<T> _TargetExecuteMethod;
        // предикат, определяющий разрешено ли выполнять действие
        Func<T, bool> _TargetCanExecuteMethod;

        // конструктор
        public Command(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        // конструктор
        public Command(Action<T> executeMethod,
            Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        // реализация метода интерфейса ICommand
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

        // реализация события интерфейса ICommand
        public event EventHandler CanExecuteChanged = delegate {};

        // реализация метода интерфейса ICommand
        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }
    }

    // класс для выполнения команды без параметра
    public class Command : ICommand
    {
        // действие, которое следует выполнить
        Action _TargetExecuteMethod;
        // предикат, определяющий разрешено ли выполнять действие
        Func<bool> _TargetCanExecuteMethod;

        // конструктор
        public Command(Action targetExecuteMethod)
        {
            _TargetExecuteMethod = targetExecuteMethod;
        }

        // конструктор
        public Command(Action targetExecuteMethod,
            Func<bool> targetCanExecuteMethod)
        {
            _TargetExecuteMethod = targetExecuteMethod;
            _TargetCanExecuteMethod = targetCanExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        // реализация метода интерфейса ICommand
        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        // реализация события интерфейса ICommand
        public event EventHandler CanExecuteChanged = delegate { };

        // реализация метода интерфейса ICommand
        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }
    }
}