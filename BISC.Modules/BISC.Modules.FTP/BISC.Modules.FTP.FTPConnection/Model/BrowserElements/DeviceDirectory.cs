﻿using BISC.Modules.FTP.Infrastructure.Keys;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Model.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model.BrowserElements
{
    public class DeviceDirectory : BrowserElementBase, IDeviceDirectory
    {
        private List<IDeviceBrowserElement> _browserElementsInDirectory;
        private IDirectoryLoader _directoryLoader;


        public DeviceDirectory(string directoryPath, IDirectoryLoader directoryLoader, string name, IDeviceDirectory deviceDirectory) : base(directoryPath, name, deviceDirectory)
        {
            _directoryLoader = directoryLoader;
        }

        #region Implementation of IDataProviderContaining

        public override async Task Load()
        {
            _browserElementsInDirectory = await _directoryLoader.LoadDeviceDirectory(ElementPath, this);
            foreach (var browserElement in BrowserElementsInDirectory)
            {
                if (browserElement is ILoadable)
                {
                    await (browserElement as ILoadable).Load();
                }

            }
        }
        #endregion

        #region Implementation of IDeviceDirectory

        public override string StrongName => FTPKeys.DeviceDirectory;
        public List<IDeviceBrowserElement> BrowserElementsInDirectory
        {
            get { return _browserElementsInDirectory; }
        }

        public async Task<bool> RemoveChildElementAsync(IDeviceBrowserElement browserElement)
        {
            return await _directoryLoader.RemoveElementFromDirectory(browserElement);
        }

        public async Task<bool> CreateNewChildDirectoryAsync(string directoryName)
        {
            return await _directoryLoader.CreateNewChildDirectoryAsync(this.ElementPath + "\\" + directoryName);
        }

        public async Task<string> AddNewChildFileAsync(byte[] file, string name, string extension)
        {
            return await _directoryLoader.CreateNewChildFileAsync(file, Name, name, extension);
        }

        #endregion
    }
}
