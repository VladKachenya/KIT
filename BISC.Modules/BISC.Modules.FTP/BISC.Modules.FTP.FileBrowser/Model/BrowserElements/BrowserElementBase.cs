using BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Model.BrowserElements
{
    public abstract class BrowserElementBase : IDeviceBrowserElement
    {
        private string _strongName;
        private string _name;

        protected BrowserElementBase(string elementPath, string name, IDeviceDirectory parentDirectory)
        {
            ElementPath = elementPath;
            Name = name;
            ParentDirectory = parentDirectory;
        }

        #region Implementation of IDeviceBrowserElement

        public IDeviceDirectory ParentDirectory { get; }

        public async Task<bool> DeleteElementAsync()
        {
            return await ParentDirectory.RemoveChildElementAsync(this);
        }

        public string ElementPath { get; }

        public string Name { get; }

        #endregion

        #region Implementation of Loadeble

        public abstract Task Load();

        #endregion

    }
}
