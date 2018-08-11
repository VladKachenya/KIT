using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.Events;

namespace BISC.Modules.Connection.Presentation.Events
{
    public class IpPingedEvent
    {
        public IpPingedEvent(string ip, bool pingResult)
        {
            Ip = ip;
            PingResult = pingResult;
        }
        public string Ip { get;}
        public bool PingResult { get; }
    }
}
