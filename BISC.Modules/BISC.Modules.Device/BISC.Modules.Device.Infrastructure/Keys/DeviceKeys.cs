using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.Keys
{
    public static class DeviceKeys
    {
        public const string DeviceAddingViewKey = "DeviceAddingView";
        public const string DeviceAddingRegionKey = "DeviceAddingRegionKey";


        public const string DeviceModelKey = "IED";
        public const string RestartDeviceContextKey = "RestartDeviceEntity";
        public const string DeviceConflictContextKey = "DeviceConflictEntity";
        public const string ReconnectDeviceContextKey = "ReconnectDeviceEntity";



        public const string DeviceTreeItemViewKey = "DeviceTreeItemView";
        public const string DeviceDetailsViewKey = "DeviceDetailsView";
        public const string DeviceConnectingViewKey = "DeviceConnectingView";
        public const string DeviceFromFileAddingViewKey = "DeviceFromFileAddingView";
        public const string DeviceLoadingTreeItemViewKey = "DeviceLoadingTreeItemView";
        public const string DeviceRestartViewKey = "DeviceRestartView";
        public const string DeviceConflictsViewKey = "DeviceConflictsView";
        public const string ReconnectDeviceViewKey = "ReconnectDeviceView";
        public const string ReconnectDeviceTreeItemViewKey = "ReconnectDeviceTreeItemView";
        public const string DeviceConfigViewKey = "DeviceConfigView";

        public static class DeviceManufacturer
        {
            public const string BemnManufacturer = "BEMN";
            public const string UnknowManufacturer = "Unknow";

        }

        public static class DeviceTypes
        {
            public const string MR5Type = "MR5";
            public const string MR801Type = "MR801";
            public const string MR851Type = "MR851";
            public const string MR741Type = "MR741";
            public const string MR761Type = "MR761";
            public const string MR762Type = "MR762";
            public const string MR763Type = "MR763";
            public const string MR771Type = "MR771";
            public const string UnknowType = "Unknow";
        }

        public static class DeviceRevisions
        {
            public const string UnknowRevision = "Unknow";
        }

        public static class ConfigurationKeys
        {
            public const string BasicConfigurationPathKey = "BasicСonfiguration.xml";
            public const string BasicConfigurationNodeKey = "BaseConfigure";
        }
    }
}
