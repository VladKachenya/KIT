using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface IGlobalSavingService
    {
        Task SaveAllDevices();
    }
}