using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Presentation.Interfaces.Menu
{
    public interface IApplicationSettingsViewModel
    {

        ICommand CloseCommand { get; }
        ICommand ConfirmCommand { get; }
        bool IsUserLoggingEnabled { get; set; }

        bool IsAutoEnabledValidityInGooseReceiving { get; set; }
        bool IsAutoEnabledQualityInGooseReceiving { get; set; }
        string MmsQueryDelay { get; set; }
        string FtpTimeOutDelay { get; set; }
        string MaxResponseTime { get; set; }
        bool IsVisibleValidadionError { get; set; }

    }
}
