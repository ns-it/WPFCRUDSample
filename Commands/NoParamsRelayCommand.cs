using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WPFCRUDSample.Commands
{
    public class NoParamsRelayCommand : ICommand
    {
        private Action _execute;
        public NoParamsRelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }

    }
}
