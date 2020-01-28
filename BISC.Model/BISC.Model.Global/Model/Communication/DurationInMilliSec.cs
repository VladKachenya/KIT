using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
    public class DurationInMilliSec:ModelElement,IDurationInMilliSec
    {
        public DurationInMilliSec()
        {
            
        }

        public DurationInMilliSec(string elName)
        {
            ElementName = elName;
        }

        public string Unit { get; set; }
        public string Multiplier { get; set; }
        public int Value { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDurationInMilliSec)) return false;
            var element = obj as IDurationInMilliSec;
            if (element.Unit != Unit) return false;
            if (element.Multiplier != Multiplier) return false;
            //if (element.Value != Value) return false;
            return true;
        }
    }
}
