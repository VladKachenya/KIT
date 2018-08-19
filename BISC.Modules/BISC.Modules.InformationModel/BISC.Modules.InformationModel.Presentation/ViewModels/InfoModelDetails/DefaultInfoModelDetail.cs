using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Interfaces.InfoModelDetails;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelDetails
{
  public  class DefaultInfoModelDetail:IInfoModelDetail
    {
        public string DetailDescription { get; set; }
        public object DetailValue { get; set; }
        public string ToolTip { get; set; }
        public bool IsGrouped { get; set; }
    }
}
