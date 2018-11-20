using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Model;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportControlLoadingService : IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IReportsModelService _reportsModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IFtpReportModelService _ftpReportModelService;
        private Dictionary<string, List<string>> _ldReportsDictionary = new Dictionary<string, List<string>>();

        public ReportControlLoadingService(IConnectionPoolService connectionPoolService,
            IReportsModelService reportsModelService, ISclCommunicationModelService sclCommunicationModelService, IFtpReportModelService ftpReportModelService)
        {
            _connectionPoolService = connectionPoolService;
            _reportsModelService = reportsModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _ftpReportModelService = ftpReportModelService;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {

        }

        #endregion

        #region Implementation of IDeviceElementLoadingService

        public async Task<int> EstimateProgress(IDevice device)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            var ldevices = (await connection.MmsConnection
                .GetLdListAsync()).Item;
            int count = 0;
            _ldReportsDictionary.Clear();
            foreach (var lDevice in ldevices)
            {

                var definitions = (await connection.MmsConnection.GetListValiablesAsync(lDevice, true)).Item;
                foreach (var definition in definitions)
                {
                    var parts = definition.Split('$');
                    if ((parts.Length == 2) && (parts[1] == "RP" || parts[1] == "BR"))
                    {
                        if (_ldReportsDictionary.ContainsKey(lDevice))
                        {
                            _ldReportsDictionary[lDevice].Add(definition);
                        }
                        else
                        {
                            _ldReportsDictionary.Add(lDevice, new List<string>() { definition });

                        }
                        count++;
                    }
                }
            }

            return count;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel,
            CancellationToken cancellationToken)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            _reportsModelService.DeleteAllReportsOfDevice(device);
            if (!_ldReportsDictionary.Values.Any()) return;
            var dynamicReports = await _ftpReportModelService.GetReportsFromDevice(device.Ip);

            List<IReportControl> reportControlsToAddInModel = new List<IReportControl>();

            foreach (var ldevice in _ldReportsDictionary.Keys)
            {
                List<IReportControl> reportControls = new List<IReportControl>();

                foreach (var reportString in _ldReportsDictionary[ldevice])
                {
                    var reportStringParts = reportString.Split('$');
                    if (reportStringParts.Length != 2) continue;
                    var res = await connection.MmsConnection.GetListReportsAsync(ldevice, reportStringParts[0],
                        device.Name, reportStringParts[1]);
                    if (res.IsSucceed)
                    {
                        reportControls.AddRange(res.Item);
                    }
                }

                var filteredRcs = FilterReportControlsByrptEnaMax(reportControls);

                filteredRcs.ForEach((control =>
                {
                    if (dynamicReports.Any((reportControl => reportControl.Name == control.Name)))
                    {
                        control.IsDynamic = true;
                    }
                }));

                _reportsModelService.AddReportsToDevice(device, filteredRcs, ldevice);
            }
        }

        public List<IReportControl> FilterReportControlsByrptEnaMax(List<IReportControl> rawReportControls)
        {
            List<IReportControl> reportControlsFiltered = new List<IReportControl>();

            var groupedRawRcs =
                rawReportControls.GroupBy((control => control.Name.Substring(0, control.Name.Length - 2))).ToList();
            

            foreach (var groupedRawRc in groupedRawRcs)
            {
                IReportControl rcFiltered = groupedRawRc.First();
                rcFiltered.RptID = rcFiltered.RptID;
                rcFiltered.Name = rcFiltered.Name.Substring(0, rcFiltered.Name.Length - 2);
                rcFiltered.RptEnabled.Value.Max = groupedRawRc.Count();
               reportControlsFiltered.Add(rcFiltered);
            }

            return reportControlsFiltered;
        }

        public int Priority => 20;

        #endregion
    }
}