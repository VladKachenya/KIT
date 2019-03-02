using System;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.HelpClasses;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IElementConflictResolver
    {
        string ConflictName { get; }
        ConflictType ConflictType { get; }
        bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject);
        Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject);
        void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject);
    }

    public enum ConflictType
    {
        AutomaticallyResolvingFromDevice,
        ManualResolveNeeded
    }
}