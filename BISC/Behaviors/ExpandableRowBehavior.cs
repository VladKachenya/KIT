using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace BISC.Behaviors
{
   public class ExpandableRowBehavior
    {


        public static bool GetIsExpanded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExpandedProperty);
        }

        public static void SetIsExpanded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExpandedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsExpandable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.RegisterAttached("IsExpanded", typeof(bool), typeof(ExpandableRowBehavior), new PropertyMetadata(true,OnExpandedChanged));

        private static void OnExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RowDefinition rowDefinition)
            {
                if ((bool) e.NewValue)
                {
                    
                }
                else
                {
                    rowDefinition.Height=GridLength.Auto;
                }
            }
        }
    }
}
