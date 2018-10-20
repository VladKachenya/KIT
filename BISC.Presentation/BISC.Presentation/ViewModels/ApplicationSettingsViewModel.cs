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

namespace BISC.Presentation.ViewModels
{
    public class ApplicationSettingsViewModel : ComplexViewModelBase, IApplicationSettingsViewModel
    {
        #region private filds
        IConfigurationService _configurationService;
        bool _isAutoEnabledValidityInGooseReceiving;
        bool _isAutoEnabledQualityInGooseReceiving;
        int _mmsQueryDelay;
        bool _isVisibleValidadionError;
        private bool _isUserLoggingEnabled;

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
            }));
            IsAutoEnabledValidityInGooseReceiving = _configurationService.IsAutoEnabledValidityInGooseReceiving;
            IsAutoEnabledQualityInGooseReceiving = _configurationService.IsAutoEnabledQualityInGooseReceiving;
            _mmsQueryDelay = _configurationService.MmsQueryDelay;
            IsVisibleValidadionError = false;
            IsUserLoggingEnabled = _configurationService.IsUserLoggingEnabled;
        }
        #endregion

        #region private methods
        private void SaveChanges()
        {
            _configurationService.IsAutoEnabledValidityInGooseReceiving = IsAutoEnabledValidityInGooseReceiving;
            _configurationService.IsAutoEnabledQualityInGooseReceiving = IsAutoEnabledQualityInGooseReceiving;
            _configurationService.MmsQueryDelay = _mmsQueryDelay;
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
                int buf = 0;
                bool res = true;
                if (value != string.Empty)
                    res = Int32.TryParse(value, out buf);
                if (!res || buf < 0 || buf > 100)
                {
                    IsVisibleValidadionError = true;
                    return;
                }
                else
                {
                    IsVisibleValidadionError = false;
                    _mmsQueryDelay = buf;
                    OnPropertyChanged();
                }

            }
        }

        public bool IsVisibleValidadionError
        {
            get => _isVisibleValidadionError;
            set => SetProperty(ref _isVisibleValidadionError, value);
        }


        public ICommand CloseCommand { get; }
        public ICommand ConfirmCommand { get; }

        public bool IsUserLoggingEnabled
        {
            get => _isUserLoggingEnabled;
            set { SetProperty(ref _isUserLoggingEnabled, value); }
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
