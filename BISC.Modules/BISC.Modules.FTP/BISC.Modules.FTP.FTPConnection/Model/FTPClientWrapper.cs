using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
using BISC.Modules.FTP.FTPConnection.Services;
using BISC.Modules.FTP.Infrastructure.Model;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BISC.Modules.FTP.FTPConnection.Model
{
    public class FTPClientWrapper : IFTPClientWrapper
    {
        //private readonly IProjectGlobalModel _projectGlobalModel;
        private FtpClient _ftpClient;
        private bool _isFtpConnected = false;
        private IGlobalEventsService _globalEventsService;

        //public FtpClientWrapper(IProjectGlobalModel projectGlobalModel)
        public FTPClientWrapper(IGlobalEventsService globalEventsService)
        {
            _globalEventsService = globalEventsService;
        }

        public async Task<FtpClient> Connect(string host, string login = null, string password = null)
        {
            await Disconnect();
            _ftpClient = new FtpClient();
            _ftpClient.DataConnectionType = FtpDataConnectionType.PORT;
            _ftpClient.Host = host;
            _ftpClient.Credentials = new NetworkCredential(login, password);
            await _ftpClient.ConnectAsync();
            IsConnected = true;
            return _ftpClient;
        }

        public async Task Disconnect()
        {
            if (_ftpClient != null && _ftpClient.IsConnected)
            {
                await _ftpClient.DisconnectAsync();
                IsConnected = false;
            }
        }

        public async Task<string> DownloadFileString(string dirPath, string fileNamesWithExt)
        {
            byte[] bytes = null;

            if (!await _ftpClient.DirectoryExistsAsync(dirPath))
            {
                throw new Exception("Ошибка загрузки файла по FTP: требуемой директории не существует");
            }
            _ftpClient.SetWorkingDirectory(dirPath);
            List<string> dirCfg=new List<string>();

            await Task.Run((() =>
            {
                dirCfg = _ftpClient.GetNameListing().ToList();
            }));

            if (!dirCfg.Contains(fileNamesWithExt))
            {
                throw new Exception($"В устройстве отсутствует требуемый файл {fileNamesWithExt}");
            }
            await Task.Run((() =>
            {
                _ftpClient.Download(out bytes, dirPath + "/" + fileNamesWithExt);
            }));
            //  bytes = await _ftpClient.DownloadAsync(dirPath + "/" + fileNamesWithExt);

            if (fileNamesWithExt.Split('.')[1] == "ZIP")
            {
                return bytes.Unzip();
            }
            else
            {
                return UTF8Encoding.UTF8.GetString(bytes);
            }

        }


        public async Task UploadFileString(List<string> filesStrings, List<string> fileNamesWithExt)
        {
            await _ftpClient.SetWorkingDirectoryAsync("1:/CFG");
            string f = _ftpClient.GetWorkingDirectory();

            for (int i = 0; i < filesStrings.Count; i++)
            {

                if (fileNamesWithExt[i].Split('.')[1] == "ZIP")
                {
                    _ftpClient.Upload(filesStrings[i].Zip(), fileNamesWithExt[i],
                        FtpExists.Overwrite);
                    continue;
                }
                await Task.Run(() => _ftpClient.Upload(UTF8Encoding.UTF8.GetBytes(filesStrings[i]), fileNamesWithExt[i],
                    FtpExists.Overwrite));
            }
        }

        public async Task ResetDeviceAsync()
        {

            FtpReply reply = _ftpClient.Execute("REST");
            if (!reply.Success)
            {
                MessageBox.Show(reply.ErrorMessage);
            }
            if (reply.Success)
            {
                //(_projectGlobalModel.DeviceConnection as IecConnection).Stop();

            }
        }

        public bool IsConnected
        {
            get
            {
                return _isFtpConnected;
            }
            protected set
            {
                _isFtpConnected = value;
                _globalEventsService.SendMessage(new FTPChangingConnectionEvent());
            }
        }
    }
}
