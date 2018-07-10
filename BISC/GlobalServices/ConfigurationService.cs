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

        public List<string> LastSuccessfullyConnectedIpAddress
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        #endregion
    }
}
