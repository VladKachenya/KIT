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
    public  class Doi:ModelElement,IDoi
    {
        public Doi()
        {
            ElementName = InfoModelKeys.ModelKeys.DoiKey;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ChildModelsList<ISdi> SdiCollection =>new ChildModelsList<ISdi>(this, InfoModelKeys.ModelKeys.SdiKey);
        public ChildModelsList<IDai> DaiCollection => new ChildModelsList<IDai>(this, InfoModelKeys.ModelKeys.DaiKey);

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDoi)) return false;
            var element = obj as IDoi;
            if (element.Name != Name) return false;
            if (element.Description != Description) return false;
            return true;
        }
    }
}
