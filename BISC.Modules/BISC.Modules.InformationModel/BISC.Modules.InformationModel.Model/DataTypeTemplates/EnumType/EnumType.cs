using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType
{
    public class EnumType:ModelElement,IEnumType
    {
        public EnumType()
        {
            EnumValList=new List<IEnumVal>();
        }
        public string Id { get; set; }
        public List<IEnumVal> EnumValList { get; }
    }
}
