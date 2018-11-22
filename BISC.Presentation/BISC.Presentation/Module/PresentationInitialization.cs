using System.Collections.Generic;
using System.Windows.Input;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;
using BISC.Presentation.Interfaces.Tree;
using Prism.Events;

namespace BISC.Presentation.Module
{
    public class PresentationInitialization
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly INavigationService _navigationService;
        private IProjectService _projectService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;
        private readonly IBiscProject _biscProject;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ICommandFactory _commandFactory;
        private readonly IMainTreeViewModel _mainTreeViewModel;
        private readonly ILoggingService _loggingService;

        public PresentationInitialization(IGlobalEventsService globalEventsService,
            INavigationService navigationService, IUiFromModelElementRegistryService uiFromModelElementRegistryService, IBiscProject biscProject
            , IUserInterfaceComposingService userInterfaceComposingService, ICommandFactory commandFactory,
            IMainTreeViewModel mainTreeViewModel, ILoggingService loggingService, IInjectionContainer injectionContainer, ISaveCheckingService saveCheckingService,
            IProjectService projectService)
        {
            _globalEventsService = globalEventsService;
            _injectionContainer = injectionContainer;
            _saveCheckingService = saveCheckingService;
            _navigationService = navigationService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            _biscProject = biscProject;
            _userInterfaceComposingService = userInterfaceComposingService;
            _commandFactory = commandFactory;
            _mainTreeViewModel = mainTreeViewModel;
            _loggingService = loggingService;
            _projectService = projectService;
            _globalEventsService.Subscribe<ShellLoadedEvent>((args =>
            {
                var fileCommands = new List<ICommand>();
                var fileCommandsName = new List<string>();
                fileCommands.Add(_commandFactory.CreatePresentationCommand(OnSaveProject));
                fileCommandsName.Add("Сохранить все изменения в проект");
                fileCommands.Add(_commandFactory.CreatePresentationCommand(OnSaveProjectAs));
                fileCommandsName.Add("Сохранить проект как...");
                fileCommands.Add(_commandFactory.CreatePresentationCommand(OnOpenProjectAs));
                fileCommandsName.Add("Открыть проект");
                fileCommands.Add(_commandFactory.CreatePresentationCommand(OnClearProject));
                fileCommandsName.Add("Очистить проект");
                _userInterfaceComposingService.AddGlobalCommandGroup(fileCommands, fileCommandsName, "ПРОЕКТ", IconsKeys.BookMultipleIconKey);
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnSaveProject), "Сохранить все изменения в проект", IconsKeys.ContentSaveAllKey, false, true);
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnApplicatoinSettingsAdding, null), "Настройки", IconsKeys.SettingsIconKey, true, false);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTreeViewName,
                    KeysForNavigation.RegionNames.MainTreeRegionKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTabHostViewName,
                    KeysForNavigation.RegionNames.MainTabHostRegionKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.HamburgerMenuViewName,
                    KeysForNavigation.RegionNames.HamburgerMenuKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.ToolBarMenuViewName,
                    KeysForNavigation.RegionNames.ToolBarMenuKey);
                _projectService.OpenDefaultProject();
                _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(_biscProject.MainSclModel.Value, null, "SCL");
                _mainTreeViewModel.ChangeTracker.SetTrackingEnabled(true);
            }));

        }

        private async void OnClearProject()
        {
            _projectService.ClearCurrentProject();
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект очистен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        private async void OnSaveProject()
        {
            _projectService.SaveCurrentProject();
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        private async void OnSaveProjectAs()
        {
            Maybe<string> listOfPaths = FileHelper.SelectFilePathToSave("Сохранение файла", ".bisc", "BISC Files (*.bisc)|*.bisc|" +
                                                                                                  "All Files (*.*)|*.*", "New project");
            if (!listOfPaths.Any()) return;
            _projectService.SaveProjectAs(listOfPaths.GetFirstValue());
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        private async void OnOpenProjectAs()
        {
            var fileMaybe = FileHelper.SelectFileToOpen("Открыть файл с устройствами", "BISC Files (*.bisc)|*.bisc|" +
                                                                                       "All Files (*.*)|*.*");
            if (!fileMaybe.Any()) return;
            _projectService.OpenProjectAs(fileMaybe.GetFirstValue().FullName);
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(_biscProject.MainSclModel.Value, null, "SCL");
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект открыт {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        private void OnApplicatoinSettingsAdding()
        {
            _injectionContainer.ResolveType<IApplicationSettingsAddingService>().OpenApplicatoinSettingsView();
        }
    }
}
