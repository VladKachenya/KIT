using BISC.Modules.FTP.FileBrowser.Interfaces.Model.BrowserElements;
using BISC.Modules.FTP.FileBrowser.Interfaces.Model.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FileBrowser.Model.BrowserElements
{
    public class DeviceFile : BrowserElementBase, IDeviceFile
    {
        private readonly IFileLoader _fileLoader;
        private byte[] _fileData;

        public DeviceFile(string elementPath, IFileLoader fileLoader, string name, IDeviceDirectory deviceDirectory) : base(elementPath, name, deviceDirectory)
        {
            _fileLoader = fileLoader;
        }

        #region Implementation of IDeviceFile

        public byte[] FileData
        {
            get { return _fileData; }
        }

        public void Download()
        {
            //_fileData = _fileLoader.LoadFileData(ElementPath);
            //string path = (StaticContainer.Container.Resolve(typeof(IApplicationGlobalCommands)) as IApplicationGlobalCommands)
            //     .SelectFilePathToSave("Сохранить файл", "", "", Name).GetFirstValue();
            //StreamWriter sw = new StreamWriter(path);
            //sw.Write(UTF8Encoding.UTF8.GetString(_fileData));
            //sw.Close();

        }

        #endregion

    }
}
