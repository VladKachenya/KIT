using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGoosesModelService
    {
        void AddGseControl(string lnName, string ldName, IModelElement devcice, IGooseControl gooseControl);
        List<IGooseInput> GetGooseInputsOfDevice(IDevice device);
        List<IGooseControl> GetGooseControlsOfDevice(IDevice device);
        void DeleteAllDeviceReferencesInGooseControlsInModel(ISclModel sclModel, string iedName);
        //void AddSubscriberDevice(IDevice device,ISclModel sclModelOfDevice,)
        void SetGooseControlSubscriber(bool isSubscribed, IGooseControl gooseControl, IDevice device);
    }
}