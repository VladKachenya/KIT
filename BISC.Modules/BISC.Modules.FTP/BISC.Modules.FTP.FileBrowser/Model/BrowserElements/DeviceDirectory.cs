using BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements;
using BISC.Modules.FTP.FileBrowser.Interfaces.Model.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Model.BrowserElements
{
    public class DeviceDirectory : BrowserElementBase, IDeviceDirectory
    {
        private List<IDeviceBrowserElement> _browserElementsInDirectory;
        private IDirectoryLoader _directoryLoader;
        private string _strongName;


        public DeviceDirectory(string directoryPath, IDirectoryLoader directoryLoader, string name, IDeviceDirectory deviceDirectory) : base(directoryPath, name, deviceDirectory)
        {
            _directoryLoader = directoryLoader;
        }


        #region Implementation of IDeviceDirectory

        public List<IDeviceBrowserElement> BrowserElementsInDirectory
        {
            get { return _browserElementsInDirectory; }
        }

        public async Task<bool> RemoveChildElementAsync(IDeviceBrowserElement browserElement)
        {
            return await _directoryLoader.RemoveElementFromDirectory(browserElement);
        }

        public async Task<bool> CreateNewChildDirectoryAsync(string directoryName)
        {
            return await _directoryLoader.CreateNewChildDirectoryAsync(this.ElementPath + "\\" + directoryName);
        }

        public async Task<string> AddNewChildFileAsync(byte[] file, string name, string extension)
        {
            return await _directoryLoader.CreateNewChildFileAsync(file, Name, name, extension);
        }

        #endregion
    }
}
