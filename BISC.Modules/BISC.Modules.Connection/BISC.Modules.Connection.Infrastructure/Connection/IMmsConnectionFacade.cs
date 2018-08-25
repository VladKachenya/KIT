using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public interface IMmsConnectionFacade
    {
        Task<bool> TryOpenConnection(string ip);
        bool CheckConnection();
        void StopConnection();
    }
}