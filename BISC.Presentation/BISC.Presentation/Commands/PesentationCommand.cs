using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Commands;
using Prism.Commands;

namespace BISC.Presentation.Commands
{
    public class PesentationCommand<T> : IPesentationCommand<T>
    {
        private DelegateCommand<T> _delegateCommand;
     
        internal void Initialize()
        {
            _delegateCommand = new DelegateCommand<T>(ExecuteAction, CanExecuteAction);
        }
        #region Implementation of IPesentationCommand

        public Action<T> ExecuteAction { get; set; }
        public Func<T, bool> CanExecuteAction { get; set; }


        public void RaiseCanExecute()
        {
            _delegateCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
