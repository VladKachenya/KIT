using BISC.Modules.FTP.Infrastructure.Keys;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Model.Loaders;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Model.BrowserElements
{
    public class DeviceFile : BrowserElementBase, IDeviceFile
    {
        private readonly IFileLoader _fileLoader;

        public DeviceFile(string elementPath, IFileLoader fileLoader, string name, IDeviceDirectory deviceDirectory) : base(elementPath, name, deviceDirectory)
        {
            _fileLoader = fileLoader;
        }

        #region Implementation of IDeviceFile

        public override string StrongName => FTPKeys.DeviceFile;

        public byte[] FileData { get; private set; }

        public void Download(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            try
            {
                FileData = _fileLoader.LoadFileData(ElementPath);
                sw.Write(UTF8Encoding.UTF8.GetString(FileData));
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                sw.Close();
            }
        }

        #endregion

        #region Implementation of IDataProviderContaining

        public override async Task Load()
        {
        }

        #endregion
    }
}
