using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private IReportsModelService _reportsModelService;
       
        public string TestValue => "I`m reports details view";

        #region Ctor
        public ReportsDetailsViewModel(ICommandFactory commandFactory, IReportsModelService reportsModelService) 
        {
            _reportsModelService = reportsModelService;
        }
        #endregion

        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            var reportControls = _reportsModelService.GetAllReportControlsOfDevice(_device);
            //SortDataSetsByIsDynamic();
            //DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            //_regionName = navigationContext.BiscNavigationParameters
            //    .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            //_saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
            //    $"DataSets устройства {_device.Name}", SaveСhangesCommand, _regionName));
            //ChangeTracker.SetTrackingEnabled(true);
            //_navigationService.NavigateViewToRegion(InfoModelKeys.InfoModelTreeItemDetailsViewKey, ModelRegionKey,
            //    new BiscNavigationParameters().AddParameterByName("IED", _device));
            base.OnNavigatedTo(navigationContext);
        }

        #endregion
    }
}
