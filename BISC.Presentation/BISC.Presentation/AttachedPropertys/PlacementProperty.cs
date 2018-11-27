using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BISC.Presentation.AttachedPropertys
{
    public class PlacementProperty
    {
        public static void SetMenuPlacement(DependencyObject obj, PlacementMode value)
        {
            obj.SetValue(MenuPlacementProperty, value);
        }

        // Using a DependencyProperty as the backing store for MenuPlacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuPlacementProperty =
            DependencyProperty.RegisterAttached("MenuPlacement",
                typeof(PlacementMode),
                typeof(PlacementProperty),
                new FrameworkPropertyMetadata(PlacementMode.Bottom, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnMenuPlacementChanged)));

        private static void OnMenuPlacementChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var menuItem = o as MenuItem;
            if (menuItem != null)
            {
                if (menuItem.IsLoaded)
                {
                    SetPopupPlacement(menuItem, (PlacementMode)e.NewValue);
                }
                else
                {
                    menuItem.Loaded += new RoutedEventHandler((m, v) => SetPopupPlacement(menuItem, (PlacementMode)e.NewValue));
                }
            }
        }

        private static void SetPopupPlacement(MenuItem menuItem, PlacementMode placementMode)
        {
            Popup popup = menuItem.Template.FindName("PART_Popup", menuItem) as Popup;
            if (popup != null)
            {
                popup.Placement = placementMode;
            }
        }
    }
}