using System;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{
    public class TreeItemIdentifier
    {
        public static string Key = "TreeItemIdentifier";
        public TreeItemIdentifier(Guid? parentId, Guid? itemId)
        {
            ParentId = parentId;
            ItemId = itemId;
        }

        public Guid? ParentId { get; }
        public Guid? ItemId { get; }
    }
  
    public interface ITreeManagementService
    {
        TreeItemIdentifier AddTreeItem(BiscNavigationParameters parameters,string viewName,Guid? parentId);
        void DeleteTreeItem(TreeItemIdentifier treeItemId);

    }
}