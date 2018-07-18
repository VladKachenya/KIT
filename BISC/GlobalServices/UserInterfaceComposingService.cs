using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;

namespace BISC.GlobalServices
{
   public class UserInterfaceComposingService: IUserInterfaceComposingService
    {
        private readonly IShellViewModel _shellViewModel;
        private readonly Func<IGlobalCommand> _globalCommandFactory;
        private readonly ObservableCollection<IGlobalCommand> _globalCommands=new ObservableCollection<IGlobalCommand>();
        public UserInterfaceComposingService(IShellViewModel shellViewModel,Func<IGlobalCommand> globalCommandFactory)
        {
            _shellViewModel = shellViewModel;
            _globalCommandFactory = globalCommandFactory;
        }
        public void AddGlobalCommand(ICommand command, string name,string iconId)
        {
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            globalCommand.IconId = iconId;
           _globalCommands.Add(globalCommand);
        }

        public ObservableCollection<IGlobalCommand> GetCommandsWithIcons()
        {
            return new ObservableCollection<IGlobalCommand>(_globalCommands.Where((command => command.IconId != null)));
        }

        public ObservableCollection<IGlobalCommand> GetAllCommands()
        {
            return _globalCommands;
        }
    }
}
