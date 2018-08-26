using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.Services
{
   public class InfoModelLoadingService:IDeviceElementLoadingService
    {
       
        private readonly IConnectionPoolService _connectionPoolService;
        private Dictionary<string, List<string>> ldDictionary = new Dictionary<string, List<string>>();
        public InfoModelLoadingService(IConnectionPoolService connectionPoolService)
        {
            _connectionPoolService = connectionPoolService;
        }
        public async Task<int> EstimateProgress(IDevice device)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
           var ldList=await connection.MmsConnection.GetLdListAsync();
            device.Name = FindSubstring(ldList.Item);
            foreach (var ld in ldList.Item)
            {
                ldDictionary.Add(ld,(await connection.MmsConnection.GetListValiablesAsync(device.Name,ld)).Item);
            }
            int lnsTotal = 0;
            foreach (var ldName in ldDictionary.Keys)
            {
                var lnNames = ldDictionary[ldName].Where((s => !s.Contains("$"))).ToList();
                lnsTotal += lnNames.Count();
            }
            return lnsTotal;
        }

        public async Task Load(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress)
        {
          
        }
        public int Priority => 10;
        public void Dispose()
        {
           
        }




        private static string FindSubstring(List<string> dirList)  //формируется имя IED из набора строк, путем поиска совпадающих символов
        {
            string retValue = "";
            string possibleName = dirList[0];
            foreach (string str in dirList)
            {
                if (str.Length < possibleName.Length) possibleName = str;
            }
            int i = 0;
            foreach (char symbol in possibleName)
            {
                bool toAdd = true;
                foreach (string entry in dirList)
                {
                    if (entry[i] != symbol) toAdd = false;
                }
                if (toAdd) retValue += symbol;
                i++;
            }
            return retValue;
        }
    }
}
