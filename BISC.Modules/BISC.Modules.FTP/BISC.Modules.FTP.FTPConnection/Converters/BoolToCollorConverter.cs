using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BISC.Modules.FTP.FTPConnection.Converters
{
    public class BoolToCollorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ( (bool?)value == true) return "LightGreen";
            if ((bool?)value == false) return "#FFF16565";
            return "#FFD1D113";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
