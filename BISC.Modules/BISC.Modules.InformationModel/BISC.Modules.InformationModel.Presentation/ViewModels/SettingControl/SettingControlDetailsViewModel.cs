using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Modules.InformationModel.Presentation.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
    public class SettingControlDetailsViewModel : NavigationViewModelBase
    {
        private readonly SettingControlViewModelFactory _settingControlViewModelFactory;
        private readonly IInfoModelService _infoModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly SettingsControlSavingService _settingsControlSavingService;

        public SettingControlDetailsViewModel(SettingControlViewModelFactory settingControlViewModelFactory, IInfoModelService infoModelService,
            ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, IConnectionPoolService connectionPoolService,
            IUserInterfaceComposingService userInterfaceComposingService, IGlobalEventsService globalEventsService, SettingsControlSavingService settingsControlSavingService)
        {
            _settingControlViewModelFactory = settingControlViewModelFactory;
            _infoModelService = infoModelService;
            _saveCheckingService = saveCheckingService;
            _connectionPoolService = connectionPoolService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _globalEventsService = globalEventsService;
            _settingsControlSavingService = settingsControlSavingService;
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveChanges);
            
        }

        public ICommand SaveСhangesCommand { get; }

        private async void OnSaveChanges()
        {
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            BlockViewModelBehavior.SetBlock("Сохранение", true);
            await _settingsControlSavingService.SaveSettingControlsAsync(SettingControlViewModels.ToList(), _device);
            BlockViewModelBehavior.Unlock();
        }

        private IDevice _device;
        private string _regionName;
        private ObservableCollection<SettingControlViewModel> _settingControlViewModels;

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
           var settingsVm=new ObservableCollection<SettingControlViewModel>();
            var settingControls = _infoModelService.GetSettingControlsOfDevice(_device);
            foreach (var settingControl in settingControls)
            {
                settingsVm.Add(_settingControlViewModelFactory.CreateSettingControlViewModel(settingControl));
            }
            SettingControlViewModels = settingsVm;
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
      
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"Setting Groups устройства {_device.Name}", SaveChangesAsync,null, _device.Name, _regionName));
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnNavigatedTo(navigationContext);
        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            if (connectionEvent.Ip != _device.Ip) return;
            foreach (var settingControlViewModel in SettingControlViewModels)
            {
                settingControlViewModel.IsEditable = connectionEvent.IsConnected;
            }
        }


        public override void OnActivate()
        {
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить Setting Groups устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);

            //_globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            //_globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDeactivate();
        }

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDisposing();
        }

        #endregion

        public ObservableCollection<SettingControlViewModel> SettingControlViewModels
        {
            get => _settingControlViewModels;
            protected set => SetProperty(ref _settingControlViewModels, value);
        }


        #endregion
    }
}
