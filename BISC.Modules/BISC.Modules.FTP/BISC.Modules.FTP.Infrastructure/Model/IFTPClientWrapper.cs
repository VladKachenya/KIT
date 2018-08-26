using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model
{
    public interface IFTPClientWrapper
    {
        Task<FtpClient> Connect(string host, string login = null, string password = null);
        Task Disconnect();
        Task<string> DownloadFileString(string dirPath, string fileName);

        Task UploadFileString(List<string> filesStrings, List<string> fileNamesWithExt);
        Task ResetDeviceAsync();
        bool IsConnected { get; }
    }
}
