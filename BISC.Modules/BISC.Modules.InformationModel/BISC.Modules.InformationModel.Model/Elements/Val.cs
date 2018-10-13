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
    public class Val : ModelElement, IVal
    {
        public Val()
        {
            ElementName = InfoModelKeys.ModelKeys.ValKey;
        }
        public string Value { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is IVal)) return false;
            var element = obj as IVal;
            if (element.Value != Value) return false;
            return true;
        }
    }
}
