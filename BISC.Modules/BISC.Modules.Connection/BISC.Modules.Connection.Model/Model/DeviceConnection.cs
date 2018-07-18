using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Model.Services;
using BISC.Presentation.BaseItems.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Model.Model
{
    public class DeviceConnection : ViewModelBase, IDeviceConnection
    {

        protected string ip;
        protected bool isPingable;

        public DeviceConnection()
        {
            Ip = "0.0.0.0";
            IsPingable = false;
        }

        public DeviceConnection(string IP)
        {
            Ip = IP;
            IsPingable = false;
        }

        #region Implementation of IDeviceConnection
        public string Ip
        {
            get { return ip; }
            set
            {
                if(!StaticValidatorIp.IsIpAddress(value)) throw new Exception("Not valid IP");
                ip = value;
                OnPropertyChanged();
            }
        }
        public bool IsPingable
        {
            get { return isPingable; }
            set
            {
                isPingable = value;
                OnPropertyChanged();
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool TryConnect()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
