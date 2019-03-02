using System;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Device.Infrastructure.Model
{
    public interface IDevice:IModelElement
    {
        Guid DeviceGuid { get; }
        void SetGuid(Guid setGuid);
        string Name { get; set; }
        string Ip { get; set; }
        string Description { get; set; }
        string Manufacturer { get; set; }
        string Type { get; set; }
        string Revision { get; set; } 
    }
}
