using System.Collections.Generic;
using BISC.Model.Infrastructure.Common;

namespace BISC.Model.Infrastructure.Device
{
    public interface IIntellectualDevice: INameableItem
    {
        List<ILogicalDevice> LogicalDevices { get; set; }
    }
}