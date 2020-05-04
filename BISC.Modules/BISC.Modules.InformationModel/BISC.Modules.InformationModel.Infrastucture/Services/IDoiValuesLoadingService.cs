using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
    public interface IDoiValuesLoadingService
    {
        Task LoadDoiValues(ISclModel sclModel,IDevice device, IDoi doi, string requiredFc = null);
    }
}