using BISC.Modules.Connection.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BISC.Modules.Connection.Model.Services
{
    public class PingService : IPingService
    {
        #region private filds
        private AutoResetEvent waiter = new AutoResetEvent(false);
        #endregion

        #region Citor
        public PingService()
        {

        }
        #endregion

        #region Implementation of IPingService
        public async Task<bool> GetPing(string ip)
        {

            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);
            var isPing = false;
            Ping pinger = new Ping();
            pinger.PingCompleted += (o, e) =>
            {
                if (e.Cancelled)
                {
                    ((AutoResetEvent)e.UserState).Set();
                }
                if (e.Error != null)
                {
                    ((AutoResetEvent)e.UserState).Set();
                }

                PingReply reply = e.Reply;
                ((AutoResetEvent)e.UserState).Set();
                isPing = reply.Status == IPStatus.Success;
                semaphoreSlim.Release();
            };
            PingOptions pingOptions = new PingOptions(128, true);
            int timeout = 1000;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            try
            {
                pinger.SendAsync(ip, timeout, buffer, pingOptions, waiter);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            await semaphoreSlim.WaitAsync();
            return isPing;
        }
        #endregion

    }
}
