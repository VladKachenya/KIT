using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BISC.Modules.Reports.Presentation.Factorys
{
    public class ReportControlFactoryViewModel : IReportControlFactoryViewModel
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly IReportControlsFactory _reportControlsFactory;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly IUniqueNameService _uniqueNameService;

        public ReportControlFactoryViewModel(IInjectionContainer injectionContainer, IReportControlsFactory reportControlsFactory, IDatasetModelService datasetModelService,
            IInfoModelService infoModelService, IUniqueNameService uniqueNameService)
        {
            _injectionContainer = injectionContainer;
            _reportControlsFactory = reportControlsFactory;
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
            _uniqueNameService = uniqueNameService;
        }
        public ObservableCollection<IReportControlViewModel> GetReportControlsViewModel(List<IReportControl> modelsList, IDevice device)
        {
            ObservableCollection<IReportControlViewModel> reportControlsColection = new ObservableCollection<IReportControlViewModel>();
            foreach (var element in modelsList)
            {
                reportControlsColection.Add(GetReportControlViewModel(element, device));
            }

            return reportControlsColection;
        }

        public IReportControlViewModel GetReportControlViewModel(IReportControl model, IDevice device)
        {
            return GetNewReportViewModel(GetLDeviceOfReportControlRecursive(model), model, device);
        }


        public IReportControlViewModel CreateReportViewModel(List<string> existingNames, IDevice device)
        {
            var reportsName = existingNames.Select(repId => repId.Split('$', '/', '.')[2]);

            var model = _reportControlsFactory.GetReportControl();
            model.Name = _uniqueNameService.GetUniqueName(reportsName.ToList(), "NewReport");
            var report = GetNewReportViewModel(_infoModelService.GetZeroLDevicesFromDevices(device), model, device);
            report.SelectidDataSetName = report.AvailableDatasets.FirstOrDefault();
            report.ConfigurationRevision = 1;
            report.ChangeTracker.SetModified();
            return report;
        }

        private IReportControlViewModel GetNewReportViewModel(ILDevice parientDevice, IReportControl model, IDevice device)
        {
            IReportControlViewModel newReport = _injectionContainer.ResolveType<IReportControlViewModel>();
            newReport.SetIsEditable(true);
            newReport.Model = model;
            newReport.SetParentLDevice(parientDevice);
            newReport.ActivateElement();
            return newReport;
        }

        private ILDevice GetLDeviceOfReportControlRecursive(IModelElement reportControl)
        {
            if (reportControl == null)
            {
                return null;
            }

            if (reportControl is ILDevice)
            {
                return reportControl as ILDevice;
            }

            return GetLDeviceOfReportControlRecursive(reportControl.ParentModelElement);
        }
    }
}
