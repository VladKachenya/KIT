using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Serviсes
{
    public interface IDeviceFileWritingServices
    {
        Task<bool> WriteFileStringInDevice(string ip, List<string> filesStrings, List<string> fileNamesWithExt);
        void OpenFtpBrowser();
        Task ResetDevice();
    }
}
