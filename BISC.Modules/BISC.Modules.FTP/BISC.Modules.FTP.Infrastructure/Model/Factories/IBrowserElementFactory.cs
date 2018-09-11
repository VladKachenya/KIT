using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Model.Factories
{
    public interface IBrowserElementFactory
    {
        IDeviceDirectory CreateRootDeviceDirectoryBrowserElement();
        IDeviceDirectory CreateDeviceDirectoryBrowserElement(string path, IDeviceDirectory parentDeviceDirectory);
        IDeviceFile CreateDeviceFileBrowserElement(string path, IDeviceDirectory parentDeviceDirectory);

        IDeviceBrowserElement CreateBrowserElement(string path, IDeviceDirectory parentDeviceDirectory);
        void SetConnectionProvider(object dataProvider);
    }
}
