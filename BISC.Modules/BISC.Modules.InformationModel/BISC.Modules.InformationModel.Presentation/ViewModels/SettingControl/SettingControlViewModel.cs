using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
   public class SettingControlViewModel:ComplexViewModelBase
    {
        private SettingGroupValue _activeSettingsGroup;
        private List<SettingGroupValue> _availableSettingGroup;
        private string _header;

        public SettingGroupValue ActiveSettingsGroup
        {
            get => _activeSettingsGroup;
            set => SetProperty(ref _activeSettingsGroup, value);
        }

        public List<SettingGroupValue> AvailableSettingGroup
        {
            get => _availableSettingGroup;
            set => SetProperty(ref _availableSettingGroup, value, true);
        }

        public string Header
        {
            get => _header;
            set => SetProperty(ref _header , value,true);
        }
        public ISettingControl Model { get; set; }
    }

    public class SettingGroupValue
    {
        public bool IsValid { get; set; }
        public int Value { get; set; }

    }
}
