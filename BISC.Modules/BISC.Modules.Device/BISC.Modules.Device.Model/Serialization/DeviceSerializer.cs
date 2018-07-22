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
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Model.Serialization
{
   public class DeviceSerializer: IModelElementSerializer<IDevice>
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

        public IDevice DeserializeModelElement(XElement xElement)
        {
            Model.Device device = new Model.Device();
            ModelElement modelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as ModelElement;
            device.ModelElementAttributes.AddRange(modelElement.ModelElementAttributes);
            device.ChildModelElements.AddRange(modelElement.ChildModelElements);
            device.Namespace = modelElement.Namespace;
            device.ElementName = modelElement.ElementName;
            device.Name = modelElement.ModelElementAttributes
                .FirstOrDefault((attribute => attribute.Name.LocalName == "name"))?.Value;

            return device;
        }

        #endregion
    }
}
