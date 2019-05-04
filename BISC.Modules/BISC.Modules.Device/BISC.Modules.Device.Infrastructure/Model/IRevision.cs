using System;

namespace BISC.Modules.Device.Infrastructure.Model
{
    public interface IRevision
    {
        int RevisionVersion { get; }
        int RevisionSubversion { get; }
        DateTime RevisionDateTime { get; }
    }
}