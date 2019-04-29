using BISC.Modules.DataSets.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DataSetNameService : IDataSetNameService
    {
        private readonly char[] _nambers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
        public bool GetIsDynamic(string dataSetName) => dataSetName.Trim(_nambers) != "DS";
    }
}