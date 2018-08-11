using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Events
{
   public class IpSelectedEvent
    {
        public IpSelectedEvent(string ip)
        {
            Ip = ip;
        }

        public string Ip { get; }
    }
}
