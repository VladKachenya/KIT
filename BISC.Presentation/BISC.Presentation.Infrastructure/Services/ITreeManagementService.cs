using System;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{

  
    public interface ITreeManagementService
    {
        void AddTreeItem(BiscNavigationParameters parameters,string viewName,Guid? parentId);
        void DeleteTreeItem(Guid treeItemId);

    }
}