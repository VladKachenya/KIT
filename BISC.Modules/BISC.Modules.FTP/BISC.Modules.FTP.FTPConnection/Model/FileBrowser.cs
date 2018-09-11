using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Model.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model
{
    public class FileBrowser : IFileBrowser
    {
        private IBrowserElementFactory _browserElementFactory;
        private IDeviceDirectory _rootDeviceDirectory;
        private string _strongName;


        public FileBrowser(IBrowserElementFactory browserElementFactory)
        {
            _browserElementFactory = browserElementFactory;

        }

        #region Implementation of IFileBrowser

        public IDeviceDirectory RootDeviceDirectory
        {
            get { return _rootDeviceDirectory; }
        }

        public async Task LoadRootDirectory()
        {

            _rootDeviceDirectory =
                _browserElementFactory.CreateRootDeviceDirectoryBrowserElement();

            await _rootDeviceDirectory?.Load();
        }

        public Task<bool> RemoveElement()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
