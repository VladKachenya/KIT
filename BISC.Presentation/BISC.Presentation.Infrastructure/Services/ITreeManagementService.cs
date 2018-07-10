using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{

  
    public interface ITreeManagementService
    {
        void AddTreeItem(IMainTreeItem treeItem,string viewName);
        
    }
}