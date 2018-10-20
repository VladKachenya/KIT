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
    public abstract class NavigationViewModelBase : ComplexViewModelBase, INavigationAware,IActiveAware
    {
        private bool _isActive;
        private bool _isNavigatedTo = false;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if(!_isNavigatedTo)return;
                if(_isActive==value)return;
                _isActive = value;
                OnPropertyChanged();
                if (value)
                {
                    try
                    {
                        OnActivate();
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                else
                {
                    try
                    {
                        OnDeactivate();
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
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

        public virtual void OnActivate()
        {
            
        }
        public virtual void OnDeactivate()
        {

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
            _isNavigatedTo = false;
        }
        protected virtual void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {

        }
         void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
         {
             _isNavigatedTo = true;
            OnNavigatedTo(BiscNavigationContext.FromNavigationContext(navigationContext));
        }
        protected virtual void OnNavigatedTo(BiscNavigationContext navigationContext)
        {

        }
    }
}