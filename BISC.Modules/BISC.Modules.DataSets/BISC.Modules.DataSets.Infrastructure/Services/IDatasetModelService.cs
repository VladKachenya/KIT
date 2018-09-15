using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IDatasetModelService
    {
        void AddDatasetToDevice(IModelElement device, string ldName, string lnName);
    }
}