using System;
using System.Windows.Input;

namespace TicTacToe
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;
        private Func<bool> _enabled;
        public Command(Action execute, Func<bool> enabled)
        {
            _execute = execute;
            _enabled = enabled;
        }
        public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, null);
        public bool CanExecute(object parameter) => _enabled();
        public void Execute(object parameter) => _execute();
    }
}