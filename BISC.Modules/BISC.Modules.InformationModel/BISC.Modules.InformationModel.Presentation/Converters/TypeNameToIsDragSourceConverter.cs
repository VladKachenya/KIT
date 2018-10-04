using BISC.Modules.InformationModel.Infrastucture;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BISC.Modules.InformationModel.Presentation.Converters
{
    public class TypeNameToIsDragSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as string) == InfoModelKeys.ModelKeys.DoiKey)
                return true;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
