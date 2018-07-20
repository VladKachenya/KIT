using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Constants;
using BISC.Infrastructure.Global.Services;
using MaterialDesignThemes.Wpf;

namespace BISC.GlobalServices
{
    public class UserNotificationService : IUserNotificationService
    {
        public void NotifyUserGlobal(string message)
        {
            Shell.Snackbar.MessageQueue.Enqueue(message);
        }

        public async Task ShowContentAsDialog(object content)
        {
            try
            {
                await DialogHost.Show(content, Constants.RootDialogKey);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        

    }
}
