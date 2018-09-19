using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class IpAddressViewModel : ComplexViewModelBase, IIpAddressViewModel
    {
        private readonly IPingService _pingService;
        private readonly IIpValidationService _ipValidationService;
        private readonly IGlobalEventsService _globalEventsService;
        private string _ipPart1;
        private string _ipPart2;
        private string _ipPart3;
        private string _ipPart4;
        private string _forToolTip;
        private bool? _isPingSuccess;

        public IpAddressViewModel(IPingService pingService, IIpValidationService ipValidationService, ICommandFactory commandFactory, IGlobalEventsService globalEventsService)
        {
            _pingService = pingService;
            _ipValidationService = ipValidationService;
            _globalEventsService = globalEventsService;
            PingCommand = commandFactory.CreatePresentationCommand(OnPingExecute, CanPingExecute);
            IpSelectedCommand = commandFactory.CreatePresentationCommand(OnIpSelectedCommand);
            ClearIpCommand = commandFactory.CreatePresentationCommand(OnClearIpExecute);
        }

        private void OnClearIpExecute()
        {
            FullIp = "...";
        }

        private void OnIpSelectedCommand()
        {
            _globalEventsService.SendMessage(new IpSelectedEvent(FullIp));
        }


        private async void OnPingExecute()
        {
            try
            {
                await PingGlobalEventAsync();
            }
            catch (Exception e)
            {
                ForToolTip = e.Message;
            }
        }

        private bool CanPingExecute()
        {
            return _ipValidationService.IsSimplifiedIpAddress(FullIp);
        }

        #region Implementation of IIpAddressViewModel

        public string FullIp
        {
            get => $"{IpPart1}.{IpPart2}.{IpPart3}.{IpPart4}";
            set
            {
                var values = value.Split('.');
                if (values.Length != 4) return;
                IpPart1 = values[0];
                IpPart2 = values[1];
                IpPart3 = values[2];
                IpPart4 = values[3];
            }
        }

        public ICommand PingCommand { get; }
        public ICommand IpSelectedCommand { get; }
        public ICommand ClearIpCommand { get; }

        public bool? IsPingSuccess
        {
            get => _isPingSuccess;
            set
            {
                SetProperty(ref _isPingSuccess, value);
                ForToolTip = IsPingSuccess != null && IsPingSuccess.Value ? "Проверка связи завершена успешно" : "Устройство не на связи";

            }
        }

        public string ForToolTip
        {
            get => _forToolTip;
            set => SetProperty(ref _forToolTip, value);
        }

        public async Task PingAsync()
        {
            IsPingSuccess = null;
            IsPingSuccess = await _pingService.GetPing(FullIp);
            Application.Current.Dispatcher.Invoke(() => OnPropertyChanged(nameof(IsPingSuccess)));
        }

        public async Task PingGlobalEventAsync()
        {
            await PingAsync();
            _globalEventsService.SendMessage(new IpPingedEvent(FullIp, IsPingSuccess != null && IsPingSuccess.Value));
        }

        #endregion

        private void ValidateIpPart(ref string valiatingIpPart)
        {
            if (valiatingIpPart.Length > 3)
            {
                valiatingIpPart = valiatingIpPart.Substring(0,3);
                valiatingIpPart = "255";
                return;
            }
            if (int.TryParse(valiatingIpPart, out var intVal))
            {
                if ((intVal <= 255) && (intVal >= 0)) valiatingIpPart = intVal.ToString();
                else if (intVal > 255) valiatingIpPart = "255";
                else if (intVal < 0) valiatingIpPart = "0";
            }
            else if (String.IsNullOrEmpty(valiatingIpPart))
            {
                valiatingIpPart = string.Empty;
            }
            IsPingSuccess = null;
            OnPropertyChanged();
        }
        
        public string IpPart1
        {
            get => _ipPart1;
            set
           {
              
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart1, value);
               (PingCommand as IPresentationCommand)?.RaiseCanExecute();

            }
        }

        public string IpPart2
        {
            get => _ipPart2;
            set
            {
               
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart2, value);
                (PingCommand as IPresentationCommand)?.RaiseCanExecute();

            }
        }

        public string IpPart3
        {
            get => _ipPart3;
            set
            {
                
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart3, value);
                (PingCommand as IPresentationCommand)?.RaiseCanExecute();

            }
        }

        public string IpPart4
        {
            get => _ipPart4;
            set
            {
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart4, value);
                (PingCommand as IPresentationCommand)?.RaiseCanExecute();

            }
        }


    }
}
