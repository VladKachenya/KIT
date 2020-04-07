using BISC.Infrastructure.Global.Common;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Keys;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;



namespace BISC.Modules.DataSets.Presentation.Services
{
    //It needs move to model!
    public class FtpDataSetModelService : IFtpDataSetModelService
    {
        private readonly IDeviceFileWritingServices _deviceFileWritingServices;
        private IConfigurationParser _dataSetConfigurationParser;

        #region ctor

        public FtpDataSetModelService(
            IDeviceFileWritingServices deviceFileWritingServices,
            IInjectionContainer injectionContainer)
        {
            _dataSetConfigurationParser =
                injectionContainer.ResolveType<IConfigurationParser>(InfrastructureKeys.ModulesKeys.DataSetModule);
            _deviceFileWritingServices = deviceFileWritingServices;
        }

        #endregion


        #region Implementation of IFtpDataSetMpdelService

        public async Task<OperationResult> WriteDataSetsToDevice(IDevice device, IEnumerable<IDataSet> dataSetsToSave)
        {
            try
            {
                var fileString = _dataSetConfigurationParser.GetConfiguration(dataSetsToSave, device).Item;

                var res = await _deviceFileWritingServices.WriteFileStringInDevice(device.Ip, new List<string>() { fileString },
                    new List<string>() { "DATASETS.CFG" });
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

        #endregion

        //private void Write(List<IDataSet> dataSetsToParse, TextWriter streamTextWriter)
        //{
        //    foreach (var dataSet in dataSetsToParse)
        //    {
        //        var lnParent = dataSet.GetFirstParentOfType<ILogicalNode>();
        //        var ldParent = dataSet.GetFirstParentOfType<ILDevice>();

        //        streamTextWriter.WriteLine(
        //            $"DSN({ldParent.Inst} {lnParent.Name}${dataSet.Name})");
        //        foreach (var fcda in dataSet.FcdaList)
        //        {
        //            string ld = fcda.LdInst;
        //            var ln = fcda.Prefix + fcda.LnClass + fcda.LnInst;
        //            var fc = fcda.Fc;
        //            var doName = fcda.DoName.Replace(".", "$");
        //            var daName = fcda.DaName;
        //            if (daName == null)
        //            {
        //                streamTextWriter.WriteLine($"DSE({ld} {ln}${fc}${doName})");
        //            }
        //            else
        //            {
        //                streamTextWriter.WriteLine($"DSE({ld} {ln}${fc}${doName}${daName})");
        //            }
        //        }
        //    }
        //}

    }
}