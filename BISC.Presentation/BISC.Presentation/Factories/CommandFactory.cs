using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Commands;
using BISC.Presentation.Infrastructure.Factories;
using Prism.Commands;

namespace BISC.Presentation.Factories
{
   public class CommandFactory:ICommandFactory
    {
        public CommandFactory()
        {
            
        }


        #region Implementation of ICommandFactory

        

        #endregion

        #region Implementation of ICommandFactory

        public ICommand CreateDelegateCommand<T>(Action<T> execute, Func<T, bool> canExecute=null)
        {
            if (canExecute == null)
            {
                return new DelegateCommand<T>(execute);
            }
            else
            {
                return new DelegateCommand<T>(execute, canExecute);
            }
        }

        public ICommand CreateDelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            if (canExecute == null)
            {
                return new DelegateCommand(execute);
            }
            else
            {
                return new DelegateCommand(execute, canExecute);
            }
        }

        #endregion
    }
}
