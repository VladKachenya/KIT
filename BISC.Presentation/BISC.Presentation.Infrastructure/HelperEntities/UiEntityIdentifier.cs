using System;

namespace BISC.Presentation.Infrastructure.HelperEntities
{
    public class UiEntityIdentifier
    {
        public static string Key = "TreeItemIdentifier";
        public UiEntityIdentifier(Guid? itemId, UiEntityIdentifier parenUiEntityIdentifier = null,string tag=null)
        {
            ParenUiEntityIdentifier = parenUiEntityIdentifier;
            ItemId = itemId;
            Tag = tag;
        }
        public string Tag { get;  }
        public UiEntityIdentifier ParenUiEntityIdentifier { get; }
        public Guid? ItemId { get; }
    }
}