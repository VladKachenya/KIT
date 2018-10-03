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
            public const string FcdaAdderViewModel = "FcdaAdderViewModel";
        }
    }
}
