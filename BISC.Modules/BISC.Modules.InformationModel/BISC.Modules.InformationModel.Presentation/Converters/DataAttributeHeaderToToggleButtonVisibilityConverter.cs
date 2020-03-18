using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BISC.Modules.InformationModel.Infrastucture.Keys;

namespace BISC.Modules.InformationModel.Presentation.Converters
{
    public class DataAttributeHeaderToToggleButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return Visibility.Collapsed;
            }

            var dataAttributeHeader = (string) value;
            if (dataAttributeHeader == InformationModelKeys.DataAttributeHeaderKeys.db || dataAttributeHeader == InformationModelKeys.DataAttributeHeaderKeys.zeroDb)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}