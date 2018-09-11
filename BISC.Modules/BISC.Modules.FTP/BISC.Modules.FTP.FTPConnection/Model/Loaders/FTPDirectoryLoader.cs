using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Model.Factory;
using BISC.Modules.FTP.Infrastructure.Model.Loaders;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model.Loaders
{
    public class FTPDirectoryLoader : IDirectoryLoader
    {
        private readonly IBrowserElementFactory _browserElementFactory;
        private FtpClient _ftpClient;

        public FTPDirectoryLoader(IBrowserElementFactory browserElementFactory)
        {
            _browserElementFactory = browserElementFactory;
        }


        #region Implementation of IDirectoryLoader

        public async Task<List<IDeviceBrowserElement>> LoadDeviceDirectory(string directoryPath, IDeviceDirectory parentDeviceDirectory)
        {
            List<IDeviceBrowserElement> deviceBrowserElements = new List<IDeviceBrowserElement>();
            string[] dirList = null;
            await Task.Run((() =>
            {
                dirList = _ftpClient.GetNameListing(parentDeviceDirectory.ElementPath);
            }));
            foreach (var dirElement in dirList)
            {
                bool isElementIsDirectory = !dirElement.Contains(".");
                if (isElementIsDirectory)
                {
                    deviceBrowserElements.Add(
                        _browserElementFactory.CreateDeviceDirectoryBrowserElement(directoryPath + "/" + dirElement,
                            parentDeviceDirectory));
                }
                else
                {
                    deviceBrowserElements.Add(
                        _browserElementFactory.CreateDeviceFileBrowserElement(directoryPath + "/" + dirElement,
                            parentDeviceDirectory));
                }
            }
            return deviceBrowserElements;
        }

        public async Task<bool> RemoveElementFromDirectory(IDeviceBrowserElement deviceBrowserElement)
        {
            try
            {

                await _ftpClient.DeleteFileAsync(deviceBrowserElement.ElementPath);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<bool> CreateNewChildDirectoryAsync(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateNewChildFileAsync(byte[] fileBytes, string directoryPath, string fileName, string extension)
        {
            throw new NotImplementedException();
        }

        public void SetDataProviderConnection(object dataProvider)
        {
            _ftpClient = dataProvider as FtpClient;
        }

        #endregion
    }
}
