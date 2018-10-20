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
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDaType)) return false;
            var element = obj as IDaType;
            if (element.Id != Id) return false;
            return true;
        }
    }
}
