using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Infrastructure.Keys
{
    public static class DatasetKeys
    {
        public static class DatasetModelKeys
        {
            public static string DataSetModelKey => "DataSet";
            public static string FcdaModelKey => "FCDA";

        }

        public static class DatasetViewModelKeys
        {
            public const string DataSetsTreeItemView = "DataSetsTreeItemView";
            public const string DataSetsDetailsView = "DataSetsDetailsView";
            public const string DatasetConflictsWindow = "DatasetConflictsWindow";

            public const string FcdaAdderViewModel = "FcdaAdderViewModel";
            public const string DatasetsConflictContextKey = "DatasetsConflictContext";

        }

        public static class DataSetWarningKeys
        {
            public const string DataSetLoadErrorWarningTagKey = "DataSetLoadErrorWarningTag";
        }
    }
}
