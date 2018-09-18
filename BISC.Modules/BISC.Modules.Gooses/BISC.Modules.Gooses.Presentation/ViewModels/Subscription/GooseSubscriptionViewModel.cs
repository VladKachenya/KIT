using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Subscription
{
    public class GooseSubscriptionViewModel : ViewModelBase
    {
        public string GooseControlName { get; set; }
        public string DeviceName { get; set; }
        public bool IsSubscribed { get; set; }

    }
}
