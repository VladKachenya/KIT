using BISC.Modules.FTP.Infrastructure.Model.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model.BrowserElements
{
    public interface IDeviceFile : ILoadable, IDeviceBrowserElement
    {
        byte[] FileData { get; }
        void Download(string path);
    }
}
