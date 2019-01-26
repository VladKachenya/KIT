using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface ITreeManagementService
    {
        TreeItemIdentifier AddTreeItem(BiscNavigationParameters parameters,string viewName, TreeItemIdentifier parentTreeItemIdentifier,string tag=null);
        void DeleteTreeItem(TreeItemIdentifier treeItemId);
	    TreeItemIdentifier GetDeviceTreeItem(string deviceName);

        void ClearMainTree();

    }
}