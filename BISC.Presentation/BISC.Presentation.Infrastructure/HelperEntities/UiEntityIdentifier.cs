using System;

namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class UiEntityIdentifier
    {
        public static string Key = "TreeItemIdentifier";
        public UiEntityIdentifier(
            Guid? itemId, 
            UiEntityIdentifier parenUiEntityIdentifier = null, 
            string tag = null, 
            string viewName = null, 
            bool isTreeItem = false)
        {
            ParenUiEntityIdentifier = parenUiEntityIdentifier;
            ItemId = itemId;
            Tag = tag;
            ViewName = viewName;
            IsTreeItem = isTreeItem;
        }
        public string Tag { get; }
        public string ViewName { get; }
        public bool IsTreeItem { get; }
        public UiEntityIdentifier ParenUiEntityIdentifier { get; }
        public Guid? ItemId { get; }
    }
}