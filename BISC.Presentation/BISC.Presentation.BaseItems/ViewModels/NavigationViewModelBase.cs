using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.ViewModel;
using Prism.Regions;
using Prism;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public abstract class NavigationViewModelBase : ComplexViewModelBase,INavigationAware,IActiveAware
    {
        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
                if (Dispatcher.CurrentDispatcher == Application.Current.Dispatcher)
                {
                    IsActiveChanged?.Invoke(this, null);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(
                        () => IsActiveChanged?.Invoke(this, null)
                    );
                }
            }
        }

        public event EventHandler IsActiveChanged;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return IsNavigationTarget(BiscNavigationContext.FromNavigationContext(navigationContext));
        }

        protected virtual bool IsNavigationTarget(BiscNavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            OnNavigatedFrom(BiscNavigationContext.FromNavigationContext(navigationContext));
        }
        protected virtual void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {

        }
         void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            OnNavigatedTo(BiscNavigationContext.FromNavigationContext(navigationContext));
        }
        protected virtual void OnNavigatedTo(BiscNavigationContext navigationContext)
        {

        }
    }
}