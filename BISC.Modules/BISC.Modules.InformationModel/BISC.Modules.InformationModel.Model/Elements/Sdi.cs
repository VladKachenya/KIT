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
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is ISdi)) return -1;
            var element = obj as ISdi;
            if (element.Name != Name) return -1;
            return 1;
        }
    }
}
