using BISC.Infrastructure.Global.IoC;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
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

        public ReportControlFactoryViewModel(IInjectionContainer injectionContainer, IReportControlsFactory reportControlsFactory, IDatasetModelService datasetModelService)
        {
            _injectionContainer = injectionContainer;
            _reportControlsFactory = reportControlsFactory;
            _datasetModelService = datasetModelService;
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
            IReportControlViewModel newReport = _injectionContainer.ResolveType<IReportControlViewModel>();
            if( device != null)
            { 
                var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
                newReport.AvailableDatasets = datasets.Select((ds => ds.Name)).ToList();
                newReport.Model = model;
            }
            //obj.ChangeTracker.SetNew();
            newReport.ActivateElement();
            return newReport;
        }
        public IReportControlViewModel GetReportControlViewModel(IDevice device)
        {
            return GetReportControlViewModel(_reportControlsFactory.GetReportControl(), device);
        }
    }
}
