using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Events
{
   public class RegionDisposingEvent
    {
        public RegionDisposingEvent(string disposingRegionName)
        {
            DisposingRegionName = disposingRegionName;
        }



        public string DisposingRegionName { get;  }
    }
}
