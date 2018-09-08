using BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Interfaces.Model
{
    public interface IFileBrowser
    {
        IDeviceDirectory RootDeviceDirectory { get; }
        Task LoadRootDirectory();
    }
}
