﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.Views;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Views;
using CommonServiceLocator;
using Prism;
using Prism.Regions;

namespace BISC.Presentation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly ILoggingService _loggingService;

        private Dictionary<string, Tuple<string, BiscNavigationParameters>> _waitingRegionsdictionary =
            new Dictionary<string, Tuple<string, BiscNavigationParameters>>();




        public NavigationService(IRegionManager regionManager, IUserNotificationService userNotificationService, IInjectionContainer injectionContainer, ILoggingService loggingService)
        {
            _regionManager = regionManager;
            _userNotificationService = userNotificationService;
            _injectionContainer = injectionContainer;
            _loggingService = loggingService;
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

                _regionManager.RequestNavigate(regionName, new Uri(viewName, UriKind.Relative), (result =>
                 {
                     if (result.Error != null)
                     {
                         _loggingService.LogException(result.Error);
                     }
                 }), navigationParameters?.ToNavigationParameters());


            });
        }

        public void DeactivateRegion(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
            {
                var reg = _regionManager.Regions[regionName];
                foreach (var view in reg.Views)
                {
                    if (view is FrameworkElement frameworkElement)
                    {
                        if (frameworkElement.DataContext is IActiveAware activeAware)
                        {
                            activeAware.IsActive = false;
                        }
                    }
                }
            }
        }

        public void ActivateRegion(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
            {
                var reg = _regionManager.Regions[regionName];
                foreach (var view in reg.Views)
                {
                    if (view is FrameworkElement frameworkElement)
                    {
                        if (frameworkElement.DataContext is IActiveAware activeAware)
                        {
                            activeAware.IsActive = true;
                        }
                    }
                }
            }
        }

        public void DisposeRegionViewModel(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
            {
                var reg = _regionManager.Regions[regionName];
                foreach (var view in reg.Views)
                {
                    if (view is FrameworkElement frameworkElement)
                    {
                        if (frameworkElement.DataContext is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                _regionManager.Regions.Remove(regionName);
            }
        }

        public void OpenInWindow(string viewName,string windowHeader, BiscNavigationParameters navigationParameters = null)
        {
            Window newWindow=new Window();
            var region=new DynamicRegion();
            var regionId = Guid.NewGuid();
            region.SetValue(DynamicRegion.RegionKeyProperty,regionId.ToString());
            newWindow.Content = region;
            newWindow.Show();
            newWindow.Title = windowHeader;
            NavigateViewToRegion(viewName,regionId.ToString(),navigationParameters);
        }

        public async Task NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null, string idOfDialogHost = null)
        {
            var content = _injectionContainer.ResolveType<object>(viewName);
            var r = new BiscNavigationContext() { BiscNavigationParameters = navigationParameters }.ToNavigationContext();
            ((content as FrameworkElement).DataContext as INavigationAware)?.OnNavigatedTo(new BiscNavigationContext() { BiscNavigationParameters = navigationParameters }.ToNavigationContext());
            try
            {
                if (idOfDialogHost != null)
                {
                    await _userNotificationService.ShowContentAsDialog(content, idOfDialogHost);
                }
                else
                {
                    await _userNotificationService.ShowContentAsDialog(content);
                }
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
