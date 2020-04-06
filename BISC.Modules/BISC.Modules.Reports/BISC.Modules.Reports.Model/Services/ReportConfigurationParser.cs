using System.Collections.Generic;
using System.IO;
using System.Linq;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.Reports.Infrastructure.Model;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportConfigurationParser : ConfigurationParser
    {
        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, TextWriter streamTextWriter)
        {
            var reportsToParse = modelElements.Cast<IReportControl>();
            foreach (var reportObj in reportsToParse)
            {
                var ld = reportObj.GetFirstParentOfType<ILDevice>().Inst;
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
                    streamTextWriter.WriteLine(
                        $"RCB({ld} {rptName + "0" + (i + 1)} {rptId} {isBuffered} {dsName} {confRev} {trgOpt} {optFields} {bufTm} {intgPd})");
                }
            }
        }
    }
}