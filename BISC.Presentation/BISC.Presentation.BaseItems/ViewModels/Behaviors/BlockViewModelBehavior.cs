using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace BISC.Presentation.BaseItems.ViewModels.Behaviors
{
    public class BlockViewModelBehavior:ViewModelBase
    {
        private bool _isBlocked;
        private string _blockingMessage;
        private bool _isBusy;
        private string _unlockMessage;
        private bool _isUnlockOptionAvailable;

        public BlockViewModelBehavior()
        {
            UnlockCommand=new DelegateCommand(OnUnlock);
        }

        private void OnUnlock()
        {
            Unlock();
        }


        public bool IsBlocked
        {
            get => _isBlocked;
            set { SetProperty(ref _isBlocked , value,true); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy , value);
        }

        public string BlockingMessage
        {
            get => _blockingMessage;
            set =>SetProperty(ref _blockingMessage, value,true);
        }
        public string UnlockMessage
        {
            get => _unlockMessage;
            set { SetProperty(ref _unlockMessage, value); }
        }

        public bool IsUnlockOptionAvailable
        {
            get => _isUnlockOptionAvailable;
            set => SetProperty(ref _isUnlockOptionAvailable , value);
        }

        public void SetBlock(string message, bool isBusy)
        {
            BlockingMessage = message;
            IsBlocked = true;
            IsBusy = isBusy;
            IsUnlockOptionAvailable = false;
        }
        public void SetBlockWithOption(string message, string unlockOption)
        {
            BlockingMessage = message;
            UnlockMessage = unlockOption;
            IsBlocked = true;
            IsUnlockOptionAvailable = true;
            IsBusy = false;
        }

        public ICommand UnlockCommand { get; }
        public void Unlock()
        {
            IsBusy = false;
            IsBlocked = false;
        }
    }
}
