using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
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
        private ChangeTrackerState _changeTrackerState = ChangeTrackerState.Unchanged;
        private IChangeTracker _parentChangeTracker;
        public ChangeTracker()
        {
            
        }


        public void SetValue(string key, object value)
        {
            if (!_valuesDictionary.ContainsKey(key))
            {
                _valuesDictionary.Add(key, value);
            }
            else
            {
                if (_valuesDictionary[key] == value) return;
            }
            TryUnSubscribeOnCollectionChanged(_valuesDictionary[key]);
            _valuesDictionary[key] = value;
            TrySubscribeOnCollectionChanged(value);
            if (_isTrackingEnabled)
            {
                ChangeTrackerState = ChangeTrackerState.Modified;
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
            foreach (var value in _valuesDictionary)
            {
                if (value.Value is IObjectWithChangeTracker objectWithChangeTracker)
                {
                    objectWithChangeTracker.ChangeTracker.StartTracking();
                }

                if (value.Value is IEnumerable enumerable)
                {
                    foreach (var eVal in enumerable)
                    {
                        if (eVal is IObjectWithChangeTracker eValWithChangeTracker)
                        {
                            eValWithChangeTracker.ChangeTracker.StartTracking();
                        }
                    }
                }
            }
            _isTrackingEnabled = true;
        }

        public void AcceptChanges()
        {
            _isTrackingEnabled = false;
            foreach (var value in _valuesDictionary)
            {
                if (value.Value is IObjectWithChangeTracker objectWithChangeTracker)
                {
                    objectWithChangeTracker.ChangeTracker.AcceptChanges();
                }

                if (value.Value is IEnumerable enumerable)
                {
                    foreach (var eVal in enumerable)
                    {
                        if (eVal is IObjectWithChangeTracker eValWithChangeTracker)
                        {
                            eValWithChangeTracker.ChangeTracker.AcceptChanges();
                        }
                    }
                }
            }
            ChangeTrackerState = ChangeTrackerState.Unchanged;
        }

        public ChangeTrackerState ChangeTrackerState
        {
            get => _changeTrackerState;
            set
            {
                _changeTrackerState = value;
                ChangeTrackerStateChanged?.Invoke();
            }
        }

        public Action ChangeTrackerStateChanged { get; set; }

        public bool GetIsModifiedRecursive()
        {
            if (ChangeTrackerState == ChangeTrackerState.Modified) return true;

            foreach (var value in _valuesDictionary)
            {
                
                if (value.Value is IObjectWithChangeTracker objectWithChangeTracker)
                {
                    if (objectWithChangeTracker.ChangeTracker.GetIsModifiedRecursive())
                    {
                        return true;
                    }
                }
                else if(value.Value is IEnumerable enumerable)
                {
                    foreach (var enumValue in enumerable)
                    {
                        if (enumValue is IObjectWithChangeTracker objectWithChangeTrackerEnum)
                        {
                            if (objectWithChangeTrackerEnum.ChangeTracker.GetIsModifiedRecursive())
                            {
                                return true;
                            }
                        }
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
            ChangeTrackerStateChanged = null;
        }
    }
}