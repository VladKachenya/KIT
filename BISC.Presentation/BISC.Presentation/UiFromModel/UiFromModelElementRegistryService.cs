using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Presentation.UiFromModel
{
    public class UiFromModelElementRegistryService : IUiFromModelElementRegistryService
    {
        private Dictionary<string, IUiFromModelElementService> _fromModelElementServicesDictionary = new Dictionary<string, IUiFromModelElementService>();

        public void RegisterModelElement(IUiFromModelElementService modelElementUiService, string elementName)
        {
            if (_fromModelElementServicesDictionary.ContainsKey(elementName))
            {
                throw new ArgumentException($"UiService with key {elementName} already exists");
            }
            _fromModelElementServicesDictionary.Add(elementName, modelElementUiService);
        }

        public bool GetIsModelElementRegistered(string elementName)
        {
            return _fromModelElementServicesDictionary.ContainsKey(elementName);
        }

        public bool TryHandleModelElementInUiByKey(IModelElement modelElement)
        {
            if (!_fromModelElementServicesDictionary.ContainsKey(modelElement.ElementName))
            {

                return false;
            }
            var uiFromModelElementService = _fromModelElementServicesDictionary[modelElement.ElementName];
            uiFromModelElementService.HandleModelElement(modelElement);
            return true;
        }


    }
}
