using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportsModelService : IReportsModelService
    {
        private readonly IInfoModelService _infoModelService;

        public ReportsModelService(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
        }

        public List<IReportControl> GetAllReportControlsOfDevice(IModelElement device)
        {
            List<IReportControl> ReportControls = new List<IReportControl>();
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in ldevices)
            {
                foreach (var logicalNode in lDevice.LogicalNodes)
                {
                    logicalNode.ChildModelElements.ForEach((element =>
                    {
                        if (element is IReportControl reportControl)
                        {
                            ReportControls.Add(reportControl);
                        }
                    }));
                }
                foreach (var element in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (element is IReportControl reportControl)
                    {
                        ReportControls.Add(reportControl);
                    }
                }

            }
            return ReportControls;
        }





        public void DeleteAllReportsOfDevice(IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in ldevices)
            {
                foreach (var logicalNode in lDevice.LogicalNodes)
                {
                    List<IReportControl> reportControlsToDelete = new List<IReportControl>();

                    logicalNode.ChildModelElements.ForEach((element =>
                    {
                        if (element is IReportControl reportControl)
                        {
                            reportControlsToDelete.Add(reportControl);
                        }
                    }));
                    reportControlsToDelete.ForEach((control => logicalNode.ChildModelElements.Remove(control)));
                }
                List<IReportControl> reportControlsToDeleteInLn0 = new List<IReportControl>();

                foreach (var element in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {

                    if (element is IReportControl reportControl)
                    {
                        reportControlsToDeleteInLn0.Add(reportControl);
                    }

                }
                reportControlsToDeleteInLn0.ForEach((control => lDevice.LogicalNodeZero.Value.ChildModelElements.Remove(control)));

            }
        }

        public void AddReportsToDevice(IDevice device, List<IReportControl> reportControls, string lDevice)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);

            foreach (var reportControl in reportControls)
            {
                var ldevice = ldevices.FirstOrDefault((device1 => device1.Inst == lDevice.Replace(device.Name, string.Empty)));
                if (ldevice != null)
                {
                    var logicalNodeToAdd = ldevice.AlLogicalNodes.FirstOrDefault((node =>
                        node.Name == reportControl.RptID.Split('$').First()));
                    if (logicalNodeToAdd != null)
                    {
                        logicalNodeToAdd.ChildModelElements.Add(reportControl);
                        reportControl.ParentModelElement = logicalNodeToAdd;
                    }
                }
            }
        }
        public void AddReportsToDevice(IDevice device, List<IReportControl> reportControls)
        {
            var lNode = _infoModelService.GetZeroLDevicesFromDevices(device).LogicalNodeZero.Value;
            var reportsInDevice = GetAllReportControlsOfDevice(device);
            foreach (var toBeAddedReport in reportControls)
            {
                IReportControl repToDev = reportsInDevice.FirstOrDefault(rep => rep.Name == toBeAddedReport.Name);
                int index = lNode.ChildModelElements.IndexOf(repToDev);
                if (index > 0)
                {
                    DeleteReportsFromDevice(device, new List<IReportControl> { repToDev });
                    lNode.ChildModelElements.Insert(index, toBeAddedReport);
                }
                else
                {
                    index = lNode.ChildModelElements.IndexOf(reportsInDevice.Last());
                    lNode.ChildModelElements.Insert(index + 1, toBeAddedReport);
                }
                toBeAddedReport.ParentModelElement = lNode;
            }
        }

        public void DeleteReportsFromDevice(IDevice device, List<IReportControl> reportControls)
        {
            //Возможно тут необходимо брать lNode из удоляемого узла
            var lNode = _infoModelService.GetZeroLDevicesFromDevices(device).LogicalNodeZero.Value;
            var reportsInDevice = GetAllReportControlsOfDevice(device);
            foreach (var toBeDeletedReport in reportControls)
            {
                IReportControl repToDev = reportsInDevice.FirstOrDefault(rep => rep.Name == toBeDeletedReport.Name);
                toBeDeletedReport.ParentModelElement = null;
                if (repToDev != null)
                    lNode.ChildModelElements.Remove(repToDev);
            }
        }
    }
}
