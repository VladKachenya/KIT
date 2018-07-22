using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType
{
   public class DaType:DefaultModelElement,IDaType
    {
        public DaType()
        {
            Bdas=new List<IBda>();
        }
        public string Id { get; set; }
        public List<IBda> Bdas { get; }
    }
}
