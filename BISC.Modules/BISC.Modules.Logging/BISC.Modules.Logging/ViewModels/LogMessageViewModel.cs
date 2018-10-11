using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;

namespace BISC.Modules.Logging.Infrastructure.ViewModels
{
  public class LogMessageViewModel
    {
        private readonly ILoggableMessage _loggableMessage;

        public LogMessageViewModel(ILoggableMessage loggableMessage)
        {
            _loggableMessage = loggableMessage;
            Severity = loggableMessage.Severity;
            Message = loggableMessage.Message;
            MessageDateTime ="["+ loggableMessage.MessageDateTime.ToString()+"]";
        }
        public string MessageDateTime { get; set; }
        public SeverityEnum Severity { get; set; }
        public string Message { get; set; }

    }
}
