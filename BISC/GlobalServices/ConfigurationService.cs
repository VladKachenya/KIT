using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Properties;

namespace BISC.GlobalServices
{
    public class ConfigurationService : IConfigurationService
    {
        private List<string> _lastConnectedIpAddresses;


        public ConfigurationService()
        {

        }


        #region Implementation of IConfigurationService

        public List<string> LastOpenedFiles
        {
            get
            {
                if (Settings.Default.LastOpenedFiles == null)
                {
                    Settings.Default.LastOpenedFiles=new StringCollection();
                    Settings.Default.Save();
                }
                return Settings.Default.LastOpenedFiles.Cast<string>().ToList();
            }
            set
            {
                var lastOpenedFiles = new StringCollection();
                lastOpenedFiles.AddRange(value.ToArray());
                Settings.Default.LastOpenedFiles = lastOpenedFiles;
                Settings.Default.Save();
            }
        }

        public List<string> LastIpAddresses
        {
            get
            {
                if (Settings.Default.LastIpAddresses == null)
                {
                    Settings.Default.LastIpAddresses = new StringCollection();
                    Settings.Default.Save();
                }
                return Settings.Default.LastIpAddresses.Cast<string>().ToList();
            }
            set
            {
                var lastIpAddress = new StringCollection();
                lastIpAddress.AddRange(value.ToArray());
                Settings.Default.LastIpAddresses = lastIpAddress;
                Settings.Default.Save();
            }
        }

        public List<string> LastConnectedIpAddresses
        {
            get
            {
                if (Settings.Default.LastConnectedIpAddress == null)
                {
                    Settings.Default.LastConnectedIpAddress = new StringCollection();
                    Settings.Default.Save();
                }
                return Settings.Default.LastConnectedIpAddress.Cast<string>().ToList();
            }
            set
            {
                var lastConnectedIpAddress = new StringCollection();
                lastConnectedIpAddress.AddRange(value.ToArray());
                Settings.Default.LastConnectedIpAddress = lastConnectedIpAddress;
                Settings.Default.Save();
            }
        }

        public string LastProjectPath
        {
            get { return Settings.Default.LastProjectPath; }
            set
            {
                Settings.Default.LastProjectPath = value;
                Settings.Default.Save();
            }
        }

        #endregion
    }
}
