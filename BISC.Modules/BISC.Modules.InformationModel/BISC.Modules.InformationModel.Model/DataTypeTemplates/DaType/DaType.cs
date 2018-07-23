using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType
{
   public class DaType:ModelElement,IDaType
    {
        public DaType()
        {
            Bdas=new List<IBda>();
            ElementName = "DAType";
        }
        public string Id { get; set; }
        public List<IBda> Bdas { get; }
    }
}
