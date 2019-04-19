using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BISC.GlobalServices
{
    public class UserInterfaceComposingService : IUserInterfaceComposingService
    {
        private readonly IShellViewModel _shellViewModel;
        private readonly Func<IGlobalCommand> _globalCommandFactory;
        private readonly Func<IGlobalCommandGroup> _globalCommandGroupFactoryFunc;
        private readonly ICommandFactory _commandFactory;
        private readonly ObservableCollection<IGlobalCommand> _globalMenuCommands = new ObservableCollection<IGlobalCommand>();
        private readonly ObservableCollection<IGlobalCommand> _globalToolbarCommands = new ObservableCollection<IGlobalCommand>();
        private readonly ObservableCollection<IGlobalCommandGroup> _globalToolbarCommandGroups = new ObservableCollection<IGlobalCommandGroup>();


        public UserInterfaceComposingService(IShellViewModel shellViewModel, Func<IGlobalCommand> globalCommandFactory,
            Func<IGlobalCommandGroup> globalCommandGroupFactoryFunc, ICommandFactory commandFactory)
        {
            _shellViewModel = shellViewModel;
            _globalCommandFactory = globalCommandFactory;
            _globalCommandGroupFactoryFunc = globalCommandGroupFactoryFunc;
            _commandFactory = commandFactory;
        }


        public void AddGlobalCommand(ICommand command, string name, string iconId, bool isAddToMenu = false, bool isAddToToolBar = false)
        {
            if (command == null)
            {
                command = _commandFactory.CreatePresentationCommand(() => { return; }, () => false);
                name = "";
                iconId = IconsKeys.DragIconKey;
            }

            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            globalCommand.IconId = iconId;
            if (isAddToToolBar && !_globalToolbarCommands.Any((command1 => command1.Command == globalCommand.Command)))
            {
                _globalToolbarCommands.Add(globalCommand);
            }
            if (isAddToMenu && !_globalMenuCommands.Any((command1 => command1.Command == globalCommand.Command)))
            {
                _globalMenuCommands.Add(globalCommand);
            }
        }

        public void InsertGlobalCommandToStart(ICommand command, string name, string iconId = null,
            bool isAddToMenu = false, bool isAddToToolBar = false)
        {
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            globalCommand.IconId = iconId;
            if (isAddToToolBar && !_globalToolbarCommands.Any((command1 => command1.Command == globalCommand.Command)))
            {
                _globalToolbarCommands.Insert(0, globalCommand);
            }
            if (isAddToMenu && !_globalMenuCommands.Any((command1 => command1.Command == globalCommand.Command)))
            {
                _globalMenuCommands.Insert(0, globalCommand);
            }
        }


        public void AddGlobalCommandGroup(List<ICommand> commands, List<string> names, string groupName, string iconId = null,
            List<string> iconIds = null, bool isAddToMenu = false, bool isAddToToolBar = false)
        {
            IGlobalCommandGroup globalCommandGroup = _globalCommandGroupFactoryFunc.Invoke();
            globalCommandGroup.CommandsName = groupName;
            globalCommandGroup.IconId = iconId;
            while (commands.Count != 0)
            {
                IGlobalCommand globalCommand = _globalCommandFactory();
                globalCommand.Command = commands.First();
                commands.Remove(globalCommand.Command);
                globalCommand.CommandName = names.FirstOrDefault();
                names.Remove(globalCommand.CommandName);
                if (iconIds != null)
                {
                    globalCommand.IconId = iconIds.FirstOrDefault();
                    iconIds.Remove(globalCommand.IconId);
                }
                globalCommandGroup.GlobalCommandsGroup.Add(globalCommand);
            }
            _globalToolbarCommandGroups.Add(globalCommandGroup);
        }



        public void DeleteGlobalCommand(ICommand command)
        {
            var deletingCommand =
                _globalMenuCommands.FirstOrDefault((globalCommand => globalCommand.Command == command));
            if (deletingCommand != null)
            {
                _globalMenuCommands.Remove(deletingCommand);
            }
            deletingCommand =
                _globalToolbarCommands.FirstOrDefault((globalCommand => globalCommand.Command == command));
            if (deletingCommand != null)
            {
                _globalToolbarCommands.Remove(deletingCommand);
            }
        }

        public void SetCurrentSaveCommand(ICommand command, string name, bool isToDevice)
        {
            ClearCurrentSaveCommand();
            IGlobalCommand globalCommand = _globalCommandFactory();
            globalCommand.Command = command;
            globalCommand.CommandName = name;
            if (isToDevice)
            {
                globalCommand.IconId = IconsKeys.UploadNetworkKey;

            }
            else
            {
                globalCommand.IconId = IconsKeys.SaveIconKey;

            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                _globalToolbarCommands.Add(globalCommand);
            });

        }

        public void ClearCurrentSaveCommand()
        {
            var existingSaveCommand =
                _globalToolbarCommands.FirstOrDefault(globalCommandFinded => globalCommandFinded.IconId == IconsKeys.SaveIconKey || globalCommandFinded.IconId == IconsKeys.UploadNetworkKey);
            if (existingSaveCommand != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _globalToolbarCommands.Remove(existingSaveCommand);
                });
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

        public ObservableCollection<IGlobalCommandGroup> GetToolBarCommandGroups()
        {
            return _globalToolbarCommandGroups;
        }
    }
}
