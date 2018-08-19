using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelDetails
{
   public class BoolInfoModelDetail:DefaultInfoModelDetail
    {
        public bool BoolValue => DetailValue != null && ((bool?)DetailValue).Value;

    }
}
