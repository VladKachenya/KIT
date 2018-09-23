using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BISC.Infrastructure.Global.IoC;
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
        private readonly IInjectionContainer _injectionContainer;
        private Dictionary<string, Tuple<string, BiscNavigationParameters>> _waitingRegionsdictionary = new Dictionary<string, Tuple<string, BiscNavigationParameters>>();

        public NavigationService(IRegionManager regionManager, IUserNotificationService userNotificationService, IInjectionContainer injectionContainer)
        {
            _regionManager = regionManager;
            _userNotificationService = userNotificationService;
            _injectionContainer = injectionContainer;
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
               
                _regionManager.RequestNavigate(regionName, new Uri(viewName, UriKind.Relative),(result =>
                {
                    if (result.Error != null)
                    {

                    }
                }), navigationParameters?.ToNavigationParameters());

            });
        }

        public void NavigateFromRegion(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
            {
              var reg=  _regionManager.Regions[regionName];
                foreach (var view in reg.Views)
                {
                    if (view is FrameworkElement frameworkElement)
                    {
                        if (frameworkElement.DataContext is INavigationAware navigationAware)
                        {
                            navigationAware.OnNavigatedFrom(null);
                        }
                    }
                }
            }
        }
    
        public async Task NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null)
        {
            var content = _injectionContainer.ResolveType<object>(viewName);
            var r = new BiscNavigationContext() {BiscNavigationParameters = navigationParameters}.ToNavigationContext();
            ((content as FrameworkElement).DataContext as INavigationAware)?.OnNavigatedTo(new BiscNavigationContext() { BiscNavigationParameters = navigationParameters }.ToNavigationContext());
            try
            {
                await _userNotificationService.ShowContentAsDialog(content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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


        #endregion
    }
}
