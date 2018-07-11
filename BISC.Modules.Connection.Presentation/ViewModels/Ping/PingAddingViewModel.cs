using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.ViewModels.Ping
{
    public class PingAddingViewModel : DisposableViewModelBase, IPingAddingViewModel
    {
        private IIpAddress _ipAddress;
        public PingAddingViewModel(IIpAddress ipAddress)
        {
            _ipAddress = ipAddress;
        }


        #region Implementation of IPingAddingViewModel
        public IIpAddress IpAddress
        {
            get { return _ipAddress; }
        }

        #endregion
    }
}
