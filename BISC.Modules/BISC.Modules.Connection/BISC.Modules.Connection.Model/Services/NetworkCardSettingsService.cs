using System.Collections.Generic;
using BISC.Modules.Connection.Infrastructure.Services;
using System.Management;


namespace BISC.Modules.Connection.Model.Services
{
    public class NetworkCardSettingsService : INetworkCardSettingsService
    {
        public List<string> GetNamesAvailableNetworkCards()
        {
            var nameList = new List<string>();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var mbo in moc)
            {
                ManagementObject mo;
                if (mbo is ManagementObject)
                {
                    mo = mbo as ManagementObject;
                    if ((bool)mo["ipEnabled"])
                    {
                        nameList.Add(mo["Caption"] as string);
                    }
                }
            }
            return nameList;
        }
    }
}