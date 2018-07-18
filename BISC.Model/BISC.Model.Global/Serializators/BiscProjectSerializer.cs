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
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
   public class BiscProjectSerializer : IModelElementSerializer
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

        public IModelElement DeserializeModelElement(XElement xElement)
        {
          
            DefaultModelElement defaultModelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as DefaultModelElement;
            IBiscProject project = new BiscProject(defaultModelElement?.ChildModelElements.First() as ISclModel);

            return project;
        }
    }
}
