using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels
{
    public class FTPServiceViewModel : ViewModelBase, IFTPServiceViewModel
    {
        #region ptivate filds
        private IFTPClientWrapper _ftpClientWrapper;
        #endregion

        #region C-tor
        public FTPServiceViewModel(IFTPClientWrapper ftpClientWrapper)
        {

        }
        #endregion

        #region private methods

        #endregion

        #region Implementation of IFTPSreviceViewModel

        public string FtpIpAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FtpPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FtpLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFtpConnected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand ConnectToDeviceCommand => throw new NotImplementedException();

        public ICommand ResetDeviceCommand => throw new NotImplementedException();
        #endregion
    }
}
