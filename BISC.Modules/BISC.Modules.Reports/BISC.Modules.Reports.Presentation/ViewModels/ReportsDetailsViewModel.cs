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
        private ISaveCheckingService _saveCheckingService;
        private IUserInterfaceComposingService _userInterfaceComposingService;
        public string TestValue => "I`m reports details view";

        #region Ctor
        public ReportsDetailsViewModel(ICommandFactory commandFactory, IReportsModelService reportsModelService,
            ISaveCheckingService saveCheckingService, IUserInterfaceComposingService userInterfaceComposingService,
            INavigationService navigationService, ILoggingService loggingService)
        {
            _reportsModelService = reportsModelService;
            _saveCheckingService = saveCheckingService;
            _userInterfaceComposingService = userInterfaceComposingService;
        }
        #endregion

        #region override of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            //_device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            //_dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            //SortDataSetsByIsDynamic();
            //DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            //_regionName = navigationContext.BiscNavigationParameters
            //    .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            //_saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
            //    $"DataSets устройства {_device.Name}", SaveСhangesCommand, _regionName));
            //ChangeTracker.SetTrackingEnabled(true);
            //_navigationService.NavigateViewToRegion(InfoModelKeys.InfoModelTreeItemDetailsViewKey, ModelRegionKey,
            //    new BiscNavigationParameters().AddParameterByName("IED", _device));
            //base.OnNavigatedTo(navigationContext);
        }



        public override void OnActivate()
        {

            //_userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            //_globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);

            base.OnActivate();

        }

        //private void OnConnectionChanged(ConnectionEvent connectionEvent)
        //{
        //    if (connectionEvent.Ip == _device.Ip)
        //    {
        //        _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", connectionEvent.IsConnected);

        //    }
        //}

        //public override void OnDeactivate()
        //{
        //    _userInterfaceComposingService.ClearCurrentSaveCommand();
        //    _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);

        //    base.OnDeactivate();
        //}



        //protected override void OnDisposing()
        //{
        //    _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
        //    _navigationService.DisposeRegionViewModel(ModelRegionKey);
        //    base.OnDisposing();
        //}


        #endregion
    }
}
