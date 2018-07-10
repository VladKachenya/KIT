﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.BaseItems.ViewModels
{
   public class DisposableViewModelBase:ViewModelBase,IDisposable
    {
        #region [Constants]

        private const string DISPOSED_MESSAGE = "Object is no longer usable as long it was disposed";

        #endregion [Constants]



        #region [Ctor's]

        /// <summary>
        /// Initializes an instance 
        /// </summary>
        protected DisposableViewModelBase() : this(new object())
        {
        }

        /// <summary>
        /// Initializes an instance 
        /// </summary>
        /// <param name="lockObject"></param>
        protected DisposableViewModelBase(object lockObject)
        {
            this.LockObject = lockObject ?? throw new ObjectDisposedException(DISPOSED_MESSAGE);
        }

        #endregion [Ctor's]


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
            /*None*/
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
