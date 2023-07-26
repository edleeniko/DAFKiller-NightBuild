using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DAFKiller_NightBuild.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canexecuteAction;

        //Constructor
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canexecuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canexecuteAction)
        {
            _executeAction = executeAction;
            _canexecuteAction = canexecuteAction;
        }

        //Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods
        public bool CanExecute(object parameter)
        {
            return _canexecuteAction == null ? true : _canexecuteAction(parameter) ;
        }

        public void Execute(object parameter)
        {
           _executeAction(parameter);
        }
    }
}
