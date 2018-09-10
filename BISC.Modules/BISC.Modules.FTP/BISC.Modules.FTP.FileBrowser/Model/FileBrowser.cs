using BISC.Modules.FTP.FileBrowser.Interfaces.Factories;
using BISC.Modules.FTP.FileBrowser.Interfaces.Model;
using BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Model
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
