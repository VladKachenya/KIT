using BISC.Modules.FTP.FileBrowser.Interfaces.Model.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements
{
    public interface IDeviceBrowserElement : ILoadable
    {
        IDeviceDirectory ParentDirectory { get; }
        Task<bool> DeleteElementAsync();
        string ElementPath { get; }
        string Name { get; }
    }
}
