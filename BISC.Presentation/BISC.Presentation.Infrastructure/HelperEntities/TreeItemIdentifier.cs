using System;

namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class TreeItemIdentifier
    {
        public static string Key = "TreeItemIdentifier";
        public TreeItemIdentifier(Guid? itemId, TreeItemIdentifier parenTreeItemIdentifier = null,string tag=null)
        {
            ParenTreeItemIdentifier = parenTreeItemIdentifier;
            ItemId = itemId;
            Tag = tag;
        }
        public string Tag { get;  }
        public TreeItemIdentifier ParenTreeItemIdentifier { get; }
        public Guid? ItemId { get; }
    }
}