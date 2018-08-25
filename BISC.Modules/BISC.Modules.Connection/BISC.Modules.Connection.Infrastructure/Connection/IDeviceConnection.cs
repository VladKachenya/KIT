using System;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public interface IDeviceConnection
    {
        string Ip { get; set; }
        bool IsConnected { get; }
        Task OpenConnection();
        void StopConnection();             
        IMmsConnectionFacade MmsConnection { get; }
    }
}