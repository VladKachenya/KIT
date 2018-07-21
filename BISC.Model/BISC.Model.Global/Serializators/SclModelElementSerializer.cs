using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
    public class SclModelElementSerializer : IModelElementSerializer<ISclModel>
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public SclModelElementSerializer(IModelElementsRegistryService modelElementsRegistryService, DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            return _defaultModelElementSerializer.SerializeModelElement(modelElement);
        }

        public ISclModel DeserializeModelElement(XElement xElement)
        {
            SclModel sclModel = new SclModel();
            DefaultModelElement defaultModelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as DefaultModelElement;
            sclModel.ModelElementAttributes.AddRange(defaultModelElement.ModelElementAttributes);
            sclModel.ChildModelElements.AddRange(defaultModelElement.ChildModelElements);
            sclModel.Namespace = defaultModelElement.Namespace;

            return sclModel;
        }
    }
}
