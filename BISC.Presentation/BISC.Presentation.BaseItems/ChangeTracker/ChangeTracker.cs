using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Presentation.BaseItems.ChangeTracker
{
    public enum ChangeTrackerState
    {
        Modified,
        Unchanged
    }

    public class ChangeTracker : IDisposable, IChangeTracker
    {
        private bool _isTrackingEnabled = false;
        private Dictionary<string, object> _valuesDictionary = new Dictionary<string, object>();

        public void SetValue(string key, object value)
        {
            if (!_valuesDictionary.ContainsKey(key))
            {
                _valuesDictionary.Add(key, value);
            }
            else
            {
                if (_valuesDictionary[key] != value)
                {
                    TryUnSubscribeOnCollectionChanged(_valuesDictionary[key]);
                    _valuesDictionary[key] = value;
                    TrySubscribeOnCollectionChanged(value);
                    if (_isTrackingEnabled)
                    {
                        ChangeTrackerState = ChangeTrackerState.Modified;
                    }
                }
            }
        }

        private void TrySubscribeOnCollectionChanged(object collection)
        {
            if (collection is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged += ObservableCollection_CollectionChanged;
            }
            else
            {
                return;
            }
        }

        private void ObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isTrackingEnabled)
            {
                ChangeTrackerState = ChangeTrackerState.Modified;
            }
        }

        private void TryUnSubscribeOnCollectionChanged(object collection)
        {
            if (collection is INotifyCollectionChanged observableCollection)
            {
                observableCollection.CollectionChanged -= ObservableCollection_CollectionChanged;
            }
            else
            {
                return;
            }
        }

        public void StartTracking()
        {
            ChangeTrackerState = ChangeTrackerState.Unchanged;
            _isTrackingEnabled = true;
        }

        public void AcceptChanges()
        {
            _isTrackingEnabled = false;
            ChangeTrackerState = ChangeTrackerState.Unchanged;
        }

        public ChangeTrackerState ChangeTrackerState = ChangeTrackerState.Unchanged;

        public bool GetIsModifiedRecursive()
        {
            foreach (var value in _valuesDictionary)
            {
                if (value.Value is IObjectWithChangeTracker objectWithChangeTracker)
                {
                    if (objectWithChangeTracker.ChangeTracker.GetIsModifiedRecursive())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void Dispose()
        {
            foreach (var value in _valuesDictionary)
            {
                TryUnSubscribeOnCollectionChanged(value);
            }
        }
    }
}