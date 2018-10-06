using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
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
            IsAutoEnabledQualityInGooseReceiving= _configurationService.IsAutoEnabledQualityInGooseReceiving;

        }
        #endregion

        #region private methods
        private void SaveChanges()
        {
            _configurationService.IsAutoEnabledValidityInGooseReceiving = IsAutoEnabledValidityInGooseReceiving;
            _configurationService.IsAutoEnabledQualityInGooseReceiving = IsAutoEnabledQualityInGooseReceiving;

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

        public ICommand CloseCommand { get; }
        public ICommand ConfirmCommand { get; }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion

    }
}
