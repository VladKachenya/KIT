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
using BISC.Presentation.BaseItems.Helpers;
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

        public DynamicRegion()
        {
            InitializeComponent();

            var globalEventsService = ServiceLocator.Current.GetInstance<IGlobalEventsService>();
            globalEventsService.Subscribe<RegionDisposingEvent>((DisposeRegion));

            regionContentControl.Unloaded += DynamicRegionBehavior_Unloaded;
            regionContentControl.Loaded += DynamicRegionBehavior_Loaded;

        }

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

        private void DynamicRegionBehavior_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);

            var dynamicRegionChildren = VisualTreeRecursiveHelper.FindVisualChildren<DynamicRegion>(this);
            foreach (DynamicRegion dynamicRegionChild in dynamicRegionChildren)
            {
                navigationService.TryNavigateToWaitingRegion(dynamicRegionChild.GetValue(RegionKeyProperty).ToString());
                navigationService.ActivateRegion(dynamicRegionChild.GetValue(RegionKeyProperty).ToString());
            }


            navigationService.TryNavigateToWaitingRegion(this.GetValue(RegionKeyProperty) as string);
            navigationService.ActivateRegion(regionName);
        }

        private void DynamicRegionBehavior_Unloaded(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = RegionManager.GetRegionName(sender as FrameworkElement);

            var dynamicRegionChildren = VisualTreeRecursiveHelper.FindVisualChildren<DynamicRegion>(this);
            foreach (DynamicRegion dynamicRegionChild in dynamicRegionChildren)
            {
                navigationService.DeactivateRegion(dynamicRegionChild.GetValue(RegionKeyProperty).ToString());
            }
            navigationService.DeactivateRegion(regionName);
        }

        #region Implementation of IDisposable

        public void DisposeRegion(RegionDisposingEvent eventDisposing)
        {
            if (GetValue(RegionKeyProperty).ToString() != eventDisposing.DisposingRegionName) return;
            var dynamicRegionChildren = VisualTreeRecursiveHelper.FindVisualChildren<DynamicRegion>(this);
            foreach (DynamicRegion dynamicRegionChild in dynamicRegionChildren)
            {
                dynamicRegionChild.Dispose();
            }
            Dispose();

        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            regionContentControl.Unloaded -= DynamicRegionBehavior_Unloaded;
            regionContentControl.Loaded -= DynamicRegionBehavior_Loaded;
            var globalEventsService = ServiceLocator.Current.GetInstance<IGlobalEventsService>();
            globalEventsService.Unsubscribe<RegionDisposingEvent>(action: (DisposeRegion));
            DynamicRegionBehavior_Unloaded(this, null);
         
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var regionName = this.GetValue(RegionKeyProperty) as string;

            var dynamicRegionChildren = VisualTreeRecursiveHelper.FindVisualChildren<DynamicRegion>(this);
            foreach (DynamicRegion dynamicRegionChild in dynamicRegionChildren)
            {
                navigationService.DisposeRegionViewModel(dynamicRegionChild.GetValue(RegionKeyProperty).ToString());
            }
            navigationService.DisposeRegionViewModel(regionName);
        }

        #endregion
    }
}