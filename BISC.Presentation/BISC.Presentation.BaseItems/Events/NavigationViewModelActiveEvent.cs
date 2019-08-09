using System;

namespace BISC.Presentation.BaseItems.Events
{
    public class NavigationViewModelActiveEvent
    {
        public Guid TreeItemIdentifier { get; }

        public NavigationViewModelActiveEvent(Guid treeItemIdentifier)
        {
            TreeItemIdentifier = treeItemIdentifier;
        }
    }
}