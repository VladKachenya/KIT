using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Keys
{
    public static class ReportsKeys
    {
        public static class ReportsModelKeys
        {
            public const string ReportControlModelKey = "ReportControl";
            public const string OptFieldsModelKey = "OptFields";
            public const string RptEnabledModelKey = "RptEnabled";
            public const string TrgOpsModelKey = "TrgOps";
        }
        public static class ReportsPresentationKeys
        {
            public const string ReportsTreeItemView = "ReportsTreeItemView";
            public const string ReportsConflictsWindow = "ReportsConflictsWindow";
            public const string ReportsConflictsContext = "ReportsConflictsContext";

            //public const string ReportEditingView = "ReportEditingView";
            //public const string ReportsAddingView = "ReportsAddingView";
            public const string ReportsFtpIncostistancyWarningTag = "ReportsFtpIncostistancyWarningTag";
            public const string ReportsIncostistancyWarningTag = "ReportsProjectAndDeviceIncostistancyWarningTag";
            public const string ReportsLoadErrorWarningTag = "ReportsLoadErrorWarningTag";
            public const string ReportsUnsavedWarningTag = "ReportsUnsavedWarningTag";


        }
    }
}
