using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BISC.Modules.InformationModel.Presentation.Converters
{
    /// <summary>
    /// Convert Level to left margin
    /// </summary>
    public class LevelToMarginConverter : IValueConverter
    {
        private const double IndentSize = 20.0;

        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            return new Thickness((int)o * IndentSize, 0, 0, 0);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
