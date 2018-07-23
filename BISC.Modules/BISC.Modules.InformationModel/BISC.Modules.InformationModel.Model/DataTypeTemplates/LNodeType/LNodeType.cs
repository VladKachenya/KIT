using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType
{
   public class LNodeType:ModelElement,ILNodeType
    {
        public LNodeType()
        {
            DoList=new List<IDo>();
            ElementName = "LNodeType";
        }
        public string Id { get; set; }
        public string LnClass { get; set; }

        public List<IDo> DoList { get; }
    }
}
