using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;

namespace BISC.Modules.Logging.Infrastructure.Model
{
   public class LoggableMessage:ILoggableMessage
    {
        #region Implementation of ILoggableMessage

        public string Message { get; set; }
        public SeverityEnum Severity { get; set; }
        public DateTime MessageDateTime { get; set; }

        #endregion
    }
}
