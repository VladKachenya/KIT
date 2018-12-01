using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl;

namespace BISC.Modules.InformationModel.Presentation.Factories
{
   public class SettingControlViewModelFactory
   {
       public SettingControlViewModel CreateSettingControlViewModel(ISettingControl settingControl)
       {
           SettingControlViewModel settingControlViewModel=new SettingControlViewModel();
           settingControlViewModel.AvailableSettingGroup=new List<SettingGroupValue>();
           for (int i = 1; i <= settingControl.NumOfSGs; i++)
           {
               settingControlViewModel.AvailableSettingGroup.Add(new SettingGroupValue(){IsValid = true,Value = i});
           }

           if (settingControl.ActSG == 0)
           {
               settingControlViewModel.AvailableSettingGroup.Add(new SettingGroupValue() { IsValid = false, Value = 0 });
            }
           settingControlViewModel.ActiveSettingsGroup = settingControlViewModel.AvailableSettingGroup.FirstOrDefault((value =>value.Value== settingControl.ActSG));

            settingControlViewModel.Header = settingControl.GetFirstParentOfType<ILDevice>().Inst;
           return settingControlViewModel;
       }

   }
}
