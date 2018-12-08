using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BISC.Modules.Device.Presentation.ViewModels.Conflicts;

namespace BISC.Modules.Device.Presentation.Views.Conflicts
{
   public class ConflictDataTemplateSelector:DataTemplateSelector
    {
        #region Overrides of DataTemplateSelector

        public  DataTemplate DatatemplateForManualConflict { get; set; }
        public DataTemplate DatatemplateForAutoConflict { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DeviceManualConflictViewModel) 
            {
                return DatatemplateForManualConflict;
            }
            if (item is DeviceAutomaticResolveConflictViewModel)
            {
                return DatatemplateForAutoConflict;
            }
            return base.SelectTemplate(item, container);
        }

        #endregion
    }
}
