using System;

namespace BISC.Presentation.Infrastructure.ChangeTracker
{
    public interface IChangeTracker:IDisposable
    {
        void SetValue(string key, object value);
        void StartTracking();
        void AcceptChanges();
        bool GetIsModifiedRecursive();
        Action ChangeTrackerStateChanged { get; set; }
    }
}