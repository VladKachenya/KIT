using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Presentation.ViewModels
{
    public class ApplicationSettingsViewModel : ComplexViewModelBase, IApplicationSettingsViewModel
    {
        #region private filds
        private IConfigurationService _configurationService;
        private bool _isAutoEnabledValidityInGooseReceiving;
        private bool _isAutoEnabledQualityInGooseReceiving;
        private int _mmsQueryDelay;
        private int _ftpTimeOutDelay;
        private int _maxResponseTime;

        private bool _isUserLoggingEnabled;
        private bool _isMmsQueryDelayValid;
        private bool _isFtpTimeOutDelayValid;
        private bool _isMaxResponseTimeValid;

        #endregion

        #region C-tor
        public ApplicationSettingsViewModel(ICommandFactory commandFactory, IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                Dispose();
            }));
            ConfirmCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                SaveChanges();
            }), () => !IsFtpTimeOutDelayValid && !IsMmsQueryDelayValid && !IsMaxResponseTimeValid);

            IsAutoEnabledValidityInGooseReceiving = _configurationService.IsAutoEnabledValidityInGooseReceiving;
            IsAutoEnabledQualityInGooseReceiving = _configurationService.IsAutoEnabledQualityInGooseReceiving;
            _mmsQueryDelay = _configurationService.MmsQueryDelay;
            _ftpTimeOutDelay = _configurationService.FtpTimeOutDelay;
            _maxResponseTime = _configurationService.MaxResponseTime;

            IsUserLoggingEnabled = _configurationService.IsUserLoggingEnabled;
        }
        #endregion

        #region private methods
        private void SaveChanges()
        {
            _configurationService.IsAutoEnabledValidityInGooseReceiving = IsAutoEnabledValidityInGooseReceiving;
            _configurationService.IsAutoEnabledQualityInGooseReceiving = IsAutoEnabledQualityInGooseReceiving;
            _configurationService.MmsQueryDelay = _mmsQueryDelay;
            _configurationService.FtpTimeOutDelay = _ftpTimeOutDelay;
            _configurationService.MaxResponseTime = _maxResponseTime;

            _configurationService.IsUserLoggingEnabled = _isUserLoggingEnabled;

        }
        #endregion


        #region Implemntation of IApplicationSettingsViewModel
        public bool IsAutoEnabledValidityInGooseReceiving
        {
            get => _isAutoEnabledValidityInGooseReceiving;
            set => SetProperty(ref _isAutoEnabledValidityInGooseReceiving, value);
        }
        public bool IsAutoEnabledQualityInGooseReceiving
        {
            get => _isAutoEnabledQualityInGooseReceiving;
            set => SetProperty(ref _isAutoEnabledQualityInGooseReceiving, value);
        }

        public string MmsQueryDelay
        {
            get => _mmsQueryDelay.ToString();
            set
            {
                if (!Int32.TryParse(value, out int buffer))
                {
                    return;
                }

                IsMmsQueryDelayValid = buffer < 0 || buffer > 100;

                _mmsQueryDelay = buffer;
                OnPropertyChanged();
            }
        }

        public bool IsMmsQueryDelayValid
        {
            get => _isMmsQueryDelayValid;
            set
            {
                SetProperty(ref _isMmsQueryDelayValid, value);
                (ConfirmCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        public string FtpTimeOutDelay
        {
            get => _ftpTimeOutDelay.ToString();
            set
            {
                if (!Int32.TryParse(value, out int buffer))
                {
                    return;
                }

                IsFtpTimeOutDelayValid = buffer < 100 || buffer > 10000;

                SetProperty(ref _ftpTimeOutDelay, buffer);
                OnPropertyChanged();

            }
        }

        public bool IsFtpTimeOutDelayValid
        {
            get => _isFtpTimeOutDelayValid;
            set
            {
                SetProperty(ref _isFtpTimeOutDelayValid, value);
                (ConfirmCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        public string MaxResponseTime
        {
            get => _maxResponseTime.ToString();
            set
            {
                if (!Int32.TryParse(value, out int buffer))
                {
                    return;
                }

                IsMaxResponseTimeValid = buffer < 500 || buffer > 10000;

                _maxResponseTime = buffer;
                OnPropertyChanged();
            }
        }

        public bool IsMaxResponseTimeValid
        {
            get => _isMaxResponseTimeValid;
            set
            {
                SetProperty(ref _isMaxResponseTimeValid, value);
                (ConfirmCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }


        public ICommand CloseCommand { get; }
        public ICommand ConfirmCommand { get; }

        public bool IsUserLoggingEnabled
        {
            get => _isUserLoggingEnabled;
            set => SetProperty(ref _isUserLoggingEnabled, value);
        }
        #endregion


        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion

    }
}
