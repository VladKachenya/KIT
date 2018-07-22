using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
   public class Sdo:ModelElement,ISdo
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
