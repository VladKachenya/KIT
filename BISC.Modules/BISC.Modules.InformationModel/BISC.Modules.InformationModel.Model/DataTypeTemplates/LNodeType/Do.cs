using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType
{
    public class Do:ModelElement,IDo
    {
        public Do()
        {
            ElementName = "DO";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDo)) return -1;
            var element = obj as IDo;
            if (element.Name != Name) return -1;
            if (element.Type != Type) return -1;
            return 1;
        }
    }
}
