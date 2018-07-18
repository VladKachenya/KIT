using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Views;
using Prism.Regions;

namespace BISC.Presentation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserNotificationService _userNotificationService;
        private Dictionary<string, Tuple<string, BiscNavigationParameters>> _waitingRegionsdictionary = new Dictionary<string, Tuple<string, BiscNavigationParameters>>();

        public NavigationService(IRegionManager regionManager, IUserNotificationService userNotificationService)
        {
            _regionManager = regionManager;
            _userNotificationService = userNotificationService;
        }


        #region Implementation of INavigationService

        public void NavigateViewToRegion(string viewName, string regionName, BiscNavigationParameters navigationParameters = null)
        {

            var isRegionExists = _regionManager.Regions.ContainsRegionWithName(regionName);
            if (!isRegionExists)
            {
                if (_waitingRegionsdictionary.ContainsKey(regionName))
                {
                    _waitingRegionsdictionary[regionName] = new Tuple<string, BiscNavigationParameters>(viewName, navigationParameters);
                }
                else
                {
                    _waitingRegionsdictionary.Add(regionName, new Tuple<string, BiscNavigationParameters>(viewName, navigationParameters));

                }
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                _regionManager.RequestNavigate(regionName, new Uri(viewName, UriKind.Relative),
                    PopulateNavigationParameters(navigationParameters));

            });
        }

        public async void NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null)
        {
            GlobalDialogView globalDialogView = new GlobalDialogView();

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

        public void TryNavigateToWaitingRegion(string regionId)
        {
            if (_waitingRegionsdictionary.ContainsKey(regionId))
            {
                NavigateViewToRegion(_waitingRegionsdictionary[regionId].Item1, regionId,
                    _waitingRegionsdictionary[regionId].Item2);
                _waitingRegionsdictionary.Remove(regionId);
            }
        }


        private NavigationParameters PopulateNavigationParameters(BiscNavigationParameters navigationParameters)
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
