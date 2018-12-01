using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
    public class SettingControlDetailsViewModel : NavigationViewModelBase
    {
        private readonly SettingControlViewModelFactory _settingControlViewModelFactory;
        private readonly IInfoModelService _infoModelService;

        public SettingControlDetailsViewModel(SettingControlViewModelFactory settingControlViewModelFactory,IInfoModelService infoModelService)
        {
            _settingControlViewModelFactory = settingControlViewModelFactory;
            _infoModelService = infoModelService;
            SettingControlViewModels=new ObservableCollection<SettingControlViewModel>();
        }
        private IDevice _device;

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            SettingControlViewModels.Clear();
            var settingControls = _infoModelService.GetSettingControlsOfDevice(_device);
            foreach (var settingControl in settingControls)
            {
                SettingControlViewModels.Add(_settingControlViewModelFactory.CreateSettingControlViewModel(settingControl));
            }
            base.OnNavigatedTo(navigationContext);
        }
        public ObservableCollection<SettingControlViewModel> SettingControlViewModels { get; }

        #endregion
    }
}
