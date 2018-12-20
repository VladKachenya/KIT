using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IFcdaInfoService
    {
        IModelElement GetModelElementFromFcda(IDevice device,IFcda fcda);
        int GetModelElementWeight(IDevice device, IModelElement modelElement, string fc);
        int GetFcdaWeight(IDevice device, IFcda fcda);
    }
}