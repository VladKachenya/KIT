using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Model.Global.Services
{
    public class ModelComposingService
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public ModelComposingService(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }

        IModelElement DeserializeModelFromFile(XElement mainElement)
        {
            IModelElement modelElement = _modelElementsRegistryService
                .GetModelElementSerializatorByKey(mainElement.Name.LocalName).DeserializeModelElement(mainElement);
            return modelElement;
        }

        
    }
}
