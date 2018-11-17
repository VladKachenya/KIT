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
        private readonly IProjectService _projectService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;
        private readonly IBiscProject _biscProject;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ICommandFactory _commandFactory;
        private readonly IMainTreeViewModel _mainTreeViewModel;
        private readonly ILoggingService _loggingService;

        public PresentationInitialization(IGlobalEventsService globalEventsService,
            INavigationService navigationService, IProjectService projectService
            , IUiFromModelElementRegistryService uiFromModelElementRegistryService, IBiscProject biscProject
            , IUserInterfaceComposingService userInterfaceComposingService, ICommandFactory commandFactory,
            IMainTreeViewModel mainTreeViewModel, ILoggingService loggingService, IInjectionContainer injectionContainer, ISaveCheckingService saveCheckingService)
        {
            _globalEventsService = globalEventsService;
            _injectionContainer = injectionContainer;
            _saveCheckingService = saveCheckingService;
            _navigationService = navigationService;
            _projectService = projectService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            _biscProject = biscProject;
            _userInterfaceComposingService = userInterfaceComposingService;
            _commandFactory = commandFactory;
            _mainTreeViewModel = mainTreeViewModel;
            _loggingService = loggingService;
            _globalEventsService.Subscribe<ShellLoadedEvent>((args =>
            {
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnSaveProject), "Сохранить все изменения в проект", IconsKeys.ContentSaveAllKey,true,true);
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnSaveProjectAs), "Сохранить проект как...", null, true, false);
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnOpenProjectAs), "Открыть проект", null, true, false);
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnApplicatoinSettingsAdding, null), "Настройки",null,true,false);
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

        private async void OnSaveProject()
        {
            _projectService.SaveCurrentProject();
            await _saveCheckingService.SaveAllUnsavedEntities(false);
            _loggingService.LogMessage($"Проект сохранен {_projectService.GetCurrentProjectPath(true)}", SeverityEnum.Info);
        }

        private async void OnSaveProjectAs()
        {
            Maybe<string> listOfPaths = FileHelper.SelectFilePathToSave("Сохранение файла", null, null, "New project");
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
