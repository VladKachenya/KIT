using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Logging.Infrastructure.Events;
using BISC.Modules.Logging.Infrastructure.Model;
using Newtonsoft.Json;
using NLog;
using NLog.Targets;

namespace BISC.Modules.Logging.Infrastructure.Services
{
    public class LoggingService:ILoggingService
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConfigurationService _configurationService;

        public LoggingService(IGlobalEventsService globalEventsService,IConfigurationService configurationService)
        {
            _globalEventsService = globalEventsService;
            _configurationService = configurationService;
        }


        #region Implementation of ILoggingService

        public void LogUserAction(string actionName)
        {
            ILoggableMessage loggableMessage = new LoggableMessage();
            loggableMessage.Message = actionName;
            loggableMessage.Severity = SeverityEnum.Info;
            loggableMessage.MessageType = MessageTypeEnum.UserAction;
            loggableMessage.MessageDateTime = DateTime.Now;
            var logger = NLog.LogManager.GetLogger("logger");
            logger.Warn(JsonConvert.SerializeObject(loggableMessage));
            if (_configurationService.IsUserLoggingEnabled)
            {
                _globalEventsService.SendMessage(new LogEvent(loggableMessage));
            }
        }

        public void LogMessage(string message, SeverityEnum severity)
        {
           ILoggableMessage loggableMessage=new LoggableMessage();
            loggableMessage.Message = message;
            loggableMessage.Severity = severity;
            loggableMessage.MessageType = MessageTypeEnum.Message;
            loggableMessage.MessageDateTime=DateTime.Now;
            var logger = NLog.LogManager.GetLogger("logger");
            logger.Warn(JsonConvert.SerializeObject(loggableMessage));
            _globalEventsService.SendMessage(new LogEvent(loggableMessage));
        }

        #endregion
    }
}
