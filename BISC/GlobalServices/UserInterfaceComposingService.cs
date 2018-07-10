using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;

namespace BISC.GlobalServices
{
   public class UserInterfaceComposingService: IUserInterfaceComposingService
    {
        private readonly IShellViewModel _shellViewModel;
        private readonly Func<IGlobalCommand> _globalCommandFactory;

        public UserInterfaceComposingService(IShellViewModel shellViewModel,Func<IGlobalCommand> globalCommandFactory)
        {
            _shellViewModel = shellViewModel;
            _globalCommandFactory = globalCommandFactory;
        }
        public void AddGlobalCommand(ICommand command, string name)
        {
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
           _shellViewModel.GlobalCommands.Add(globalCommand);
        }
    }
}
