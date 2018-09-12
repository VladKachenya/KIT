using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model.BrowserElements
{
    public abstract class BrowserElementBase : IDeviceBrowserElement
    {

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
        public abstract string StrongName { get; }

        #endregion

        #region Implementation of Loadeble

        public abstract Task Load();

        #endregion
    }
}
