using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Events
{
   public class SaveCheckEvent
    {
        public SaveCheckEvent(string regionName, bool isHaveChanges)
        {
            RegionName = regionName;
            IsHaveChanges = isHaveChanges;
        }
        public string RegionName { get; }
        public bool IsHaveChanges { get; }
    }
}
