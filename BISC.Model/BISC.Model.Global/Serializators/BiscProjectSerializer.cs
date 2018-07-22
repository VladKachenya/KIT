using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
   public class BiscProjectSerializer : IModelElementSerializer<IBiscProject>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;
        public BiscProjectSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            return _defaultModelElementSerializer.SerializeModelElement(modelElement);
        }

        public IBiscProject DeserializeModelElement(XElement xElement)
        {
          
            ModelElement modelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as ModelElement;
            IBiscProject project = new BiscProject(modelElement?.ChildModelElements.First() as ISclModel);

            return project;
        }
    }
}
