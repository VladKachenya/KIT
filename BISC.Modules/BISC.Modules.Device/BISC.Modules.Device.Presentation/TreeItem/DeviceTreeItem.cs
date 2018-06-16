using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Infrastructure.Tree.TreeCommands;

namespace BISC.Modules.Device.Presentation.TreeItem
{
   public class DeviceTreeItem:IMainTreeItem
    {

        public DeviceTreeItem()
        {
            
        }


        #region Implementation of IMainTreeItem

        public string TreeItemName { get; set; }
        public ObservableCollection<IMainTreeItem> ChildTreeItems { get; }
        public ObservableCollection<ITreeCommand> TreeItemCommands { get; }
        public ICommand TreeItemSelectedCommand { get; }
        public string TreeItemDetailsRegionName { get; }

        #endregion
    }
}
