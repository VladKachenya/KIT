using System;
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
        Task<OperationResult<List<string>>> GetListValiablesAsync(string ldInst);
        Task<OperationResult<MmsTypeDescription>> GetMmsTypeDescription(string ldName, string lnName);
    }

    public class MmsTypeDescription
    {
        public MmsTypeDescription()
        {
            Components=new List<MmsTypeDescription>();
        }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsStructure { get; set; }
        public bool IsArray { get; set; }
        public List<MmsTypeDescription> Components { get; }
    }
}