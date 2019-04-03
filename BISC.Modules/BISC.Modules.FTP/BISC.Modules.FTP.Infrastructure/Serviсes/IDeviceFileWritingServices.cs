using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

namespace BISC.Modules.FTP.Infrastructure.Serviсes
{
    public interface IDeviceFileWritingServices
    {
        Task<OperationResult> WriteFileStringInDevice(string ip, List<string> filesStrings, List<string> fileNamesWithExt);
		
        Task<OperationResult<string>> ReadFileStringFromDevice(string ip, string dirPath, string fileNamesWithExt);

        Task ResetDevice(string ip);
    }
}
