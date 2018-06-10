using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Commands;
using BISC.Presentation.Infrastructure.Factories;

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

        public ICommand CreateDelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
