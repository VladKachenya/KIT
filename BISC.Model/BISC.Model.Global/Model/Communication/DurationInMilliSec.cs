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
    }
}
