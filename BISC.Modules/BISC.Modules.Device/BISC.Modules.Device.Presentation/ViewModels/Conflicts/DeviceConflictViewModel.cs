using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
    public class DeviceConflictViewModel:ViewModelBase
    {
        public DeviceConflictViewModel()
        {
            
        }
        public string ConflictTitle { get; set; }
        public bool IsConflictResolved { get; set; }
        public bool IsConflictOk { get; set; }
        public ICommand ShowConflictInTool { get; }
        public ICommand SelectDeviceOptionCommand { get; }
        public ICommand SelectProjectOptionCommand { get; }
        public ICommand CancelSelectionCommand { get; }
    }
}