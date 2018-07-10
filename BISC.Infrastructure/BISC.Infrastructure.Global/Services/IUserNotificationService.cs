using System.Threading.Tasks;

namespace BISC.Infrastructure.Global.Services
{
    public interface IUserNotificationService
    {
        void NotifyUserGlobal(string message);
        Task ShowContentAsDialog(object content);

    }
}