using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class InfoModelService : IInfoModelService
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;

        public InfoModelService(ISclCommunicationModelService sclCommunicationModelService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
        }


        public void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint, ILDevice lDevice)
        {
            var existingLdevice =
                deviceAccessPoint.DeviceServer.Value.LDevicesCollection.FirstOrDefault((deviceExisting => deviceExisting.Inst == lDevice.Inst));

            if (existingLdevice != null)
            {
                deviceAccessPoint.DeviceServer.Value.LDevicesCollection.Remove(existingLdevice);
                deviceAccessPoint.DeviceServer.Value.ChildModelElements.Remove(existingLdevice);

            }

            deviceAccessPoint.DeviceServer.Value.LDevicesCollection.Add(lDevice);

        }

        public void InitializeInfoModel(IModelElement device,string deviceName,ISclModel sclModel)
        {
            IDeviceAccessPoint deviceAccessPoint = new DeviceAccessPoint();
            
            deviceAccessPoint.DeviceServer.Value = new DeviceServer();
            
            deviceAccessPoint.Name =
                _sclCommunicationModelService.GetConnectedAccessPoint(sclModel, deviceName).ApName;
            device.ChildModelElements.Add(deviceAccessPoint);
            deviceAccessPoint.ParentModelElement = device;
        }

        public List<ILDevice> GetLDevicesFromDevices(IModelElement device)
        {
            return (device.ChildModelElements.First((element => element is DeviceAccessPoint)) as DeviceAccessPoint)
                .DeviceServer.Value.LDevicesCollection.ToList();
        }
    }
}