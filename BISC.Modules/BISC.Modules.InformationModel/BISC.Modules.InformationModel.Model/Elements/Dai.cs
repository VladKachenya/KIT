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
    public class Dai:ModelElement,IDai
    {
        public Dai()
        {
            ElementName = InfoModelKeys.ModelKeys.DaiKey;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ChildModelProperty<IVal> Value =>new ChildModelProperty<IVal>(this, InfoModelKeys.ModelKeys.ValKey);
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDai)) return -1;
            var element = obj as IDai;
            if (element.Name != Name) return -1;
            if (element.Description != Description) return -1;
            return 1;
        }
    }
}
