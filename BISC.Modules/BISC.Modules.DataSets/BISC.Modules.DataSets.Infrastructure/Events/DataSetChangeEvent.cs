using System;

namespace BISC.Modules.DataSets.Infrastructure.Events
{
    public class DataSetChangeEvent
    {
        public string DataSetName { get; }
        public Guid DeviceGuid { get; }

        public DataSetChangeEvent(string dataSetName, Guid deviceGuid)
        {
            DataSetName = dataSetName;
            DeviceGuid = deviceGuid;
        }
    }
}