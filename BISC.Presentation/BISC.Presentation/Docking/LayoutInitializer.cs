﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Interfaces;
using Xceed.Wpf.AvalonDock.Layout;

namespace BISC.Presentation.Docking
{
    public class LayoutInitializer : ILayoutUpdateStrategy
    {
        #region Implementation of ILayoutUpdateStrategy

        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow,
            ILayoutContainer destinationContainer)
        {
            //AD wants to add the anchorable into destinationContainer
            //just for test provide a new anchorablepane 
            //if the pane is floating let the manager go ahead
            if (destinationContainer != null &&
                destinationContainer.FindParent<LayoutFloatingWindow>() != null)
                return false;

            if (anchorableToShow.Content is ITabViewModel)
            {
                string paneToSearch = "";
                //switch ((anchorableToShow.Content as ITabViewModel).Placement)
                //{
                //    case PlacementEnum.Top:

                //        break;
                //    case PlacementEnum.Left:
                //        paneToSearch = "LeftAnchorablePane";
                //        break;
                //    case PlacementEnum.Right:
                //        break;
                //    case PlacementEnum.Bottom:
                //        paneToSearch = "BottomAnchorablePane";
                //        break;
                //    default:
                //        throw new ArgumentOutOfRangeException();
                //}
                var anchorablePaneToAddChild = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == paneToSearch);
                if (anchorablePaneToAddChild != null)
                {
                    anchorablePaneToAddChild.Children.Add(anchorableToShow);
                    return true;
                }
            }
            return false;
        }

        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {

        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }

        #endregion
    }
}
