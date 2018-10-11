using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
    public class Val : ModelElement, IVal
    {
        public Val()
        {
            ElementName = InfoModelKeys.ModelKeys.ValKey;
        }
        public string Value { get; set; }
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IVal)) return -1;
            var element = obj as IVal;
            if (element.Value != Value) return -1;
            return 1;
        }
    }
}
