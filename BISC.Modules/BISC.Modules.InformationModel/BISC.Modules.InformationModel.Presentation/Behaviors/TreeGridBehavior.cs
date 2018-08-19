using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;
using BISC.Modules.InformationModel.Presentation.Interfaces;

namespace BISC.Modules.InformationModel.Presentation.Behaviors
{
    public class TreeGridBehavior : Behavior<ListView>
    {
        protected override void OnAttached()
        {

            AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;

            base.OnAttached();
        }

        private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;

            var dataContext = ((FrameworkElement)e.OriginalSource).DataContext;
            var hit = e.OriginalSource as DependencyObject;
            while (hit != null && !(hit is ToggleButton))
                hit = VisualTreeHelper.GetParent(hit);
            if(hit is ToggleButton)return;



            if ((sender as ListView).SelectedItem == dataContext)
                ((sender as ListView).SelectedItem as IInfoModelItemViewModel).Checked?.Invoke(null);
        }
    }
}
