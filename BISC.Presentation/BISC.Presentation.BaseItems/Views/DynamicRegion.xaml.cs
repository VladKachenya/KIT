using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Services;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace BISC.Presentation.BaseItems.Views
{
    /// <summary>
    /// Логика взаимодействия для DynamivRegion.xaml
    /// </summary>
    public partial class DynamicRegion : UserControl
    {

        public static void SetRegionKey(DependencyObject target, string value)
        {
            target.SetValue(RegionKeyProperty, value);
        }

        public static readonly DependencyProperty RegionKeyProperty =
            DependencyProperty.RegisterAttached(
                "RegionKey",
                typeof(string),
                typeof(DynamicRegion),
                new UIPropertyMetadata(string.Empty, OnSetRegionKey));

        private static void OnSetRegionKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DynamicRegion).OnSetRegionKey(e.NewValue as string);
        }


        private void OnSetRegionKey(string regionKey)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager.Regions.ContainsRegionWithName(regionKey))
            {
                regionManager.Regions.Remove(regionKey);
            }
            RegionManager.SetRegionName(regionContentControl, regionKey);
            RegionManager.SetRegionManager(regionContentControl, regionManager);
        }


        public DynamicRegion()
        {
            InitializeComponent();

            var globalEventsService = ServiceLocator.Current.GetInstance<IGlobalEventsService>();
            void Dispose(RegionDisposingEvent eventDisposing)
            {
                if (GetValue(RegionKeyProperty).ToString() != eventDisposing.DisposingRegionName) return;
                regionContentControl.Unloaded -= DynamicRegionBehavior_Unloaded;
                regionContentControl.Loaded -= DynamicRegionBehavior_Loaded;
                globalEventsService.Unsubscribe<RegionDisposingEvent>((Dispose));
                DynamicRegionBehavior_Unloaded(this,null);
            }
            globalEventsService.Subscribe<RegionDisposingEvent>((Dispose));

            regionContentControl.Unloaded += DynamicRegionBehavior_Unloaded;
            regionContentControl.Loaded += DynamicRegionBehavior_Loaded;
    
        }

        private void DynamicRegionBehavior_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);
            navigationService.TryNavigateToWaitingRegion(this.GetValue(RegionKeyProperty) as string);
            navigationService.ActivateRegion(regionName);
        }

        private void DynamicRegionBehavior_Unloaded(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);
            navigationService.DeactivateRegion(regionName);
        }
    }
}