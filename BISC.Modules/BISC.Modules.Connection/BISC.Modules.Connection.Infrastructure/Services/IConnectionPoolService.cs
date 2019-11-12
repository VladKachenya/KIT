using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Modules.Connection.Infrastructure.Services
{
    public interface IConnectionPoolService
    {
        IDeviceConnection GetConnection(string ip);
        bool GetIsDeviceConnect(string ip);
    }
}