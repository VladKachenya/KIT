using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements
{
    public interface IDeviceDirectory 
    {
        List<IDeviceBrowserElement> BrowserElementsInDirectory { get; }
        Task<bool> RemoveChildElementAsync(IDeviceBrowserElement browserElement);
        Task<bool> CreateNewChildDirectoryAsync(string directoryName);
        Task<string> AddNewChildFileAsync(byte[] file, string name, string extension);
    }
}
