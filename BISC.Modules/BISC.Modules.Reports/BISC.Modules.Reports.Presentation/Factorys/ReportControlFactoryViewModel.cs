using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.Factorys
{
    public class ReportControlFactoryViewModel : IReportControlFactoryViewModel
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly IReportControlsFactory _reportControlsFactory;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;

        public ReportControlFactoryViewModel(IInjectionContainer injectionContainer, IReportControlsFactory reportControlsFactory, IDatasetModelService datasetModelService, IInfoModelService infoModelService)
        {
            _injectionContainer = injectionContainer;
            _reportControlsFactory = reportControlsFactory;
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
        }
        public ObservableCollection<IReportControlViewModel> GetReportControlsViewModel(List<IReportControl> modelsList, IDevice device)
        {
            ObservableCollection<IReportControlViewModel> reportControlsColection = new ObservableCollection<IReportControlViewModel>();
            foreach (var element in modelsList)
                reportControlsColection.Add(GetReportControlViewModel(element, device));
            return reportControlsColection;
        }

        public IReportControlViewModel GetReportControlViewModel(IReportControl model, IDevice device)
        {
            return GetNewReportViewModel(GetLDeviceOfReportControlRecursive(model), model, device);
        }


        public IReportControlViewModel CreateReportViewModel(List<string> existingNames, IDevice device)
        {
            var reportsName = existingNames.Select(repId => repId.Split('$','/','.')[2]);
            var model = _reportControlsFactory.GetReportControl();
            model.Name = GetUniqueNameOfReport(reportsName);
            var report = GetNewReportViewModel(_infoModelService.GetZeroLDevicesFromDevices(device), model, device);
            report.ChangeTracker.SetNew();
            return report;
        }

        private IReportControlViewModel GetNewReportViewModel(ILDevice parientDevice, IReportControl model, IDevice device)
        {
            IReportControlViewModel newReport = _injectionContainer.ResolveType<IReportControlViewModel>();
            var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
            newReport.AvailableDatasets = datasets.Select((ds => ds.Name)).ToList();
            newReport.Model = model;
            newReport.SetParentLDevice(parientDevice);
            newReport.ActivateElement();
            return newReport;
        }


        private string GetUniqueNameOfReport(IEnumerable<string> existingNames)
        {
            string nameBody = "NewReport";
            string result;
            int i = 0;
            bool isFind;
            do
            {
                i++;
                result = nameBody + i.ToString();
                isFind = false;
                foreach (var element in existingNames)
                {
                    if (result == element)
                        isFind = true;
                }
            } while (isFind);

            return result;
        }

        private ILDevice GetLDeviceOfReportControlRecursive(IModelElement reportControl)
        {
            if (reportControl == null) return null;
            if (reportControl is ILDevice) return reportControl as ILDevice;
            return GetLDeviceOfReportControlRecursive(reportControl.ParentModelElement);
        }
    }
}
