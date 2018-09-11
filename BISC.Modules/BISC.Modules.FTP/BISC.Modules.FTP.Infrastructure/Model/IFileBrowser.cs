using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model
{
    public interface IFileBrowser
    {
        IDeviceDirectory RootDeviceDirectory { get; }
        Task LoadRootDirectory();
    }
}
