using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using System;
using System.Globalization;

namespace BISC.Modules.Device.Model.Model
{
    public class Device : ModelElement, IDevice
    {
        private string _revision;

        public Device()
        {
            ElementName = DeviceKeys.DeviceModelKey;
            DeviceGuid = Guid.NewGuid();
        }

        #region Implementation of IDevice
        public Guid DeviceGuid { get; private set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }

        public string Revision
        {
            get => _revision;
            set
            {
                _revision = value;
                RevisionDetails = new Revision(value);
            }
        }

        public IRevision RevisionDetails { get; protected set; }

        public void SetGuid(Guid setGuid)
        {
            DeviceGuid = setGuid;
        }
        #endregion

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj))
            {
                return false;
            }

            if (!(obj is IDevice))
            {
                return false;
            }

            var element = obj as IDevice;
            if (element.Name != Name)
            {
                return false;
            }

            if (element.Ip != Ip)
            {
                return false;
            }

            if (element.Description != Description)
            {
                return false;
            }

            if (element.Manufacturer != Manufacturer)
            {
                return false;
            }

            if (element.Type != Type)
            {
                return false;
            }

            if (element.Revision != Revision)
            {
                return false;
            }

            return true;
        }
    }
}
