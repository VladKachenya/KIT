using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Services
{
   public class GoosesLoadingService: IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private Dictionary<string,List<string>> _ldGoosesDictionary=new Dictionary<string, List<string>>();
        public GoosesLoadingService(IConnectionPoolService connectionPoolService,
            IGoosesModelService goosesModelService,ISclCommunicationModelService sclCommunicationModelService,IFtpGooseModelService ftpGooseModelService)
        {
            _connectionPoolService = connectionPoolService;
            _goosesModelService = goosesModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _ftpGooseModelService = ftpGooseModelService;
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
            _ldGoosesDictionary.Clear();
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

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel,CancellationToken cancellationToken)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            _goosesModelService.DeleteAllGoosesFromDevice(device);
            if (_ldGoosesDictionary.Values.Any())
            {

                var dynamicGooseControls =await _ftpGooseModelService.GetGooseDtosFromDevice(device.Ip);
               


                foreach (var ldevice in _ldGoosesDictionary.Keys)
                {
                    foreach (var gooseString in _ldGoosesDictionary[ldevice])
                    {
                        var goParts = gooseString.Split('$');

                        var gooses =
                            (await connection.MmsConnection.GetListGoosesAsync(ldevice, goParts[0], device.Name)).Item;
                        foreach (var gooseDto in gooses)
                        {
                            IGooseControl gooseControl = new GooseControl();
                            gooseControl.Name = gooseDto.Name;
                            gooseControl.ConfRev = gooseDto.ConfRev;
                            gooseControl.AppId = gooseDto.GoId;
                            gooseControl.DataSet = gooseDto.DatSet;

                            var gooseFtpDto =
                                dynamicGooseControls.FirstOrDefault((dto => dto.Name == gooseControl.Name));
                            if (gooseFtpDto != null)
                            {
                                gooseControl.IsDynamic = true;
                            }

                            _goosesModelService.AddGseControl(goParts[0], ldevice.Replace(device.Name, ""), device,
                                gooseControl);

                            gooseControl.GooseType = "GOOSE";
                            gooseControl.FixedOffs = false;
                            IGse gse = new Gse();
                            gse.ChildModelElements.Add(new SclAddress());
                            gse.CbName = gooseDto.CbName;
                            gse.LdInst = gooseDto.LdInst;
                            gse.MaxTime.Value = new DurationInMilliSec("MaxTime")
                            {
                                Multiplier = "m",
                                Value = (int) gooseDto.MaxTime,
                                Unit = "s"
                            };
                            gse.MinTime.Value = new DurationInMilliSec("MinTime")
                            {
                                Multiplier = "m",
                                Value = (int) gooseDto.MinTime,
                                Unit = "s"
                            };
                            gse.MacAddress = gooseDto.MAC_Address;
                            gse.AppIdDec = gooseDto.APPID.ToString("D4");
                            gse.VlanId = gooseDto.VLAN_ID.ToString();
                            gse.VlanPriority = (int) gooseDto.VLAN_PRIORITY;
                            _sclCommunicationModelService.AddGse(gse, sclModel, device.Name);
                        }

                    }

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
