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
    public class UserInterfaceComposingService : IUserInterfaceComposingService
    {
        private readonly IShellViewModel _shellViewModel;
        private readonly Func<IGlobalCommand> _globalCommandFactory;
        private readonly ObservableCollection<IGlobalCommand> _globalMenuCommands = new ObservableCollection<IGlobalCommand>();
        private readonly ObservableCollection<IGlobalCommand> _globalToolbarCommands = new ObservableCollection<IGlobalCommand>();

        public UserInterfaceComposingService(IShellViewModel shellViewModel, Func<IGlobalCommand> globalCommandFactory)
        {
            _shellViewModel = shellViewModel;
            _globalCommandFactory = globalCommandFactory;
        }
        public void AddGlobalCommand(ICommand command, string name, string iconId,bool isAddToMenu=false, bool isAddToToolBar = false)
        {
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            globalCommand.IconId = iconId;
            if (isAddToToolBar)
            {
                _globalToolbarCommands.Add(globalCommand);
            }
            if (isAddToMenu)
            {
                _globalMenuCommands.Add(globalCommand);
            }
        }

        public void SetCurrentSaveCommand(ICommand command, string name)
        {
            ClearCurrentSaveCommand();
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            globalCommand.IconId = IconsKeys.SaveIconKey;
            _globalToolbarCommands.Add(globalCommand);
        }

        public void ClearCurrentSaveCommand()
        {
            var existingSaveCommand =
                _globalToolbarCommands.FirstOrDefault(globalCommandFinded => globalCommandFinded.IconId == IconsKeys.SaveIconKey);
            if (existingSaveCommand != null)
            {
                _globalToolbarCommands.Remove(existingSaveCommand);
            }
        }

        public ObservableCollection<IGlobalCommand> GetToolBarCommands()
        {
            return _globalToolbarCommands;
        }

        public ObservableCollection<IGlobalCommand> GetMenuCommands()
        {
            return _globalMenuCommands;
        }
    }
}
