using BISC.Modules.FTP.FTPConnection.Model;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

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

        public async Task<OperationResult> WriteFileStringInDevice(string ip, List<string> filesStrings, List<string> fileNamesWithExt)
        { 
            try
            {
                await _ftpClientWrapper.Connect(ip);
                await _ftpClientWrapper.UploadFileString(filesStrings, fileNamesWithExt);
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }
            return OperationResult.SucceedResult;
        }

        public async Task<OperationResult<string>> ReadFileStringFromDevice(string ip, string dirPath, string fileNamesWithExt)
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
                return new OperationResult<string>(e.Message);
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }
            return new OperationResult<string>(file,true);
        }

        public async Task<OperationResult> DeletFileStringFromDevice(string ip, string filePath)
        {
            try
            {
                var ftpClient = await _ftpClientWrapper.Connect(ip);
                await ftpClient.DeleteFileAsync(filePath);
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();

            }
            return new OperationResult();
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
