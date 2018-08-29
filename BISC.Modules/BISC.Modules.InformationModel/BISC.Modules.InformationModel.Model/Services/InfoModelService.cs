using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class InfoModelService : IInfoModelService
    {
        public InfoModelService()
        {

        }


        public void AddOrReplaceLDevice(IModelElement device, ILDevice lDevice)
        {
            device.TryGetFirstChildOfType(out IDeviceServer server);
            var existingLdevice =
                server.LDevicesCollection.FirstOrDefault((deviceExisting => deviceExisting.Inst == lDevice.Inst));

            if (existingLdevice != null)
            {
                server.LDevicesCollection.Remove(existingLdevice);
            }

            server.LDevicesCollection.Add(lDevice);

        }
    }
}