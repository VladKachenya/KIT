using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Model.Infrastructure.Services;

namespace BISC.Model.Global.Services
{

    public class ModelComposingService : IModelComposingService
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public ModelComposingService(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }

        public ISclModel DeserializeModelFromFile(XElement mainElement)
        {
            IModelElement modelElement = _modelElementsRegistryService.DeserializeModelElement<IModelElement>(mainElement);

            return modelElement as ISclModel;
        }

        public void SerializeModelInFile(string filePath, IModelElement modelElement, SerializingType serializingType)
        {
           XElement xElement= _modelElementsRegistryService.SerializeModelElement(modelElement,serializingType);
            xElement.Save(filePath);
        }
    }
}
