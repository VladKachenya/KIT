using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Interfaces.Ping
{
    public interface IIpAddress
    {
        string GetIp();
        bool IsIpValid();

        Task<bool> GetPing();

        Action<bool> PingAction { get; set; }
    }
}
