using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TemplateGallery.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        // event EventHandler? CanExecuteChanged;
        private readonly Action<T> _Execute;
        private readonly Predicate<T> _CanExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute != null || _CanExecute((T)parameter); 
        }

        public void Execute(object? parameter)
        {
            _Execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
