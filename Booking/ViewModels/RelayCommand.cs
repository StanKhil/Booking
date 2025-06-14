﻿using System.Windows.Input;

namespace Booking.ViewModels
{
    class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => execute(parameter);
    }
}
