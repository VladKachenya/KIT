using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Presentation.Factories;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Interfaces.Menu;
using BISC.Presentation.Interfaces.Tree;
using BISC.Presentation.Services;
using BISC.Presentation.UiFromModel;
using BISC.Presentation.ViewModels;
using BISC.Presentation.ViewModels.Tab;
using BISC.Presentation.ViewModels.Tree;
using BISC.Presentation.Views;

namespace BISC.Presentation.Module
{
   public class GlobalPresentationModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public GlobalPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {
            _injectionContainer.RegisterType<ITabHostViewModel, TabHostViewModel>(true);

            _injectionContainer.RegisterType<ITabManagementService, TabManagementService>(true);
            _injectionContainer.RegisterType<ITabViewModel, TabViewModel>();

            _injectionContainer.RegisterType<INavigationService, NavigationService>(true);
            _injectionContainer.RegisterType<IMainTreeViewModel, MainTreeViewModel>(true);
            _injectionContainer.RegisterType<ICommandFactory, CommandFactory>(true);
            _injectionContainer.RegisterType<ITreeManagementService, TreeManagementService>(true);
            _injectionContainer.RegisterType<IHamburgerMenuViewModel, HamburgerMenuViewModel>();
            _injectionContainer.RegisterType<IToolBarMenuViewModel, ToolBarMenuViewModel>();
            _injectionContainer.RegisterType<IUiFromModelElementRegistryService, UiFromModelElementRegistryService>(true);

            _injectionContainer.RegisterTypeForNavigation<TabHostView>(KeysForNavigation.ViewNames.MainTabHostViewName);
            _injectionContainer.RegisterTypeForNavigation<MainTreeView>(KeysForNavigation.ViewNames.MainTreeViewName);
            _injectionContainer.RegisterTypeForNavigation<HamburgerMenuView>(KeysForNavigation.ViewNames.HamburgerMenuViewName);
            _injectionContainer.RegisterTypeForNavigation<ToolBarMenuView>(KeysForNavigation.ViewNames.ToolBarMenuViewName);

            _injectionContainer.RegisterType<PresentationInitialization>(true);

            PresentationInitialization presentationInitialization = _injectionContainer.ResolveType(typeof(PresentationInitialization)) as PresentationInitialization;

          

        }
    }
}
