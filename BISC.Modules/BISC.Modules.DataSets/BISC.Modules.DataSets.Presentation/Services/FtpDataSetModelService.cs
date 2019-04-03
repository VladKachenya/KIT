using BISC.Infrastructure.Global.Common;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;


namespace BISC.Modules.DataSets.Presentation.Services
{
    public class FtpDataSetModelService : IFtpDataSetModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;

        #region ctor

        public FtpDataSetModelService(IDeviceFileWritingServices deviceFileWritingServices)
        {
            _deviceFileWritingServices = deviceFileWritingServices;
        }

        #endregion


        #region Implementation of IFtpDataSetMpdelService

        public async Task<OperationResult> WriteDatasetsToDevice(string ip, List<IDataSetViewModel> dataSetsToSave)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter streamWriter = new StringWriter(sb);
                Write(dataSetsToSave, streamWriter);
                var fileString = sb.ToString();
	            var res = await _deviceFileWritingServices.WriteFileStringInDevice(ip, new List<string>() {fileString},
		            new List<string>() {"DATASETS.CFG"});
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

        #endregion

        private void Write(List<IDataSetViewModel> dataSetsToParse, TextWriter streamTextWriter)
        {
            foreach (var dataSetViewModel in dataSetsToParse)
            {
                streamTextWriter.WriteLine(
                    $"DSN({dataSetViewModel.SelectedParentLd} {dataSetViewModel.SelectedParentLn}${dataSetViewModel.EditableNamePart})");
                foreach (var fcdaViewModel in dataSetViewModel.FcdaViewModels)
                {
                    IFcda fcda = fcdaViewModel.GetFcda();
                    string ld = fcda.LdInst;
                    var ln = fcda.Prefix + fcda.LnClass + fcda.LnInst;
                    var fc = fcda.Fc;
                    var doName = fcda.DoName.Replace(".", "$");
                    var daName = fcda.DaName;
                    if (daName == null)
                    {
                        streamTextWriter.WriteLine($"DSE({ld} {ln}${fc}${doName})");
                    }
                    else
                    {
                        streamTextWriter.WriteLine($"DSE({ld} {ln}${fc}${doName}${daName})");
                    }
                }
            }
        }

    }
}