using System.Collections.Generic;
using BISC.Model.Infrastructure.Device;

namespace BISC.Model.Infrastructure.Project
{
    public interface ISclModel
    {
        List<IIntellectualDevice> IntellectualDevices { get; set; }
        void AddIntellectualDevice(IIntellectualDevice device);
        bool DeleteIntellectualDevice(string deviceName);
    }
}