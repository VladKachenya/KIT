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

namespace BISC.Modules.Reports.Model.Services
{
    public class FtpReportModelService : IFtpReportModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private readonly ILoggingService _loggingService;

        public FtpReportModelService(IDeviceFileWritingServices deviceFileWritingServices, ILoggingService loggingService)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
            _loggingService = loggingService;
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

        public async Task<OperationResult> WriteReportsToDevice(string ip, List<IReportControl> reportControls, ILDevice lDevice)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter streamWriter = new StringWriter(sb);
                Write(reportControls, streamWriter, lDevice);
                var fileString = sb.ToString();
                var res = await _deviceFileWritingServices.WriteFileStringInDevice(ip, new List<string>() { fileString },
                    new List<string>() { "XRCB.CFG" });
                if (!res.IsSucceed)
                {
                    return new OperationResult($"{ip}: FTP не отвечает: {res.GetFirstError()}");
                }
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message + Environment.NewLine + e.StackTrace);
            }
            return OperationResult.SucceedResult;


        }

        private void Write(List<IReportControl> reportsToParse, TextWriter streamWriter, ILDevice lDevice)
        {
            using (streamWriter)
            {
                foreach (var reportObj in reportsToParse)
                {
                    var ld = lDevice.Inst;
                    string rptName = reportObj.Name;
                    string rptId = reportObj.RptID;
                    int isBuffered = reportObj.Buffered ? 1 : 0;
                    string dsName = reportObj.DataSet;
                    uint confRev = 0;
                    confRev = (uint)reportObj.ConfRev;

                    var trgOpt = reportObj.TrgOps.Value.TriggerOptionsToInt();
                    var optFields = reportObj.OptFields.Value.ReportOptionsToInt();

                    var bufTm = reportObj.BufTime;
                    var intgPd = reportObj.IntgPd;

                    for (int i = 0; i < reportObj.RptEnabled.Value.Max; i++)
                    {
                        streamWriter.WriteLine(
                            $"RCB({ld} {rptName + "0" + (i + 1)} {rptId} {isBuffered} {dsName} {confRev} {trgOpt} {optFields} {bufTm} {intgPd})");
                    }
                }
            }
        }
    }
    #endregion
}
