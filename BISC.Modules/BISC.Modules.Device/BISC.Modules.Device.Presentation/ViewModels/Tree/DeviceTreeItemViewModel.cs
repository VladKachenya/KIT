using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.ViewModels.Tree
{
   public class DeviceTreeItemViewModel:IMainTreeItem
    {
        public DeviceTreeItemViewModel()
        {
            ChildTreeItems=new ObservableCollection<IMainTreeItem>();
        }

        #region Implementation of IMainTreeItem

        public string TreeRegionName { get; set; }
        public string TreeItemName { get; set; }
        public ObservableCollection<IMainTreeItem> ChildTreeItems { get; }

        public string DeviceName { get; set; }
        public IDevice Device { get; set; }
        #endregion
    }
}
