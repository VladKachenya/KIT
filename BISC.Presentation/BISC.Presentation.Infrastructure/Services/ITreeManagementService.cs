using System;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{
    public class TreeItemIdentifier
    {
        public static string Key = "TreeItemIdentifier";
        public TreeItemIdentifier(Guid? itemId, TreeItemIdentifier parenTreeItemIdentifier = null)
        {
            ParenTreeItemIdentifier = parenTreeItemIdentifier;
            ItemId = itemId;
        }

        public TreeItemIdentifier ParenTreeItemIdentifier { get; }
        public Guid? ItemId { get; }
    }
  
    public interface ITreeManagementService
    {
        TreeItemIdentifier AddTreeItem(BiscNavigationParameters parameters,string viewName, TreeItemIdentifier parentTreeItemIdentifier);
        void DeleteTreeItem(TreeItemIdentifier treeItemId);

        void ClearMainTree();

    }
}