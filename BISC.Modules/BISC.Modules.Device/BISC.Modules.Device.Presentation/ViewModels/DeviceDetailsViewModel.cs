using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceDetailsViewModel : NavigationViewModelBase, IDeviceDetailsViewModel
    {

        private readonly ISclCommunicationModelService _sclCommunicationModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IDeviceIdentificationService _deviceIdentificationService;
        private readonly IDeviceIpChangingService _deviceIpChangingService;
        private readonly IUserInteractionService _userInteractionService;

        private string _deviceName;
        private IDevice _device;
        private UiEntityIdentifier _uiEntityIdentifier;
        private string _deviceIp;
        private bool _isIpUnchangeable;

        public DeviceDetailsViewModel(ISclCommunicationModelService sclCommunicationModel,
            IConnectionPoolService connectionPoolService, IBiscProject biscProject,
            IIpAddressViewModelFactory ipAddressViewModelFactory, IGlobalEventsService globalEventsService,
            ICommandFactory commandFactory, IDeviceIdentificationService deviceIdentificationService,
            IDeviceIpChangingService deviceIpChangingService, IUserInteractionService userInteractionService)
            : base(globalEventsService)
        {
            _sclCommunicationModel = sclCommunicationModel;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _globalEventsService = globalEventsService;
            _deviceIdentificationService = deviceIdentificationService;
            _deviceIpChangingService = deviceIpChangingService;
            _userInteractionService = userInteractionService;
            ChengeIpCommand = commandFactory.CreatePresentationCommand(OnChengeIpCommand, () => !IsIpUnchangeable);
        }

        #region public methods
        public bool IsBemnManufacturer { get; protected set; }
        public string DeviceName
        {
            get => _deviceName;
            set { SetProperty(ref _deviceName, value); }
        }

        public bool IsIpUnchangeable
        {
            get => _isIpUnchangeable;
            set
            {
                SetProperty(ref _isIpUnchangeable, value, true);
                (ChengeIpCommand as IPresentationCommand).RaiseCanExecute();
            }
        }

        public IIpAddressViewModel IpAddressViewModel { get; protected set; }

        public ICommand ChengeIpCommand { get; }

        #endregion

        #region override methods
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _uiEntityIdentifier = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key);
            DeviceName = _device.Name;
            IsBemnManufacturer = (_device.Manufacturer == DeviceKeys.DeviceManufacturer.BemnManufacturer);
            string ip = _sclCommunicationModel.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);

            IsIpUnchangeable = _connectionPoolService.GetConnection(ip).IsConnected || !IsBemnManufacturer;
            IpAddressViewModel = string.IsNullOrWhiteSpace(ip) ? _ipAddressViewModelFactory.GetPingItemViewModel(isReadonly: IsIpUnchangeable) :
                _ipAddressViewModelFactory.GetPingItemViewModel(ip, IsIpUnchangeable);


            _globalEventsService.Subscribe<LossConnectionEvent>(OnLostConnectionEvent);
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<LossConnectionEvent>(OnLostConnectionEvent);
            base.OnDisposing();
        }
        #endregion

        #region private methods

        private void OnLostConnectionEvent(LossConnectionEvent lossConnectionEvent)
        {
            if (!IsBemnManufacturer) { return; }
            if (lossConnectionEvent.Ip == _device.Ip)
            {
                IsIpUnchangeable = _connectionPoolService.GetConnection(_device.Ip).IsConnected || !IsBemnManufacturer;

                (IpAddressViewModel as ComplexViewModelBase)?.SetIsEditable(!_connectionPoolService.GetConnection(_device.Ip).IsConnected && IsBemnManufacturer);
            }
        }

        private async void OnChengeIpCommand()
        {
            try
            {
                _deviceIpChangingService.ChengeDeviceIp(_device, IpAddressViewModel.FullIp,
                    _uiEntityIdentifier.ParenUiEntityIdentifier);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    await _userInteractionService.ShowOptionToUser("Ошибка смены IP устройства", e.Message,
                        new List<string>() { "Ок" });
                }
            }
        }


        #endregion

    }
}
