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
using Prism.Mvvm;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
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
    }
}
