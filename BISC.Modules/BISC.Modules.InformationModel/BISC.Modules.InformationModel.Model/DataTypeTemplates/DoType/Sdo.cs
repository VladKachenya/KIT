using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
   public class Sdo:ModelElement,ISdo
    {
        public Sdo()
        {
            ElementName = "SDO";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDa)) return -1;
            var element = obj as IDa;
            if (element.Name != Name) return -1;
            if (element.Type != Type) return -1;
            return 1;
        }
    }
}
