using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
  public  class DoType:ModelElement, IDoType
    {
        public DoType()
        {
            DaList=new List<IDa>();
            SdoList=new List<ISdo>();
            ElementName = "DOType";
        }



        public string Id { get; set; }
        public string Cdc { get; set; }
        public List<IDa> DaList { get; }
        public List<ISdo> SdoList { get; }
    }
}
