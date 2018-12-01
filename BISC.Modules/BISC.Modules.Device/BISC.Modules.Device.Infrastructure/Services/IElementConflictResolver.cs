using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.HelpClasses;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IElementConflictResolver
    {
        string ConflictName { get; }
        bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject);
        Task<ResolvingResult> ResolveConflict(bool isFromDevice, string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject);
        void ShowConflicts(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject);
    }
}