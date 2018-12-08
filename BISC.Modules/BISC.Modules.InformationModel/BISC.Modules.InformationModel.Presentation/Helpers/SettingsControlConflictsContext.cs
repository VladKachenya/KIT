using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl;

namespace BISC.Modules.InformationModel.Presentation.Helpers
{
   public class SettingsControlConflictsContext
    {
        public SettingsControlConflictsContext(List<SettingControlViewModel> settingControlViewModelsInDevice, List<SettingControlViewModel> settingControlViewModelsInProject)
        {
            SettingControlViewModelsInDevice = settingControlViewModelsInDevice;
            SettingControlViewModelsInProject = settingControlViewModelsInProject;
        }

        public List<SettingControlViewModel> SettingControlViewModelsInDevice { get;  }
        public List<SettingControlViewModel> SettingControlViewModelsInProject { get;  }

    }
}
