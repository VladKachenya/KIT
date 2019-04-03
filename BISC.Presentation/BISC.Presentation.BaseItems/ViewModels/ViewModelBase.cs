using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BISC.Presentation.BaseItems.Annotations;
using BISC.Presentation.Infrastructure.ChangeTracker;
using Prism.Mvvm;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable,IObjectWithChangeTracker
    {

        #region [Ctor's]

        /// <summary>
        /// Initializes an instance 
        /// </summary>
        protected ViewModelBase() : this(new object())
        {
        }

        /// <summary>
        /// Initializes an instance 
        /// </summary>
        /// <param name="lockObject"></param>
        protected ViewModelBase(object lockObject)
        {
            this.LockObject = lockObject ?? throw new ObjectDisposedException(DISPOSED_MESSAGE);
            ChangeTracker=new ChangeTracker.ChangeTracker();
        }

        #endregion [Ctor's]



        public IChangeTracker ChangeTracker { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,bool isExludeFromTracking=false, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            if (!isExludeFromTracking)
            {
                  ChangeTracker.SetValue(propertyName,value);
            }
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (Dispatcher.CurrentDispatcher == Application.Current.Dispatcher)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Application.Current.Dispatcher.Invoke(
                    () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName))
                );
            }
        }




        #region [Constants]

        private const string DISPOSED_MESSAGE = "Object is no longer usable as long it was disposed";

        #endregion [Constants]




        #region [Properties]

        /// <summary>
        /// Gets a value which specifies whether the object has already disposed or not
        /// </summary>
        protected bool IsDisposed { get; set; }

        /// <summary>
        /// Gets or sets synchronization object
        /// </summary>
        protected object LockObject { get; set; }

        #endregion [Properties]


        #region [Templated members]

        /// <summary>
        /// Provides basic implementation of Disposable pattern
        /// </summary>
        /// <param name="disposing">A value which specifies whether this method is called from Dispose method or from finalize method</param>
        protected void Dispose(bool disposing)
        {
            if (this.IsDisposed) return;
            if (disposing)
            {
                lock (this.LockObject)
                    this.OnDisposing();
            }
            this.IsDisposed = true;
        }

        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
    protected virtual void OnDisposing()
        {
            ChangeTracker?.Dispose();
        }

        #endregion [Templated members]


        #region [Protected members]

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/> exception in case the object has already been disposed
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(DISPOSED_MESSAGE);
        }

        #endregion [Protected members]


        #region [IDisposable members]

        /// <summary>
        ///  Performs application-defined tasks associated with freeing, releasing, or
        ///   resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion [IDisposable members]

    }
}
