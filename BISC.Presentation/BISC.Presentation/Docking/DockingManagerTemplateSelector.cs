using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BISC.Presentation.Interfaces;

namespace BISC.Presentation.Docking
{
    public class DockingManagerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TabTemplate { get; set; }

        #region Overrides of DataTemplateSelector

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ITabViewModel)
            {
                return TabTemplate;
            }
            return base.SelectTemplate(item, container);
        }

        #endregion
    }
}
