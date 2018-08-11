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
                PingReply reply = e.Reply;
                isPing = reply.Status == IPStatus.Success;
                semaphoreSlim.Release();
            };
            PingOptions pingOptions = new PingOptions(128, true);
            int timeout = 1000;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            try
            {
                pinger.SendAsync(ip, timeout, buffer, pingOptions, null);
            }
            catch {
                isPing = false;
                semaphoreSlim.Release();
            }
            await semaphoreSlim.WaitAsync();
            return isPing;
        }
        #endregion

    }
}
