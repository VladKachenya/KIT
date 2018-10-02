using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;

namespace BISC.Modules.Logging.Infrastructure.Services
{
    public class LoggingService:ILoggingService
    {
        public LoggingService()
        {
            
        }


        #region Implementation of ILoggingService

        public void LogUserAction(string actionName)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message, SeverityEnum severity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
