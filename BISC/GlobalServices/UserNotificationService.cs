using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;

namespace BISC.GlobalServices
{
    public class UserNotificationService : IUserNotificationService
    {
        public void NotifyUserGlobal(string message)
        {
            Shell.Snackbar.MessageQueue.Enqueue(message);
        }
    }
}
