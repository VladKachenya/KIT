using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Services;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace BISC.Presentation.BaseItems.Behaviors
{
    public class DynamicRegionBehavior
    {
        //  private readonly IRegionManager _regionManager;


        public static void SetRegionKey(DependencyObject target, string value)
        {
            target.SetValue(RegionKeyProperty, value);
        }

        public static readonly DependencyProperty RegionKeyProperty =
            DependencyProperty.RegisterAttached(
                "RegionKey",
                typeof(string),
                typeof(DynamicRegionBehavior),
                new UIPropertyMetadata(string.Empty, OnSetRegionKey));

        private static void OnSetRegionKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager.Regions.ContainsRegionWithName(e.NewValue as string))
            {
                regionManager.Regions.Remove(e.NewValue as string);
            }
            RegionManager.SetRegionName(d, e.NewValue as string);
            RegionManager.SetRegionManager(d, regionManager);
            var globalEventsService = ServiceLocator.Current.GetInstance<IGlobalEventsService>();





            void Dispose(RegionDisposingEvent eventDisposing)
            {
                if ((d as FrameworkElement)?.GetValue(RegionKeyProperty).ToString() != eventDisposing.DisposingRegionName) return;
                (d as FrameworkElement).Unloaded -= DynamicRegionBehavior_Unloaded;
                (d as FrameworkElement).Loaded -= DynamicRegionBehavior_Loaded;
            }
            globalEventsService.Subscribe<RegionDisposingEvent>((Dispose));
          
            (d as FrameworkElement).Unloaded += DynamicRegionBehavior_Unloaded;
            (d as FrameworkElement).Loaded += DynamicRegionBehavior_Loaded;

            OnRegionInitialized(e.NewValue as string);
        }

      

        private static void DynamicRegionBehavior_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);
            navigationService.ActivateRegion(regionName);
        }



        private static void OnRegionInitialized(string regionId)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.TryNavigateToWaitingRegion(regionId);
        }

        private static void DynamicRegionBehavior_Unloaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Unloaded -= DynamicRegionBehavior_Unloaded;
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);
            navigationService.DeactivateRegion(regionName);
        }

        public DynamicRegionBehavior()
        {
            // _regionManager = regionManager;
        }

    }
}