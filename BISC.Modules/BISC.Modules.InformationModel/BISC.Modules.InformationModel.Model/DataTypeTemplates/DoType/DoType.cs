using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
  public  class DoType:ModelElement, IDoType
    {
        public DoType()
        {
            ElementName = "DOType";
        }
        public string Id { get; set; }
        public string Cdc { get; set; }
        public ChildModelsList<IDa> DaList => new ChildModelsList<IDa>(this, "DA");
        public ChildModelsList<ISdo> SdoList=>new ChildModelsList<ISdo>(this,"SDO");
    }
}
