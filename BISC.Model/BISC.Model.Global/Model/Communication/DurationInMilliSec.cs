using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDurationInMilliSec)) return -1;
            var element = obj as IDurationInMilliSec;
            if (element.Unit != Unit) return -1;
            if (element.Multiplier != Multiplier) return -1;
            if (element.Value != Value) return -1;
            return 1;
        }
    }
}
