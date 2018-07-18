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
    public class PanesStyleSelector : StyleSelector
    {
        public Style ProjectPaneStyle { get; set; }

        public Style FragmentsPaneStyle { get; set; }

        public Style BottomWindowStyle { get; set; }

        public Style LeftWindowStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is ITabViewModel)
                return FragmentsPaneStyle;
            //if (item is IAnchorableWindow)
            //{

            //    switch ((item as IAnchorableWindow).AnchorableDefaultPlacementEnum)
            //    {
            //        case PlacementEnum.Top:
            //            break;
            //        case PlacementEnum.Left:
            //            return LeftWindowStyle;
            //            break;
            //        case PlacementEnum.Right:
            //            break;
            //        case PlacementEnum.Bottom:
            //            return BottomWindowStyle;
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }

            //}

            return base.SelectStyle(item, container);
        }
    }
}
