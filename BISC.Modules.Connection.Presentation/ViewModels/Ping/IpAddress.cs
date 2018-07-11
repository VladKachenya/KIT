using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.ViewModels.Ping
{
    public class IpAddress : IIpAddress
    {
        public IpAddress()
        {
        }

        public Action<bool> PingAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetIp()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPing()
        {
            throw new NotImplementedException();
        }

        public bool IsIpValid()
        {
            throw new NotImplementedException();
        }
    }
}
