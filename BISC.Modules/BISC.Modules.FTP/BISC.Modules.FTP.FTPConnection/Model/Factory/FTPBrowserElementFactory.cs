using BISC.Modules.FTP.FTPConnection.Model.BrowserElements;
using BISC.Modules.FTP.FTPConnection.Model.Loaders;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Model.Factory;
using BISC.Modules.FTP.Infrastructure.Model.Loaders;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model.Factory
{
    public class FTPBrowserElementFactory : IBrowserElementFactory
    {
        public FTPBrowserElementFactory()
        {
        }


        #region Implementation of IBrowserElementFactory

        private FtpClient _ftpClient;
        public IDeviceDirectory CreateRootDeviceDirectoryBrowserElement()
        {
            if (_ftpClient == null) return null;
            _ftpClient.SetWorkingDirectory("1:");
            var f = _ftpClient.GetWorkingDirectory();
            IDirectoryLoader ftpDirectoryLoader = new FTPDirectoryLoader(this);
            ftpDirectoryLoader.SetDataProviderConnection(_ftpClient);
            IDeviceDirectory deviceDirectory = new DeviceDirectory(f, ftpDirectoryLoader, f, null);
            return deviceDirectory;
        }
        public IDeviceDirectory CreateDeviceDirectoryBrowserElement(string path,
            IDeviceDirectory parentDeviceDirectory)
        {
            _ftpClient.SetWorkingDirectory(path);
            string curDir = _ftpClient.GetWorkingDirectory();
            FTPDirectoryLoader ftpDirectoryLoader = new FTPDirectoryLoader(this);
            ftpDirectoryLoader.SetDataProviderConnection(_ftpClient);
            IDeviceDirectory deviceDirectory = new DeviceDirectory(curDir, ftpDirectoryLoader, curDir, parentDeviceDirectory);
            return deviceDirectory; ;
        }

        public IDeviceFile CreateDeviceFileBrowserElement(string path,
            IDeviceDirectory parentDeviceDirectory)
        {
            IFileLoader fileLoader = new FTPFileLoader();
            fileLoader.SetDataProvider(_ftpClient);
            IDeviceFile deviceFile = new DeviceFile(path, fileLoader, path.Split('/').Last(), parentDeviceDirectory);
            return deviceFile; ;
        }

        public IDeviceBrowserElement CreateBrowserElement(string path,
            IDeviceDirectory parentDeviceDirectory)
        {
            throw new NotImplementedException();
        }

        public void SetConnectionProvider(object dataProvider)
        {
            _ftpClient = dataProvider as FtpClient;
        }

        #endregion
    }
}
