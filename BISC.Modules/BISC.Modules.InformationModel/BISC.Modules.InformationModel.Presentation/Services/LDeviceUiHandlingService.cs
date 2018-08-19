using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.InformationModel.Presentation.Services
{
   public class LDeviceUiHandlingService: IUiFromModelElementService
    {
        private readonly ITreeManagementService _treeManagementService;

        public LDeviceUiHandlingService(ITreeManagementService treeManagementService)
        {
            _treeManagementService = treeManagementService;
        }


        public void HandleModelElement(IModelElement modelElement, TreeItemIdentifier uiParentId, string uiKey)
        {
            if (uiParentId == null) return;
            List<ILDevice> lDevices=new List<ILDevice>();
            modelElement.GetAllChildrenOfType(ref lDevices);
            foreach (var lDevice in lDevices)
            {
                BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
                biscNavigationParameters.AddParameterByName(InfoModelKeys.ModelKeys.LDeviceKey, lDevice);
                var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
                    InfoModelKeys.LdeviceTreeItemViewKey, uiParentId.ItemId);
            }
          
        }
    }
}
