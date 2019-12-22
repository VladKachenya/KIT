using System;

namespace BISC.Presentation.Infrastructure.ChangeTracker
{
    public interface IChangeTracker:IDisposable
    {
        void SetValue(string key, object value);
        void SetTrackingEnabled(bool isTrackingEnabled);
        void AcceptChanges(bool generateCheckEvent=true);
        bool GetIsModifiedRecursive();
        void SetModified();
    }
}