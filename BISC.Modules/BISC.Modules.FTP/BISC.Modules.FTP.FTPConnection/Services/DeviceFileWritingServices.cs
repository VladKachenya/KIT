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

            // }
        }

        public async Task ResetDevice()
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
