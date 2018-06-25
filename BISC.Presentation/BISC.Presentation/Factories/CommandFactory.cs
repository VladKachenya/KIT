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

        public ICommand CreateDelegateCommand(Action<CommandArguments> execute, Func<CommandArguments, bool> canExecute)
        {
            if (canExecute == null)
            {
                return new DelegateCommand<CommandArguments>(execute);
            }
            else
            {
                return new DelegateCommand<CommandArguments>(execute, canExecute);
            }
        }

        #endregion
    }
}
