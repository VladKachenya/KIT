using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace BISC.Modules.Gooses.Presentation.Converters
{
    public class DataRowViewConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataGridCell cell = value as DataGridCell;
            if (cell == null)
                return null;

            System.Data.DataRowView drv = cell.DataContext as System.Data.DataRowView;
            if (drv == null)
                return null;

            return drv.Row[cell.Column.SortMemberPath];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
