using System;

namespace BISC.Modules.Device.Infrastructure.Model.Revision
{
    public interface IRevision : IVersionComparable
    {
        int RevisionVersion { get; }
        int RevisionSubversion { get; }
        DateTime RevisionDateTime { get; }
    }
}