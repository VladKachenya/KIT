using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
    public class FtpReportModelService : IFtpReportModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly ILoggingService _loggingService;
        private readonly IInjectionContainer _injectionContainer;

        public FtpReportModelService(
            IDeviceFileWritingServices deviceFileWritingServices, 
            ILoggingService loggingService, 
            IInjectionContainer injectionContainer)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
            _loggingService = loggingService;
            _injectionContainer = injectionContainer;
        }



        #region Implementation of IFtpReportModelService

        public async Task<List<IReportControl>> GetReportsFromDevice(string ip)
        {
            List<IReportControl> reportControls = new List<IReportControl>();
            try
            {
                var fileStringRes = await _deviceFileWritingServices.ReadFileStringFromDevice(ip, "1:/CFG", "XRCB.CFG");
                if (!fileStringRes.IsSucceed)
                {
                    _loggingService.LogMessage($"Ошибка чтения отчетов: {fileStringRes.GetFirstError()}", SeverityEnum.Warning);
                    return new List<IReportControl>();
                }

                var fileString = fileStringRes.Item;
                TextReader textReader = new StringReader(fileString);
                string reportPatternBr = "RCB(.*[$]BR[$].*)";
                Regex reportRegexBr = new Regex(reportPatternBr);
                string reportPatternRp = "RCB(.*[$]RP[$].*)";
                Regex reportRegexRp = new Regex(reportPatternRp);

                string line;
                do
                {
                    line = textReader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (reportRegexBr.IsMatch(line))
                        {
                            var reportStrings = line.Split(new string[] { "$BR$" }, StringSplitOptions.RemoveEmptyEntries);
                            var reportName = reportStrings[1].Split(' ')[0];
                            //Почему тут не через фабирику?
                            reportControls.Add(new ReportControl()
                            {
                                Name = reportName,
                                // IsDynamic = true
                            });
                        }
                        if (reportRegexRp.IsMatch(line))
                        {
                            var reportStrings = line.Split(new string[] { "$RP$" }, StringSplitOptions.RemoveEmptyEntries);
                            var reportName = reportStrings[1].Split(' ')[0];
                            //Почему тут не через фабирику?
                            reportControls.Add(new ReportControl()
                            {
                                Name = reportName,
                                //IsDynamic = true
                            });
                        }
                    }
                } while (!string.IsNullOrEmpty(line));
            }
            catch (Exception e)
            {

            }
            return reportControls;
        }

        public async Task<OperationResult> WriteReportsToDevice(IDevice device, IEnumerable<IReportControl> reportControls)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var fileString =
                    _injectionContainer.ResolveType<IConfigurationParser>(InfrastructureKeys.ModulesKeys.ReportModule).
                        GetConfiguration(reportControls, device).Item;

                var res = await _deviceFileWritingServices.WriteFileStringInDevice(device.Ip, new List<string>() { fileString },
                    new List<string>() { "XRCB.CFG" });
                if (!res.IsSucceed)
                {
                    return new OperationResult($"{device.Ip}: FTP не отвечает: {res.GetFirstError()}");
                }
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message + Environment.NewLine + e.StackTrace);
            }
            return OperationResult.SucceedResult;
        }
    }
    #endregion
}
