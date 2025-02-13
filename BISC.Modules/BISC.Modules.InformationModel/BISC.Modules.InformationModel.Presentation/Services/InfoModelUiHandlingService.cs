﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.InformationModel.Presentation.Services
{
    public class InfoModelUiHandlingService : IUiFromModelElementService
    {
        private readonly ITreeManagementService _treeManagementService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;

        public InfoModelUiHandlingService(ITreeManagementService treeManagementService,IUiFromModelElementRegistryService uiFromModelElementRegistryService)
        {
            _treeManagementService = treeManagementService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
        }

        public UiEntityIdentifier HandleModelElement(IModelElement modelElement, UiEntityIdentifier uiParentId, string uiKey)
        {

            if (uiParentId == null)
            {
                return null;
            }
            var treeItemIdInfoModel= _treeManagementService.AddTreeItem(
                   new BiscNavigationParameters() { new BiscNavigationParameter("IED", modelElement) },
                   InfoModelKeys.InfoModelTreeItemViewKey, uiParentId);
            var treeItemIdSettingsControl = _treeManagementService.AddTreeItem(
                new BiscNavigationParameters() { new BiscNavigationParameter("IED", modelElement) },
                InfoModelKeys.SettingsControlTreeItemViewKey, uiParentId);
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(modelElement, treeItemIdInfoModel,
                InfoModelKeys.ModelKeys.LDeviceKey);
            return treeItemIdInfoModel;

        }
    }
}