using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.ViewModels.Tree;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.ViewModels.Factories
{
   public class DeviceTreeItemViewModelFactory:ITreeItemViewModelFactory
    {
        private readonly Func<DeviceTreeItemViewModel> _deviceTreeItemCreator;

        public DeviceTreeItemViewModelFactory(Func<DeviceTreeItemViewModel> deviceTreeItemCreator)
        {
            _deviceTreeItemCreator = deviceTreeItemCreator;
        }



        #region Implementation of ITreeItemViewModelFactory

        public IMainTreeItem CreateTreeItem(IModelElement modelElement)
        {
            IDevice device=modelElement as IDevice;
            
            DeviceTreeItemViewModel deviceTreeItemViewModel = _deviceTreeItemCreator();
            deviceTreeItemViewModel.DeviceName = device.Name;

            return deviceTreeItemViewModel;
        }

        #endregion

     
    }
}
