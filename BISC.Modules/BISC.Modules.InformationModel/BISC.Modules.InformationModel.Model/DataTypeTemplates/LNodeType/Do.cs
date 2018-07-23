using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType
{
    public class Do:ModelElement,IDo
    {
        public Do()
        {
            ElementName = "DO";
        }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
