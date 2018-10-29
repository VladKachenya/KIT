using BISC.Modules.FTP.FTPConnection.Model;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Services
{
    public class DeviceFileWritingServices : IDeviceFileWritingServices
    {
        private readonly IFTPClientWrapper _ftpClientWrapper;

        public DeviceFileWritingServices( IFTPClientWrapper ftpClientWrapper)
        {
            _ftpClientWrapper = ftpClientWrapper;
            //_modelLoadingController = modelLoadingController;
        }

        #region Implementation of IDeviceFileWritingController

        public async Task<bool> WriteFileStringInDevice(string ip, List<string> filesStrings, List<string> fileNamesWithExt)
        { 
            try
            {
                await _ftpClientWrapper.Connect(ip);
                await _ftpClientWrapper.UploadFileString(filesStrings, fileNamesWithExt);
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }
            return true;
        }

        public async Task<string> ReadFileStringFromDevice(string ip, string dirPath, string fileNamesWithExt)
        {
            //string dirPath = path;
            //string fileNamesWithExt;
            string file;
            try
            {
                await _ftpClientWrapper.Connect(ip);
                file = await _ftpClientWrapper.DownloadFileString(dirPath, fileNamesWithExt);
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }
            return file;
        }


        public async Task ResetDevice(string ip)
        {
            try
            {
                await _ftpClientWrapper.Connect(ip);
                await ResetDevice();
            }
            catch (Exception e)
            {
                return ;
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }

        }


        private async Task ResetDevice()
        {
            try
            {
                await _ftpClientWrapper.ResetDeviceAsync();

            }
            catch (Exception e)
            {
                //_userInteractionService.ShowMessageToUser("Произошла ошибка", e.Message);
            }
            finally
            {
            }

        }

        #endregion
    }
}
