using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType
{
   public class DaType:ModelElement,IDaType
    {
        public DaType()
        {
            ElementName = "DAType";
        }
        public string Id { get; set; }
        public ChildModelsList<IBda> Bdas =>new ChildModelsList<IBda>(this, "BDA");
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDaType)) return -1;
            var element = obj as IDaType;
            if (element.Id != Id) return -1;
            return 1;
        }
    }
}
