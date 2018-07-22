using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType
{
  public  class Bda:DefaultModelElement,IBda
    {
        public string Name { get; set; }
        public string BType { get; set; }
        public string Type { get; set; }
    }
}
