using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Presentation.UiFromModel
{
    public class UiFromModelElementRegistryService : IUiFromModelElementRegistryService
    {
        private List<Tuple<string,IUiFromModelElementService>> _servicesKeysList = new List<Tuple<string, IUiFromModelElementService>>();
        
        public void RegisterModelElement(IUiFromModelElementService modelElementUiService, string uiKey)
        {
            _servicesKeysList.Add(new Tuple<string, IUiFromModelElementService>(uiKey,modelElementUiService));

        }

        public bool TryHandleModelElementInUiByKey(IModelElement modelElement, TreeItemIdentifier parentTreeItemIdentifier,
            string uiKey)
        {
            foreach (var serviceKeyTuple in _servicesKeysList)
            {
                if (serviceKeyTuple.Item1 == uiKey)
                {
                    serviceKeyTuple.Item2.HandleModelElement(modelElement,parentTreeItemIdentifier,uiKey);
                }
            }

            return true;
        }
    }
}