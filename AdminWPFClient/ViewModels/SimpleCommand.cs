using System;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CommandSucceeded { get; set; }

        public Predicate<object> CanExecuteDelegate { get; set; }

        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate(parameter);
        } 

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
                CommandSucceeded = true;
            }
        }

        protected void RaiseCanExecuteChangedEvent()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, null);
        }
    }
}
