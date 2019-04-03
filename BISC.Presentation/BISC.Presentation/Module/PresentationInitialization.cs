using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Constants;
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
        private readonly IInjectionContainer _injectionContainer;


        public PresentationInitialization(IGlobalEventsService globalEventsService, IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
            globalEventsService.Subscribe<ShellLoadedEvent>(ActivatePresentation);
        }

        private void ActivatePresentation(ShellLoadedEvent loadedEvent)
        {
            var mainTreeViewModel =
                _injectionContainer.ResolveType<IMainTreeViewModel>();
            var navigationService =
                _injectionContainer.ResolveType<INavigationService>();
            var uiFromModelElementRegistryService = 
                _injectionContainer.ResolveType<IUiFromModelElementRegistryService>();
            var biscProject =
                _injectionContainer.ResolveType<IBiscProject>();
            var userInterfaceComposingService =
                _injectionContainer.ResolveType<IUserInterfaceComposingService>();
            var commandFactory =
                _injectionContainer.ResolveType<ICommandFactory>();
            var projectManagementService =
                _injectionContainer.ResolveType<IProjectManagementService>();

            var fileCommands = new List<ICommand>();
            var fileCommandsName = new List<string>();
            fileCommands.Add(commandFactory.CreatePresentationCommand(projectManagementService.СreateNewProjectAsync));
            fileCommandsName.Add("Новый проект");
            fileCommands.Add(commandFactory.CreatePresentationCommand(projectManagementService.SaveProjectAsync));
            fileCommandsName.Add("Сохранить все изменения");
            fileCommands.Add(commandFactory.CreatePresentationCommand(projectManagementService.SaveProjectAsAsync));
            fileCommandsName.Add("Сохранить проект как...");
            fileCommands.Add(commandFactory.CreatePresentationCommand(projectManagementService.OpenProjectAsAsync));
            fileCommandsName.Add("Открыть проект");
            fileCommands.Add(commandFactory.CreatePresentationCommand(projectManagementService.ClearCurrentProjectAsync));
            fileCommandsName.Add("Очистить текущий проект");
            userInterfaceComposingService.AddGlobalCommandGroup(fileCommands, fileCommandsName, "ПРОЕКТ", IconsKeys.BookMultipleIconKey);

            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(projectManagementService.SaveProjectAsync),
                "Сохранить все изменения", IconsKeys.ContentSaveAllKey, false, true);
            userInterfaceComposingService.AddGlobalCommand(null, null, null, false, true); //Separator
            userInterfaceComposingService.AddGlobalCommand
                (commandFactory.CreatePresentationCommand(OnApplicatoinSettingsAdding, null), "Настройки", IconsKeys.SettingsIconKey, true, false);

            navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTreeViewName,
                KeysForNavigation.RegionNames.MainTreeRegionKey);
            navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTabHostViewName,
                KeysForNavigation.RegionNames.MainTabHostRegionKey);
            navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.HamburgerMenuViewName,
                KeysForNavigation.RegionNames.HamburgerMenuKey);
            navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.ToolBarMenuViewName,
                KeysForNavigation.RegionNames.ToolBarMenuKey);

            projectManagementService.OpenDefaultProjectAsync();

            uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(biscProject.MainSclModel.Value, null, "SCL");
            mainTreeViewModel.ChangeTracker.SetTrackingEnabled(true);
        }

        private void OnApplicatoinSettingsAdding()
        {
            _injectionContainer.ResolveType<IApplicationSettingsAddingService>().OpenApplicatoinSettingsView();
        }
    }
}
