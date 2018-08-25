using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Modules.Connection.MMS.MmsClientServices
{
    public class MmsConnectionFacade : IMmsConnectionFacade
    {
        private string _ip;
        private readonly Iec61850State _state;

        public MmsConnectionFacade()
        {
            _state = new Iec61850State();
        }

        public Task<bool> TryOpenConnection(string ip)
        {
            _ip = ip;
            return new InitService(_state).TryOpenConnection(_ip);
        }

        public bool CheckConnection()
        {
            if (_ip == null) return false;
            return TcpRw.CheckConnection(_state);
        }

        public void StopConnection()
        {
            TcpRw.StopClient(_state);
        }
    }
}