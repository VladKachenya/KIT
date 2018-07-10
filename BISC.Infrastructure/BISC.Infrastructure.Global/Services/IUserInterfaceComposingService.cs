using System.Windows.Input;

namespace BISC.Infrastructure.Global.Services
{
    public interface IUserInterfaceComposingService
    {
        void AddGlobalCommand(ICommand command,string name);
    }
}