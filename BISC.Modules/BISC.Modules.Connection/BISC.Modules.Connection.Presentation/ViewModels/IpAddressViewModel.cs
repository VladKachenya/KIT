using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
   public class IpAddressViewModel:ComplexViewModelBase, IIpAddressViewModel
    {
        private readonly IPingService _pingService;
        private readonly IIpValidationService _ipValidationService;
        private string _ipPart1;
        private string _ipPart2;
        private string _ipPart3;
        private string _ipPart4;
        private bool _isIpPart1Focused;
        private bool _isIpPart2Focused;
        private bool _isIpPart3Focused;
        private bool _isIpPart4Focused;
        private bool _isPingSuccess;

        public IpAddressViewModel(IPingService pingService,IIpValidationService ipValidationService,ICommandFactory commandFactory)
        {
            _pingService = pingService;
            _ipValidationService = ipValidationService;
            PingCommand = commandFactory.CreatePresentationCommand(OnPingExecute,CanPingExecute);
        }

        private async void OnPingExecute()
        {
            _isPingSuccess = await _pingService.GetPing(FullIp);
            Application.Current.Dispatcher.Invoke(() => OnPropertyChanged(nameof(IsPingSuccess)));
        }

        private  bool CanPingExecute()
        {
            return _ipValidationService.IsExactFormIpAddress(FullIp);
        }

        #region Implementation of IIpAddressViewModel

        public string FullIp { get; set; }
        public ICommand PingCommand { get; }

        public bool IsPingSuccess => _isPingSuccess;

        #endregion

        private void ValidateIpPart(ref string valiatingIpPart)
        {
            if (valiatingIpPart.Length > 3)
            {
                valiatingIpPart = String.Empty;return;
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
            (PingCommand as IPresentationCommand)?.RaiseCanExecute();
            OnPropertyChanged();
        }

        private bool GetIsIpPartOverflow(string ipPart)
        {
            return (ipPart.Length > 3);
        }

        public string IpPart1
        {
            get => _ipPart1;
            set
            {
                if (GetIsIpPartOverflow(value))
                {
                    IsIpPart2Focused = true;
                    return;
                }
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart1, value);

            }
        }

        public string IpPart2
        {
            get => _ipPart2;
            set
            {
                if (GetIsIpPartOverflow(value))
                {
                    IsIpPart3Focused = true;
                    return;
                }
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart1, value);
            }
        }

        public string IpPart3
        {
            get => _ipPart3;
            set
            {
                if (GetIsIpPartOverflow(value))
                {
                    IsIpPart4Focused = true;
                    return;
                }
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart1, value);
            }
        }

        public string IpPart4
        {
            get => _ipPart4;
            set
            {
                if (GetIsIpPartOverflow(value))
                {
                    return;
                }
                ValidateIpPart(ref value);
                SetProperty(ref _ipPart1, value);
            }
        }

        public bool IsIpPart1Focused
        {
            get => _isIpPart1Focused;
            set { SetProperty(ref _isIpPart1Focused, value); }
        }

        public bool IsIpPart2Focused
        {
            get => _isIpPart2Focused;
            set { SetProperty(ref _isIpPart2Focused, value); }

        }

        public bool IsIpPart3Focused
        {
            get => _isIpPart3Focused;
            set { SetProperty(ref _isIpPart3Focused, value); }

        }

        public bool IsIpPart4Focused
        {
            get => _isIpPart4Focused;
            set { SetProperty(ref _isIpPart4Focused, value); }

        }

    }
}
