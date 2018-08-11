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
        public string Ip { get; set; }
        public bool PingResult { get; set; }
    }
}
