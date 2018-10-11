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

        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDoi)) return -1;
            var element = obj as IDoi;
            if (element.Name != Name) return -1;
            if (element.Description != Description) return -1;
            return 1;
        }
    }
}
