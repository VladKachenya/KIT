using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using BISC.Infrastructure.Global.Logging;

namespace BISC.Modules.Logging.Infrastructure.Converters
{
   public class ServerityToImageConverter:IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var r=new BitmapImage(new Uri("/Error.png", UriKind.Relative));
            SeverityEnum severity;
            if (value is SeverityEnum)
            {
                severity = (SeverityEnum)value;
                switch (severity)
                {
                    case SeverityEnum.Info: return new BitmapImage(new Uri("../Icons/Info.png", UriKind.Relative));
                    case SeverityEnum.Warning: return new BitmapImage(new Uri("../Icons/Warning.png", UriKind.Relative));
                    case SeverityEnum.Critical: return new BitmapImage(new Uri("../Icons/Error.png", UriKind.Relative));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
