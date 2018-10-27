using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BISC.Presentation.BaseItems.Converters
{
    public class BoolToVisConverter : MarkupExtension, IValueConverter

    {

        public bool Invert { get; set; }
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;



        public override object ProvideValue(IServiceProvider serviceProvider)

        {

            return this;

        }



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)

        {

            bool isVisible = true.Equals(value);

            if (Invert) isVisible = !isVisible;

            return isVisible ? Visibility.Visible : FalseVisibility;

        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)

        {

            bool isVisible = Visibility.Visible.Equals(value);

            if (Invert) isVisible = !isVisible;

            return isVisible;

        }

    }
}
