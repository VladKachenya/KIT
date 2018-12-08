using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Presentation.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
   public class SettingsControlConflivtsViewModel:NavigationViewModelBase
    {
        private SettingsControlConflictsContext _settingsControlConflictsContext;

        public SettingsControlConflivtsViewModel()
        {
            SettingControlViewModelsInDevice=new ObservableCollection<SettingControlViewModel>();
            SettingControlViewModelsInProject=new ObservableCollection<SettingControlViewModel>();
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _settingsControlConflictsContext= navigationContext.BiscNavigationParameters.GetParameterByName<SettingsControlConflictsContext>(InfoModelKeys
                .SettingsControlConflictsContextKey);
            SettingControlViewModelsInDevice.Clear();
            _settingsControlConflictsContext.SettingControlViewModelsInDevice.ForEach((model =>
            {
                model.IsEditable = false;
                SettingControlViewModelsInDevice.Add(model);
            }));
            SettingControlViewModelsInProject.Clear();
            _settingsControlConflictsContext.SettingControlViewModelsInProject.ForEach((model =>
            {
                model.IsEditable = false;
                SettingControlViewModelsInProject.Add(model);
            }));
            base.OnNavigatedTo(navigationContext);
        }
        public ObservableCollection<SettingControlViewModel> SettingControlViewModelsInDevice { get; }
        public ObservableCollection<SettingControlViewModel> SettingControlViewModelsInProject { get; }

        #endregion
    }
}
