namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IDataSetNameService
    {
        bool GetIsDynamic(string dataSetName);
    }
}