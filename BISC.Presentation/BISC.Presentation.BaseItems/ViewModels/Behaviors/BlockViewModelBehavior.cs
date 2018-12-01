using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Commands;
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
            UnlockCommands=new ObservableCollection<UnlockCommandEntity>();
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
        public void SetBlockWithOption(string message, params UnlockCommandEntity[] unlockOptions)
        {
            BlockingMessage = message;
        UnlockCommands.Clear();
            foreach (var unlockCommandEntity in unlockOptions)
            {
                unlockCommandEntity.UnlockCommand=new DelegateCommand((() =>
                {
                    OnUnlock();
                    unlockCommandEntity.UnlockCustomCommand?.Execute(null);
                }));
                UnlockCommands.Add(unlockCommandEntity);
            }
            IsBlocked = true;
            IsUnlockOptionAvailable = true;
            IsBusy = false;
        }

        public ObservableCollection<UnlockCommandEntity> UnlockCommands { get; }
        public void Unlock()
        {
            IsBusy = false;
            IsBlocked = false;
            UnlockCommands.Clear();
        }
    }

    public class UnlockCommandEntity
    {
        public UnlockCommandEntity(string unlockOptionHeader, ICommand unlockCustomCommand=null)
        {
         
            UnlockOptionHeader = unlockOptionHeader;
            UnlockCustomCommand = unlockCustomCommand;
        }

        public string UnlockOptionHeader { get; }
        public ICommand UnlockCustomCommand { get; }
        public ICommand UnlockCommand { get; set; }
    }
}
