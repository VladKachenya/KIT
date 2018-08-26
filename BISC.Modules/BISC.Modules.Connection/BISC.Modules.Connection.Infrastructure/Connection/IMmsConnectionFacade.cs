using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

namespace BISC.Modules.Connection.Infrastructure.Connection
{
    public interface IMmsConnectionFacade
    {
        Task<bool> TryOpenConnection(string ip);
        bool CheckConnection();
        void StopConnection();
        Task<OperationResult<List<string>>> IdentifyAsync();
        Task<OperationResult<List<string>>> GetLdListAsync();
        Task<OperationResult<List<string>>> GetListValiablesAsync(string deviceName, string ldInst);

    }
}