using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.Events
{
   public class ConnectionEvent
    {
        public ConnectionEvent(bool isConnected, string ip)
        {
            IsConnected = isConnected;
            Ip = ip;
        }

        public  string Ip { get; }
        public bool IsConnected { get; }
    }
}
