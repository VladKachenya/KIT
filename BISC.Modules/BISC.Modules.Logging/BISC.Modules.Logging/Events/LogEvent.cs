using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;

namespace BISC.Modules.Logging.Infrastructure.Events
{
   public class LogEvent
    {
        public LogEvent(ILoggableMessage message)
        {
            Message = message;
        }
        public ILoggableMessage Message { get; }

    }
}
