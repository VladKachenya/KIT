using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;

namespace BISC.Modules.InformationModel.Presentation.TemplateSelectors
{
   public class ModelItemTemplateSelector:DataTemplateSelector
    {

        public DataTemplate NoValueDataTemplate { get; set; }
        public DataTemplate ValueDataTemplate { get; set; }
        public DataTemplate DoiDataTemplate { get; set; }


        #region Overrides of DataTemplateSelector

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DaiInfoModelItemViewModel)
            {
                return ValueDataTemplate;
            }

            if (item is DoiInfoModelItemViewModel)
            {
                return DoiDataTemplate;
            }
            return NoValueDataTemplate;
        }

        #endregion
    }
}
