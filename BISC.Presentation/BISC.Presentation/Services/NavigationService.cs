using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Parameters;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Views;
using Prism.Regions;

namespace BISC.Presentation.Services
{
   public class NavigationService: INavigationService
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserNotificationService _userNotificationService;

        public NavigationService(IRegionManager regionManager, IUserNotificationService userNotificationService)
        {
            _regionManager = regionManager;
            _userNotificationService = userNotificationService;
        }


        #region Implementation of INavigationService

        public void NavigateViewToRegion(string viewName, string regionName, List<NavigationParameter> navigationParameters=null)
        {
           _regionManager.RequestNavigate(regionName,new Uri(viewName,UriKind.Relative),PopulateNavigationParameters(navigationParameters));
        }

        public async void NavigateViewToGlobalRegion(string viewName, List<NavigationParameter> navigationParameters = null)
        {
            GlobalDialogView globalDialogView=new GlobalDialogView();

           await _userNotificationService.ShowContentAsDialog(globalDialogView);
            var region = _regionManager.Regions[KeysForNavigation.RegionNames.GlobalDialogRegionKey];
            _regionManager.RequestNavigate(KeysForNavigation.RegionNames.GlobalDialogRegionKey, new Uri(viewName, UriKind.Relative),
                (callback) =>
                {
                    if (callback.Error != null)
                    {
                        throw callback.Error;
                    }
                }, PopulateNavigationParameters(navigationParameters));

        }


        private NavigationParameters PopulateNavigationParameters(List<NavigationParameter> navigationParameters)
        {
            NavigationParameters navigationParametersToRegion = new NavigationParameters();

            navigationParameters?.ForEach((parameter =>
            {
                navigationParametersToRegion.Add(parameter.ParameterName, parameter.Parameter);
            }));
            return navigationParametersToRegion;
        }


        #endregion
    }
}
