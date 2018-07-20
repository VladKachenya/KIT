using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IProjectService _projectService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;
        private readonly IBiscProject _biscProject;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ICommandFactory _commandFactory;
        private readonly IMainTreeViewModel _mainTreeViewModel;

        public PresentationInitialization(IEventAggregator eventAggregator, 
            INavigationService navigationService,IProjectService projectService
            ,IUiFromModelElementRegistryService uiFromModelElementRegistryService,IBiscProject biscProject
            ,IUserInterfaceComposingService userInterfaceComposingService,ICommandFactory commandFactory,IMainTreeViewModel mainTreeViewModel)
        {

            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _projectService = projectService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            _biscProject = biscProject;
            _userInterfaceComposingService = userInterfaceComposingService;
            _commandFactory = commandFactory;
            _mainTreeViewModel = mainTreeViewModel;
            _eventAggregator.GetEvent<ShellLoadedEvent>().Subscribe((args =>
            {
                _userInterfaceComposingService.AddGlobalCommand(_commandFactory.CreatePresentationCommand(OnSaveProject),"Сохранить проект",IconsKeys.SaveIconKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTreeViewName,
                    KeysForNavigation.RegionNames.MainTreeRegionKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTabHostViewName,
                    KeysForNavigation.RegionNames.MainTabHostRegionKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.HamburgerMenuViewName,
                    KeysForNavigation.RegionNames.HamburgerMenuKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.ToolBarMenuViewName,
                    KeysForNavigation.RegionNames.ToolBarMenuKey);
                _projectService.OpenDefaultProject();
                _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(_biscProject.MainSclModel);
                _mainTreeViewModel.ChangeTracker.StartTracking();
            }));

        }

        private void OnSaveProject()
        {
            _projectService.SaveCurrentProject();
        }

    }
}
