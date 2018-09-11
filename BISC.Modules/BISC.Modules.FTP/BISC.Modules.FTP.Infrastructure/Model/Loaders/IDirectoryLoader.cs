using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model.Loaders
{
    public interface IDirectoryLoader
    {
        Task<List<IDeviceBrowserElement>> LoadDeviceDirectory(string directoryPath, IDeviceDirectory parentDeviceDirectory);
        Task<bool> RemoveElementFromDirectory(IDeviceBrowserElement deviceBrowserElement);
        Task<bool> CreateNewChildDirectoryAsync(string directoryPath);
        Task<string> CreateNewChildFileAsync(byte[] fileBytes, string directoryPath, string fileName, string extension);
        void SetDataProviderConnection(object dataProvider);
    }
}
