using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Gooses.Model.Services
{
   public class GoosesLoadingService: IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private Dictionary<string,List<string>> _ldGoosesDictionary=new Dictionary<string, List<string>>();
        public GoosesLoadingService(IConnectionPoolService connectionPoolService)
        {
            _connectionPoolService = connectionPoolService;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            
        }

        #endregion

        #region Implementation of IDeviceElementLoadingService

        public async Task<int> EstimateProgress(IDevice device)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            var ldevices = (await connection.MmsConnection
                .GetLdListAsync()).Item;
            int count = 0;
            foreach (var lDevice in ldevices)
            {
             
               var definitions=(await connection.MmsConnection.GetListValiablesAsync(lDevice, true)).Item;
                foreach (var definition in definitions)
                {
                    var parts = definition.Split('$');
                    if (parts.Length == 2 && parts[1] == "GO")
                    {
                        if (_ldGoosesDictionary.ContainsKey(lDevice))
                        {
                            _ldGoosesDictionary[lDevice].Add(definition);
                        }
                        else
                        {
                            _ldGoosesDictionary.Add(lDevice, new List<string>(){definition});

                        }
                        count ++;
                    }
                }
            }

            return count;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel)
        {
            if (_ldGoosesDictionary.Values.Any())
            {
                foreach (var ldevice in _ldGoosesDictionary.Keys)
                {
                    
                }
            }
            else
            {
                return;
            }
        }

        public int Priority => 15;

        #endregion
    }
}
