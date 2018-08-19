using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;

namespace BISC.Modules.InformationModel.Presentation.Interfaces
{
  public  interface IInfoModelItemWithDetails:IInfoModelItemViewModel
    {
        List<IInfoModelDetail> TreeItemDetails { get; }
        bool IsSelected { get; set; }
        bool IsChildItemsShowing { get; }
    }
}
