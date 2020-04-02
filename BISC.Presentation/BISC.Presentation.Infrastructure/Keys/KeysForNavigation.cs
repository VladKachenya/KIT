namespace BISC.Presentation.Infrastructure.Keys
{
    public static class KeysForNavigation
    {
        public static class RegionNames
        {
            public static string MainTreeRegionKey = "TreeRegion";
            public static string MainTabHostRegionKey => "TabHostRegion";
            public static string GlobalDialogRegionKey = "GlobalDialogRegion";
            public static string HamburgerMenuKey = "HamburgerMenuRegion";
            public static string ToolBarMenuKey = "ToolBarMenuRegion";
            public static string NotificationBarKey = "NotificationBarRegion";

            public static string InfoModelTreeItemDetailsViewKey => nameof(InfoModelTreeItemDetailsViewKey);
            public static string DataSetsDetailsViewKey => nameof(DataSetsDetailsViewKey);
            public static string ReportsDetailsViewKey => nameof(ReportsDetailsViewKey);
            public static string GooseControlsTabViewKey => nameof(GooseControlsTabViewKey);
            public static string GooseMatrixTabLightKey => nameof(GooseMatrixTabLightKey);

        }

        public static class NavigationParameter
        {
            public static string IsReadOnly => nameof(IsReadOnly);
            public static string IsFromDeviceDetails => nameof(IsFromDeviceDetails);
        }

        public static class ViewNames
        {
            public static string MainTreeViewName = "MainTree";
            public static string MainTabHostViewName = "TabHost";
            public static string HamburgerMenuViewName = "HamburgerMenu";
            public static string ToolBarMenuViewName = "ToolBarMenu";
            public static string SaveChangesViewName = "SaveChangesView";
            public static string ApplicationSettingsViewName = "ApplicationSettingsView";
            public static string UserInteractionOptionsViewName = "UserInteractionOptionsView";

        }

        public static class AppGuids
        {
            public static string NullGuid = "00000000-0000-0000-0000-000000000000";
        }

    }
}
