using System;
using System.Windows;
using System.Windows.Input;

namespace Core.MVVM
{
    public class AsyncCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        private readonly Action<AsyncCommandExecutionArgs> _callbAction;


        public event EventHandler CanExecuteChanged
        {

            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }

        }

        public AsyncCommand(Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = execute;
        }

        public AsyncCommand(Action<object> commandExecuted, Func<object, bool> commandCanExecute, Action<AsyncCommandExecutionArgs> callbAction = null)
        {
            if (commandExecuted == null) throw new ArgumentNullException(nameof(commandExecuted));
            if (commandCanExecute == null) throw new ArgumentNullException(nameof(commandCanExecute));
            _execute = commandExecuted;
            _canExecute = commandCanExecute;
            _callbAction = callbAction;
        }




        private void AsyncMethod(object parameter, Action<AsyncCommandExecutionArgs> callback)
        {
            var commandExecutionArgs = new AsyncCommandExecutionArgs();
            try
            {
                _execute.Invoke(parameter);

            }
            catch (Exception ex)
            {
                commandExecutionArgs.Error = ex;
            }
            finally
            {
                if (callback != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {

                        try
                        {
                            callback.Invoke(commandExecutionArgs);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }));
                }
                OnExecuted();
                Application.Current.Dispatcher.Invoke(new Action(CommandManager.InvalidateRequerySuggested));
            }
        }

        public event EventHandler Executed;

        private void OnExecuted()
        {
            try
            {
                Executed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Execute(object parameter, Action<AsyncCommandExecutionArgs> callback)
        {
            Action<object, Action<AsyncCommandExecutionArgs>> commandAction = AsyncMethod;
            commandAction.BeginInvoke(parameter, callback, commandAction.EndInvoke, null);
        }

        public void Execute(object parameter)
        {
            Execute(parameter, _callbAction);
        }

        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute.Invoke(parameter);
            }
            return false;
        }

        public void ExecuteSync(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }

    public class AsyncCommandExecutionArgs : EventArgs
    {
        public bool HasError => Error != null;

        public Exception Error
        {
            get;
            set;
        }
    }
}
