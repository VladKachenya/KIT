using System;
using System.Windows.Data;

namespace BISC.Presentation.BaseItems.Converters
{
    /// <summary>
    /// It needs use fore binding when we need to drop the parameter to command function and can execute method
    /// </summary>
    public class TriggerConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // First value is target value.
            // All others are update triggers only.
            if (values.Length < 1) return Binding.DoNothing;
            return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}