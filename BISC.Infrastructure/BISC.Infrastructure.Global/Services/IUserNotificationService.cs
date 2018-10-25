using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.Services
{
    public interface IUserNotificationService
    {
        Task ShowContentAsDialog(object content);

    }



}