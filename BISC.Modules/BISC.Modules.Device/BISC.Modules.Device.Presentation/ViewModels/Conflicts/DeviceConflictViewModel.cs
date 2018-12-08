using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
   public abstract class DeviceConflictViewModel:ViewModelBase
    {
        public string ConflictTitle { get; set; }

        private bool _isConflictOk;

        public bool IsConflictOk
        {
            get => _isConflictOk;
            set => SetProperty(ref _isConflictOk, value);
        }
        public ICommand ShowConflictInTool { get; set; }
      
    }
}
