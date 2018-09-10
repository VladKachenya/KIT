using BISC.Modules.FTP.FileBrowser.Interfaces.Model.Loaders;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model
{
    public class FTPFileLoader : IFileLoader
    {
        private FtpClient _ftpClient;
        #region Implementation of IDataProviderContaining


        public byte[] LoadFileData(string filePath)
        {
            _ftpClient.Download(out var result, filePath);
            return result;
        }

        public void SetDataProvider(object dataprovider)
        {
            _ftpClient = dataprovider as FtpClient;
        }

        #endregion
    }
}
