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
using System.Windows.Input;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsDetailsViewModel : NavigationViewModelBase
    {
        private string _regionName;
        private IDevice _device;
        private IReportsModelService _reportsModelService;
        private ISaveCheckingService _saveCheckingService;


        #region Ctor
        public ReportsDetailsViewModel(ICommandFactory commandFactory, IReportsModelService reportsModelService, ISaveCheckingService saveCheckingService) 
        {
            _reportsModelService = reportsModelService;
            _saveCheckingService = saveCheckingService;
        }
        #endregion

        #region public interface

        public ICommand SaveСhangesCommand { get; }

        #endregion

        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            var reportControls = _reportsModelService.GetAllReportControlsOfDevice(_device);
            //DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"DataSets устройства {_device.Name}", SaveСhangesCommand, _regionName));
            ChangeTracker.SetTrackingEnabled(true);
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            base.OnDisposing();
        }
        #endregion
    }
}
