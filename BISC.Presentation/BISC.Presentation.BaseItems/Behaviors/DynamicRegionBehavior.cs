using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
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
            RegionManager.SetRegionName(d,e.NewValue as string);
            RegionManager.SetRegionManager(d,regionManager);
         //   (d as FrameworkElement).Unloaded += DynamicRegionBehavior_Unloaded;

            OnRegionInitialized(e.NewValue as string);
        }

   

        private static void OnRegionInitialized(string regionId)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.TryNavigateToWaitingRegion(regionId);
        }

        //private static void DynamicRegionBehavior_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    (sender as FrameworkElement).Unloaded -= DynamicRegionBehavior_Unloaded;
        //    var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        //    var regionName = RegionManager.GetRegionName(sender as FrameworkElement);
        //    if (regionManager.Regions.ContainsRegionWithName(regionName))
        //    {
                
        //        regionManager.Regions.Remove(regionName);
        //    }
            
        //}

        public DynamicRegionBehavior()
        {
           // _regionManager = regionManager;
        }
      
    }
}
