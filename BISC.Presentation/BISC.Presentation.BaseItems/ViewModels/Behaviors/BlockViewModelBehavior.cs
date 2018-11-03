using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.BaseItems.ViewModels.Behaviors
{
    public class BlockViewModelBehavior:ViewModelBase
    {
        private bool _isBlocked;
        private string _blockingMessage;
        private bool _isBusy;

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

        public void SetBlock(string message, bool isBusy)
        {
            BlockingMessage = message;
            IsBlocked = true;
            IsBusy = isBusy;
        }

        public void Unlock()
        {
            IsBusy = false;
            IsBlocked = false;
        }
    }
}
