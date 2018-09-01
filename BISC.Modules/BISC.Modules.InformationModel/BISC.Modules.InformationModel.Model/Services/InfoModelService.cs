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
        private readonly IBiscProject _biscProject;

        public InfoModelService(ISclCommunicationModelService sclCommunicationModelService,IBiscProject biscProject)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
        }


        public void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint, ILDevice lDevice)
        {
            var existingLdevice =
                deviceAccessPoint.DeviceServer.LDevicesCollection.FirstOrDefault((deviceExisting => deviceExisting.Inst == lDevice.Inst));

            if (existingLdevice != null)
            {
                deviceAccessPoint.DeviceServer.LDevicesCollection.Remove(existingLdevice);
            }

            deviceAccessPoint.DeviceServer.LDevicesCollection.Add(lDevice);

        }

        public void InitializeInfoModel(IModelElement device,string deviceName)
        {
            IDeviceAccessPoint deviceAccessPoint = new DeviceAccessPoint();
            deviceAccessPoint.DeviceServer = new DeviceServer();
            deviceAccessPoint.Name =
                _sclCommunicationModelService.GetConnectedAccessPoint(_biscProject.MainSclModel, deviceName).ApName;
            device.ChildModelElements.Add(deviceAccessPoint);

        }
    }
}