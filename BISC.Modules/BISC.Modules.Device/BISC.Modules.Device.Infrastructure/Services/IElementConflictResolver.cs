using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IElementConflictResolver
    {
        string ConflictName { get; }
        bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject);

    }
}