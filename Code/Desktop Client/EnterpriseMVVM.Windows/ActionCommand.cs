
namespace EnterpriseMVVM.Windows
{
    using System;
    using System.Windows.Input;
    public class ActionCommand: ICommand
    {

        private readonly Action<Object> action;
        private readonly Predicate<Object> predicate;

        public ActionCommand(Action<Object> action): this(action, null)
        {
        }

        public ActionCommand(Action<Object> action, Predicate<Object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "You must specify and Action<T>.");
            }
            this.action = action;
            this.predicate = predicate;

        }


        #region ICommand implementation

        public bool CanExecute(object parameter)
        {
            if (predicate == null)
                return true;

            return predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }


        public void Execute()
        {
            Execute(null);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}
