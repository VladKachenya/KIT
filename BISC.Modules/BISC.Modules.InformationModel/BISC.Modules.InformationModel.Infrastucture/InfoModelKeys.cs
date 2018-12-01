using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.InformationModel.Infrastucture
{
   public static class InfoModelKeys
    {

        public static class DataTypeTemplateKeys
        {
            public  const string DataTypeTemplatesModelItemKey = "DataTypeTemplates";
            public const string LNodeTypeModelItemKey = "LNodeType";
            public const string DoItemKey = "DO";

            public const string DOTypeModelItemKey = "DOType";
            public const string DaItemKey = "DA";
            public const string SdoItemKey = "SDO";

            public const string DATypeModelItemKey = "DAType";
            public const string DaTypeItemKey = "DAType";
            public const string BDAItemKey = "BDA";

            public const string EnumTypeModelItemKey = "EnumType";
            public const string EnumValItemKey = "EnumVal";
        }

        public static class ModelKeys
        {
            public const string LDeviceKey = "LDevice";
            public const string LogicalNodeKey = "LN";
            public const string LogicalNodeZeroKey = "LN0";
            public const string DoiKey = "DOI";
            public const string SdiKey = "SDI";
            public const string DaiKey = "DAI";
            public const string ValKey = "Val";
            public const string ServerKey = "Server";
            public const string AccessPointKey = "AccessPoint";
            public const string FcSetKey = "FC";
            public const string SettingControlKey = "SettingControl";
        }


        public static string SettingControlDetailsViewKey = "SettingControlDetailsView";
        public static string SettingsControlTreeItemViewKey = "SettingsControlTreeItemView";


        public static string InfoModelTreeItemViewKey = "InfoModelTreeItemView";
        public static string InfoModelTreeItemDetailsViewKey = "InfoModelTreeItemDetailsView";

        public static string LdeviceTreeItemViewKey = "LDeviceTreeItemView";

    }
}
