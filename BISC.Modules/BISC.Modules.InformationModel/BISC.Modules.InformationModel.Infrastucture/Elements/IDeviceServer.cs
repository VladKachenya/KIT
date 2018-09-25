using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDeviceServer:IModelElement
    {
        ChildModelsList<ILDevice> LDevicesCollection { get; }
    }
}