using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingItemViewModel : ViewModelBase, IPingItemViewModel, ICloneable
    {
        #region private filds
        private string _ip;
        private string forToolTip;
        private IPingService _pingService;
        private bool? _isPing;
        private ICommandFactory _commandFactory;
        IPingItemsViewModelFactory _pingItemsViewModelFactory;

        #endregion

        #region Citor
        public PingItemViewModel(IPingService pingService, ICommandFactory commandFactory, IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            _pingService = pingService;
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            DeleteItemCommand = _commandFactory.CreatePresentationCommand(OnDeleteItemCommand);
            ItemClickCommand = _commandFactory.CreatePresentationCommand(OnItemClickCommand);
            IsPing = null;
        }
        #endregion

        #region private metods
        private void OnItemClickCommand()
        {
            try
            {
                SetAsSelectedIP.Invoke(this);
            }
            catch
            {
                throw new Exception("Не задан делегат SetAsSelectedIP!");
            }
        }

        private async void OnPingCommand()
        {
            await OnPing();
        }

        private void OnDeleteItemCommand()
        {
            DeleteItem.Invoke(this);
            // Разберись как работает Dispose по приложению.
            //this.Dispose();
        }
        #endregion

        #region Public methods

        public async Task OnPing()
        {
            IsPing = null;
            try
            {
                IsPing = await _pingService.GetPing(IP);
                if (IsPing == true)
                {
                    ForToolTip = "PING прошёл успешно";
                }
                else
                {
                    ForToolTip = "PING прошёл неуспешно";
                }
            }
            catch (Exception ex)
            {
                IsPing = false;
                ForToolTip = ex.Message;
            }
        }

        #endregion

        #region Implementation of IPingItemViewModel
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        public string ForToolTip
        {
            get { return forToolTip; }
            set
            {
                forToolTip = value;
                OnPropertyChanged();
            }
        }

        public bool? IsPing
        {
            get { return _isPing; }
            set
            {
                if (value == null)
                    ForToolTip = "Состояние не установленно";
                _isPing = value;
                OnPropertyChanged();
            }
        }

        public Action<IPingItemViewModel> SetAsSelectedIP { get; set; }
        public Action<IPingItemViewModel> DeleteItem { get; set; }

        public ICommand ItemClickCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand PingCommand { get; }
        #endregion

        #region Implementation of ICloneble
        public object Clone()
        {
            var obj = _pingItemsViewModelFactory.GetPingItemViewModel(IP, SetAsSelectedIP, DeleteItem);
            obj.IsPing = IsPing;
            return obj;
        }
        #endregion
    }
}
