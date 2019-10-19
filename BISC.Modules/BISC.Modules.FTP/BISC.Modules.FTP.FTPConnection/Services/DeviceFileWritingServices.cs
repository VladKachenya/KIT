using BISC.Modules.FTP.FTPConnection.Model;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Services;

namespace BISC.Modules.FTP.FTPConnection.Services
{
    public class DeviceFileWritingServices : IDeviceFileWritingServices
    {
        private readonly IFTPClientWrapper _ftpClientWrapper;
        private readonly IConfigurationService _configurationService;

        public DeviceFileWritingServices(IFTPClientWrapper ftpClientWrapper, IConfigurationService configurationService)
        {
            _ftpClientWrapper = ftpClientWrapper;
            _configurationService = configurationService;
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
                var fileTask = ReadFileStringFromDeviceAsync(ip, dirPath, fileNamesWithExt);
                var timer = Task.Delay(_configurationService.FtpTimeOutDelay);
                await Task.WhenAny(fileTask, timer);
                if (fileTask.IsFaulted)
                {
                    if (fileTask.Exception != null)
                        if (fileTask.Exception.InnerException != null)
                            throw fileTask.Exception.InnerException;
                }
                if (!fileTask.IsCompleted)
                {
                    return new OperationResult<string>("Ftp server is not responding!!!");
                }
                file = fileTask.Result;
            }
            catch (Exception e)
            {
                return new OperationResult<string>(e.Message);
            }
            //finally
            //{
            //    await _ftpClientWrapper.Disconnect();

            //}
            return new OperationResult<string>(file, true);
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
                return;
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

        private async Task<string> ReadFileStringFromDeviceAsync(string ip, string dirPath, string fileNamesWithExt)
        {
            try
            {
                await _ftpClientWrapper.Connect(ip);
                var file = await _ftpClientWrapper.DownloadFileString(dirPath, fileNamesWithExt);
                return file;
            }
            finally
            {
                await _ftpClientWrapper.Disconnect();
            }
        }

        #endregion
    }
}
