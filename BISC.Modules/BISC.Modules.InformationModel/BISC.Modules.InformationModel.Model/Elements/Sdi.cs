using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class Sdi:ModelElement,ISdi
    {
        public Sdi()
        {
            ElementName = InfoModelKeys.ModelKeys.SdiKey;
        }


        public string Name { get; set; }
        public ChildModelsList<ISdi> SdiCollection=>new ChildModelsList<ISdi>(this, InfoModelKeys.ModelKeys.SdiKey);
        public ChildModelsList<IDai> DaiCollection =>new ChildModelsList<IDai>(this, InfoModelKeys.ModelKeys.DaiKey);
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is ISdi)) return false;
            var element = obj as ISdi;
            if (element.Name != Name) return false;
            return true;
        }
    }
}
