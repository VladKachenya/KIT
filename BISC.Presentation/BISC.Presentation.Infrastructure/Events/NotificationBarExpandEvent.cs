using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Events
{
   public class NotificationBarExpandEvent
    {
        public NotificationBarExpandEvent(bool isExpanded)
        {
            IsExpanded = isExpanded;
        }
        public bool IsExpanded { get; }
    }
}
