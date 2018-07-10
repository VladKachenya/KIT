using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Model.Serialization
{
   public class DeviceSerializer: IModelElementSerializer
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DeviceSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        #region Implementation of IModelElementSerializer

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            XElement xElement= _defaultModelElementSerializer.SerializeModelElement(modelElement);
            xElement.SetAttributeValue("name",(modelElement as IDevice)?.Name);
            return xElement;
        }

        public IModelElement DeserializeModelElement(XElement xElement)
        {
            Model.Device device = new Model.Device();
            DefaultModelElement defaultModelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as DefaultModelElement;
            device.ModelElementAttributes.AddRange(defaultModelElement.ModelElementAttributes);
            device.ChildModelElements.AddRange(defaultModelElement.ChildModelElements);
            device.Namespace = defaultModelElement.Namespace;
            device.ElementName = defaultModelElement.ElementName;
            device.Name = defaultModelElement.ModelElementAttributes
                .FirstOrDefault((attribute => attribute.Name.LocalName == "name"))?.Value;

            return device;
        }

        #endregion
    }
}
