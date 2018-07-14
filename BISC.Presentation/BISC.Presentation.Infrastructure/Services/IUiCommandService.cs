using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface IUiCommandService
    {
        void OnCommandExecute(ITrackableCommand trackableCommand);
    }
}