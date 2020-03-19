using System;
using System.Windows.Input;

namespace ChronometerWPF.Commands
{
    public class DelegateCommand : ICommand
    {
        #region Fields

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        #endregion

        #region Public methods

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion
    }
}
