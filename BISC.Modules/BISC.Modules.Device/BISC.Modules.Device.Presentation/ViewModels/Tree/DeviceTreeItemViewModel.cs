using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.ViewModels.Tree
{
   public class DeviceTreeItemViewModel:NavigationViewModelBase, IDeviceTreeItemViewModel
    {
        
        private string _deviceName;

        public DeviceTreeItemViewModel()
        {
         
        }

        #region Implementation of IMainTreeItem
        

        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        public IDevice Device { get; set; }
        #endregion

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            IDevice device= navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            
            base.OnNavigatedTo(navigationContext);
        }

        public ICommand NavigateToDetailsCommand { get; }
    }
}
