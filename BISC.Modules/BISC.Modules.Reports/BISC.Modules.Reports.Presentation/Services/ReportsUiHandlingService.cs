﻿using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportsUiHandlingService : IUiFromModelElementService
    {
        private readonly ITreeManagementService _treeManagementService;

        public ReportsUiHandlingService(ITreeManagementService treeManagementService)
        {
            _treeManagementService = treeManagementService;
        }
        public UiEntityIdentifier HandleModelElement(IModelElement modelElement, UiEntityIdentifier uiParentId, string uiKey)
        {
            if (uiParentId == null)
            {
                return null;
            }
          return  _treeManagementService.AddTreeItem(
                new BiscNavigationParameters() { new BiscNavigationParameter("IED", modelElement) },
                ReportsKeys.ReportsPresentationKeys.ReportsTreeItemView, uiParentId);
        }
    }
}
