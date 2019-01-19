using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface IGlobalSavingService
    {
        Task SaveAllDevices();
    }
}