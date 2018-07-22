using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
    public class SclModelElementSerializer : IModelElementSerializer<ISclModel>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public SclModelElementSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            return _defaultModelElementSerializer.SerializeModelElement(modelElement);
        }

        public ISclModel DeserializeModelElement(XElement xElement)
        {
            SclModel sclModel = new SclModel();
            ModelElement modelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as ModelElement;
            sclModel.ModelElementAttributes.AddRange(modelElement.ModelElementAttributes);
            sclModel.ChildModelElements.AddRange(modelElement.ChildModelElements);
            sclModel.Namespace = modelElement.Namespace;

            return sclModel;
        }
    }
}
