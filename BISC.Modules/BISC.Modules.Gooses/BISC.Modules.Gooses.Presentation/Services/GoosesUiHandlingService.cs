﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Gooses.Presentation.Services
{
   public class GoosesUiHandlingService: IUiFromModelElementService
    {
        private readonly ITreeManagementService _treeManagementService;

        public GoosesUiHandlingService(ITreeManagementService treeManagementService)
        {
            _treeManagementService = treeManagementService;
        }
        public UiEntityIdentifier HandleModelElement(IModelElement modelElement, UiEntityIdentifier uiParentId, string uiKey)
        {

            if (uiParentId == null)
            {
                return null;
            }
            return _treeManagementService.AddTreeItem(
                new BiscNavigationParameters() { new BiscNavigationParameter("IED", modelElement) },
                GooseKeys.GoosePresentationKeys.GooseGroupTreeItemViewKey, uiParentId);
         
        }
    }
}
