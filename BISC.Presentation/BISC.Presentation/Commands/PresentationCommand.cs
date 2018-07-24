using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Commands;
using Prism.Commands;

namespace BISC.Presentation.Commands
{
    public class PresentationCommand<T> : IPresentationCommand<T>
    {
        private DelegateCommand<T> _delegateCommand;

        public PresentationCommand(DelegateCommand<T> delegateCommand)
        {
            _delegateCommand = delegateCommand;
        }
        
        
        #region Implementation of IPesentationCommand

        public Action<T> ExecuteAction => _delegateCommand.Execute;
        public Func<T, bool> CanExecuteAction => _delegateCommand.CanExecute;


        public void RaiseCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            //_delegateCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Implementation of ICommand

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
           ExecuteAction.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
    public class PresentationCommand : IPresentationCommand
    {
        private DelegateCommand _delegateCommand;

        public PresentationCommand(DelegateCommand delegateCommand)
        {
            _delegateCommand = delegateCommand;
        }


        #region Implementation of IPesentationCommand

        public Action ExecuteAction => _delegateCommand.Execute;
        public Func<bool> CanExecuteAction => _delegateCommand.CanExecute;


        public void RaiseCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            //_delegateCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Implementation of ICommand

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction == null) return true;
            return CanExecuteAction.Invoke();
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
