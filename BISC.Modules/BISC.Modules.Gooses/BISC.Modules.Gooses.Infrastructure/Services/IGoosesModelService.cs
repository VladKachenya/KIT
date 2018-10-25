using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

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
        void AddGooseExternalReferenceToDevice(IFcda fcda, IDevice device, string deviceNameOfFcda);
        List<Tuple<IDevice, IGooseControl>> GetGooseControlsSubscribed(IDevice deviceSubscriber, ISclModel sclModel);

        void SetGooseMatrixForDevice(IDevice device,IGooseMatrix gooseMatrix);

        IGooseMatrix GetGooseMatrixForDevice(IDevice device);
        void DeleteGooseCbAndGseByName(string name, IDevice device);
        bool CompareFcdaAndExtRef(IExternalGooseRef externalGooseRef, IFcda fcda);
        void DeleteAllGoosesFromDevice(IDevice device);
    }
}