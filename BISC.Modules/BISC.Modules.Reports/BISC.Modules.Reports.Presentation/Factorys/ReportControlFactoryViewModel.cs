using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace BISC.Modules.Reports.Presentation.Factorys
{
    public class ReportControlFactoryViewModel : IReportControlFactoryViewModel
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly IReportControlsFactory _reportControlsFactory;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly IUniqueNameService _uniqueNameService;
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public ReportControlFactoryViewModel(IInjectionContainer injectionContainer, IReportControlsFactory reportControlsFactory, IDatasetModelService datasetModelService,
            IInfoModelService infoModelService, IUniqueNameService uniqueNameService, IModelElementsRegistryService modelElementsRegistryService)
        {
            _injectionContainer = injectionContainer;
            _reportControlsFactory = reportControlsFactory;
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
            _uniqueNameService = uniqueNameService;
            _modelElementsRegistryService = modelElementsRegistryService;
        }

        #region Implementation of IReportControlFactoryViewModel

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

            var model = ConfigureModel();
            model.Name = _uniqueNameService.GetUniqueName(reportsName.ToList(), "NewReport");
            var report = GetNewReportViewModel(_infoModelService.GetZeroLDevicesFromDevices(device), model, device);
            report.SelectidDataSetName = report.AvailableDatasets.FirstOrDefault();
            report.ConfigurationRevision = 1;
            report.ChangeTracker.SetModified();
            return report;
        }

        #endregion

        private IReportControlViewModel GetNewReportViewModel(ILDevice parientDevice, IReportControl model, IDevice device)
        {
            IReportControlViewModel newReport = _injectionContainer.ResolveType<IReportControlViewModel>();
            newReport.SetIsEditable(true);
            newReport.Model = model;
            newReport.SetParentLDevice(parientDevice);
            newReport.ActivateElement();
            return newReport;
        }

        private IReportControl ConfigureModel()
        {
            IReportControl model;
            try
            {
                XDocument xdoc = XDocument.Load(DeviceKeys.ConfigurationKeys.BasicConfigurationPathKey);
                XElement BasicReportConfiguration =
                    xdoc.Element(DeviceKeys.ConfigurationKeys.BasicConfigurationNodeKey)
                        ?.Element(ReportsKeys.ReportsModelKeys.ReportControlModelKey);
                model = _modelElementsRegistryService.DeserializeModelElement<IReportControl>(
                    BasicReportConfiguration);
            }
            catch (Exception e)
            {
                model = _reportControlsFactory.GetReportControl();
            }


            return model;
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
