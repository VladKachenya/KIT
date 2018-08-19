using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelDetails
{
   public class GroupInfoModelDetail:DefaultInfoModelDetail
    {
        public List<IInfoModelDetail> TreeItemDetails => ((List<IInfoModelDetail>)DetailValue);

    }
}
