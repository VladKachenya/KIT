using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BISC.Modules.Gooses.Presentation.Converters
{
    public class IsEnabledToSelectableBoxBackgroundConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Brushes.White;
            if (value.Equals(false))
            {
                return new SolidColorBrush(Color.FromArgb(50, 0x80, 0x80, 0x80));
            }
            else
            { return Brushes.White; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
