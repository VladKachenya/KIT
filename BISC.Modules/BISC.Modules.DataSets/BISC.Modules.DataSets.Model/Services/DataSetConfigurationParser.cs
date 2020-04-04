using System.Collections.Generic;
using System.IO;
using System.Linq;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DataSetConfigurationParser : ConfigurationParser
    {
        private readonly IDatasetModelService _datasetModelService;

        public DataSetConfigurationParser(IDatasetModelService datasetModelService)
        {
            _datasetModelService = datasetModelService;
        }
        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, TextWriter streamTextWriter)
        {
            var dataSetsToParse = modelElements.Cast<IDataSet>();
            foreach (var dataSet in dataSetsToParse)
            {
                var lnParent = dataSet.GetFirstParentOfType<ILogicalNode>();
                var ldParent = dataSet.GetFirstParentOfType<ILDevice>();

                streamTextWriter.WriteLine(
                    $"DSN({ldParent.Inst} {lnParent.Name}${dataSet.Name})");
                foreach (var fcda in dataSet.FcdaList)
                {
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