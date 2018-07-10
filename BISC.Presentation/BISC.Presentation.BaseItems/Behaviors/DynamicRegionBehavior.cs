using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace BISC.Presentation.BaseItems.Behaviors
{
    public class DynamicRegionBehavior : Behavior<ContentControl>
    {
        private readonly IRegionManager _regionManager;


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
        }

        

        public DynamicRegionBehavior(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
      
    }
}
